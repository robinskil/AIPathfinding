using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DefenseGame.Maps
{
    public abstract class MapObject
    {
        public Tile TilePos { get; }
        public MapObject(Tile t)
        {
            TilePos = t;
        }
        public abstract void DrawObject(SpriteBatch SpriteBatch);
        public virtual bool Collision(Vector2 Location){
            return false;
        }
    }
}
