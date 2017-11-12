# Transform

[![NuGet](https://img.shields.io/nuget/v/Transform.svg?label=NuGet)](https://www.nuget.org/packages/Transform/) [![Donate](https://img.shields.io/badge/donate-paypal-yellow.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=ZJZKXPPGBKKAY&lc=US&item_name=GitHub&item_number=0000001&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted)

![sample](doc/sample.gif)

Base Monogame objects for managing relative transforms.


## Quickstart

```csharp
// Hierarchy
this.arm.Transform.Position = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);

this.arm2.Transform.Position = new Vector2(this.arm.Length, 0);
this.arm2.Transform.Parent = this.arm.Transform;

this.arm3.Transform.Position = new Vector2(this.arm2.Length, 0);
this.arm3.Transform.Parent = this.arm2.Transform;

// Animation
this.tween = new Tween2D(TimeSpan.FromSeconds(5), this.arm.Transform, new Transform2D()
{
    Rotation = 5,
    Position = this.arm.Transform.Position,
    Scale = new Vector2(2,2),
}, Ease.ElasticInOut);

this.velocity2 = new Velocity2D(arm2.Transform)
{
    Rotation = 2f,
    Position = Vector2.UnitX * -2f,
};

this.velocity3 = new Velocity2D(arm3.Transform)
{
    Rotation = -3.5f,
};
```

## Usage

### Transform2D

Represents a 2 dimensions transformation. A transform could be attached to another transform by setting its `Parent` property. The relative `Position`, `Scale`, `Rotation` could be used to update each component of the transform (relative to the parent transform if set, else absolute position). The effective absolute output world components can be accessed through `AbsolutePosition`, `AbsoluteScale`, `AbsoluteRotation`.

### Velocity2D

Represents a transform linear velocity following `Position`, `Scale`, `Rotation` independant components. 

### Acceleration2D

Represents a velocity linear acceleration following `Position`, `Scale`, `Rotation` independant components. 

### ITween

Represents an animation.

#### Tween2D

Tweens a value between two given transform, in a given amount of time, and with a given curve function.

#### Sequence

Chains several `ITween` to create complex animations.

#### Parallel

Runs a given set of `ITween` in parallel until all are finished.

#### Delay

Waits the given amount of time.

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)