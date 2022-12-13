using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

public class Day13 : Day
{
    public Day13(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        string[] packetPairs = File.ReadAllText(InputData).Split("\n\n");

        int sumOfIndices = 0;
        // for each paire of packets, check they are in the right order
        for (int p = 0; p < packetPairs.Length; p++)
        {
            string[] packets = packetPairs[p].Split("\n");
            sumOfIndices += (RightOrder(packets[0], packets[1]) == 1) ? p : 0;
        }

        return base.CalculateAnswer();
    }
    /*
     * packets are in the right order if they follow the following rules:
     *  1) if both values are integers, compare them. Lower integer should come first (ie Left < Right). If Right > left then the order is not correct
     *  2) if both values are lists: compare each value of the list following rule 1. If the left list runs out of items first
     *  3) if one value is an integer, convert it to a list and recompare.
     *  
     *  The function will return 1 for right order, 0 for undecided and -1 for wrong order.
     */

    public int RightOrder(string packetOne, string packetTwo)
    {
        return 1;
    }
}

