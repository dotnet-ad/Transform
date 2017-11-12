#addin "Cake.Git"

var args = new 
{
    OutputDirectory = Argument("output", "build"),
    Target = Argument("target", "Default"),
    RepositoryPath = Argument("repositoryPath", "."),
	AddCommitToDescription = Argument("addCommitToDescription", true),
    Configuration = Argument("configuration", "Release"),
    Version = Argument<string>("packageVersion"),
    Nuget = new 
	{
		Source = Argument("nugetSource", "https://www.nuget.org/api/v2/package"),
		ApiKey = Argument("nugetApiKey", ""),
	},
};

var buildDirectory = MakeAbsolute(Directory(args.OutputDirectory));

Task("Tools.PrintArguments").Does(() => 
{
    Information($"Arguments:");
    Information($"  * Target: {args.Target}");
    Information($"  * AddCommitToDescription: {args.AddCommitToDescription}");
    Information($"  * OutputDirectory: {args.OutputDirectory}");
    Information($"  * Configuration: {args.Configuration}");
    Information($"  * Version: {args.Version}");
    Information($"  * RepositoryPath: {args.RepositoryPath}");
    Information($"  * NugetApiKey: <*>");
});

Task("Tools.Clean").Does(() => 
{
	CleanDirectories(args.OutputDirectory);
	CleanDirectories("./**/bin");
	CleanDirectories("./**/obj");
});

Task("Build.Solutions")
	.IsDependentOn("Tools.Clean")
    .Does(() =>
{
    var slnFiles = GetFiles("**/*.sln");
    foreach(var sln in slnFiles)
    {
        NuGetRestore(sln);
        MSBuild(sln, c => 
        {
            c.Configuration = args.Configuration;
            c.MSBuildPlatform = Cake.Common.Tools.MSBuild.MSBuildPlatform.x86;
        });
    }
});

Task("Nuget.Pack")
	.IsDependentOn("Build.Solutions")
	.Does(() =>
{
    if(!DirectoryExists(buildDirectory.FullPath))
    {
        CreateDirectory(buildDirectory.FullPath);
    }

    var nuspecFiles = GetFiles("nuget/**/*.nuspec");
    foreach(var nuspec in nuspecFiles)
    {
        var wd = MakeAbsolute(nuspec).GetDirectory();
        var settings = new NuGetPackSettings 
        { 
            Version = args.Version,
            OutputDirectory = buildDirectory.FullPath,
            BasePath = wd,
        };

		if(args.AddCommitToDescription)
		{
			// Extract description from nuspec and concat current commit hash and datetime
			var description = XmlPeek(nuspec, $"/package/metadata/description");
			var lastCommit = GitLogTip(args.RepositoryPath);
			description = $"{description}\n\nCommit : {lastCommit.Sha}, {lastCommit.Author?.When}";
			Information($"Updated package description : {description}");
			settings.Description = description;
		}

        NuGetPack(nuspec, settings);
    }
});

Task("Nuget.Push")
    .IsDependentOn("Tools.PrintArguments")
	.IsDependentOn("Nuget.Pack")
	.Does(() =>
{
	if(string.IsNullOrEmpty(args.Nuget.ApiKey))
	{
		throw new ArgumentException("Missing 'nugetApiKey' arguments");
	}
    
    var packages = GetFiles($"**/*.{args.Version}.nupkg");
    NuGetPush(packages, new NuGetPushSettings 
    {
		Source = args.Nuget.Source,
		ApiKey = args.Nuget.ApiKey,
	});
});

Task("Default")
    .IsDependentOn("Tools.PrintArguments")
    .IsDependentOn("Nuget.Pack");

RunTarget(args.Target);