using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Transactions;

namespace Monogame___Lesson_4
{
    // Raihan Carder
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D bombTexture;
        Texture2D earthTexture;
        Rectangle bombRect;
        Rectangle earthRect;
        SpriteFont bombTimer;
        MouseState mouseState;
        SoundEffect explode;

        int mouseX, mouseY;
        bool bombExplosion = false;
        float seconds;
        float startTime;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            explode = Content.Load<SoundEffect>("explosion");
            bombTimer = Content.Load<SpriteFont>("Time");
            bombTexture = Content.Load<Texture2D>("Bomb2");
            earthTexture = Content.Load<Texture2D>("Earth");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            mouseX = mouseState.X;
            mouseY = mouseState.Y;


            // start time = gametime. ...
            // Needed so I can reset timer


            if (mouseX >= 296 && mouseX <=301 && mouseY >= 192 && mouseY <=197 && (mouseState.LeftButton == ButtonState.Pressed))
            {
                startTime = (float)gameTime.TotalGameTime.TotalSeconds; 
            }

            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here           
       

            if (seconds > 15 && bombExplosion == false)
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


            _spriteBatch.Draw(earthTexture, earthRect, Color.White);
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
          

            if (bombExplosion == false)
            {
                _spriteBatch.DrawString(bombTimer, (15 - seconds).ToString("00.0"), new Vector2(_graphics.PreferredBackBufferWidth / 2 - 65, 230), Color.Black);
            }
            else
            {
                // Put Explosion
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}