using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Fagblo.Managers;

namespace Fagblo.Utils
{
    public class Camera
    {
        private Vector2 pos;
        private Vector2 savedPlayerPos;

        private int screenWidth;
        private int screenHeight;

        private int xStart;
        private int xEnd;
        private int yStart;
        private int yEnd;

        private int xRemain;
        private int yRemain;



        public Camera()
        {
            pos = new Vector2(-1, -1);
            savedPlayerPos = new Vector2(-1, -1);
        }

        public Camera(Vector2 playerPos)
        {
            pos = playerPos;
            savedPlayerPos = playerPos;
        }

        public Camera(Vector2 playerPos, int width, int height)
        {
            pos = playerPos;
            savedPlayerPos = playerPos;
            screenWidth = width;
            screenHeight = height;
        }

        public void update()
        {
            pos = Fagblo.getInstance().currentPlayer.getPos();


            MapManager.getInstance().currentMap.translatePixelToMapCell((int)pos.X - (screenWidth / 2), (int)pos.Y - (screenHeight / 2), out xStart, out yStart);
            MapManager.getInstance().currentMap.translatePixelToMapCell((int)pos.X + (screenWidth / 2), (int)pos.Y + (screenHeight / 2), out xEnd, out yEnd);

            //boundary cases
            if (xStart <= 0)
            {
                xStart = 0;
                xEnd = screenWidth / MapManager.TILE_WIDTH;
                xRemain = 0;
            }
            else if (xEnd > MapManager.getInstance().currentMap.width_pix / MapManager.getInstance().currentMap.TILE_WIDTH)
            {
                xEnd = MapManager.getInstance().currentMap.width_pix / MapManager.getInstance().currentMap.TILE_WIDTH;
                xStart = xEnd - ( screenWidth / MapManager.TILE_WIDTH );
                xRemain = 0;
            }
            else
            {
                //xRemain = Math.Abs((int)(pos.X - (screenWidth / 2) % MapManager.getInstance().currentMap.TILE_WIDTH));

                if (xStart == 0)
                {
                    //yRemain = MapManager.TILE_HEIGHT - Math.Abs((int)((pos.Y - (screenHeight / 2)) % MapManager.getInstance().currentMap.TILE_HEIGHT));
                    xRemain = MapManager.TILE_WIDTH - Math.Abs((int)(pos.X - (screenWidth / 2)));
                }
                else
                {
                    xRemain = Math.Abs(MapManager.TILE_WIDTH - (int)((pos.X - (screenWidth / 2)) % MapManager.getInstance().currentMap.TILE_WIDTH));
                }
            }

            if (yStart <= 0)
            {
                yStart = 0;
                yEnd = screenHeight / MapManager.TILE_HEIGHT;
                yRemain = 0;
            }
            else if (yEnd > MapManager.getInstance().currentMap.height_pix / MapManager.getInstance().currentMap.TILE_HEIGHT)
            {
                yEnd = MapManager.getInstance().currentMap.height_pix / MapManager.getInstance().currentMap.TILE_HEIGHT;
                yStart = yEnd - ( screenHeight / MapManager.TILE_HEIGHT );
                yRemain = 0;
            }
            else
            {
                //yRemain = Math.Abs((int)(pos.Y - (screenHeight / 2) % MapManager.getInstance().currentMap.TILE_HEIGHT));

                if (yStart == 0)
                {
                    //yRemain = MapManager.TILE_HEIGHT - Math.Abs((int)((pos.Y - (screenHeight / 2)) % MapManager.getInstance().currentMap.TILE_HEIGHT));
                    yRemain = MapManager.TILE_HEIGHT - Math.Abs((int)(pos.Y - (screenHeight / 2)));
                }
                else
                {
                    yRemain = Math.Abs(MapManager.TILE_HEIGHT - (int)((pos.Y - (screenHeight / 2)) % MapManager.getInstance().currentMap.TILE_HEIGHT));
                }
                
            }
   
        }

        public void draw(SpriteBatch spriteBatch)
        {
            //draw all terrain
            int xScreen = 0 + xRemain;
            int yScreen = 0 + yRemain;

            for (int i = yStart; i < yEnd; i++)
            {
                xScreen = 0 + xRemain;
                for (int j = xStart; j < xEnd; j++)
                {
                    spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[j, i].texture, new Rectangle(xScreen, yScreen, MapManager.TILE_WIDTH, MapManager.TILE_HEIGHT), Color.White);
                    xScreen= xScreen + MapManager.getInstance().currentMap.TILE_WIDTH;
                }
                yScreen = yScreen + MapManager.getInstance().currentMap.TILE_HEIGHT;
            }

            //boundary terrain
            if (xRemain != 0)
            {
                yScreen = 0 + yRemain;
                for (int i = yStart; i < yEnd; i++)
                {
                    spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[xStart, i].texture, new Rectangle(0, yScreen, xRemain,MapManager.TILE_HEIGHT), new Rectangle(MapManager.TILE_WIDTH - xRemain, 0, xRemain, MapManager.TILE_HEIGHT), Color.White);
                    yScreen = yScreen + MapManager.getInstance().currentMap.TILE_HEIGHT;
                }

                if (xEnd == MapManager.TILE_WIDTH * MapManager.getInstance().currentMap.width_pix - 1)
                {
                    yScreen = 0 + yRemain;
                    for (int i = yStart; i < yEnd; i++)
                    {
                        spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[xEnd, i].texture, new Rectangle(0, yScreen, xRemain, MapManager.TILE_HEIGHT), new Rectangle(MapManager.TILE_WIDTH - xRemain, 0, xRemain, MapManager.TILE_HEIGHT), Color.White);
                        yScreen = yScreen + MapManager.getInstance().currentMap.TILE_HEIGHT;
                    }
                }
            }

            if (yRemain != 0)
            {
                xScreen = 0 + xRemain;
                for (int i = xStart; i < xEnd; i++)
                {
                    spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[i, yStart].texture, new Rectangle(xScreen, 0, MapManager.TILE_WIDTH, yRemain), new Rectangle(0, MapManager.TILE_HEIGHT - yRemain, MapManager.TILE_WIDTH, yRemain), Color.White);
                    xScreen = xScreen + MapManager.getInstance().currentMap.TILE_WIDTH;
                }

                if (yEnd == MapManager.TILE_HEIGHT * MapManager.getInstance().currentMap.height_pix - 1)
                {
                    xScreen = 0 + xRemain;
                    for (int i = xStart; i < xEnd; i++)
                    {
                        spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[i, yEnd].texture, new Rectangle(xScreen, 0, MapManager.TILE_WIDTH, yRemain), new Rectangle(0, MapManager.TILE_HEIGHT - yRemain, MapManager.TILE_WIDTH, yRemain), Color.White);
                        xScreen = xScreen + MapManager.getInstance().currentMap.TILE_WIDTH;
                    }
                }
            }

            if (xRemain != 0 && yRemain != 0 && Fagblo.getInstance().currentPlayer.playerDirection == State.Direction.NorthWest)
            {
                if (xStart - 1 >= 0 && yStart - 1 >= 0)
                {
                    spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[xStart - 1, yStart - 1].texture, new Rectangle(0, 0, xRemain, yRemain), new Rectangle(MapManager.TILE_WIDTH - xRemain, MapManager.TILE_HEIGHT - yRemain, xRemain, yRemain), Color.White);
                }
            }
            else if (xRemain != 0 && yRemain != 0 && Fagblo.getInstance().currentPlayer.playerDirection == State.Direction.SouthEast)
            {
                if (xStart - 1 >= 0 && yStart - 1 >= 0)
                {
                    spriteBatch.Draw(MapManager.getInstance().currentMap.tileGrid[xStart - 1, yStart - 1].texture, new Rectangle(0, 0, xRemain, yRemain), new Rectangle(MapManager.TILE_WIDTH - xRemain, MapManager.TILE_HEIGHT - yRemain, xRemain, yRemain), Color.White);
                }
            }
            

            //TODO: draw map objects

            //TODO: draw entities

            //draw player
            Fagblo.getInstance().currentPlayer.draw(spriteBatch);
        }

        public Vector2 getPlayerOnScreenPosition()
        {
            return new Vector2(this.pos.X - (xStart * MapManager.TILE_WIDTH) + xRemain, this.pos.Y - (yStart * MapManager.TILE_HEIGHT) + yRemain);
        }

    }
}
