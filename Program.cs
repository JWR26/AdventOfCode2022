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
