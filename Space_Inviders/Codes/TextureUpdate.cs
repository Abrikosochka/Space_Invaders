using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Space_Inviders
{
    public class TextureUpdate
    {
        SpriteBatch SpriteBatch;
        Point CurrentFrame;
        Point MaxFrame;
        
        public int SpriteSizeDestroy;
        int current_time;
        int CurrentTime;
        float Zoom;

        public int Period { get; set; }
        public Point SpriteSize { get; set; }
        public Texture2D Texture { get; set; }       
        public float x { get; set; }
        public int y { get; set; }
        public TextureUpdate(float zoom, float x, int y, Texture2D texture, Point maxFrame, int period, int w, int h, SpriteBatch spriteBatch)
        {
            Zoom = zoom;
            this.x = x;
            this.y = y;
            MaxFrame = maxFrame;
            CurrentFrame = new Point(0, 0);
            SpriteSize = new Point(w, h);
            Texture = texture;
            Period = period;
            CurrentTime = 0;
            current_time = 0;
            SpriteSizeDestroy = 0;
            SpriteBatch = spriteBatch;
        }
        public void Draw() 
        {
            SpriteBatch.Draw(Texture, new Vector2(x, y), new Rectangle(CurrentFrame.X * SpriteSize.X, 
            CurrentFrame.Y * SpriteSize.Y, SpriteSize.X, SpriteSize.Y), Color.White, 0, Vector2.Zero,
            Zoom, SpriteEffects.None, 0);
        }
        public void Update(GameTime gameTime) 
        {
            CurrentTime += gameTime.ElapsedGameTime.Milliseconds;
            current_time = CurrentTime;
            if (CurrentTime > Period)
            {
                CurrentTime -= Period;
                CurrentFrame.X++;
                if (CurrentFrame.X >= MaxFrame.X)
                {
                    CurrentFrame.X = 0;
                    CurrentFrame.Y++;
                    if (CurrentFrame.Y >= MaxFrame.Y)
                        CurrentFrame.Y = 0;
                }
            }
        }
        public bool DestroyTextureUpdate(GameTime gameTime, int maxFrame)
        {
            CurrentFrame.X = SpriteSizeDestroy;
            current_time += gameTime.ElapsedGameTime.Milliseconds;
            if (current_time > Period)
            {               
                current_time -= Period;
                SpriteSizeDestroy++;
                if (SpriteSizeDestroy == maxFrame)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
