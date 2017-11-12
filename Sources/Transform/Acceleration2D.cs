using System;
using Microsoft.Xna.Framework;

namespace Transform
{
    public class Acceleration2D
    {
        public Acceleration2D(Velocity2D velocity)
        {
            this.Velocity = velocity ?? throw new ArgumentException(nameof(velocity));
        }

        public Velocity2D Velocity { get; }

        public Vector2 Position { get; set; }

        public Vector2 Scale { get; set; }

        public float Rotation { get; set; }

        public void Update(GameTime time)
        {
            var delta = (float)time.ElapsedGameTime.TotalSeconds;
            this.Velocity.Position += this.Position * delta;
            this.Velocity.Scale += this.Scale * delta;
            this.Velocity.Rotation += this.Rotation * delta;

            this.Velocity.Update(time);
        }
    }
}
