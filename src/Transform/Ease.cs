namespace Transform
{
    using System;

    public enum Ease
    {
        InOut,
        In,
        Out,
        ElasticIn,
        ElasticOut,
        ElasticInOut,
    }

    public static class EaseFunctions
    {
        private static readonly float PI_2 = (float)(Math.PI / 2);

        public static float In(float t) => t * t;

        public static float Out(float t) => t * (2 - t);

        public static float InOut(float t) => t < 0.5f ? 2 * t * t : -1 + (4 - 2 * t) * t;

        public static float ElasticIn(float t) => (float)(Math.Sin(13 * PI_2 * t) * Math.Pow(2, 10 * (t - 1)));

        public static float ElasticOut(float t) => (float)(Math.Sin(-13 * PI_2 * (t + 1)) * Math.Pow(2, -10 * t) + 1);

        public static float ElasticInOut(float t) 
        {
            if (t < 0.5f)
            {
                return (float)(0.5 * Math.Sin(13 * PI_2 * (2 * t)) * Math.Pow(2, 10 * ((2 * t) - 1)));
            }
            return (float)(0.5 * (Math.Sin(-13 * PI_2 * ((2 * t - 1) + 1)) * Math.Pow(2, -10 * (2 * t - 1)) + 2));
        }

        public static Func<float,float> Get(Ease ease)
        {
            switch (ease)
            {
                case Ease.In: return In;
                case Ease.Out: return Out;
                case Ease.InOut: return InOut;
                case Ease.ElasticIn: return ElasticIn;
                case Ease.ElasticOut: return ElasticOut;
                case Ease.ElasticInOut: return ElasticInOut;
            }

            throw new NotSupportedException();
        }
    }
}
