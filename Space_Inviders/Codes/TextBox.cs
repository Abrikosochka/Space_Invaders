using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Space_Inviders
{
    internal class TextBox
    {
        int x, y, w, h;
        static public Texture2D _textBox { get; set; }
        public TextBox(int x, int y, int w, int h) 
        { 
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textBox, new Rectangle(675, 780, 570, 40), Color.AliceBlue);
        }
    }
}
