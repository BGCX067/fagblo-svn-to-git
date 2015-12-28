using System;
using System.Collections.Generic;
using System.Text;

using Fagblo.Classes;
using Fagblo.Utils;

namespace Fagblo.Managers
{
    public class MapManager
    {
        private static MapManager instance;

        public static int TILE_WIDTH = 64;
        public static int TILE_HEIGHT = 64;

        public MapBase currentMap;

        private MapManager()
        {

        }

        public static void initializeMapManager()
        {
            instance = new MapManager();
        }

        public static MapManager getInstance()
        {
            if (instance == null)
            {
                instance = new MapManager();
            }

            return instance;
        }

        public MapBase setCurrentLevel(int level)
        {
            //FOR TESTING
            if (level == 1)
            {
                currentMap = new MapBase(2048, 2048, 1280, 720);
            }

            return currentMap;
        }
    }
}
