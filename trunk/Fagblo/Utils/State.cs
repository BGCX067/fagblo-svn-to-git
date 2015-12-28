using System;
using System.Collections.Generic;
using System.Text;

namespace Fagblo.Utils
{
    public static class State
    {
        public enum GameState
        {
            None,   //Default value; no state set
            
            Initializing,   //Program just started

            Menu,

            Character_Creation,

            Load_Game,

            Save_Game,

            In_Play,

            Paused,

            Options,

        }

        public enum Direction
        {
            North,

            NorthEast,

            NorthWest,

            South,

            SouthEast,

            SouthWest,

            East,

            West,

        }

        public enum EntityId
        {
            HUMAN,

            COMPUTER,

            NEUTRAL,
        }

        public enum PlayerState
        {
            Idle,

            Moving,

            Attacking,

            Dead,
        }

        public enum ButtonType
        {
            MenuButton,

            UtilityBarButton,
        }
    }
}
