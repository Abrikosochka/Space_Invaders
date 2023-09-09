using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Space_Inviders
{
    enum Stat
    {
        MenuStart,
        Game,
        MenuEnd
    }
    public class Game1 : Game
    {
        private Dictionary<Keys, bool> pressedKeys = new Dictionary<Keys, bool>();
        Random random = new Random();
        Stat Stat = Stat.MenuStart;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        KeyboardState oldKeyboardState, keyboardState, currentKeyboardState, prevKeyboardState;
        MenuStart MenuStart;
        GamePole GamePole;
        MenuEnd MenuEnd;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {          
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MenuStart.Logo = Content.Load<Texture2D>("logo");
            MenuStart.Background = Content.Load<Texture2D>("menustart");
            StartButton.ButtonStart = Content.Load<Texture2D>("start");
            ExitButton.ButtonExit = Content.Load<Texture2D>("exit");
            GamePole.Background = Content.Load<Texture2D>("background");  
            GamePole.spriteFont = Content.Load<SpriteFont>("score_lives_font");
            Ship.ShipTexture = Content.Load<Texture2D>("spaceship");
            Ship.Shot = Content.Load<SoundEffect>("shoot");
            Ship.Death = Content.Load<SoundEffect>("shipdeath");
            Enemy.Death = Content.Load<SoundEffect>("kill");
            Enemy.EnemyTexture = Content.Load<Texture2D>("enemy");
            Enemy.EnemyDestroyTexture = Content.Load<Texture2D>("enemydestroy");
            Ship.ShipDestroyTexture = Content.Load<Texture2D>("shipdestroy");
            FireShip.FireShipTexture = Content.Load<Texture2D>("fire");
            FireEnemy.FireEnemyTexture = Content.Load<Texture2D>("fire_enemy");
            Heart.heartred = Content.Load<Texture2D>("live");
            Heart.heartblack = Content.Load<Texture2D>("death");
            Heart.Popadanie = Content.Load<SoundEffect>("popadanie");
            MainMenuButton.MainMenu = Content.Load<Texture2D>("mainmenu");
            MenuEnd.Background = Content.Load<Texture2D>("menuend");
            MenuEnd.spriteFont = Content.Load<SpriteFont>("textfont");
            MenuEnd.GameOver = Content.Load<Texture2D>("gameover");
            TextBox._textBox = Content.Load<Texture2D>("textbox2");
            MenuStart = new MenuStart(_spriteBatch);
        }

        protected override void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            switch (Stat)
            {
                case Stat.MenuStart:
                    MenuStart.Update(gameTime);
                    if (MenuStart.ButtonStartClick(mouseState) && mouseState.LeftButton == ButtonState.Pressed)
                    {
                        GamePole = new GamePole(_spriteBatch);
                        Stat = Stat.Game;
                    }
                    if (MenuStart.ButtonExitClick(mouseState) && mouseState.LeftButton == ButtonState.Pressed) Exit();
                    break;
                case Stat.Game:
                    GamePole.Restart();
                    GamePole.Move_Fire();
                    if(!GamePole.Update(gameTime))
                    {
                        GamePole.Destroy_Enemy();                   
                        GamePole.Enemy_Move();
                        GamePole.Destroy_Ship();
                        if (keyboardState.IsKeyDown(Keys.Escape)) Stat = Stat.MenuStart;
                        if (keyboardState.IsKeyDown(Keys.Left)) GamePole.MoveShip(false);
                        if (keyboardState.IsKeyDown(Keys.Right)) GamePole.MoveShip(true);
                        if (keyboardState.IsKeyDown(Keys.Space) && oldKeyboardState.IsKeyUp(Keys.Space)) GamePole.Add_Ship_Fire();                   
                        GamePole.Add_Enemy_Fire(random.Next(0, 150));
                    }
                    else 
                    {
                        MenuEnd = new MenuEnd(GamePole.Score);
                        Stat = Stat.MenuEnd;
                    }
                    break;
                case Stat.MenuEnd:
                    if (keyboardState.IsKeyDown(Keys.Escape)) Exit();
                    currentKeyboardState = Keyboard.GetState();
                    if (currentKeyboardState.GetPressedKeys().Length == 1 &&
                        prevKeyboardState.GetPressedKeys().Length == 0)
                        {
                            char text = (char)currentKeyboardState.GetPressedKeys()[0];
                            if (text >= 'A' && text <= 'Z' || text >= '0' && text <= '9' || text == '\b' || text == '\r')
                            {
                                MenuEnd.Save_Players(text);
                            }
                        }
                    prevKeyboardState = currentKeyboardState;
                    if(MenuEnd.Perehod(mouseState) && mouseState.LeftButton == ButtonState.Pressed) Stat = Stat.MenuStart;
                    break;
            }
            oldKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            switch (Stat)
            {
                case Stat.MenuStart:
                    _graphics.PreferredBackBufferHeight = 1080;
                    _graphics.PreferredBackBufferWidth = 1920;
                    _graphics.ApplyChanges();
                    MenuStart.DrawElements(_spriteBatch);
                    break;
                case Stat.Game:
                    _graphics.PreferredBackBufferHeight = 1080;
                    _graphics.PreferredBackBufferWidth = 1000;
                    _graphics.ApplyChanges();
                    GamePole.DrawElements();
                    break;
                case Stat.MenuEnd:
                    _graphics.PreferredBackBufferHeight = 1080;
                    _graphics.PreferredBackBufferWidth = 1920;
                    _graphics.ApplyChanges();
                    MenuEnd.Draw(_spriteBatch);
                    break;

            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}