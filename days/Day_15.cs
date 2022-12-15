using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day15 : Day
{
    public Day15(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        int part1 = Part1(2000000);

        int part2 = Part2();

        sw.Stop();
        return ($"{part1}", "not done yet...");
    }

    public int Part1(int row)
    {
        HashSet<long> points = new HashSet<long>();

        string rawdata = File.ReadAllText(InputData).Replace(" closest beacon is at x=", "");

        string[] sensorData = rawdata.Replace("Sensor at x=", "").Replace(", y=", ":").Split("\n");

        for (int i = 0; i < sensorData.Length; i++)
        {
            string[] numbers = sensorData[i].Split(":");
            // get the coordinates for the sensor and beacon
            long sensorX = long.Parse(numbers[0]);
            long sensorY = long.Parse(numbers[1]);
            long beaconX = long.Parse(numbers[2]);
            long beaconY = long.Parse(numbers[3]);

            long manhattan = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);

            if (sensorY - manhattan < row || sensorY + manhattan > row)
            {
                long x = manhattan - Math.Abs(sensorY - row); // remainder of the manhattan to go from side to side

                for (long j = sensorX - x; j < sensorX + x; j++)
                {
                    points.Add(j);
                }
            }
        }

        return points.Count;
    }

    public int Part2()
    {
        return 0;
    }
}

