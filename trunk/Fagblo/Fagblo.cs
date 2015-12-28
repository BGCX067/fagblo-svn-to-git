using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

using Fagblo.Classes;
using Fagblo.Managers;
using Fagblo.Utils;

namespace Fagblo
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Fagblo : Microsoft.Xna.Framework.Game
    {
        private static Fagblo instance;

        public Player currentPlayer;
        /*{
            get { return this.currentPlayer; }
            set { this.currentPlayer = value; }
        }*/

        

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Initializing Variables and Declarations////

        public State.GameState currentState
        {
            get { return this.state; }
            set { this.state = value; }
        }
        State.GameState state = State.GameState.None;

        public Camera camera = new Camera();

        public Fagblo()
        {
            Fagblo.instance = this;

            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;

            Content.RootDirectory = "Content";
        }

        public static Fagblo getInstance()
        {
            return instance;
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
            currentState = State.GameState.Initializing;

            //Initialize Managers////
            InterfaceManager.initializeInterfaceManager(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            MapManager.initializeMapManager();
            InputManager.initializeInputManager();

            base.Initialize();

            currentState = State.GameState.Menu;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load all Game Textures
            TextureManager.initializeTextureManager(Content);

            //Load the Game's Font
            InterfaceManager.getInstance().setFont(Content.Load<SpriteFont>(@"Arial"));

            //Load Interface Textures into InterfaceManager
            InterfaceManager.getInstance().loadTextures();

            //TODO: get rid of this after testing
            //Load Terrain into a testmap
            MapManager.getInstance().setCurrentLevel(1);
            for (int i = 0; i < MapManager.getInstance().currentMap.width_pix / MapManager.getInstance().currentMap.TILE_WIDTH; i++)
            {
                for (int j = 0; j < MapManager.getInstance().currentMap.height_pix / MapManager.getInstance().currentMap.TILE_HEIGHT; j++)
                {
                    MapManager.getInstance().currentMap.addMapEntity(new BaseMapEntity(TextureBank.TerrainTextures.grass, true, new Vector2(i, j)), i, j);
                }
            }
           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            
            InterfaceManager.getInstance().update();
            InputManager.getInstance().Update();


            //PERFORM UPDATE ACTIONS ACCORDING TO THE CURRENT STATE////
            switch (state)
            {
                case State.GameState.Initializing:

                    break;
                    
                case State.GameState.Menu:

                    break;

                case State.GameState.Load_Game:

                    break;

                case State.GameState.Save_Game:

                    break;

                case State.GameState.Character_Creation:
                    currentPlayer = new Player(new Vector2(400, 300));
                    camera = new Camera(currentPlayer.getPos(), graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                    currentState = State.GameState.In_Play;
                    break;

                case State.GameState.Paused:

                    break;

                case State.GameState.Options:

                    break;

                case State.GameState.In_Play:
                    MapManager.getInstance().currentMap.update();
                    //currentPlayer.move(State.Direction.South);
                    camera.update();

                    break;

            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            if (state == State.GameState.Menu)
            {
                graphics.GraphicsDevice.Clear(Color.Wheat);
            }
            else if(state == State.GameState.Character_Creation)
            {
                graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else if (state == State.GameState.Paused)
            {
                graphics.GraphicsDevice.Clear(Color.Wheat);
            }

            //PERFORM UPDATE ACTIONS ACCORDING TO THE CURRENT STATE////
            switch (state)
            {
                case State.GameState.Initializing:

                    break;

                case State.GameState.Menu:

                    break;

                case State.GameState.Load_Game:

                    break;

                case State.GameState.Save_Game:

                    break;

                case State.GameState.Character_Creation:

                    break;

                case State.GameState.Paused:

                    break;

                case State.GameState.Options:

                    break;

                case State.GameState.In_Play:
                    camera.draw(spriteBatch);

                    break;

            }


            InterfaceManager.getInstance().draw(spriteBatch);

            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
