using System;
using System.Collections.Generic;
using System.Text;

using Fagblo.Classes;
using Fagblo.Managers;
using Fagblo.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fagblo.Classes
{
    public class Player
    {
        private Vector2 PlayerPos;
        public State.PlayerState currentState;
        public State.Direction playerDirection;
        public Color lolcolor = Color.Red;

        private float drawOffSetX = TextureBank.EntityTextures.Player.Width / 2;
        private float drawOffSetY = TextureBank.EntityTextures.Player.Height * (float)0.90;

        public String name = "Player";
        public int maxHealth = 100;
        public int health = 85;
        public int maxEnergy = 100;
        public int energy = 45;
        public int strength = 1;
        public int intellect = 1;
        //TODO: other stats

        public long experience = 0;
        public long experienceToLevel = 100;
        public long level = 1;
        public long MONETARY_UNIT = 0;


        
        public Player(Vector2 PlayerPos)
        {
            this.PlayerPos = PlayerPos;
            this.currentState = State.PlayerState.Idle;
        }

        public Vector2 getPos()
        {
            return PlayerPos;
        }

        public void move(State.Direction direction)
        {
            this.playerDirection = direction;

            switch(direction)
            {
                case State.Direction.North:
                    PlayerPos.Y = PlayerPos.Y - 2;
                    break;

                case State.Direction.NorthEast:
                    PlayerPos.Y = PlayerPos.Y - 2;
                    PlayerPos.X = PlayerPos.X + 2;
                    break;

                case State.Direction.NorthWest:
                    PlayerPos.Y = PlayerPos.Y - 2;
                    PlayerPos.X = PlayerPos.X - 2;
                    break;

                case State.Direction.East:
                    PlayerPos.X = PlayerPos.X + 2;
                    break;

                case State.Direction.West:
                    PlayerPos.X = PlayerPos.X - 2;
                    break;

                case State.Direction.South:
                    PlayerPos.Y = PlayerPos.Y + 2;
                    break;

                case State.Direction.SouthEast:
                    PlayerPos.Y = PlayerPos.Y + 2;
                    PlayerPos.X = PlayerPos.X + 2;
                    break;

                case State.Direction.SouthWest:
                    PlayerPos.Y = PlayerPos.Y + 2;
                    PlayerPos.X = PlayerPos.X - 2;
                    break;
            }

        }

        public void draw(SpriteBatch spriteBatch)
        {
            Vector2 positionOnScreen = MapManager.getInstance().currentMap.positionOnScreen(PlayerPos);

            if (lolcolor == Color.Red)
            {
                lolcolor = Color.White;
            }
            else if (lolcolor == Color.White)
            {
                lolcolor = Color.Red;
            }


            switch (this.currentState)
            {
                //CASE IDLE////
                case State.PlayerState.Idle:
                    /*switch (this.playerDirection)
                    {
                        case State.Direction.East:

                            break;

                        case State.Direction.North:

                            break;

                        case State.Direction.NorthEast:

                            break;

                        case State.Direction.NorthWest:

                            break;

                        case State.Direction.South:

                            break;

                        case State.Direction.SouthEast:

                            break;

                        case State.Direction.SouthWest:

                            break;

                        case State.Direction.West:

                            break;
                    }*/

                    spriteBatch.Draw(TextureBank.EntityTextures.Player, Fagblo.getInstance().camera.getPlayerOnScreenPosition(), Color.White);

                    break;

                //CASE MOVING////
                case State.PlayerState.Moving:
                    /*switch (this.playerDirection)
                    {
                        case State.Direction.East:

                            break;

                        case State.Direction.North:

                            break;

                        case State.Direction.NorthEast:

                            break;

                        case State.Direction.NorthWest:

                            break;

                        case State.Direction.South:

                            break;

                        case State.Direction.SouthEast:

                            break;

                        case State.Direction.SouthWest:

                            break;

                        case State.Direction.West:

                            break;
                    }*/

                    spriteBatch.Draw(TextureBank.EntityTextures.Player, Fagblo.getInstance().camera.getPlayerOnScreenPosition(), Color.Blue);

                    break;

                //CASE ATTACKING////
                case State.PlayerState.Attacking:
                    switch (this.playerDirection)
                    {
                        case State.Direction.East:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING EAST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.North:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING NORTH", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.NorthEast:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING NORTHEAST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.NorthWest:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING NORTHWEST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.South:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING SOUTH", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.SouthEast:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING SOUTHEAST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.SouthWest:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING SOUTHWEST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                        case State.Direction.West:
                            spriteBatch.DrawString(InterfaceManager.getInstance().getFont(), "ATTACKING WEST", new Vector2(Fagblo.getInstance().camera.getPlayerOnScreenPosition().X, Fagblo.getInstance().camera.getPlayerOnScreenPosition().Y - 100), lolcolor);
                            break;

                            
                    }

                    spriteBatch.Draw(TextureBank.EntityTextures.Player, Fagblo.getInstance().camera.getPlayerOnScreenPosition(), Color.Red);
                    
                    break;
                
                //CASE DEAD////
                case State.PlayerState.Dead:
                    switch (this.playerDirection)
                    {
                        case State.Direction.East:

                            break;

                        case State.Direction.North:

                            break;

                        case State.Direction.NorthEast:

                            break;

                        case State.Direction.NorthWest:

                            break;

                        case State.Direction.South:

                            break;

                        case State.Direction.SouthEast:

                            break;

                        case State.Direction.SouthWest:

                            break;

                        case State.Direction.West:

                            break;
                    }
                    break;
            }
        }
    }
    
    

}
