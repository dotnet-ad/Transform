using System;
using Microsoft.Xna.Framework;

namespace Transform
{
    public class Delay : ITween
    {
        public Delay(TimeSpan duration)
        {
            this.Duration = duration;
        }

        public TimeSpan Time { get; private set; }

        public TimeSpan Duration { get; }

        public bool IsFinished { get; private set; }

        public void Reset()
        {
            this.IsFinished = false;
            this.Time = TimeSpan.Zero;
        }

        public bool Update(GameTime time)
        {
            if (!this.IsFinished)
            {
                var delta = (float)time.ElapsedGameTime.TotalSeconds;
                this.Time += time.ElapsedGameTime;
                this.IsFinished = (this.Time >= this.Duration);
            }

            return this.IsFinished;
        }
    }
}
