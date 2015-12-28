using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fagblo.Classes
{
    public class BaseMapEntity
    {
        public bool passable;
        public Texture2D texture;
        public Vector2 location;

        public BaseMapEntity()
        {
            passable = true;
        }

        public BaseMapEntity(Texture2D tex, bool passable, int x, int y)
        {
            this.texture = tex;
            this.passable = passable;
            this.location = new Vector2(x, y);
        }

        public BaseMapEntity(Texture2D tex, bool passable, Vector2 coords)
        {
            this.texture = tex;
            this.passable = passable;
            this.location = coords;
        }

        public virtual void setOccupancy(ref byte[,] grid)
        {
            grid[(int)location.X, (int)location.Y] = (byte)(passable ? 1 : 0);
        }

        public virtual void addToMap(MapBase map)
        {
            map.addMapEntity(this, (int)location.X, (int)location.Y);
            this.setOccupancy(ref map.occupancyGrid);
        }
    }
}
