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

        public float Length { get; }

        private List<Arm> children = new List<Arm>();

        public Color Color { get; set; } = Color.White;

        public Transform2D Transform { get; } = new Transform2D();

        public Arm CreateArm(float length, Color color)
        {
            var arm = new Arm(length);
            arm.Color = color;
            arm.Transform.Position = new Vector2(this.Length, 0);
            arm.Transform.Parent = this.Transform;
            this.children.Add(arm);
            return arm;
        }

        private static Texture2D pixel;

        public static void LoadContent(GraphicsDevice device)
        {
            if(pixel == null)
            {
                pixel = new Texture2D(device, 1, 1);
                pixel.SetData(new[] { Color.White });
            }
        }

        public void Draw(SpriteBatch sb)
        {
            var dest = new Rectangle((int)this.Transform.AbsolutePosition.X, (int)this.Transform.AbsolutePosition.Y - 5, (int)this.Length, 10);
            sb.Draw(texture: pixel, position: new Vector2(dest.X, dest.Y), sourceRectangle: pixel.Bounds, rotation: this.Transform.AbsoluteRotation, scale:  this.Transform.AbsoluteScale * new Vector2(dest.Width,  dest.Height), color: this.Color);

            foreach (var child in this.children)
            {
                child.Draw(sb);
            }
        }
    }
}
