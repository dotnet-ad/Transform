using System;
using Microsoft.Xna.Framework;

namespace Transform
{
    public interface ITween
    {
        TimeSpan Time { get; }

        TimeSpan Duration { get; }

        bool IsFinished { get; }

        void Reset();

        bool Update(GameTime time);
    }
}
