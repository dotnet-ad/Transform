namespace Transform
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;

    public class Parallel : ITween
    {
        public Parallel(params ITween[] tweens)
        {
            this.tweens = tweens.ToArray();
        }

        private ITween[] tweens;

        public TimeSpan Time => this.tweens.Max(x => x.Time);

        public TimeSpan Duration => this.tweens.Max(x => x.Duration);

        public bool IsFinished => this.tweens.All(x => x.IsFinished);

        public void Reset()
        {
            foreach (var tween in this.tweens)
            {
                tween.Reset();
            }
        }


        public bool Update(GameTime time)
        {
            foreach (var tween in this.tweens)
            {
                if(!tween.IsFinished)
                {
                    tween.Update(time);
                }
            }

            return this.IsFinished;
        }
    }
}
