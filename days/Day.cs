using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExtensions;


namespace AdventOfCode2022.days
{
    public class Day
    {
        public Stopwatch sw = new Stopwatch();

        private int dayNumber;
        public int DayNumber { get { return dayNumber; } set { dayNumber = value; } }

        public string InputData;

        public string filepath = "C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input\\";

        public (string part1, string part2) answers;

        public Day(int d)
        {
            DayNumber = d;
        }

        public string GetSolution()
        {
            answers = CalculateAnswer();

            string solution = string.Format("Day {0}\n\n    Solution to Part 1: {1} \n    Solution to Part 2: {2} \n\nExecution time: {3}ms. \nNumber of Ticks: {4}", DayNumber, answers.part1, answers.part2, sw.ElapsedMilliseconds, sw.ElapsedTicks);

            return solution;
        }

        public virtual (string a, string b) CalculateAnswer()
        {
            return ("part 1 not done", "part 2 not done");
        }
    }
}
