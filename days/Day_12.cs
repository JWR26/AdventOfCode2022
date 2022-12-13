using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day12 : Day
{
    public Day12(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        string letters = File.ReadAllText(InputData);

        string[] input = File.ReadAllText(InputData).Split("\n");

        // part 1 - get the shortest distance from start to end

        Grid grid = new Grid(input);
        Node start = grid.GetPoint('S');
        Node end = grid.GetPoint('E');
        AStar aStar = new AStar();

        int distance = aStar.GetShortestDistance(start, end, grid);

        int shortestFromA = distance; // initalise 'S' as the lowest point
        
        for (int row = 0; row < input.Length; row++)
        {
            for (int col = 0; col < input[row].Length; col++)
            {
                if (input[row][col] == 'a')
                {
                    Node a = new Node('a', row, col, null);
                    int d = aStar.GetShortestDistance(a, end, grid);
                    shortestFromA = ( d > 0 && d < shortestFromA) ? d : shortestFromA ;
                }   
            }
        }
        
        sw.Stop();

        return ($"The shortest path from 'S' is {distance}", $"The shortest path from any 'a' is {shortestFromA}");
    }
}


class AStar
{
    // returns the number of steps to get from the start node to the end node for a given grid.
    public int GetShortestDistance(Node start, Node end, Grid grid)
    {
        /*
         * We shall start by declaring two lists, one open and one closed.
         * The open list will contain "unresolved" nodes who have not explored all their neighbours.
         * The closed list will contain all "resolved nodes that have been visited and explored all their neighbours.
        */

        List<Node> open = new List<Node>();
        List<Node> closed = new List<Node>();

        // start by adding the start node to the open list
        open.Add(start);

        // begin a loop, that will end when either all points on the grid become explored or until the end node is found.
        while (open.Count > 0)
        {
            // start by identifying the current node. This shall be the node with the lowest cost
            open.Sort();
            Node current = open[0];

            // remove the current node from the open list & add it to the closed list
            open.RemoveAt(0);
            closed.Add(current);

            // if the current node is the end node, the search is complete
            if ( current.Equals(end) )
            {
                List<Node> path = new List<Node>();

                while (current.sucessor != null)
                {
                    path.Add(current);
                    current = current.sucessor;
                }

                return path.Count;
            }

            // get the current nodes neighbours

            List<Node> neighbours = grid.GetNeighbours(current);

            // loop through the neighbours
            for (int n = 0; n < neighbours.Count; n++)
            {
                // if the neighbour is on the closed list, move on to the next
                if ( closed.Contains(neighbours[n]) )
                {
                    continue;   
                }

                // update the cost values of the neighbour
                neighbours[n].g = current.g + 1; // all nodes are 1 unit from their neighbour
                neighbours[n].CalculateManhattanDistance(end.row, end.column);
                neighbours[n].UpdateCost();

                // If the neighbour is on the open list, add it if it has a lower cost
                if ( open.Contains(neighbours[n]))
                {
                    Node copy = open[open.IndexOf(neighbours[n])];
                    if (copy.f > neighbours[n].f)
                    {
                        open.Add(neighbours[n]);
                    }
                }
                // add the neighbour to the open list
                open.Add(neighbours[n]);
            }
        }
        // if the search has failed, return 0.
        return 0;
    }
}

class Grid
{
    public string[] grid;

    private int height;
    private int width;

    public Grid(string[] grid)
    {
        this.grid = grid;
        // initialise the private height & width variables for the grid
        this.height = this.grid.Length - 1; 
        this.width = this.grid[0].Length - 1;
    }
    // returns a list of neighbouring nodes in the four cardinal directions
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        // North
        if ( node.row > 0)
        {
            Node north = new Node(grid[node.row - 1][node.column], node.row - 1, node.column, node);
            if ( north.elevation - node.elevation < 2 )
            {
                neighbours.Add(north);
            }
        }
        // East
        if (node.column < width)
        {
            Node east = new Node(grid[node.row][node.column + 1], node.row, node.column + 1, node);
            if ( east.elevation - node.elevation < 2 )
            {
                neighbours.Add(east);
            }
        }
        // South
        if (node.row < height)
        {
            Node south = new Node(grid[node.row + 1][node.column], node.row + 1, node.column, node);
            if ( south.elevation - node.elevation < 2 )
            {
                neighbours.Add(south);
            }
        }
        // West
        if (node.column > 0)
        {
            Node west = new Node(grid[node.row][node.column - 1], node.row, node.column - 1, node);
            if ( west.elevation - node.elevation < 2 )
            {
                neighbours.Add(west);
            }
        }

        return neighbours;
    }
    public Node GetPoint(char c)
    {
        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
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
}

class Node : IComparable<Node>
{
    public char elevation;
    public int row;
    public int column;

    public int f = 0; // nodes total cost
    public int g = 0; // cost from start node
    public int h = 0; // huristic to end point - I shall use the Manhattan Distance

    public Node? sucessor; // can be null for the start and the end.

    public Node(char elevation, int r, int c, Node? sucessor)
    {
        this.elevation = (elevation == 'E') ? 'z' : (elevation == 'S') ? 'a' : elevation; // ensures the start and end point are identified by their elevation.
        this.row = r;
        this.column = c;
        this.sucessor = sucessor;
    }
    public void UpdateCost()
    {
        f = g + h;
    }
    public void CalculateManhattanDistance(int goalRow, int goalColumn)
    {
        h = Math.Abs(this.row - goalRow) + Math.Abs(this.column - goalColumn);
    }
    // Checks if two nodes are the same by comparing the coordinates.
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        Node node = obj as Node;
        if (node == null)
        {
            return false;
        }

        return Equals(node);
    }

    public bool Equals(Node other)
    {
        return (this.row == other.row && this.column == other.column);
    }
    // Custom compare method to enable sorting of Node lists by ascending cost.
    public int CompareTo(Node other)
    {
        return this.f.CompareTo(other.f);
    }
}

