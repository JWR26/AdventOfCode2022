using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdventOfCode2022
{
    public class Day
    {
        private Stopwatch sw = new Stopwatch();

        private int dayNumber = 0;

        public int DayNumber { get { return dayNumber; } set { dayNumber = value; } }

        private string dataFile = "";
        public string DataFile 
        { 
            get { return dataFile; }
            set { dataFile = value; }
        }

        public string directory = "C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\";

        private string inputData = "";
        public string InputData
        {
            get
            {
                return inputData;
            }
            private set
            {
                string filePath = String.Format("{0}{1}", directory, dataFile);
                inputData = File.ReadAllText(filePath);
            }
        }

        public Day(int day = 0)
        {
            DayNumber = day;
        }
        
        public virtual string GetSolution()
        {
            sw.Start();
            (int part1, int part2) answer = CalculateAnswer();
            sw.Stop();

            string solution = String.Format("Day {0}\n\nSolution to Part 1: {1} \nSolution to Part 2: {2} \n\nExecution time: {3}ms. Number of Ticks: {4}", DayNumber, answer.part1, answer.part2, sw.ElapsedMilliseconds, sw.ElapsedTicks);

            return solution;
        }

        private (int, int) CalculateAnswer()
        {
            return (0, 0);
        }
    }
}
