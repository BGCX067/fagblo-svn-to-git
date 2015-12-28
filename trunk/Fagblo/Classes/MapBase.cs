using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Fagblo.Utils;
using Fagblo.Managers;

namespace Fagblo.Classes
{
    public class MapBase
    {
        public int width_pix;
        public int height_pix;

        public BaseMapEntity[,] tileGrid;    //terrain tiles such as grass, water
        public List<BaseMapEntity> objectList;   //map objects such as trees, barrels

        public byte[,] occupancyGrid;

        public int minViewableX;
        public int maxViewableX;
        public int minViewableY;
        public int maxViewableY;

        public int TILE_WIDTH = MapManager.TILE_WIDTH;
        public int TILE_HEIGHT = MapManager.TILE_HEIGHT;

        public MapBase()
        {
            width_pix = 0;
            height_pix = 0;
            this.minViewableX = 0;
            this.maxViewableX = 0;
            this.minViewableY = 0;
            this.maxViewableY = 0;
            this.occupancyGrid = new byte[0, 0];
            this.tileGrid = new BaseMapEntity[0, 0];
            this.objectList = new List<BaseMapEntity>();
        }

        public MapBase(int width, int height, int screenWidth, int screenHeight)
        {
            this.width_pix = width;
            this.height_pix = height;
            this.tileGrid = new BaseMapEntity[width_pix / TILE_WIDTH, height_pix / TILE_HEIGHT];
            this.occupancyGrid = new byte[width_pix / TILE_WIDTH, height_pix / TILE_HEIGHT];
            this.objectList = new List<BaseMapEntity>();
            this.minViewableX = 0;
            this.minViewableY = 0;
            this.maxViewableX = screenWidth / TILE_WIDTH;
            this.maxViewableY = screenHeight / TILE_HEIGHT;

            //initialize occupancyGrid
            for (int x = 0; x < this.height_pix / TILE_HEIGHT; x++)
            {
                for (int y = 0; y < this.width_pix / TILE_WIDTH; y++)
                {
                    occupancyGrid[x, y] = 1;
                }
            }
        }

        public virtual void initializeMap()
        {
            this.loadTerrain();
        }
        
        public void update()
        {
            //do we need this?////
        }

        public void draw(SpriteBatch spriteBatch)
        {
            int screenX = 0;
            int screenY = 0;

            for (int j = minViewableY; j < maxViewableY; j++)
            {
                screenX = 0;
                for (int i = minViewableX; i < maxViewableX; i++)
                {
                    spriteBatch.Draw(tileGrid[i, j].texture, new Rectangle(screenX * TILE_WIDTH, screenY * TILE_HEIGHT, TILE_WIDTH, TILE_HEIGHT), Color.White);
                    screenX++;
                }
                screenY++;
            }

            objectList.ForEach(delegate(BaseMapEntity e)
            {
                int xPos;
                int yPos;
                this.translateMapCellToPixel((int)e.location.X, (int)e.location.Y, out xPos, out yPos);

                if (this.isOnScreen(new Vector2(xPos, yPos)))
                {
                    Vector2 locationOnScreen = this.positionOnScreen(new Vector2(xPos, yPos));

                    spriteBatch.Draw(e.texture, new Rectangle((int)(locationOnScreen.X - (e.texture.Width / 2)), (int)(locationOnScreen.Y - (e.texture.Height * 0.90)), e.texture.Width, e.texture.Height), Color.White);
                }
            });
        }

        public Vector2 positionOnScreen(Vector2 coords)
        {
            if (isOnScreen(coords))
            {
                return new Vector2(coords.X - (this.minViewableX * TILE_WIDTH), coords.Y - (this.minViewableY * TILE_HEIGHT));
            }
            else
            {
                return new Vector2(-1, -1);
            }
        }

        public void addMapEntity(BaseMapEntity entity, int xPos, int yPos)
        {
            if (xPos >= 0 && xPos < this.width_pix / TILE_WIDTH && yPos >= 0 && yPos < this.height_pix / TILE_HEIGHT)
            {
                this.tileGrid[xPos, yPos] = entity;
                this.occupancyGrid[xPos, yPos] = entity.passable ? (byte)1 : (byte)0;
            }
        }

        public void addObjectEntity(BaseMapEntity entity)
        {
            if (entity.location.X >= 0 && entity.location.X < this.width_pix / TILE_WIDTH && entity.location.Y >= 0 && entity.location.Y < this.height_pix / TILE_HEIGHT)
            {
                this.objectList.Add(entity);
                entity.setOccupancy(ref occupancyGrid);
            }
        }

        public bool isOnScreen(Vector2 coords)
        {
            int xPos = (int)coords.X / TILE_WIDTH;
            int yPos = (int)coords.Y / TILE_HEIGHT;


            if (xPos >= this.minViewableX && xPos <= this.maxViewableX && yPos >= this.minViewableY && yPos <= this.maxViewableY)
            {
                return true;
            }

            return false;
        }

        public void moveCameraToEvent(Vector2 coords)
        {
            int cellX, cellY;
            this.translatePixelToMapCell((int)coords.X, (int)coords.Y, out cellX, out cellY);

            int viewableX = maxViewableX - minViewableX;
            int viewableY = maxViewableY - minViewableY;

            if (cellX - (viewableX / 2) < 0)
            {
                minViewableX = 0;
                maxViewableX = viewableX;
            }
            else if (cellX + (viewableX / 2) > this.width_pix / TILE_WIDTH)
            {
                minViewableX = this.width_pix / TILE_WIDTH - viewableX;
                maxViewableX = this.width_pix / TILE_WIDTH;
            }
            else
            {
                minViewableX = cellX - (viewableX / 2);
                maxViewableX = cellX + (viewableX / 2);
            }

            if (cellY - (viewableY / 2) < 0)
            {
                minViewableY = 0;
                maxViewableY = viewableY;
            }
            else if (cellY + (viewableY / 2) > this.height_pix / TILE_HEIGHT)
            {
                minViewableY = this.height_pix / TILE_HEIGHT - viewableY;
                maxViewableY = this.height_pix / TILE_HEIGHT;
            }
            else
            {
                minViewableY = cellY - (viewableY / 2);
                maxViewableY = cellY + (viewableY / 2);
            }
        }

        /// <summary>
        /// Compares two cells, using 2 pixel-coordinates.
        /// If cells are the same, returns true (can move, duh).
        /// If cells are different, returns true if cell containing
        ///     (x2, y2) is passable, false otherwise
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        public bool requestMove(int x1, int y1, int x2, int y2)
        {
            int a, b, c, d;

            this.translatePixelToMapCell(x1, y1, out a, out b);
            this.translatePixelToMapCell(x2, y2, out c, out d);

            if (a == c && b == d)
            {
                //same cell, is ok
                return true;
            }
            else
            {
                return this.occupancyGrid[c, d] == 1;
            }
        }

        public byte getCellOccupancy_cells(int x, int y)
        {
            if (x < occupancyGrid.GetLowerBound(0) || x > occupancyGrid.GetUpperBound(0) ||
                y < occupancyGrid.GetLowerBound(1) || y > occupancyGrid.GetUpperBound(1))
                return (byte)0;
            else
                return this.occupancyGrid[x, y];
        }

        public byte getCellOccupancy_pixels(int pixX, int pixY)
        {
            int x, y;
            this.translatePixelToMapCell(pixX, pixY, out x, out y);
            return this.occupancyGrid[x, y];
        }

        public void setSingleCellOccupancy(int pixX, int pixY, byte occupied)
        {
            int x, y;
            this.translatePixelToMapCell(pixX, pixY, out x, out y);
            this.occupancyGrid[x, y] = occupied;
        }

        public int getCellDistance(Vector2 c1, Vector2 c2)
        {
            int a, b, c, d;

            this.translatePixelToMapCell((int)c1.X, (int)c1.Y, out a, out b);
            this.translatePixelToMapCell((int)c2.X, (int)c2.Y, out c, out d);

            Vector2 v1 = new Vector2(a, b);
            Vector2 v2 = new Vector2(c, d);

            return Convert.ToInt32(Math.Abs(Vector2.Distance(v1, v2)));

        }

        public void translateMapCellToPixel(int indexX, int indexY, out int pixX, out int pixY)
        {
            pixX = (indexX * TILE_WIDTH + (TILE_WIDTH / 2));
            pixY = (indexY * TILE_HEIGHT + (TILE_HEIGHT / 2));
        }

        public void translatePixelToMapCell(int pixX, int pixY, out int indexX, out int indexY)
        {
            indexX = pixX / TILE_WIDTH;
            indexY = pixY / TILE_HEIGHT;
        }


        //thanks Mitch Martin////
        //we'll worry about this when we worry about pathing////
        /*public Vector2 getClosestPassableLocation_new(Vector2 origin, Vector2 point)
        {

            if (getCellOccupancy_pixels((int)point.X, (int)point.Y) == (byte)1)
            {
                return point;
            }

            Vector2 cellOrigin = new Vector2();
            Vector2 cellDestination = new Vector2();

            int outX;
            int outY;

            //translate pixel points
            translatePixelToMapCell((int)origin.X, (int)origin.Y, out outX, out outY);
            cellOrigin.X = outX;
            cellOrigin.Y = outY;
            translatePixelToMapCell((int)point.X, (int)point.Y, out outX, out outY);
            cellDestination.X = outX;
            cellDestination.Y = outY;

            //find a path from the origin to the destination with length l & return the last node
            List<PathReturnNode> path = MapManager.getInstance().pathFinder.FindPath(cellOrigin.toPoint(), cellDestination.toPoint(), 1);

            //convert back to pixels
            //If path not found, return origin
            if (path == null)
                this.translateMapCellToPixel(cellOrigin.x, cellOrigin.y, out cellDestination.x, out cellDestination.y);
            else
                this.translateMapCellToPixel(path[0].PosX, path[0].PosY, out cellDestination.x, out cellDestination.y);

            return (cellDestination);
        }*/

        //Thanks Mitch Martin
        public Vector2 getClosestPassableLocation(Vector2 origin, Vector2 point)
        {

            if (getCellOccupancy_pixels((int)point.X, (int)point.Y) == (byte)1)
            {
                return point;
            }

            Vector2 cellOrigin = new Vector2();
            Vector2 cellDestination = new Vector2();

            //translate pixel points
            int outX;
            int outY;

            translatePixelToMapCell((int)origin.X, (int)origin.Y, out outX, out outY);
            cellOrigin.X = outX;
            cellOrigin.Y = outY;
            translatePixelToMapCell((int)point.X, (int)point.Y, out outX, out outY);
            cellDestination.X = outX;
            cellDestination.Y = outY;


            //find where origin is in relation to point; there's only 8 possible directions...

            //dir vars: true == left/up;
            //          false == right/down;
            bool horizontalLeft = (cellOrigin.X > cellDestination.X);
            bool verticalUp = (cellOrigin.Y > cellDestination.Y);

            //2 special cases: (4 cases total
            if (cellOrigin.X == cellDestination.X)
            {
                if (verticalUp)
                {
                    //origin.y < dest.y; add to y value
                    while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.Y <= this.height_pix)
                    {
                        cellDestination.Y += 1;
                    }
                }
                else
                {
                    //subtract from y value
                    while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.Y >= 0)
                    {
                        cellDestination.Y -= 1;
                    }
                }
            }


            else if (cellOrigin.Y == cellDestination.Y)
            {
                if (horizontalLeft)
                {
                    //origin.x < dest.x; add to x value
                    while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X <= this.width_pix)
                    {
                        cellDestination.X += 1;
                    }
                }
                else
                {
                    //subtract from x value
                    while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X >= 0)
                    {
                        cellDestination.X -= 1;
                    }
                }
            }

            //TODO: MAKE MORE ROBUST
            else if (horizontalLeft && verticalUp)
            {
                //origin to northwest of destination, add to x && y values
                while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X <= this.width_pix && cellDestination.Y <= this.height_pix)
                {
                    cellDestination.X += 1;
                    cellDestination.Y += 1;
                }
            }

            else if (horizontalLeft && !verticalUp)
            {
                //origin to southeast of destination, add to x, subtract from y
                while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X <= this.width_pix && cellDestination.Y >= 0)
                {
                    cellDestination.X += 1;
                    cellDestination.Y -= 1;
                }
            }

            else if (!horizontalLeft && verticalUp)
            {
                //origin northeast of destination, subtract from x, add to y
                while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X >= 0 && cellDestination.Y <= this.height_pix)
                {
                    cellDestination.X -= 1;
                    cellDestination.Y += 1;
                }
            }

            else
            {
                //origin to southeast of destination, subtract from x && y
                while (getCellOccupancy_cells((int)cellDestination.X, (int)cellDestination.Y) == (byte)0 &&
                    cellDestination.X >= 0 && cellDestination.Y >= 0)
                {
                    cellDestination.X -= 1;
                    cellDestination.Y -= 1;
                }
            }

            //convert back to pixels
            this.translateMapCellToPixel((int)cellDestination.X, (int)cellDestination.Y, out outX, out outY);
            cellDestination.X = outX;
            cellDestination.Y = outY;

            return cellDestination;
        }

        public int getTileHeight()
        {
            return TILE_HEIGHT;
        }

        public int getTileWidth()
        {
            return TILE_WIDTH;
        }

        public int getMinimumX()
        {
            return minViewableX;
        }

        public int getMinimumY()
        {
            return minViewableY;
        }

        public virtual void loadTerrain()
        {
            //TODO: load terrain
        }
    }
}
