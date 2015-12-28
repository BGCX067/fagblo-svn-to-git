using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fagblo.Classes;
using Fagblo.Interface;
using Fagblo.Managers;
using Fagblo.Utils;

namespace Fagblo.Managers
{
    public class InterfaceManager
    {
        private static InterfaceManager instance;
        SpriteFont font;
        public Cursor cursor;
        State.GameState currentState;

        private static int screenWidth;
        private static int screenHeight;

        //Player Attributes////
        double healthRatio;
        double energyRatio;

        //UtilityBar Components////
        bool mouseOverHealth = false;
        bool mouseOverEnergy = false;
        Button utilityBarMenuButton;

        //Paused State Components////
        Button resumeButton;
        Button exitFromPausedButton;

        //Menu State Components////
        Button newGameButton;
        Button exitGameButton;


        //Character Creation Components////
        Button backButton;

        public static InterfaceManager initializeInterfaceManager(int width, int height)
        {
            instance = new InterfaceManager();
            screenWidth = width;
            screenHeight = height;
            return instance;
        }

        public static InterfaceManager getInstance()
        {
            if (instance == null)
            {
                //uh oh, InterfaceManager was not initialized
            }
            return instance;
        }

        private InterfaceManager()
        {
            cursor = new Cursor(new Vector2(0, 0));
        }

        public void loadTextures()
        {
            newGameButton = new Button(new Vector2(580, 500), TextureBank.InterfaceTextures.newGameButton, State.ButtonType.MenuButton);
            exitGameButton = new Button(new Vector2(580, 540), TextureBank.InterfaceTextures.exitGameButton, State.ButtonType.MenuButton);
            backButton = new Button(new Vector2(580, 500), TextureBank.InterfaceTextures.backButton, State.ButtonType.MenuButton);
            utilityBarMenuButton = new Button(new Vector2(screenWidth / 2 - 59, screenHeight - 135), TextureBank.InterfaceTextures.utilityBarMenuButton, State.ButtonType.UtilityBarButton);
            resumeButton = new Button(new Vector2(580, 500), TextureBank.InterfaceTextures.backButton, State.ButtonType.MenuButton);
            exitFromPausedButton = new Button(new Vector2(580, 540), TextureBank.InterfaceTextures.exitGameButton, State.ButtonType.MenuButton);


        }

        public void setFont(SpriteFont newFont)
        {
            font = newFont;
        }

        public SpriteFont getFont()
        {
            return font;
        }

        public int getUtilityBarBoundary()
        {
            return screenHeight - 135;
        }

        public void update()
        {
            this.mouseOverEnergy = false;
            this.mouseOverHealth = false;

            currentState = Fagblo.getInstance().currentState;

            cursor.update();

            //UPDATE INTERFACE FOR THE CURRENT STATE////
            switch (currentState)
            {
                case State.GameState.Menu:
                    newGameButton.update(cursor);
                    exitGameButton.update(cursor);

                    //interactions////
                    if (newGameButton.wasJustPressed())
                    {
                        Fagblo.getInstance().currentState = State.GameState.Character_Creation;
                        newGameButton.committedAction();
                    }

                    if (exitGameButton.wasJustPressed())
                    {
                        Fagblo.getInstance().Exit();
                        exitGameButton.committedAction();
                    }
                    break;

                case State.GameState.Load_Game:

                    break;

                case State.GameState.Save_Game:

                    break;

                case State.GameState.Character_Creation:
                    backButton.update(cursor);

                    //interactions////
                    if (backButton.wasJustPressed())
                    {
                        Fagblo.getInstance().currentState = State.GameState.Menu;
                        backButton.committedAction();
                    }
                    break;

                case State.GameState.Paused:
                    resumeButton.update(cursor);
                    exitFromPausedButton.update(cursor);

                    //interactions////
                    if (resumeButton.wasJustPressed())
                    {
                        Fagblo.getInstance().currentState = State.GameState.In_Play;
                        resumeButton.committedAction();
                    }

                    if (exitFromPausedButton.wasJustPressed())
                    {
                        Fagblo.getInstance().Exit();
                        exitFromPausedButton.committedAction();
                    }

                    break;

                case State.GameState.Options:

                    break;

                case State.GameState.In_Play:
                    healthRatio = (float)Fagblo.getInstance().currentPlayer.health / Fagblo.getInstance().currentPlayer.maxHealth;
                    energyRatio = (float)Fagblo.getInstance().currentPlayer.energy / Fagblo.getInstance().currentPlayer.maxEnergy;

                    //if mouse cursor is hovering over health bar, tell the player their current/max health
                    if (cursor.getPos().X >= screenWidth * 0.10 && cursor.getPos().X <= screenWidth * 0.10 + TextureBank.InterfaceTextures.healthBar.Width &&
                        cursor.getPos().Y >= screenHeight - 135 && cursor.getPos().Y <= screenHeight - 135 + TextureBank.InterfaceTextures.healthBar.Height)
                    {
                        this.mouseOverHealth = true;
                    }

                    //if mouse cursor is hovering over energy bar, tell the player their current/max energy
                    if (cursor.getPos().X >= screenWidth * 0.55 && cursor.getPos().X <= screenWidth * 0.55 + TextureBank.InterfaceTextures.energyBar.Width &&
                        cursor.getPos().Y >= screenHeight - 135 && cursor.getPos().Y <= screenHeight - 135 + TextureBank.InterfaceTextures.energyBar.Height)
                    {
                        this.mouseOverEnergy = true;
                    }

                    utilityBarMenuButton.update(cursor);

                    //interactions////
                    if (utilityBarMenuButton.wasJustPressed())
                    {
                        Fagblo.getInstance().currentState = State.GameState.Paused;
                        utilityBarMenuButton.committedAction();
                    }

                    break;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {

            //DRAW ONLY FOR THE CURRENT STATE////
            switch (currentState)
            {
                case State.GameState.Menu:
                    spriteBatch.DrawString(font, "Current State: Menu", new Vector2(100, 100), Color.White);
                    newGameButton.draw(spriteBatch);
                    exitGameButton.draw(spriteBatch);
                    break;

                case State.GameState.Load_Game:
                    spriteBatch.DrawString(font, "Current State: Load_Game", new Vector2(100, 100), Color.White);
                    break;

                case State.GameState.Save_Game:
                    spriteBatch.DrawString(font, "Current State: Save_Game", new Vector2(100, 100), Color.White);
                    break;

                case State.GameState.Character_Creation:
                    spriteBatch.DrawString(font, "Current State: Character_Creation", new Vector2(100, 100), Color.White);
                    backButton.draw(spriteBatch);
                    break;

                case State.GameState.Paused:
                    spriteBatch.DrawString(font, "Current State: Paused", new Vector2(100, 100), Color.White);
                    resumeButton.draw(spriteBatch);
                    exitFromPausedButton.draw(spriteBatch);
                    break;

                case State.GameState.Options:
                    spriteBatch.DrawString(font, "Current State: Options", new Vector2(100, 100), Color.White);
                    break;

                case State.GameState.In_Play:
                    spriteBatch.DrawString(font, "Current State: In_Play", new Vector2(100, 100), Color.White);
                    spriteBatch.Draw(TextureBank.InterfaceTextures.utilityBar, new Rectangle(-1, screenHeight - TextureBank.InterfaceTextures.utilityBar.Height, screenWidth + 1, TextureBank.InterfaceTextures.utilityBar.Height), Color.White);
                    spriteBatch.Draw(TextureBank.InterfaceTextures.healthBar, new Rectangle((int)(0.10 * screenWidth), screenHeight - 135, (int)(healthRatio * TextureBank.InterfaceTextures.healthBar.Width), TextureBank.InterfaceTextures.healthBar.Height), Color.White);
                    spriteBatch.Draw(TextureBank.InterfaceTextures.energyBar, new Rectangle((int)(0.55 * screenWidth), screenHeight - 135, (int)(energyRatio * TextureBank.InterfaceTextures.energyBar.Width), TextureBank.InterfaceTextures.energyBar.Height), Color.White);
                    
                    utilityBarMenuButton.draw(spriteBatch);

                    if (mouseOverHealth)
                    {
                        spriteBatch.DrawString(font, Fagblo.getInstance().currentPlayer.health + " / " + Fagblo.getInstance().currentPlayer.maxHealth, new Vector2((float)( screenWidth * 0.10 + TextureBank.InterfaceTextures.healthBar.Width / 2), screenHeight - 133), Color.White);
                    }

                    if (mouseOverEnergy)
                    {
                        spriteBatch.DrawString(font, Fagblo.getInstance().currentPlayer.energy + " / " + Fagblo.getInstance().currentPlayer.maxEnergy, new Vector2((float)( screenWidth * 0.55 + TextureBank.InterfaceTextures.energyBar.Width / 2), screenHeight - 133), Color.White);
                    }
                    
                    break;
            }


            cursor.draw(spriteBatch);
        }
    }
}
