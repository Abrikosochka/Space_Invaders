using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Space_Inviders
{
    internal class GamePole
    {
        int width = 1000, height = 1080;
        int frameHeight = 540;
        int frameWidth = 500;
        float speed_game = 1;
        int game_level = 3;
        int n = 3, m = 10;
        int score = 0;
                         
        TextureUpdate textureUpdate;
        List<Fire> fires_enemy;
        List<Fire> fires_ship;
        List<Heart> lives;
        Enemy[,] enemes;
        Ship ship;
        
        public int Score { get { return score; } set { score = value;} }
        static public SpriteBatch SpriteBatch { get; set; }
        static public SpriteFont spriteFont { get; set; }
        static public Texture2D Background { get; set; }   
        public void Restart()
        {
            bool end = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemes[i, j] != null)
                    {
                        end = false;
                        break;
                    }
                }
                if(!end)
                    break;
            }
            if (end)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        enemes[i, j] = new Enemy(j * 70, (1 + i) * 60, 50, 50, speed_game, 1);
                    }
                }
                game_level++;
            }
        }
        public GamePole(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
            enemes = new Enemy[n, m];
            fires_ship = new List<Fire>();
            fires_enemy = new List<Fire>();
            lives = new List<Heart> { new Heart(850, 10, 40, 40, true), new Heart(900, 10, 40, 40, true), new Heart(950, 10, 40, 40, true) };
            textureUpdate = new TextureUpdate(2, 0, 0, Background, new Point(5, 4), 50, frameWidth, frameHeight, SpriteBatch);
            ship = new Ship(400, 880, 70, 100, 5, 3);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    enemes[i, j] = new Enemy(j * 70, (1 + i) * 60, 50, 50, speed_game, 1);
                }
            }
        }
        public void DrawElements()
        {           
            textureUpdate.Draw();
            SpriteBatch.DrawString(spriteFont, $"Score: {score}", new Vector2(0, 0), Color.Red);
            SpriteBatch.DrawString(spriteFont, $"Lives:", new Vector2(700, 0), Color.Red);
            ship.Draw();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemes[i, j] != null)
                        enemes[i, j].Draw();
                }
            }
            foreach (Fire fire in fires_ship)
                fire.Draw();
            foreach (Fire fire in fires_enemy)
                fire.Draw();
            foreach (Heart heart in lives)
                if (heart != null)
                    heart.Draw();
        }
        public bool Update(GameTime gameTime)
        {
            foreach (Heart heart in lives)
            {
                if (heart.good == true)
                    heart.Update(gameTime);
                else
                {
                    if (heart.Destroy(gameTime))
                    {
                        lives.Remove(heart);
                        break;
                    }
                }
            }
            if (ship.Lives == 0)
                return ship.Destroy(gameTime);           
            else
            {
                textureUpdate.Update(gameTime);
                ship.Update(gameTime);
                foreach (Fire fire in fires_ship)
                    fire.Update(gameTime);
                foreach (Fire fire in fires_enemy)
                    fire.Update(gameTime);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (enemes[i, j] != null && enemes[i, j].Lives == 0)
                        {
                            if (enemes[i, j].Destroy(gameTime))
                            {
                                enemes[i, j] = null;
                                break;
                            }
                        }
                        else if (enemes[i, j] != null)
                            enemes[i, j].Update(gameTime);
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemes[i, j] != null && enemes[i,j].Y+ enemes[i, j].H == ship.Y)
                    {
                        ship.Lives = 0;
                    }
                }
            }
            return false;
        }
        public void Destroy_Enemy()
        {
            foreach (Fire fire in fires_ship)
            {
                bool nice = false;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {                       
                        Ships enemyCrash = fire.Destroy(enemes[i, j]);
                        if (enemyCrash != null && enemes[i, j].Lives != 0)
                        {
                            nice = true;
                            enemes[i, j].Lives = 0;
                            fires_ship.Remove(fire);
                            score += 100;
                        }
                    }
                    if (nice) break;
                }
                if (nice) break;
            }
        }
        public void Destroy_Ship()
        {
            foreach (Fire fire in fires_enemy)
            {
                bool poadenie = false;
                Ships enemyCrash = fire.Destroy(ship);
                if (enemyCrash != null && ship.Lives != 0)
                {
                    ship.Lives -= 1;
                    lives[ship.Lives].good = false;                   
                    poadenie = true;
                    fires_enemy.Remove(fire);                  
                }
                if (poadenie) break;
            }
        }
        public void MoveShip(bool go_on)
        {
            if (go_on && ship.X + ship.W <= width)
            {
                ship.Move_Right();
            }
            if (!go_on && ship.X >= 0)
            {
                ship.Move_Left();
            }
        }
        public void Add_Ship_Fire()
        {
            if (fires_ship.Count < game_level)
                fires_ship.Add(ship.Make_Fire());
        }
        public void Add_Enemy_Fire(int precent)
        {
            bool shot_time = false;
            for (int i = 1; i < n+1; i++)
            {
                for (int j = 1; j < m+1; j++)
                {
                    if (precent == 100)
                    {
                        if(enemes[i - 1, j - 1] != null)
                        {
                            fires_enemy.Add(enemes[i-1, j-1].Make_Fire());
                            shot_time = true;
                            break;
                        }
                    }
                    if (shot_time) break;
                }
                if (shot_time) break;
            }
        }
        public void Move_Fire()
        {
            foreach (Fire fire in fires_ship)
            {
                fire.MoveFire();
                if (fire.y <= 0)
                {
                    fires_ship.Remove(fire);
                    break;
                }
            }
            foreach (Fire fire in fires_enemy)
            {
                fire.MoveFire();
                if (fire.y >= height)
                {
                    fires_enemy.Remove(fire);
                    break;
                }
            }         
        }

        public void Enemy_Move()
        {
            bool enemyBorderRight = false;
            bool enemyBorderLeft = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (enemes[i, j] != null && enemes[i, j].X + enemes[i, j].W > width)
                        enemyBorderRight = true;
                    if (enemes[i, j] != null && enemes[i, j].X < 0)
                        enemyBorderLeft = true;
                    if (enemes[i, j] != null)
                        enemes[i, j].Move_Left_Right();
                }
            }
            if (enemyBorderLeft)
            {
                speed_game += 0.1f;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (enemes[i, j] != null)
                        {
                            enemes[i, j].Speed = speed_game;
                            enemes[i, j].Move_Down();
                        }
                    }
                }
            }
            if (enemyBorderRight)
            {
                speed_game += 0.1f;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (enemes[i, j] != null)
                        {
                            enemes[i, j].Speed = -speed_game;
                            enemes[i, j].Move_Down();
                        }
                    }
                }
            }
        }
    }
}
