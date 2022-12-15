using AdventOfCode2022.days;
using Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Day14 : Day
{
    public Day14(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        
        string rockData = File.ReadAllText(InputData);

        // find how low to make the grid
        int lowestRock = FindLowestRock(rockData) + 2; 

        // initialise a grid to represent the cave. The cave will be 1000 columns wide (the sands origin will start at (0,500) so make the grid wide to allow for the pile to spread out. will initialise the rows on the grid by the lowest point of a rock.
        PointGrid cave = new PointGrid('.', 1000, lowestRock + 1); // adding 1 to accout for first row being 'O'

        string[] rockStructures = rockData.Split('\n');

        for (int i = 0; i < rockStructures.Length; i++)
        {
            // split each rock by " -> " to get the coordinates
            string[] rockPoints = rockStructures[i].Split(" -> ");
            for (int j = 1; j < rockPoints.Length; j++)
            {
                int p1x = int.Parse(rockPoints[j].Substring(0, rockPoints[j].IndexOf(',')));
                int p1y = int.Parse(rockPoints[j].Substring(rockPoints[j].IndexOf(',') + 1));

                int p0x = int.Parse(rockPoints[j-1].Substring(0, rockPoints[j-1].IndexOf(',')));
                int p0y = int.Parse(rockPoints[j-1].Substring(rockPoints[j-1].IndexOf(',') + 1));
                // two loops, one accross and one down, only straight lines are given. 

                int xdir = Math.Sign(p1x - p0x);
                int ydir = Math.Sign(p1y - p0y);
                
                while (p0x != p1x)
                {
                    cave.SetPointValue(p0y, p0x, '#');
                    p0x += xdir;
                }
                while (p0y != p1y)
                {
                    cave.SetPointValue(p0y, p0x, '#');
                    p0y += ydir;
                }

                cave.SetPointValue(p0y, p0x, '#');
            }
        }

        int part1 = PourSand(cave, 500);

        // draw the floor
        cave.SetAllValuesOnRow(lowestRock, '#');

        int part2 = part1 + PourSand(cave, 500);

        sw.Stop();
        return ($"{part1}", $"{part2}");
    }


    public int PourSand(PointGrid cave, int source)
    {
        // Directions to move down in 
        Point DOWN = new Point(0, 1, '.');
        Point DOWN_LEFT = new Point(-1, 1 , '.');
        Point DOWN_RIGHT = new Point(1, 1, '.');

        int unitsOfSand = 0;

        // now the cave is set up, produce one grain of sand at a time until one "falls off the grid". to avoid an infinite while loop, it will be conditioned on the sand's "source" being empty on our grid.
        while (cave.IsEmpty(source, 0))
        {
            Point sand = new Point(source, 0, 'o');
            
            // move the sand down until it cannot move any more
            while ( cave.IsEmpty(sand + DOWN) || cave.IsEmpty(sand + DOWN_LEFT) || cave.IsEmpty(sand + DOWN_RIGHT) )
            {
                // move down if space below is empty
                if (cave.IsEmpty(sand + DOWN))
                {
                    sand += DOWN;
                }
                // move down left
                else if (cave.IsEmpty(sand + DOWN_LEFT))
                {
                    sand += DOWN_LEFT;
                }
                // move down right
                else if (cave.IsEmpty(sand + DOWN_RIGHT))
                {
                    sand += DOWN_RIGHT;
                }
                // if the sand moves off the grid, or if the space below is not on the grid, return the units of sand that have already settled
                if (!cave.IsOnGrid(sand) || !cave.IsOnGrid(sand + DOWN))
                {
                    return unitsOfSand;
                }
            }
            // once the sand can no longer move, and hasn't fallen into the abyss mark its position on the grid
            cave.SetPointValue(sand);
            unitsOfSand++; // increase the count of units of sand
        }
        // at this point no more sand can pour into the cave. Return the number of sand grains in the cave.
        return unitsOfSand;
    }

    public int FindLowestRock(string data)
    {
        int lowestRock= 1;

        string[] coordinates = data.Replace("\n", ":").Replace(" -> ", ":").Split(":");

        for (int i = 0; i < coordinates.Length; i++)
        {
            int y = int.Parse(coordinates[i].Substring(coordinates[i].IndexOf(',') + 1));
            lowestRock = ( y > lowestRock ) ? y : lowestRock ;
        }

        return lowestRock;
    }
}
