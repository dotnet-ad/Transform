using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Transform.Samples.Portable
{
    public class Sample : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Sample()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

#if !__MOBILE__

            graphics.PreferredBackBufferWidth = 1400;
            graphics.PreferredBackBufferHeight = 800;
#endif
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Arm.LoadContent(this.GraphicsDevice);

        }

        private Arm arm;

        private Velocity2D velocity2, velocity3;

        private ITween tween;

        protected override void Initialize()
        {
            base.Initialize();

            this.arm = new Arm(100);
            var arm2 = this.arm.CreateArm(50, Color.Red);
            var arm3 = arm2.CreateArm(30, Color.Blue);

            this.arm.Transform.Position = new Vector2(this.GraphicsDevice.Viewport.Width / 2, this.GraphicsDevice.Viewport.Height / 2);

            var t1 = new Tween2D(TimeSpan.FromSeconds(5), this.arm.Transform, new Transform2D()
            {
                Rotation = 5,
                Position = this.arm.Transform.Position,
                Scale = new Vector2(2, 2),
            }, Ease.ElasticInOut);

            this.tween = new Sequence(new Delay(TimeSpan.FromSeconds(3)), t1);

            this.velocity2 = new Velocity2D(arm2.Transform)
            {
                Rotation = 2f,
                Position = Vector2.UnitX * -2f,
            };

            this.velocity3 = new Velocity2D(arm3.Transform)
            {
                Rotation = -3.5f,
            };
        }

        protected override void Update(GameTime gameTime)
        {
            this.tween.Update(gameTime);
            this.velocity2.Update(gameTime);
            this.velocity3.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            this.spriteBatch.Begin();
            this.arm.Draw(this.spriteBatch);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
