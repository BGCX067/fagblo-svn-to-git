using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Fagblo.Managers;

namespace Fagblo.Interface
{
    public class Cursor
    {
        private Vector2 pos;
        private MouseState mouseState;
        private MouseState previousState;

        public Cursor(Vector2 pos)
        {
            this.pos = pos;
        }

        public void update()
        {
            previousState = mouseState; //remember previous state
            mouseState = Mouse.GetState();
            this.pos.X = mouseState.X;
            this.pos.Y = mouseState.Y;
 
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (isLeftPressed() || isRightPressed())
            {

            }
            else
            {
                spriteBatch.Draw(TextureBank.InterfaceTextures.cursor, pos, Color.White);
            }
        }

        public Vector2 getPos()
        {
            return pos;
        }

        public MouseState getMouseState()
        {
            return mouseState;
        }        

        public bool isLeftPressed()
        {
            return (mouseState.LeftButton == ButtonState.Pressed);
        }

        public bool isRightPressed()
        {
            return (mouseState.RightButton == ButtonState.Pressed);
        }

        public bool isLeftReleased()
        {
            return (mouseState.LeftButton == ButtonState.Released);
        }

        public bool isRightReleased()
        {
            return (mouseState.RightButton == ButtonState.Released);
        }

        public bool wasLeftJustReleased()
        {
            if (previousState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public bool wasRightJustReleased()
        {
            if (previousState.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

    }
}
