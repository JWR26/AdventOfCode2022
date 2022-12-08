using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022;

public class Day09 : Day
{
    public Day09(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // manipulate inputdata
        string[] patch = File.ReadAllText(InputData).Split("\n");

        sw.Stop();
        return ("part 1 not done", "part 2 not done");
    }
}
