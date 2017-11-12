# Transform

![sample](doc/sample.gif)

A base Monogame tools for managing relative transforms.


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