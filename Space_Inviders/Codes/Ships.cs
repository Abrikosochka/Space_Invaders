using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Audio;

namespace Space_Inviders
{
    abstract class Ships
    {
        protected TextureUpdate textureUpdate;
        protected int y, w, h, lives;
        protected float x, speed;
        public float Speed { get { return speed; } set { speed = value; } }
        public int Lives { get { return lives; } set { lives = value; } }
        public float X { get { return x; } set { x = value; } }
        public int Y { get { return y; } set { y = value; } }
        public int H { get { return h; } set { h = value; } }
        public int W { get { return w; } set { w = value; } }
        public Ships(float x, int y, int w, int h, float speed, int lives)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
            this.lives = lives;
            this.speed = speed;
        }
        public abstract void Draw();
        public abstract void Update(GameTime gameTime);
        public abstract Fire Make_Fire();
        public abstract bool IsIntersect(Rectangle rectangle);
        public abstract bool Destroy(GameTime gameTime);
    }
    internal class Enemy: Ships
    {
        static public SoundEffect Death { get; set; }
        static public Texture2D EnemyTexture { get; set; }
        static public Texture2D EnemyDestroyTexture { get; set; }
        public Enemy(float x, int y, int w, int h, float speed, int lives) : base(x, y, w, h, speed, lives) 
        {
            textureUpdate = new TextureUpdate(1, x, y, EnemyTexture, new Point(8, 0), 50, w, h, GamePole.SpriteBatch);
        }
        public override void Draw()
        {
            textureUpdate.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public override Fire Make_Fire()
        {
            Fire fire = new FireEnemy(x + 93, y, 5, 10);
            return fire;
        }
        public void Move_Left_Right()
        {
            x += speed;
            textureUpdate.x = x;
        }
        public void Move_Down()
        {
            y += 10;
            textureUpdate.y = y;
        }
        public override bool Destroy(GameTime gameTime)
        {
            if (textureUpdate.SpriteSizeDestroy == 0)
            {
                textureUpdate.Texture = EnemyDestroyTexture;
                Death.Play();
            }            
            return textureUpdate.DestroyTextureUpdate(gameTime, 9);
        }
        public override bool IsIntersect(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle((int)x, y, w, h));
        }
    }
    internal class Ship: Ships
    {     
        static public SoundEffect Shot { get; set; }
        static public SoundEffect Death { get; set; }
        static public Texture2D ShipTexture { get; set; }
        static public Texture2D ShipDestroyTexture { get; set; }
        public Ship(int x, int y, int w, int h, int speed, int lives) : base(x, y, w, h, speed, lives) 
        {
            textureUpdate = new TextureUpdate(1, x, y, ShipTexture, new Point(8, 0), 50, w, h, GamePole.SpriteBatch);
        }
        public override void Draw()
        {
            textureUpdate.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public override Fire Make_Fire()
        {
            Fire fire = new FireShip(x+28, y, 15, 30);
            Shot.Play();
            return fire;
        }
        public void Move_Left()
        {
            x -= speed;
            textureUpdate.x = x;
        }
        public void Move_Right()
        {
            x += speed;
            textureUpdate.x = x;
        }
        public override bool IsIntersect(Rectangle rectangle)
        {
            return rectangle.Intersects(new Rectangle((int)x, y, w, h));
        }
        public override bool Destroy(GameTime gameTime)
        {
            if(textureUpdate.SpriteSizeDestroy == 0)
            {
                textureUpdate.SpriteSize = new Point(107, 102);
                textureUpdate.Texture = ShipDestroyTexture;
                Death.Play();

            }
            return textureUpdate.DestroyTextureUpdate(gameTime, 18);
        }
    }
}
