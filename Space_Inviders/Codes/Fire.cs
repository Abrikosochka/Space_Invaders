using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Space_Inviders
{
    abstract class Fire
    {
        protected TextureUpdate textureUpdate;        
        protected int w, h;
        protected float x;
        public int y { get; set; }
        public Fire(float x, int y, int w, int h)
        {
            
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;           
        }
        public abstract void Draw();
        public abstract void Update(GameTime gameTime);
        public abstract void MoveFire();
        public abstract Ships Destroy(Ships ship);
    }
    internal class FireShip: Fire
    {
        static public Texture2D FireShipTexture { get; set; }

        public FireShip(float x, int y, int w, int h) : base(x, y, w, h) 
        { 
            textureUpdate = new TextureUpdate(1, x, y, FireShipTexture, new Point(4, 0), 50, w, h, GamePole.SpriteBatch); 
        }
        
        public override void Draw()
        {
            textureUpdate.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public override void MoveFire()
        {
            y -= 5;
            textureUpdate.y = y;
        }
        public override Ships Destroy(Ships enemyes) 
        {
            if (enemyes!= null && enemyes.IsIntersect(new Rectangle((int)x, y, w, h)))  
                    return enemyes;
            return null;
        }
    }
    internal class FireEnemy: Fire
    {
        static public Texture2D FireEnemyTexture { get; set; }
        public FireEnemy(float x, int y, int w, int h) : base(x, y, w, h) 
        { 
            textureUpdate = new TextureUpdate(2 , x, y, FireEnemyTexture, new Point(8, 0), 50, w, h, GamePole.SpriteBatch);
        }
        public override void Draw()
        {
            textureUpdate.Draw();
        }
        public override void Update(GameTime gameTime)
        {
            textureUpdate.Update(gameTime);
        }
        public override void MoveFire()
        {
            y += 5;
            textureUpdate.y = y;
        }
        public override Ships Destroy(Ships ship)
        {
            if (ship.IsIntersect(new Rectangle((int)x, y, w, h)))
                    return ship;
            return null;
        }
    }
}
