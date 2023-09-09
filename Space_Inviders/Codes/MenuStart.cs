using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Space_Inviders
{
    internal class MenuStart
    {
        TextureUpdate textureUpdate;
        StartButton startButton;
        ExitButton exitButton;

        int Width = 960;
        int Height = 540;
        static public Texture2D Logo { get; set; }
        static public Texture2D Background { get; set; }
        public MenuStart(SpriteBatch spriteBatch)
        {
            textureUpdate = new TextureUpdate(2, 0, 0, Background, new Point(3, 3), 50, Width, Height, spriteBatch);
            startButton = new StartButton(830, 440, 250, 100);
            exitButton = new ExitButton(830, 540, 250, 100);
        }
        public void DrawElements(SpriteBatch spriteBatch)
        {
            textureUpdate.Draw();
            startButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            spriteBatch.Draw(Logo, new Rectangle(620, 40, 640, 268), Color.AliceBlue);
        }
        public void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public bool ButtonStartClick(MouseState mouseState)
        {
            return startButton.CLick(mouseState);
        }
        public bool ButtonExitClick(MouseState mouseState)
        {
            return exitButton.CLick(mouseState);
        }
    }
}
