using Microsoft.VisualBasic;
using MyExtensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using AdventOfCode2022;
using System.Linq;

class AdventOfCode
{
    // declare each day as a variable.
    private Day01 d01 = new Day01("input_01.txt");
    private Day02 d02 = new Day02("input_02.txt");
    private Day03 d03 = new Day03("input_03.txt");
    private Day04 d04 = new Day04("input_04.txt");
    private Day05 d05 = new Day05("input_05.txt");
    private Day06 d06 = new Day06("input_01.txt");
    private Day07 d07 = new Day07("input_01.txt");
    /*private Day08 d08 = new Day08();
    private Day09 d09 = new Day09();
    private Day10 d10 = new Day10();
    private Day11 d11 = new Day11();
    private Day12 d12 = new Day12();
    private Day13 d13 = new Day13();
    private Day14 d14 = new Day14();
    private Day15 d15 = new Day15();
    private Day16 d16 = new Day16();
    private Day17 d17 = new Day17();
    private Day18 d18 = new Day18();
    private Day19 d19 = new Day19();
    private Day20 d20 = new Day20();
    private Day10 d10 = new Day10();*/

    static void Main()
    {
        Console.WriteLine(GetTitle());
        
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine(new Day1().answer(1));
        Console.WriteLine(new Day1().answer(3));
        sw.Stop();

        Console.WriteLine("Day 1 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Day2 d2 = new Day2();
        sw.Restart();
        Console.WriteLine(d2.answer());
        Console.WriteLine(d2.answer2());
        sw.Stop();

        Console.WriteLine("Day 2 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Day3 d3 = new Day3();
        sw.Restart();
        Console.WriteLine(d3.answer());
        Console.WriteLine(d3.answer2());
        sw.Stop();

        Console.WriteLine("Day 3 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Day4 d4 = new Day4();
        sw.Restart();
        Console.WriteLine(d4.answer());
        sw.Stop();

        Console.WriteLine("Day 4 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Day5 d5 = new Day5();
        sw.Restart();
        Console.WriteLine(d5.answer());
        sw.Stop();

        Console.WriteLine("Day 5 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Day6 d6 = new Day6();
        sw.Restart();
        Console.WriteLine(d6.answer());
        sw.Stop();

        Console.WriteLine("Day 6 execution time: {0}ms. Ticks {1}", sw.ElapsedMilliseconds, sw.ElapsedTicks);

        Console.ReadLine();
    }

    public string GetTodaysSolution() => DateTime.Today.Day switch
    {
        1 => d01.GetSolution(),
        2 => d02.GetSolution(),
        3 => d03.GetSolution(),
        4 => d04.GetSolution(),
        5 => d05.GetSolution(),
        6 => d06.GetSolution(),
        7 => d07.GetSolution(),
        _ => "\nNo puzzle today...\n"
    };

    public static string GetTitle()
    {
        String title = "*********************************************\n*********************************************\n             ADVENT OF CODE 2022 \n*********************************************\n*********************************************\n\n\n";

        return title;
    }
}

class Day1
{
    public int answer(int numberOfElves)
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_01.txt").Split("\n\n");

        List<int> calories = new List<int>() { 0 };

        int elfCount = 0;

        for (int i = 0; i < data.Count(); i++)
        {
            foreach (string number in data[i].Split("\n"))
            {
                calories[elfCount] += Int32.Parse(number);
            }
            elfCount++;
            calories.Add(0);
        }

        calories.Sort();
        calories.Reverse();

        int totalCalories = 0;
        
        for (int i = 0; i < numberOfElves; i++)
        {
            totalCalories += calories[i];
        }

        return totalCalories;
    }
}

class Day2
{
    public int answer()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_02.txt").Split("\n");

        int score = 0;

        for (int i = 0; i < data.Count(); i++)
        {
            string opp = convertToWord(data[i][0]);
            string you = convertToWord(data[i][2]);
            score += selectedShapeScore(you);
            score += roundResult(opp + " " + you);
        }

        return score;
    }

    public int answer2()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_02.txt").Split("\n");

        int score = 0;

        for (int i = 0; i < data.Count(); i++)
        {
            string opp = convertToWord(data[i][0]);
            string you = newHand(opp + " " + result(data[i][2]));
            score += selectedShapeScore(you);
            score += roundResult(opp + " " + you);
        }

        return score;
    }

    private int selectedShapeScore(string shape) => shape switch
    {
        "Rock" => 1,
        "Paper" => 2,
        "Scisors" => 3,
        _ => 0
    };

    private int roundResult(string outcome) => outcome switch
    {
        "Rock Paper" => 6,
        "Rock Scisors" => 0,
        "Paper Rock" => 0,
        "Paper Scisors" => 6,
        "Scisors Rock" => 6,
        "Scisors Paper" => 0,
        _ => 3
    };

    private string convertToWord(char l) => l switch
    {
        'A' or 'X' => "Rock",
        'B' or 'Y' => "Paper",
        'C' or 'Z' => "Scisors",
        _ => ""
    };

    private string result(char r) => r switch
    {
        'X' => "Lose",
        'Y' => "Draw",
        'Z' => "Win",
        _ => ""
    };

    private string newHand(string outcome) => outcome switch
    {
        "Rock Draw" => "Rock",
        "Rock Win" => "Paper",
        "Rock Lose" => "Scisors",
        "Paper Draw" => "Paper",
        "Paper Win" => "Scisors",
        "Paper Lose" => "Rock",
        "Scisors Draw" => "Scisors",
        "Scisors Win" => "Rock",
        "Scisors Lose" => "Paper",
        _ => ""
    };

}

class Day3
{
    public int answer()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_03.txt").Split("\n");

        int sum = 0;

        for (int i = 0; i < data.Length; i++)
        {
            char commonItem;
            
            string secondHalf = data[i].Substring(data[i].Length / 2);

            for(int j = 0; j < secondHalf.Length; j++)
            {
                if (secondHalf.Contains(data[i][j]))
                {
                    commonItem = data[i][j];
                    sum += priority(commonItem);
                    break;
                }                
            }
        }

        return sum;
    }

    public int answer2()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_03.txt").Split("\n");

        int sum = 0;

        for (int i = 0; i < data.Length; i+=3)
        {
            char commonItem;

            string elf1 = data[i];
            string elf2 = data[i + 1];
            string elf3 = data[i + 2];

            for (int j = 0; j < elf1.Length; j++)
            {
                if (elf2.Contains(elf1[j]) && elf3.Contains(elf1[j]))
                {
                    commonItem = elf1[j];
                    sum += priority(commonItem);
                    break;
                }
            }
        }

        return sum;
    }

    private int priority(char c) => Char.IsLower(c) ? ( (int)c - (int)'a' + 1 ) : ( (int)c - (int)'A' + 27 );
}

class Day4
{
    public (int, int) answer()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_04.txt").Split("\n");

        (int containing, int overlapping) sum = (0, 0);

        for(int i=0; i<data.Length; i++)
        {
            string[] strings = data[i].Replace(',', '-').Split("-");

            int[] ints = toIntArray(strings);

            sum.containing += elfCovered(ints) ? 1 : 0;
            sum.overlapping += elfOverlapped(ints) || elfCovered(ints) ? 1 : 0;
        }

        return sum;
    }

    int[] toIntArray(string[] strings)
    {
        int[] ints = new int[strings.Length];
        
        for(int i = 0; i < ints.Length; i++)
        {
            ints[i] = Convert.ToInt32(strings[i]);
        }

        return ints;
    }

    bool elfCovered(int[] ints) => (ints[0] <= ints[2] && ints[1] >= ints[3]) || (ints[0] >= ints[2] && ints[1] <= ints[3]);

    bool elfOverlapped(int[] ints) => (ints[0] <= ints[2] && ints[1] >= ints[2] || ints[0] <= ints[3] && ints[1] >= ints[3]);
}

class Day5
{
    public (string, string) answer()
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_05.txt").Split("\n\n");

        string[] stacks = StacksAsStrings(data[0]);
        string[] stacksOptimised = new string[stacks.Length];
        stacks.CopyTo(stacksOptimised, 0);

        int[,] instructions = GetInstructions(data[1]);

        for (int i = 0; i < instructions.GetLength(0); i++)
        {
            // get the "move", "from" & to "numbers" from the instructions
            int number = instructions[i,0];
            int from = instructions[i, 1] -1;
            int to = instructions[i, 2] -1;
            // CrateMover 9000 - copy the reversed box letters to the destination, then remove from origin
            stacks[to] = string.Concat(stacks[to], stacks[from].Substring(stacks[from].Length - number).ReverseString());
            stacks[from] = stacks[from].Substring(0, stacks[from].Length - number);
            // CrateMover 9001 - copy box letters to the desitnation then remove from origin
            stacksOptimised[to] = string.Concat(stacksOptimised[to], stacksOptimised[from].Substring(stacksOptimised[from].Length - number));
            stacksOptimised[from] = stacksOptimised[from].Substring(0, stacksOptimised[from].Length - number);
        }

        // return the last letter from each stack as a string.
        return (GetLetters(stacks), GetLetters(stacksOptimised));
    }

    public string GetLetters(string[] stacks)
    {
        StringBuilder sb = new StringBuilder();
        
        for(int i = 0; i < stacks.Length;i++)
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

        for (int i = 0; i < data.Length-1;i++)
        {
            string s = data[i];

            for(int j = 0; j < s.Length; j++)
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
        string[] strings = instructions.Replace(" ","").Split('\n');
        
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


class Day6
{
    public (int, int) answer()
    {
        string data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_06.txt").ToLower();

        (int part1, int part2) answers = (PositionAfterFirstUniqueSubstring(data, 4), PositionAfterFirstUniqueSubstring(data, 14));

        return answers;
    }

    public int PositionAfterFirstUniqueSubstring(string letters, int n)
    {
        for(int i = 0; i < letters.Length - n; i++)
        {
            if (CharactersAreUnique(letters.Substring(i, n)))
            {
                return i+n;
            }
        }
        // returns -1 if no substring has been found.
        return -1;
    }

    public bool CharactersAreUnique(string letters)
    {
        List<char> chars = new List<char>();

        for (int l = 0; l < letters.Length; l++)
        {
            if (!chars.Contains(letters[l]))
            {
                chars.Add(letters[l]);
            }
        }
        
        return (chars.Count == letters.Length);
    }
}

namespace MyExtensions // must have "using MyExtensions" to grant access to the extension methods.
{
    static class MyExtensions // Note to self: Extension methods must be in a non-generic static class!
    {
        // Extention method for the String class - returns the reverse of a string.
        public static string ReverseString(this string word) // Note to self: Methods must be static and append the type with "this "
        {
            char[] letters = word.ToCharArray();
            Array.Reverse(letters);
            return new string(letters);
        }
    }
}
