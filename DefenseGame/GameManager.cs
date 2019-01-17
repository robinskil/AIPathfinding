using System;
using System.Collections.Generic;
using DefenseGame.AI;
using DefenseGame.Maps;
using DefenseGame.Pathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DefenseGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameManager : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map map;
        List<Zombie> zombies = new List<Zombie>();
        Path p;
        double PreviousSpawnZombie = 0;
        private double SpawnInterval = 0.3;

        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1000;
            graphics.PreferredBackBufferHeight = 1000;
            this.IsMouseVisible = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.map = new Map();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(Zombie z in zombies){
                z.Move();
            }
            // TODO: Add your update logic here
            this.PreviousSpawnZombie += gameTime.ElapsedGameTime.TotalSeconds;
            if(this.PreviousSpawnZombie >= SpawnInterval){
                Zombie z = new Zombie(this.map.SpawnLocation());
                z.SetPath(this.map.FindPath(z.CurrentTile, new Tile(26, 26)));
                zombies.Add(z);
                this.PreviousSpawnZombie = 0;
            }
            Console.WriteLine(zombies.Count);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            this.map.DrawMap(spriteBatch);
            foreach(Zombie z in zombies){
                z.Draw(spriteBatch);
            }
            //p.DrawPath(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
