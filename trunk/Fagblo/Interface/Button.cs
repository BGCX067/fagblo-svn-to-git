using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fagblo.Managers;
using Fagblo.Classes;
using Fagblo.Utils;

namespace Fagblo.Interface
{
    public class Button
    {
        public Vector2 position;
        State.ButtonType buttonType;
        bool isPressed;
        bool isFocus;
        bool isHighlighted;
        bool pressedNoActionTaken;
        Texture2D passive;

        public Button()
        {
            
        }

        public Button(Vector2 pos)
        {
            position = pos;
            isPressed = false;
            isFocus = false;
            isHighlighted = false;
            pressedNoActionTaken = false;
        }

        public Button(Vector2 pos, Texture2D passive, State.ButtonType type)
        {
            position = pos;
            this.passive = passive;
            isPressed = false;
            isFocus = false;
            isHighlighted = false;
            pressedNoActionTaken = false;
            buttonType = type;
        }

        public bool getPressed()
        {
            return isPressed;
        }

        public bool getFocus()
        {
            return isFocus;
        }

        public bool wasJustPressed()
        {
            return pressedNoActionTaken;
        }

        public void committedAction()
        {
            pressedNoActionTaken = false;
        }

        public void update(Cursor cursor)
        {
            if (cursor.getPos().X >= position.X && cursor.getPos().X <= position.X + passive.Width && cursor.getPos().Y >= position.Y && cursor.getPos().Y <= position.Y + passive.Height)
            {
                isHighlighted = true;
                if (cursor.isLeftPressed())
                {
                    isPressed = true;
                    isFocus = true;
                }
                else
                {
                    isPressed = false;
                    isFocus = false;
                }
            }
            else
            {
                isHighlighted = false;

                if (cursor.getPos().X < position.X || cursor.getPos().X > position.X + passive.Width || cursor.getPos().Y < position.Y || cursor.getPos().Y > position.Y + passive.Height && isPressed)
                {
                    isFocus = false;
                    isPressed = false;
                }
                else
                {
                    isPressed = false;
                }
            }

            if (cursor.getPos().X >= position.X && cursor.getPos().X <= position.X + passive.Width && cursor.getPos().Y >= position.Y && cursor.getPos().Y <= position.Y + passive.Height && cursor.wasLeftJustReleased())
            {
                pressedNoActionTaken = true;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isPressed)
            {
                spriteBatch.Draw(passive, new Vector2(position.X + 2, position.Y + 2), Color.White);
            }
            else
            {
                spriteBatch.Draw(passive, position, Color.White);
            }

            if (isHighlighted)
            {
                if (isPressed)
                {
                    switch (buttonType)
                    {
                        case State.ButtonType.MenuButton:
                            spriteBatch.Draw(TextureBank.InterfaceTextures.buttonHightLight, new Vector2(position.X + 2, position.Y + 2), Color.White);
                            break;

                        case State.ButtonType.UtilityBarButton:
                            spriteBatch.Draw(TextureBank.InterfaceTextures.utilityBarMenuHighLight, new Vector2(position.X + 2, position.Y + 2), Color.White);
                            break;

                    }
                    
                }
                else
                {
                    switch (buttonType)
                    {
                        case State.ButtonType.MenuButton:
                            spriteBatch.Draw(TextureBank.InterfaceTextures.buttonHightLight, position, Color.White);
                            break;

                        case State.ButtonType.UtilityBarButton:
                            spriteBatch.Draw(TextureBank.InterfaceTextures.utilityBarMenuHighLight, position, Color.White);
                            break;

                    }

                    
                }
                
            }
        }
    }
}
