namespace Transform
{
    using Microsoft.Xna.Framework;

    public static class Extensions
    {
        public static Transform2D ToTransform(this Vector2 position)
        {
            return new Transform2D()
            {
                Position = position,
            };
        }

        public static Velocity2D WithVelocity(this Transform2D transform)
        {
            return new Velocity2D(transform);
        }

        public static Acceleration2D WithAcceleration(this Velocity2D velocity)
        {
            return new Acceleration2D(velocity);
        }
    }
}
