using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Transform.Samples.Portable
{
    public class Arm
    {
        public Arm(float length)
        {
            this.Length = length;
        }

        public float Length { get;  }

        private List<Arm> children = new List<Arm>();

        public Transform2D Transform { get; } = new Transform2D();

        public Arm CreateArm(float length)
        {
            var arm = new Arm(length);
            arm.Transform.Position = new Vector2(this.Length, 0);
            arm.Transform.Parent = this.Transform.Parent;
            this.children.Add(arm);
            return arm;
        }

        private static Texture2D pixel;

        public void LoadContent(GraphicsDevice device)
        {
            if(pixel == null)
            {
                pixel = new Texture2D(device, 1, 1);
                pixel.SetData(new[] { Color.White });
            }
        }

        public void Draw(SpriteBatch sb)
        {
            /*sb.Draw(pixel, 
                    position:this.Transform.AbsolutePosition,
                    sourceRectangle: pixel.Bounds,
                    scale: new Vector2()*/

            foreach (var child in this.children)
            {
                child.Draw(sb);
            }
        }
    }
}
