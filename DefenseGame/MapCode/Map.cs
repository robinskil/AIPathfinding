using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using DefenseGame.Pathfinding;
using DefenseGame.MapCode;

namespace DefenseGame.Maps
{
    public class Map
    {
        MapObject[,] mapObjects;
        public Node[,] mapNodes{ get; private set; }
        public Map()
        {
            GenerateMap();
        }

        private void GenerateMap()
        {
            Random r = new Random();
            this.mapObjects = new MapObject[(int)MapConstants.MapWidth, (int)MapConstants.MapHeight];
            this.mapNodes = new Node[(int)MapConstants.MapWidth, (int)MapConstants.MapHeight];
            for (int i = 0; i < (int)MapConstants.MapWidth; i++)
            {
                for (int j = 0; j < (int)MapConstants.MapHeight; j++)
                {
                    if(r.Next(100) < 80)
                    {
                        mapObjects[i, j] = new Grass(new Tile(i, j));
                        mapNodes[i, j] = new Node(new Tile(i,j));
                    }
                    else
                    {
                        mapObjects[i, j] = new Wall(new Tile(i, j));
                        mapNodes[i, j] = new Node(new Tile(i,j),Walkable:false);
                    }
                }
            }
            mapObjects[26, 26] = new Tower(new Tile(26,26));
            mapNodes[26, 26] = new Node(new Tile(26,26));
        }
        public void DrawMap(SpriteBatch SpriteBatch)
        {
            foreach (MapObject m in mapObjects)
            {
                m.DrawObject(SpriteBatch);
            }
        }
        public Tile SpawnLocation()
        {
            Random r = new Random();
            int counter = 0;
            List<Tile> SpawnLocations = new List<Tile>();
            foreach(MapObject m in mapObjects){
                if(m.GetType() == typeof(Grass)){
                    counter++;
                    SpawnLocations.Add(m.TilePos);
                }
            }
            return SpawnLocations[r.Next(counter)];
        }
        public Path FindPath(Tile Start, Tile Target)
        {
            List<Node> OpenList = new List<Node>();
            List<Node> ClosedList = new List<Node>();
            Node endNode = mapNodes[Target.X, Target.Y];
            bool done = false;
            OpenList.Add(mapNodes[Start.X,Start.Y]);
            int counter = 0;
            while (!done)
            {
                //DO STUFF
                counter++;
                Console.WriteLine(counter);
                //DO ALGORITHM
                if (OpenList.Count==0)
                {
                    return null;
                }
                Node current = OpenList[0];
                //Get lowest f cost here , the one that gets you the closets aprox...
                foreach (Node n in OpenList)
                {
                    if (n.fCost < current.fCost)
                    {
                        current = n;
                    }
                }
                Console.WriteLine(current.Position.X + "\t" + current.Position.Y);
                ClosedList.Add(current);
                OpenList.Remove(current);
                if (current.Position.Equals(Target))
                {
                    return new Path(CalculatePath(mapNodes[Start.X,Start.Y], current));
                }
                //Get neighbours for every node
                List<Node> neighbours = GetNeighbours(current.Position);
                foreach(Node n in neighbours){
                    if(!ClosedList.Contains(n)){
                        if (!OpenList.Contains(n))
                        {
                            n.SetParent(current);
                            n.sethCosts(endNode);
                            n.setGCost(current);
                            OpenList.Add(n);
                        }
                        else{
                            if(n.gCost > n.calculategCosts(current)){
                                n.SetParent(current);
                                n.setGCost(current);
                            }
                        }
                    }
                }
                if (counter > 10000)
                {
                    throw new Exception("Cant find path LOL");
                }
            }
            return null;
        }
        private bool ValidNode(int x, int y)
        {
            return (x >= 0 && x < (int)MapConstants.MapWidth && y >= 0 && y < (int)MapConstants.MapHeight && this.mapNodes[x,y].Walkable);
        }
        private bool ValidDiagonalMove(int x , int y){
            return false;
        }
        private List<Node> GetNeighbours(Tile t){
            List<Node> neighbours = new List<Node>();
            if (ValidNode(t.X, t.Y - 1)){
                neighbours.Add(this.mapNodes[t.X, t.Y - 1]);
                this.mapNodes[t.X, t.Y - 1].SetDiagonal(false);
            }
            if (ValidNode(t.X, t.Y + 1)){
                neighbours.Add(this.mapNodes[t.X, t.Y + 1]);
                this.mapNodes[t.X, t.Y + 1].SetDiagonal(false);
            }
            if (ValidNode(t.X - 1, t.Y)){
                neighbours.Add(this.mapNodes[t.X - 1, t.Y]);
                this.mapNodes[t.X - 1, t.Y].SetDiagonal(false);
            }
            if (ValidNode(t.X + 1, t.Y)){
                neighbours.Add(this.mapNodes[t.X + 1, t.Y]);
                this.mapNodes[t.X + 1, t.Y].SetDiagonal(false);
            }
            if (ValidNode(t.X + 1, t.Y + 1) && this.mapNodes[t.X + 1 , t.Y].Walkable && this.mapNodes[t.X,t.Y + 1].Walkable){
                neighbours.Add(this.mapNodes[t.X + 1, t.Y + 1]);
                this.mapNodes[t.X + 1, t.Y + 1].SetDiagonal(true);
            }
            if (ValidNode(t.X - 1, t.Y + 1) && this.mapNodes[t.X - 1 , t.Y].Walkable && this.mapNodes[t.X,t.Y + 1].Walkable){
                neighbours.Add(this.mapNodes[t.X - 1, t.Y + 1]);
                this.mapNodes[t.X - 1, t.Y + 1].SetDiagonal(true);
            }
            if (ValidNode(t.X - 1, t.Y - 1) && this.mapNodes[t.X - 1 , t.Y].Walkable && this.mapNodes[t.X,t.Y - 1].Walkable){
                neighbours.Add(this.mapNodes[t.X - 1, t.Y - 1]);
                this.mapNodes[t.X - 1, t.Y - 1].SetDiagonal(true);
            }
            if (ValidNode(t.X + 1, t.Y - 1) && this.mapNodes[t.X , t.Y - 1].Walkable && this.mapNodes[t.X + 1,t.Y].Walkable){
                neighbours.Add(this.mapNodes[t.X + 1, t.Y - 1]);
                this.mapNodes[t.X + 1, t.Y - 1].SetDiagonal(true);
            }
            return neighbours;
        }

        private List<Node> CalculatePath(Node Start, Node Target)
        {
            List<Node> PathNodes = new List<Node>();
            bool done = false;
            Node curr = Target;
            if (curr.Position.Equals(Start.Position))
            {
                return PathNodes;
            }
            while (!done)
            {
                PathNodes.Add(curr);
                curr = curr.Parent;
                if (curr.Position.Equals(Start.Position))
                {
                    done = true;
                }
            }
            return PathNodes;
        }
    }
}
