using DefenseGame.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DefenseGame.MapCode
{
    public class Wall : MapObject
    {
        public Wall(Tile t) : base(t)
        {
        }

        public override void DrawObject(SpriteBatch SpriteBatch)
        {
            SpriteBatch.FillRectangle(TilePos.ToRect(), Color.Brown);
            //SpriteBatch.DrawRectangle(TilePos.ToRect(), Color.Black);
        }
    }
}