using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class Day18 : Day
{

    public int min = 0;
    public int max = 22;

    public List<Cube> LavaCubes = new List<Cube>();
    public Day18(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // get the coordinates of each rock
        string[] cubeData = File.ReadAllText(this.InputData).Split("\n");
        // part 1 - count the faces of cubes not touching other cubes
        int surfaceArea = 6 * cubeData.Length; 

        for (int c = 0; c < cubeData.Length; c++)
        {
            string[] coordinates = cubeData[c].Split(",");

            Cube cube = new Cube(int.Parse(coordinates[0]), int.Parse(coordinates[1]), int.Parse(coordinates[2]));

            LavaCubes.Add(cube);

            for (int i = 0; i < cubeData.Length; i++)
            {
                string[] strings = cubeData[i].Split(",");

                if ( i == c )
                {
                    continue;
                }
                // if a cube is ajacent to the current, decrease the surface area by 1.
                if ( cube.IsAdjacent( int.Parse(strings[0]), int.Parse(strings[1]), int.Parse(strings[2]) ) )
                {
                    surfaceArea--;
                }
            }
        }

        Cube start = new Cube(min, min, min);
        // outer surface area
        int outerSurfaceArea = 0;
        // create a queue
        Queue<Cube> queue = new Queue<Cube>();
        // list of cubes already explored
        List<Cube> explored = new List<Cube>();
        // add the start to the queue
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            // get the first item of the queue and mark it as explored
            Cube current = queue.Dequeue();
            explored.Add(current);

            List<Cube> neighbours = Neighbours(current, min, max);
            // check the current queues neighbours for lava cubes
            for(int n = 0; n < neighbours.Count; n++)
            {
                // add one to surface area if the neighbour is a lava cube
                if (LavaCubes.Contains(neighbours[n]))
                {
                    outerSurfaceArea++;
                }
                // add the cube to the queue if it isn't there already and has not been explored
                else if ( !queue.Contains(neighbours[n]) && !explored.Contains(neighbours[n]) )
                {
                    queue.Enqueue(neighbours[n]);
                }
            }
        }

        sw.Stop();
        return ($"Total Surface area is {surfaceArea}", $"The outer surface area is {outerSurfaceArea}");
    }

    public List<Cube> Neighbours(Cube cube, int lowerBound, int upperBound)
    {
        List<Cube> neighbours = new List<Cube>();

        if ( cube.X - 1 >= lowerBound && cube.X-1 <= upperBound )
        {
            neighbours.Add(new Cube(cube.X - 1, cube.Y, cube.Z));
        }
        if (cube.X + 1 >= lowerBound && cube.X + 1 <= upperBound)
        {
            neighbours.Add(new Cube(cube.X + 1, cube.Y, cube.Z));
        }

        if (cube.Y - 1 >= lowerBound && cube.Y - 1 <= upperBound)
        {
            neighbours.Add(new Cube(cube.X, cube.Y - 1, cube.Z));
        }
        if (cube.Y + 1 >= lowerBound && cube.Y + 1 <= upperBound)
        {
            neighbours.Add(new Cube(cube.X, cube.Y + 1, cube.Z));
        }

        if (cube.Z - 1 >= lowerBound && cube.Z - 1 <= upperBound)
        {
            neighbours.Add(new Cube(cube.X, cube.Y, cube.Z - 1));
        }
        if (cube.Z + 1 >= lowerBound && cube.Z + 1 <= upperBound)
        {
            neighbours.Add(new Cube(cube.X, cube.Y, cube.Z + 1));
        }

        return neighbours;
    }
}



public class Cube
{
    public int X;
    public int Y;
    public int Z;

    public Cube(int x, int y, int z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public bool IsAdjacent(int x, int y, int z)
    {
        if ( X - x == -1 || X - x == 1 )
        {
            return ( Y == y && Z == z );
        }
        if ( Y - y == -1 || Y - y == 1 )
        {
            return ( X == x && Z == z );
        }
        if ( Z - z == -1 || Z - z == 1 )
        {
            return (X == x && Y == y);
        }

        return false;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        Cube cube = obj as Cube;
        if (cube == null)
        {
            return false;
        }

        return Equals(cube);
    }

    public bool Equals(Cube other)
    {
        return (this.X == other.X && this.Y == other.Y && this.Z == other.Z);
    }
}