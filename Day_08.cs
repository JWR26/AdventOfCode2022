using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022;

public class Day08 : Day
{
    public Day08(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        string[] heightmap = InputData.Split("\n");

        int x = heightmap[0].Length;
        int y = heightmap.Length;

        int treesVisible = 2 * (x + y - 2);
        
        // loop rows
        for (int r = 1; r < y - 1; r++)
        {
            // from left
            for (int t = 1; t < x - 1; t++)
            {
                // if the tree is taller than all those before it add to visible trees
            }
            // from right
            for (int t = x-2; t > 0; t--)
            {
                // if the tree is taller than all those before it add to visible trees
            }
        }

        sw.Stop();
        return (treesVisible.ToString(), "part 2 not done");
    }
}
