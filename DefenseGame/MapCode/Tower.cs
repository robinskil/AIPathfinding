using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace DefenseGame.Maps
{
    public class Tower : MapObject
    {
        public Tower(Tile t) : base(t)
        {
        }

        public override void DrawObject(SpriteBatch SpriteBatch)
        {
            SpriteBatch.FillRectangle(TilePos.ToRect(), Color.Blue);
        }
    }
}