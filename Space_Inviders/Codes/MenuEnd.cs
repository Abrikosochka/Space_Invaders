using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;
using System.Linq;
using SharpDX.Direct3D9;

namespace Space_Inviders
{
    internal class MenuEnd
    {
        bool registration = false;
        int height = 1080;
        int width = 1920;
        string text = "";
        int score;

        MainMenuButton mainMenuButton;
        List<Player> players;
        TextBox textBox;
        
        static public Texture2D GameOver { get; set;}
        static public Texture2D Background { get; set; }
        static public SpriteFont spriteFont { get; set; }
        public MenuEnd(int score)
        {
            this.score = score;
            mainMenuButton = new MainMenuButton(675, 830, 570, 60);
            textBox = new TextBox(675, 780, 570, 40);
            players = new List<Player>();
            StreamReader sr = new StreamReader("users.txt", true);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line != "")
                {                
                    string[] s = line.Split(' ');
                    players.Add(new Player(s[0], Convert.ToInt32(s[1])));
                }
            }
            sr.Close();
            players = players.OrderByDescending(player => player.Score).ToList();
        }

        public void Draw(SpriteBatch spriteBatch)
        {            
            spriteBatch.Draw(Background, new Rectangle(0, 0, width, height), Color.AliceBlue);
            spriteBatch.Draw(GameOver, new Rectangle(780, 70, 378 , 194), Color.AliceBlue);
            textBox.Draw(spriteBatch);
            spriteBatch.DrawString(spriteFont, text, new Vector2(689, 783), Color.Blue);
            spriteBatch.DrawString(spriteFont, "Lider Tabel", new Vector2(870, 270), Color.Blue);
            for (int i = 0; i < 13; i++)
            {
                spriteBatch.DrawString(spriteFont, players[i].Name, new Vector2(700, 310 + 35*i), Color.Blue);
                spriteBatch.DrawString(spriteFont, $"{players[i].Score}", new Vector2(1000, 310 + 35 * i), Color.Blue);
            }
            mainMenuButton.Draw(spriteBatch);
        }
        public bool Perehod(MouseState mouseState)
        {
            return mainMenuButton.CLick(mouseState);
        }
        public void Save_Players(char key)
        {
            if (!registration)
            {
                if (key == '\b' && text != "")
                {
                    int x = text.Length - 1;
                    text = text.Remove(x);
                }
                else if (key == '\r' && text.Length > 0 && score > 0)
                {
                    bool eq = true;
                    foreach(Player player in players)
                        if(text == player.Name)
                        {
                            eq = false;
                            StreamReader sr = new StreamReader("users.txt", true); 
                            StreamWriter sw = new StreamWriter("temp.txt", true);
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] s = line.Split(' ');
                                if (s[0] == text && Convert.ToInt32(s[1]) < score)
                                {
                                    line = s[0] + " " + Convert.ToString(score);
                                    player.Score = score;
                                }               
                                sw.WriteLine(line);
                            }
                            sr.Close();
                            sw.Close();
                            File.Delete("users.txt");
                            File.Move("temp.txt", "users.txt");

                            text = "";
                            registration = true;
                            break;                            
                        }
                    if (eq)
                    {
                        StreamWriter sw = new StreamWriter("users.txt", true);
                        sw.WriteLine($"{text} {score}");
                        sw.Close();
                        players.Add(new Player(text, score));
                        players = players.OrderByDescending(player => player.Score).ToList();
                        text = "";
                        registration = true;
                    }
                }
                else if (key != '\b' && key != '\r')
                    if (text.Length < 11)
                        text += key;
            }
        }
    }
}
