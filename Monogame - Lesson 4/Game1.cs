using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;

namespace Monogame___Lesson_4
{
    // Raihan Carder
    public class Game1 : Game
    {
        List<int> frame = new List<int>();
        List<Texture2D> earthExplosionTexture = new List<Texture2D>();
        
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Texture2D earthTexture;
        Rectangle bombRect;
        Rectangle earthRect;
        SpriteFont bombTimer;
        MouseState mouseState;
        SoundEffect explode;
        int animation = 0;
        int mouseX, mouseY;
        bool bombExplosion = false;
        float seconds;
        float startTime;
        float animationTimeStamp;
        float animationInterval;
        float animationTime;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            this.Window.Title = "Time and Sound";

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here


            bombRect = new Rectangle(_graphics.PreferredBackBufferWidth/2-250, _graphics.PreferredBackBufferHeight/2-100, 500, 200);       
            earthRect = new Rectangle(0, 0, 800, 500);
            animationInterval = 0.1f;
            
            for (int i = 0; i < 28; i++)
            {
                frame.Add(i);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i = 0; i < frame.Count; i++)
            {
                earthExplosionTexture.Add(Content.Load<Texture2D>($"frame_{i}_delay-0.05s"));
            }

            explode = Content.Load<SoundEffect>("explosion");
            bombTimer = Content.Load<SpriteFont>("Time");
            bombTexture = Content.Load<Texture2D>("Bomb2");
            earthTexture = Content.Load<Texture2D>("Earth");
            // TODO: use this.Content to load your game content here
            animationTimeStamp = 0;

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;

            if (bombExplosion)
            {
                // Gets amount of time since last timestamp
                animationTime = (float)gameTime.TotalGameTime.TotalSeconds - animationTimeStamp;
                if (animationTime > animationInterval)
                {
                    animationTimeStamp = (float)gameTime.TotalGameTime.TotalSeconds;
                    animation += 1;
                    if (animation >= frame.Count)
                    {

                        Environment.Exit(0);
                    }
                        

                }
               
            }

            // start time = gametime. ...
            // Needed so I can reset timer


            if (mouseX >= 296 && mouseX <=301 && mouseY >= 192 && mouseY <=197 && (mouseState.LeftButton == ButtonState.Pressed) && !bombExplosion)
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds; 
            }

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here           
       

            if (seconds > 15 && !bombExplosion)
            {
                bombExplosion = true;
                explode.Play();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
          

            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();


  

            if (bombExplosion == false)
            {
                _spriteBatch.Draw(earthTexture, earthRect, Color.White);
                _spriteBatch.Draw(bombTexture, bombRect, Color.White);
                _spriteBatch.DrawString(bombTimer, (15 - seconds).ToString("00.0"), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 65, 230), Color.Black);
            }
            else
            {
                if (animation < frame.Count)
                {
                    _spriteBatch.Draw(earthExplosionTexture[animation], new Rectangle(0, 0, 800, 500), Color.White);
                }
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}