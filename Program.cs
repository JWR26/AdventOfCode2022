using MyExtensions;
using System.Text;

class AdventOfCode
{
    static void Main()
    {
        Console.WriteLine(GetTitle());

        AdventOfCode aoc = new AdventOfCode();
        Console.WriteLine(aoc.GetDaySolution(DateTime.Today.Day));

        Console.ReadLine();

        Console.WriteLine(GetPreviousDays());

        for (int i = 1; i < DateTime.Today.Day; i++)
        {
            AdventOfCode previousDay = new AdventOfCode();
            Console.WriteLine(previousDay.GetDaySolution(i));
            Console.WriteLine("\n*********************************************\n");
        }

        Console.ReadLine();
    }
    public string GetDaySolution(int d) => d switch
    {
        1 => new Day01(1, "input_01.txt").GetSolution(),
        2 => new Day02(2, "input_02.txt").GetSolution(),
        3 => new Day03(3, "input_03.txt").GetSolution(),
        4 => new Day04(4, "input_04.txt").GetSolution(),
        5 => new Day05(5, "input_05.txt").GetSolution(),
        6 => new Day06(6, "input_06.txt").GetSolution(),
        7 => new Day07(7, "input_07.txt").GetSolution(),
        8 => new Day08(8, "input_08.txt").GetSolution(),
        9 => new Day09(9, "input_09.txt").GetSolution(),
        10 => new Day10(10, "input_10.txt").GetSolution(),
        11 => new Day11(11, "input_11.txt").GetSolution(),
        12 => new Day12(12, "input_12.txt").GetSolution(),
        13 => new Day13(13, "input_13.txt").GetSolution(),
        14 => new Day14(14, "input_14.txt").GetSolution(),
        15 => new Day15(15, "input_15.txt").GetSolution(),
        16 => new Day16(16, "input_16.txt").GetSolution(),
        17 => new Day17(17, "input_17.txt").GetSolution(),
        18 => new Day18(18, "input_18.txt").GetSolution(),
        19 => new Day19(19, "input_19.txt").GetSolution(),
        20 => new Day20(20, "input_20.txt").GetSolution(),
        21 => new Day21(21, "input_21.txt").GetSolution(),
        22 => new Day22(22, "input_22.txt").GetSolution(),
        23 => new Day23(23, "input_23.txt").GetSolution(),
        24 => new Day24(24, "input_24.txt").GetSolution(),
        25 => new Day25(25, "input_25.txt").GetSolution(),
        _ => "\nNo puzzle today...\n"
    };
    public static string GetTitle()
    {
        String title = "*********************************************\n*********************************************\n             ADVENT OF CODE 2022 \n*********************************************\n*********************************************\n";

        return title;
    }
    public static string GetPreviousDays()
    {
        String separator = "\n*********************************************\n           Previous Days Solutions \n*********************************************\n";

        return separator;
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
