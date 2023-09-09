using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Inviders
{
    internal class Heart
    {
        private TextureUpdate textureUpdate;
        static public SoundEffect Popadanie { get; set; }
        static public Texture2D heartred { get; set; }
        static public Texture2D heartblack { get;set; }
        public bool good { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
        public Heart(int x, int y, int w, int h, bool good) 
        {
            this.good = good;
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            textureUpdate = new TextureUpdate(1, x, y, heartred, new Point(2, 0), 300, w, h, GamePole.SpriteBatch);
        }
        public void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public void Draw()
        {
            textureUpdate.Draw();
        }
        public bool Destroy(GameTime gameTime)
        {
            if (textureUpdate.SpriteSizeDestroy == 0)
            {
                textureUpdate.Period = 120;
                textureUpdate.Texture = heartblack;
                Popadanie.Play();
            }
            return textureUpdate.DestroyTextureUpdate(gameTime, 9);
        }
    }
}
