using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grids
{
    public class PointGrid
    {
        // gives the 
        private int rows;
        private int columns;

        private char emptySpace;

        private string[] grid;
        public string[] Grid { get { return grid; } set { grid = value;  } }



        public PointGrid(char emptySpace = '.', int cols = 1, int rows = 1)
        {
            this.emptySpace = emptySpace;
            // auto intialises the grid
            this.Grid= new string[rows];
            string row = new String(emptySpace, cols);
            for (int r = 0; r < rows; r++)
            {
                this.Grid[r] = row;
            }
            // initialise the private height & width variables for the grid 
            this.rows = this.Grid.Length;
            this.columns = this.Grid[0].Length;
        }

        public PointGrid(string[] grid)
        {
            this.grid = grid;
            // initialise the private height & width variables for the grid
            this.rows = this.grid.Length - 1;
            this.columns = this.grid[0].Length - 1;
        }

        /// <summary>
        /// Returns true if the point for a given row and colum on the grid is occupied by the character representing empty space.
        /// </summary>
        public bool IsEmpty(int col, int row) 
        { 
            return (Grid[row][col] == emptySpace);
        }
        /// <summary>
        /// Returns true if the given point on the grid is occupied by the character representing empty space.
        /// </summary>
        public bool IsEmpty(Point p)
        {
            return (Grid[p.row][p.column] == emptySpace);
        }
        /// <summary>
        /// Check if a point on the grid is empty at a given distance away from a point
        /// </summary>
        public bool IsEmpty(Point point, Point distance)
        {
            return (Grid[point.row - distance.row][point.column - distance.column] == emptySpace);
        }
        public bool IsNeighbourEmpty(Point p, string dir)
        {
            return false;
        }
        /// <summary>
        /// Returns false if the point is not within the grid dimensions
        /// </summary>
        public bool IsOnGrid(Point p)
        {
            return ( p.row >= 0 && p.column >= 0 && p.row < rows && p.column < columns);
        }
        /// <summary>
        /// Sets the character in a given row at a specified column a new value.
        /// </summary>
        public void SetPointValue(int row, int column, char value)
        {
            StringBuilder updatedRow = new StringBuilder(Grid[row]);
            updatedRow[column] = value;
            Grid[row] = updatedRow.ToString();
        }
        /// <summary>
        /// Sets the character on the grid at the Points row and column to the Point's ID
        /// </summary>
        public void SetPointValue(Point p)
        {
            StringBuilder updatedRow = new StringBuilder(Grid[p.row]);
            updatedRow[p.column] = p.id;
            Grid[p.row] = updatedRow.ToString();
        }
        
        // returns a list of neighbouring nodes in the four cardinal directions
        public List<Node> GetNeighbours(Node node)
        {
            List<Node> neighbours = new List<Node>();
            // North
            if (node.row > 0)
            {
                Node north = new Node(grid[node.row - 1][node.column], node.row - 1, node.column, node);
                if (north.elevation - node.elevation < 2)
                {
                    neighbours.Add(north);
                }
            }
            // East
            if (node.column < columns)
            {
                Node east = new Node(grid[node.row][node.column + 1], node.row, node.column + 1, node);
                if (east.elevation - node.elevation < 2)
                {
                    neighbours.Add(east);
                }
            }
            // South
            if (node.row < rows)
            {
                Node south = new Node(grid[node.row + 1][node.column], node.row + 1, node.column, node);
                if (south.elevation - node.elevation < 2)
                {
                    neighbours.Add(south);
                }
            }
            // West
            if (node.column > 0)
            {
                Node west = new Node(grid[node.row][node.column - 1], node.row, node.column - 1, node);
                if (west.elevation - node.elevation < 2)
                {
                    neighbours.Add(west);
                }
            }

            return neighbours;
        }
        public Node GetPoint(char c)
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (grid[row][column] == c)
                    {
                        char elevation = (c == 'E') ? 'z' : 'a';
                        return new Node(elevation, row, column, null);
                    }
                }
            }
            // if not found return a default start point at 0,0
            return new Node(grid[0][0], 0, 0, null);
        }

        public char GetValueAtPoint(Point p)
        {
            return Grid[p.row][p.column];
        }
        /// <summary>
        /// Replaces a given row with a row of a single character
        /// </summary>
        /// <param name="row"></param>
        /// <param name="c"></param>
        public void SetAllValuesOnRow(int row, char c)
        {
            string newRow = new String(c, columns);
            Grid[row] = newRow;
        }

        public void DrawGrid()
        {
            foreach (string line in Grid)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("\n");
        }
        public void DrawGrid(int from)
        {
            foreach (string line in Grid)
            {
                Console.WriteLine(line.Substring(from));
            }
            Console.WriteLine("\n");
        }
        public void DrawGrid(int from, int to)
        {
            foreach (string line in Grid)
            {
                Console.WriteLine(line.Substring(from, to - from));
            }
            Console.WriteLine("\n");
        }

    }

    /// <summary>
    /// Class <c>Point</c> models a point in a two-dimensional plane.
    /// </summary>
    public class Point
    {   
        public int row { get; set; }
        public int column { get; set; }

        public char id;
        public Point(int column, int row, char id)
        {
            this.row = row;
            this.column = column;
            this.id = id;
        }

        public static Point operator +(Point a, Point b) => new Point(a.column + b.column, a.row + b.row, a.id);
    }


}
