using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Fagblo.Managers
{
    class TextureManager
    {
        private static TextureManager instance;

        /// <summary>
        /// Loads a texture definition file, loading all appropriate textures into Content
        /// </summary>
        /// <param name="content">Game's content manager</param>
        /// <returns>TextureManager instance</returns>
        public static TextureManager initializeTextureManager(ContentManager content)
        {
            instance = new TextureManager(content);
            return instance;
        }

        public static TextureManager getInstance()
        {
            if (instance == null)
            {
                //uh oh, TextureManager was not initialized
            }
            return instance;
        }

        private TextureManager(ContentManager content)
        {
            //TODO: load texture definitions
            //Example:
            //TextureBank.EntityTextures.example = Content.Load<Texture2D>(@"example");

            //ENTITY TEXTURES////
            TextureBank.EntityTextures.Player = content.Load<Texture2D>(@"Entities/Player");

            //TERRAIN TEXTURES////
            TextureBank.TerrainTextures.grass = content.Load<Texture2D>(@"Terrain/Grass");


            //INTERFACE TEXTURES/////
            TextureBank.InterfaceTextures.cursor = content.Load<Texture2D>(@"Interface/Cursor");
            TextureBank.InterfaceTextures.attackcursor = content.Load <Texture2D>(@"Interface/attackCursor");
            TextureBank.InterfaceTextures.buttonHightLight = content.Load<Texture2D>(@"Interface/ButtonHighLight");
            TextureBank.InterfaceTextures.newGameButton = content.Load<Texture2D>(@"Interface/NewGameButton");
            TextureBank.InterfaceTextures.exitGameButton = content.Load<Texture2D>(@"Interface/ExitGameButton");
            TextureBank.InterfaceTextures.backButton = content.Load<Texture2D>(@"Interface/BackButton");
            TextureBank.InterfaceTextures.utilityBar = content.Load<Texture2D>(@"Interface/UtilityBar");
            TextureBank.InterfaceTextures.healthBar = content.Load<Texture2D>(@"Interface/HealthBar");
            TextureBank.InterfaceTextures.energyBar = content.Load<Texture2D>(@"Interface/EnergyBar");
            TextureBank.InterfaceTextures.utilityBarMenuHighLight = content.Load<Texture2D>(@"Interface/UtilityButtonHightLight");
            TextureBank.InterfaceTextures.utilityBarMenuButton = content.Load<Texture2D>(@"Interface/UtilityBarMenuButton");

        }
    }
}
