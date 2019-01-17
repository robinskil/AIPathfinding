using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DefenseGame.Maps
{
    public class Grass : MapObject
    {
        public Grass(Tile t) : base(t)
        {
        }
        public override void DrawObject(SpriteBatch SpriteBatch)
        {
            SpriteBatch.FillRectangle(TilePos.ToRect(), Color.Green);
            //SpriteBatch.DrawRectangle(TilePos.ToRect(), Color.Black);
        }
    }
}