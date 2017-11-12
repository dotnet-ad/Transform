namespace Transform
{
    using System;
    using Microsoft.Xna.Framework;

    public interface ITween
    {
        TimeSpan Time { get; }

        TimeSpan Duration { get; }

        bool IsFinished { get; }

        void Reset();

        bool Update(GameTime time);
    }
}
