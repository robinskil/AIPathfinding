using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DefenseGame.Maps
{
    public struct Tile
    {
        public int X { get; }
        public int Y { get; }
        public Vector2 MiddlePoint {get { return new Vector2((X * (int)MapConstants.TileWidth) + (int)MapConstants.TileWidth/2, (Y * (int)MapConstants.TileHeight) + (int)MapConstants.TileWidth/2); } }
        public Tile(float xPos, float yPos)
        {
            X = (int)xPos / (int)MapConstants.TileWidth;
            Y = ((int)yPos) / (int)MapConstants.TileHeight;
        }
        public Tile(int xPos, int yPos)
        {
            X = xPos;
            Y = yPos;
        }
        public Vector2 GetPreciseLocation()
        {
            return new Vector2(X * (int)MapConstants.TileWidth, Y * (int)MapConstants.TileHeight);
        }
        public Rectangle ToRect()
        {
            return new Rectangle(X * (int)MapConstants.TileWidth, Y * (int)MapConstants.TileHeight,
             (int)MapConstants.TileWidth, (int)MapConstants.TileHeight);
        }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            else
            {
                Tile other = (Tile)obj;
                if (this.X == other.X && this.Y == other.Y)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
    }
}
