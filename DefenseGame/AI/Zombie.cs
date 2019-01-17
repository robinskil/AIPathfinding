using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefenseGame.Maps;
using DefenseGame.Pathfinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
namespace DefenseGame.AI
{
    public class Zombie
    {
        private const float speed = 4;
        private readonly Point size = new Point(10);
        public Tile CurrentTile{ get; private set; }
        private Vector2 Location;
        private Vector2 MiddlePoint;
        private Rectangle ZombieBox;
        private Path path;
        public Zombie(Tile SpawnPos)
        {
            this.CurrentTile = SpawnPos;
            this.Location = this.CurrentTile.GetPreciseLocation();
            this.Location.X += 5;
            this.Location.Y += 5;
            this.MiddlePoint = new Vector2(this.Location.X + this.size.X / 2 , this.Location.Y + this.size.Y / 2);
            this.ZombieBox = new Rectangle(this.Location.ToPoint(), this.size);
        }
        public void SetPath(Path p){
            this.path = p;
        }
        public void Move(){
            if(this.path != null)
            {
                if (this.path.GetPath != null && this.path.GetPath.Count > 0)
                {
                    MoveTo(this.path.GetNextMove(this.MiddlePoint, speed));
                    // Tile moveTo = this.path.GetPath.Last();
                    // Console.WriteLine(CurrentTile.X + "\t" + CurrentTile.Y);
                    // Console.WriteLine(moveTo.X + "\t" + moveTo.Y);
                    // if(!(moveTo.GetPreciseLocation().X + 10 == this.MiddlePoint.X && moveTo.GetPreciseLocation().Y + 10 == this.MiddlePoint.Y)){
                    //     Console.WriteLine("Move");
                    //     if(moveTo.X > CurrentTile.X){
                    //         MoveRight();
                    //     }
                    //     if(moveTo.X < CurrentTile.X){
                    //         MoveLeft();
                    //     }
                    //     if(moveTo.Y > CurrentTile.Y){
                    //         MoveDown();
                    //     }
                    //     if(moveTo.Y < CurrentTile.Y){
                    //         MoveUp();
                    //     }
                    // }
                    // else
                    // {
                    //     this.path.GetPath.Remove(this.path.GetPath.Last());
                    //     this.CurrentTile = new Tile(this.MiddlePoint.X, this.MiddlePoint.Y);
                    // }
                }
            }
        }
        private void MoveTo(Vector2 MiddlePoint){
            this.MiddlePoint = MiddlePoint;
            this.Location.X = this.MiddlePoint.X - size.X / 2;
            this.Location.Y = this.MiddlePoint.Y - size.Y / 2;
            this.ZombieBox.X = (int)this.Location.X;
            this.ZombieBox.Y = (int)this.Location.Y;
        }
        public Rectangle GetRectangle(){
            return this.ZombieBox;
        }
        private void MoveUp(){
            this.Location.Y -= speed;
            this.MiddlePoint.Y -= speed;
            this.ZombieBox.Offset(0, -speed);
        }
        private void MoveDown(){
            this.Location.Y += speed;
            this.MiddlePoint.Y += speed;
            this.ZombieBox.Offset(0, speed);
        }
        private void MoveRight(){
            this.Location.X += speed;
            this.MiddlePoint.X += speed;
            this.ZombieBox.Offset(speed, 0);
        }
        private void MoveLeft(){
            this.Location.X -= speed;
            this.MiddlePoint.X -= speed;
            this.ZombieBox.Offset(-speed, 0);
        }
        public Vector2 GetPreciseLocation(){
            return this.Location;
        }
        private void UpdateTile(){
            // if(new Tile(this.Location.X,this.Location.Y).Equals(new Tile(this.Location.X + this.size.X,this.Location.Y+this.size.Y))){
            //     this.CurrentTile = new Tile(this.MiddlePoint.X , this.MiddlePoint.Y);
            // }
            //Console.WriteLine(this.MiddlePoint);
        }
        public void Draw(SpriteBatch SpriteBatch){
            SpriteBatch.FillRectangle(this.ZombieBox, new Color(new Vector4(0.8F,0,0.8F,0.8F)));
            //this.path.DrawPath(SpriteBatch);
        }
    }
}
