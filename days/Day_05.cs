using AdventOfCode2022.days;
using MyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day05 : Day
{
    public Day05(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        string[] data = File.ReadAllText(InputData).Split("\n\n");

        string[] stacks = StacksAsStrings(data[0]);
        string[] stacksOptimised = new string[stacks.Length];
        stacks.CopyTo(stacksOptimised, 0);

        int[,] instructions = GetInstructions(data[1]);

        for (int i = 0; i < instructions.GetLength(0); i++)
        {
            // get the "move", "from" & to "numbers" from the instructions
            int number = instructions[i, 0];
            int from = instructions[i, 1] - 1;
            int to = instructions[i, 2] - 1;
            // CrateMover 9000 - copy the reversed box letters to the destination, then remove from origin
            stacks[to] = string.Concat(stacks[to], stacks[from].Substring(stacks[from].Length - number).ReverseString());
            stacks[from] = stacks[from].Substring(0, stacks[from].Length - number);
            // CrateMover 9001 - copy box letters to the desitnation then remove from origin
            stacksOptimised[to] = string.Concat(stacksOptimised[to], stacksOptimised[from].Substring(stacksOptimised[from].Length - number));
            stacksOptimised[from] = stacksOptimised[from].Substring(0, stacksOptimised[from].Length - number);
        }
        sw.Stop();
        // return the last letter from each stack as a string.
        return (GetLetters(stacks), GetLetters(stacksOptimised));
    }

    public string GetLetters(string[] stacks)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < stacks.Length; i++)
        {
            sb.Append(stacks[i][stacks[i].Length - 1]);
        }

        return sb.ToString();
    }

    public string[] StacksAsStrings(string stacks)
    {
        // reformat the line for easy splitting
        string[] data = stacks.Replace("    ", " [$]").Replace("] [", "][").Replace("[", "").Replace("]", "").Replace("$", " ").Split("\n");

        string[] d2 = stacks.Split("\n");

        string[] stackStrings = new string[9];

        for (int i = 0; i < data.Length - 1; i++)
        {
            string s = data[i];

            for (int j = 0; j < s.Length; j++)
            {
                /* filtering whitespace here gives no performance gain, on the contrary - approx 25% slower after multiple tests.
                string l = (stackStrings[j] != " ") ? stackStrings[j] : "";*/
                stackStrings[j] = String.Format("{0}{1}", s[j], stackStrings[j]);
            }

        }

        // remove whitespace
        for (int i = 0; i < stackStrings.Length; i++)
        {
            stackStrings[i] = stackStrings[i].TrimEnd();
        }

        return stackStrings;
    }

    public int[,] GetInstructions(string instructions)
    {
        string[] strings = instructions.Replace(" ", "").Split('\n');

        int[,] numbers = new int[strings.Length, 3];

        for (int i = 0; i < strings.Length; i++)
        {
            string move = strings[i].Substring(strings[i].IndexOf("move") + 4, (strings[i].IndexOf("from") - 4));
            string from = strings[i].Substring(strings[i].IndexOf("from") + 4, (strings[i].IndexOf("to") - strings[i].IndexOf("from") - 4));
            string to = strings[i].Substring(strings[i].IndexOf("to") + 2);
            numbers[i, 0] = int.Parse(move);
            numbers[i, 1] = int.Parse(from);
            numbers[i, 2] = int.Parse(to);
        }

        return numbers;
    }
}

