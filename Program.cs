using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;

class Solution
{
    static void Main()
    {      
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Console.WriteLine(new Day1().answer(1));
        Console.WriteLine(new Day1().answer(3));
        sw.Stop();

        Console.WriteLine("Day 1 execution time: {0}ms.", sw.ElapsedMilliseconds);

        Day2 d2 = new Day2();
        sw.Restart();
        Console.WriteLine(d2.answer());
        Console.WriteLine(d2.answer2());
        sw.Stop();

        Console.WriteLine("Day 2 execution time: {0}ms.", sw.ElapsedMilliseconds);

        Day3 d3 = new Day3();
        sw.Restart();
        Console.WriteLine(d3.answer());
        Console.WriteLine(d3.answer2());
        sw.Stop();

        Console.WriteLine("Day 3 execution time: {0}ms.", sw.ElapsedMilliseconds);

        Console.ReadLine();
    } 
}

class Day1
{
    public int answer(int numberOfElves)
    {
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_01_01.txt").Split("\n\n");

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
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_02_01.txt").Split("\n");

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
        string[] data = File.ReadAllText("C:\\Users\\jonat\\source\\repos\\AdventOfCode2022\\input_02_01.txt").Split("\n");

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
        return 0;
    }

    public int answer2()
    {
        return 0;
    }
}