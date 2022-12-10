using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.days;

public class Day09 : Day
{
    public Day09(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // Get the motions of the head
        string[] motions = File.ReadAllText(InputData).Split("\n");

        // part 1 - Rope with two knots
        int part1 = uniqueTailPositions(2, motions);
        // part 2 - Rope with ten knots
        int part2 = uniqueTailPositions(10, motions);

        sw.Stop();

        return (part1.ToString(), part2.ToString());
    }

    public int uniqueTailPositions( int ropeLength, string[] motions)
    {
        (int, int) start = (0, 0);
        RopeEnd[] rope = new RopeEnd[ropeLength];
        // create rope "head" and add it to the front of the rope
        RopeEnd head = new RopeEnd(start);
        rope[0] = head;
        // now add the rest of the "tails" to the rope. Each tail is led by the previous point on the rope
        for (int r = 1; r < ropeLength; r++)
        {
            RopeTail tail = new RopeTail(start, rope[r-1]);
            rope[r] = tail;
        }
        // initialise haslist for unique tail positions
        HashSet<(int, int)> tailPositions = new HashSet<(int, int)>();
        // add the current position of the last element of the rope before starting simulation
        tailPositions.Add(rope[ropeLength-1].Pos);

        // loop over the motions, moving the head accordingly and then updating the tails position. Add the new tail position to the list of tail positions
        for (int m = 0; m < motions.Length; m++)
        {
            // position of tail follows the Head, so must increment 1 "step" at a time
            int steps = int.Parse(motions[m].Substring(2));
            char direction = motions[m][0];
            for (int s = 1; s <= steps; s++)
            {
                // move the "head" of the rope
                switch (motions[m][0])
                {
                    case 'L':
                        head.updatePosition(-1, 0);
                        break;
                    case 'R':
                        head.updatePosition(1, 0);
                        break;
                    case 'U':
                        head.updatePosition(0, 1);
                        break;
                    case 'D':
                        head.updatePosition(0, -1);
                        break;
                    default:
                        break;
                }
                // adjust each "tail" position based on it's leaders new position
                for (int t = 1; t < ropeLength; t++)
                {
                    rope[t].followLeader();
                }
                // add  the tail position to the list
                tailPositions.Add(rope[ropeLength - 1].Pos);
            }
        }

        return tailPositions.Count;
    }
}

public class RopeEnd
{
    public (int X, int Y) Pos;

    public RopeEnd((int x, int y) p)
    {
        this.Pos = p;
    }

    public void updatePosition(int x, int y)
    {
        Pos.X += x;
        Pos.Y += y;
    }
    public virtual void followLeader() { }
    public override int GetHashCode()
    {
        return this.Pos.GetHashCode();
    }
}

public class RopeTail : RopeEnd
{
    public RopeEnd leader;
    public RopeTail((int x, int y) p, RopeEnd l) : base(p)
    {
        this.leader = l;
    } 
    public override void followLeader()
    {
        // logic to move the tail with respect to the head.
        // calculate position difference
        int xDiff = leader.Pos.X - Pos.X;
        int yDiff = leader.Pos.Y - Pos.Y;
        // update position of tail if it is more than 1 place away.
        if ( xDiff == 2 && yDiff == 0 || xDiff == -2 && yDiff == 0 )
        {
            updatePosition(Math.Sign(xDiff),0);
        }
        else if ( xDiff == 0 && yDiff == 2 || xDiff == 0 && yDiff == -2)
        {
            updatePosition(0 ,Math.Sign(yDiff));
        }
        else if (xDiff == 2 && yDiff != 0 || xDiff == -2 && yDiff != 0 || xDiff != 0 && yDiff == 2 || xDiff != -2 && yDiff == -2)
        {
            updatePosition(Math.Sign(xDiff), Math.Sign(yDiff));
        }
        
    }
}