using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Transform
{
    public class Sequence : ITween
    {
        public Sequence(params ITween[] tweens)
        {
            this.tweens = tweens.ToArray();
        }

        private ITween[] tweens;

        public TimeSpan Time 
        {
            get
            {
                var result = TimeSpan.Zero;

                for (int i = 0; i < this.tweens.Length; i++)
                {
                    var tween = this.tweens[i];

                    if(i == current)
                        return result + tween.Time;
                    
                    result += tween.Duration;
                }

                return result;
            }
        }

        public TimeSpan Duration => new TimeSpan(tweens.Sum(x => x.Duration.Ticks));

        public bool IsFinished { get; private set; }

        public void Reset()
        {
            this.current = 0;
            this.IsFinished = false;
        }

        private int current;

        public bool Update(GameTime time)
        {
            if (!this.IsFinished)
            {
                var tween = this.tweens[current];

                if(tween.Update(time))
                {
                    current++;
                }
                this.IsFinished = current >= tweens.Length;
            }

            return this.IsFinished;
        }
    }
}
