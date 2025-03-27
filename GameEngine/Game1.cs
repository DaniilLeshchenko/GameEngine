using GameEngine.Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private GameEngine.Engine.Managers.GameEngine gameEngine;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            gameEngine = new GameEngine.Engine.Managers.GameEngine(this);
        }

        protected override void Initialize()
        {
            Utilities.Content = Content;
            Utilities.GraphicsDevice = GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            gameEngine.LoadScene(new TestScene());
        }

        protected override void Update(GameTime gameTime)
        {
            Utilities.GameTime = gameTime;

            if (InputManager.IsKeyPressed(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }
    }
}
