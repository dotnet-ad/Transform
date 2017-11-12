namespace Transform
{
    using System;
    using Microsoft.Xna.Framework;

    public class Velocity2D
    {
        public Velocity2D(Transform2D transform)
        {
            this.Transform = transform ?? throw new ArgumentException(nameof(transform));
        }

        public Transform2D Transform { get; }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get; set; }

        public float Rotation { get; set; }

        public void Update(GameTime time)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            this.Transform.Position += this.Position * delta;
            this.Transform.Scale += this.Scale * delta;
            this.Transform.Rotation += this.Rotation * delta;
        }
    }
}
