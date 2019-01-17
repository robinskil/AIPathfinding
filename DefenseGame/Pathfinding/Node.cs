using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefenseGame.Maps;
namespace DefenseGame.Pathfinding
{

    public class Node
    {
        private const int MovementCost = 10;
        private const int DiagonalMovementCost = 14;
        public bool Walkable { get; private set; }
        public bool DiagonalMove { get; private set; }
        public Tile Position { get; }
        public Node Parent { get; private set; }
        public List<Node> Neighbours { get; private set; }
        public int gCost { get; private set; }
        public int hCost { get; private set; }
        public int fCost { get { return hCost + gCost; } }
        public Node(Tile t, bool DiagonalMove = false , bool Walkable = true)
        {
            this.Position = t;
            this.DiagonalMove = DiagonalMove;
            this.Walkable = Walkable;
        }
        public void SetParent(Node node)
        {
            if (node != this)
            {
                this.Parent = node;
            }
        }
        public void SetWalkable(bool state){
            this.Walkable = state;
        }
        public void SetNeighbours(List<Node> Neighbours)
        {
            this.Neighbours = Neighbours;
        }
        public void SetDiagonal(bool state){
            this.DiagonalMove = state;
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
                Node n = (Node)obj;
                if (n.Position.Equals(this.Position) || this.GetHashCode() == n.GetHashCode()) return true;
                return false;
            }
        }
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void setGCost(Node n)
        {
            if (DiagonalMove) this.gCost = n.gCost + DiagonalMovementCost;
            else this.gCost = n.gCost + MovementCost;
        }
        public int calculategCosts(Node n)
        {
            return DiagonalMove ? (n.gCost + DiagonalMovementCost) : (n.gCost + MovementCost);
        }
        public void sethCosts(Node endNode) {
            this.hCost = (absolute(this.Position.X - endNode.Position.X)
                    + absolute(this.Position.Y - endNode.Position.Y))
                    * MovementCost;
        }
        private int absolute(int a) {
            return a > 0 ? a : -a;
        }
    }
}
