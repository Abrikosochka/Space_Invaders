using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Inviders
{
    abstract class Buttons
    {
        protected int x, y, w, h;
        public Buttons(int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        abstract public void Draw(SpriteBatch spriteBatch);
        public virtual bool CLick(MouseState mouseState)
        {
            if (x - 10 < mouseState.X && mouseState.X < (x + w - 30) &&
                y - 10 < mouseState.Y && mouseState.Y < (y + h - 30))
                return true;
            return false;
        }
    }
    internal class MainMenuButton: Buttons
    {
        static public Texture2D MainMenu { get; set; }
        public MainMenuButton(int x, int y, int w, int h): base (x, y, w, h) { }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(MainMenu, new Rectangle(x, y, w, h), Color.AliceBlue);
        }
    }
    internal class StartButton : Buttons
    {
        static public Texture2D ButtonStart { get; set; }
        public StartButton(int x, int y, int w, int h) : base(x, y, w, h) { }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ButtonStart, new Rectangle(x, y, w, h), Color.AliceBlue);
        }
    }
    internal class ExitButton : Buttons
    {
        static public Texture2D ButtonExit { get; set; }
        public ExitButton(int x, int y, int w, int h) : base(x, y, w, h) { }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ButtonExit, new Rectangle(x, y, w, h), Color.AliceBlue);
        }
    }
}
