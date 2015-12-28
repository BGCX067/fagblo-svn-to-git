using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

using Fagblo.Classes;
using Fagblo.Interface;
using Fagblo.Managers;
using Fagblo.Utils;

namespace Fagblo.Managers
{
    public class InputManager
    {
        private static InputManager instance;

        public KeyboardState CurrentKeyboardState;

        public KeyboardState PreviousKeyboardState;
        State.GameState currentState;

        public static InputManager initializeInputManager()
        {
            instance = new InputManager();
            return instance;
        }

        public static InputManager getInstance()
        {
            if (instance == null)
            {
                //uh oh, InputManager was not initialized
            }
            return instance;
        }

        public void Update()
        {
            PreviousKeyboardState = CurrentKeyboardState;

            CurrentKeyboardState = Keyboard.GetState();
            currentState = Fagblo.getInstance().currentState;

            switch (currentState)
            {
                case State.GameState.Menu:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }
                    break;

                case State.GameState.Load_Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }
                    break;

                case State.GameState.Save_Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }
                    break;

                case State.GameState.Character_Creation:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }
                    break;

                case State.GameState.Paused:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }

                    break;

                case State.GameState.Options:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //EXIT MENU
                    }
                    break;

                case State.GameState.In_Play:

                    if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.NorthWest);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.NorthEast);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.SouthWest);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.SouthEast);
                    }
                    else if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        //MOVE PLAYER UP
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.North);
                    }               
                    else if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        //MOVE PLAYER DOWN
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.South);
                    }                   
                    else if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        //MOVE PLAYER RIGHT
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.East);
                    }                   
                    else if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        //MOVE PLAYER LEFT
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Moving;
                        Fagblo.getInstance().currentPlayer.move(State.Direction.West);
                    }
                    
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //OPEN MENU
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.N))
                    {
                        //OPEN CHARACTER MENU
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.I))
                    {
                        //OPEN INVENTORY
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        //OPEN MAP MENU
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        //PAUSE GAME
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        //OPEN OPTIONS
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.F8))
                    {
                        //OPEN SAVE MENU
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.F9))
                    {
                        //OPEN LOAD MENU
                    }
                    //MOVING TO IDLE STATE CHANGES
                    if (PreviousKeyboardState.IsKeyDown(Keys.W) == true && CurrentKeyboardState.IsKeyUp(Keys.W) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    if (PreviousKeyboardState.IsKeyDown(Keys.S) == true && CurrentKeyboardState.IsKeyUp(Keys.S) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    if (PreviousKeyboardState.IsKeyDown(Keys.D) == true && CurrentKeyboardState.IsKeyUp(Keys.D) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    if (PreviousKeyboardState.IsKeyDown(Keys.A) == true && CurrentKeyboardState.IsKeyUp(Keys.A) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    //NE
                    if (PreviousKeyboardState.IsKeyDown(Keys.W) && PreviousKeyboardState.IsKeyDown(Keys.D) == true &&
                        CurrentKeyboardState.IsKeyUp(Keys.W) && CurrentKeyboardState.IsKeyUp(Keys.D) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    //NW
                    if (PreviousKeyboardState.IsKeyDown(Keys.W) && PreviousKeyboardState.IsKeyDown(Keys.A) == true &&
                        CurrentKeyboardState.IsKeyUp(Keys.W) && CurrentKeyboardState.IsKeyUp(Keys.A) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    //SE
                    if (PreviousKeyboardState.IsKeyDown(Keys.S) && PreviousKeyboardState.IsKeyDown(Keys.D) == true &&
                        CurrentKeyboardState.IsKeyUp(Keys.S) && CurrentKeyboardState.IsKeyUp(Keys.D) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }
                    //SW
                    if (PreviousKeyboardState.IsKeyDown(Keys.S) && PreviousKeyboardState.IsKeyDown(Keys.A) == true &&
                        CurrentKeyboardState.IsKeyUp(Keys.S) && CurrentKeyboardState.IsKeyUp(Keys.A) == true)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }

                    //determine if we're clicking the playfield
                    if (InterfaceManager.getInstance().cursor.isLeftPressed() && InterfaceManager.getInstance().cursor.getPos().Y < InterfaceManager.getInstance().getUtilityBarBoundary())
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Attacking;

                        //now to determine the direction
                        //the basic algorithm will be to split up the playfield into 8 fields, one for all eight directions
                        //once the field is split, we then find which one the cursor is in
                        //i hate math
                        Vector2 mousePos = InterfaceManager.getInstance().cursor.getPos();
                        Vector2 playerPos = MapManager.getInstance().currentMap.positionOnScreen(Fagblo.getInstance().currentPlayer.getPos());

                        double angle;

                        //easy cases first
                        if (mousePos.X == playerPos.X && mousePos.Y < playerPos.Y)
                        {
                            Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.North;
                        }
                        else if(mousePos.X == playerPos.X && mousePos.Y >= playerPos.Y){
                            Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.South;
                        }
                        else if (mousePos.X >= playerPos.X && mousePos.Y == playerPos.Y)
                        {
                            Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.East;
                        }
                        else if (mousePos.X < playerPos.X && mousePos.Y == playerPos.Y)
                        {
                            Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.West;
                        }
                        else
                        {

                            //quadrant based, man this sucks
                            if (playerPos.X < mousePos.X && playerPos.Y > mousePos.Y)
                            {
                                angle = (180 / Math.PI) * Math.Atan((playerPos.Y - mousePos.Y) / (mousePos.X - playerPos.X));
                            }
                            else if (playerPos.X > mousePos.X && playerPos.Y > mousePos.Y)
                            {
                                angle = 90 + (90 - (180 / Math.PI) * Math.Atan((playerPos.Y - mousePos.Y) / (playerPos.X - mousePos.X)));
                            }
                            else if (playerPos.X > mousePos.X && playerPos.Y < mousePos.Y)
                            {
                                angle = 180 + (180 / Math.PI) * Math.Atan((mousePos.Y - playerPos.Y) / (playerPos.X - mousePos.X));
                            }
                            else if (playerPos.X < mousePos.X && playerPos.Y < mousePos.Y)
                            {
                                angle = 270 + (90 - (180 / Math.PI) * Math.Atan((mousePos.Y - playerPos.Y) / (mousePos.X - playerPos.X)));
                            }
                            else
                            {
                                angle = 0;
                            }



                            //NOW, find which field the cursor is in
                            if (angle > 330 || angle <= 30)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.East;
                            }
                            else if (angle > 30 && angle <= 60)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.NorthEast;
                            }
                            else if (angle > 60 && angle <= 120)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.North;
                            }
                            else if (angle > 120 && angle <= 150)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.NorthWest;
                            }
                            else if (angle > 150 && angle <= 210)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.West;
                            }
                            else if (angle > 210 && angle <= 240)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.SouthWest;
                            }
                            else if (angle > 240 && angle <= 300)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.South;
                            }
                            else if (angle > 300 && angle <= 330)
                            {
                                Fagblo.getInstance().currentPlayer.playerDirection = State.Direction.SouthEast;
                            }
                            else
                            {
                                //if we got here, uh oh
                            }
                        }
                    }

                    //now check to see if we released
                    if (InterfaceManager.getInstance().cursor.wasLeftJustReleased() && Fagblo.getInstance().currentPlayer.currentState == State.PlayerState.Attacking)
                    {
                        Fagblo.getInstance().currentPlayer.currentState = State.PlayerState.Idle;
                    }

                    break;
            }
        }
    }
}
