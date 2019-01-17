using System.Collections.Generic;
using System.Linq;
using DefenseGame.Maps;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace DefenseGame.Pathfinding
{

    public class Path
    {
        public List<Tile> GetPath { get; private set; }
        public Path(List<Node> PathNodes)
        {
            this.GetPath = new List<Tile>();
            foreach (Node n in PathNodes)
            {
                this.GetPath.Add(n.Position);
            }
        }
        public void DrawPath(SpriteBatch SpriteBatch)
        {
            if (GetPath != null)
            {
                foreach (Tile t in GetPath)
                {
                    SpriteBatch.FillRectangle(t.ToRect(), Color.FromNonPremultiplied(0, 0, 255, 100));
                }
            }
        }
        public Vector2 GetNextMove(Vector2 Position, float speed)
        {
            Vector2 Target = this.GetPath.Last().MiddlePoint;
            if (Target.X > Position.X && Target.Y > Position.Y)
            {
                Position.X += speed;
                Position.Y += speed;
                if (Position.X >= Target.X && Position.Y >= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X < Position.X && Target.Y > Position.Y)
            {
                Position.X -= speed;
                Position.Y += speed;
                if (Position.X <= Target.X && Position.Y >= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X > Position.X && Target.Y < Position.Y)
            {
                Position.X += speed;
                Position.Y -= speed;
                if (Position.X >= Target.X && Position.Y <= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X < Position.X && Target.Y < Position.Y)
            {
                Position.X -= speed;
                Position.Y -= speed;
                if (Position.X <= Target.X && Position.Y <= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            //DIAGONAL DONE
            else if (Target.X < Position.X && Target.Y == Position.Y)
            {
                Position.X -= speed;
                if (Position.X <= Target.X && Position.Y == Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X > Position.X && Target.Y == Position.Y)
            {
                Position.X += speed;
                if (Position.X >= Target.X && Position.Y == Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X == Position.X && Target.Y < Position.Y)
            {
                Position.Y -= speed;
                if (Position.X == Target.X && Position.Y <= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            else if (Target.X == Position.X && Target.Y > Position.Y)
            {
                Position.Y += speed;
                if (Position.X == Target.X && Position.Y >= Target.Y)
                {
                    this.GetPath.Remove(this.GetPath.Last());
                }
            }
            return Position;
        }
    }
}