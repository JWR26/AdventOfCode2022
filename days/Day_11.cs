using AdventOfCode2022.days;
using MyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Day11 : Day
{
    public Day11(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // get the information for each monkey
        string[] monkeyInfo = File.ReadAllText(InputData).Split("\n\n");

        // PART 1 - play 20 rounds and calculate monkey business
        ulong part1 = PlayMonkeyBusiness(monkeyInfo, 20, false);

        // PART 2 - play 10000 rounds and calculate monkey business

        ulong part2 = PlayMonkeyBusiness(monkeyInfo, 10000, true);

        sw.Stop();
        return (part1.ToString(), part2.ToString());
    }

    public ulong PlayMonkeyBusiness(string[] monkeyInfo, int rounds, bool worried)
    {
        // intialise a list of monkeys
        List<Monkey> monkeys = new List<Monkey>();

        ulong lcm = 1;

        for (int m = 0; m < monkeyInfo.Length; m++)
        {
            Monkey monkey = new Monkey(monkeyInfo[m]);
            monkeys.Add(monkey);
            lcm *= monkey.testThreshold;
        }

        Console.WriteLine(lcm);

        // give each monkey it's target
        for (int m = 0; m < monkeyInfo.Length; m++)
        {
            monkeys[m].IdentifyTargetMonkeys(monkeys);
        }

        for (int r = 0; r < rounds; r++)
        {
            for (int m = 0; m < monkeys.Count; m++)
            {
                monkeys[m].InspectAllItems(worried, lcm);
            }
        }

        List<ulong> monkeyActivity = new List<ulong>();

        for (int m = 0; m < monkeys.Count; m++)
        {
            monkeyActivity.Add(monkeys[m].InspectedItems);
        }

        monkeyActivity.Sort();
        monkeyActivity.Reverse();

        ulong monkeyBusiness = monkeyActivity[0] * monkeyActivity[1];

        return monkeyBusiness;
    }
}

public class Monkey
{
    public List<ulong> items = new List<ulong>(); // what items the monkey is currently holding

    public string operation;

    public ulong InspectedItems = 0; // number of items a monkey has inspected

    public ulong testThreshold; // self explanatory

    public Monkey passTestTarget;
    public Monkey failTestTarget;

    (int p, int f) target;

    public Monkey(string info)
    {
        // initialise monkey based on it's info.
        string[] lines = info.Split("\n");
        // seconds line contains the monkey's starting items
        string[] itemStartValues = lines[1].Substring(lines[1].IndexOf("items: ") + 7).Split(", ");

        for (int v = 0; v < itemStartValues.Length; v++)
        {
            this.items.Add(ulong.Parse(itemStartValues[v]));
        }
        // thrid line is the operation to perform after the "= ". 
        this.operation = lines[2].Substring(lines[2].IndexOf("= ") + 2);
        // fourth line of monkey info gives the test threshold after the word "by "
        this.testThreshold = ulong.Parse(lines[3].Substring(lines[3].IndexOf("by ") + 3));
        // last two lines are who to throw the item to  
        this.target.p = lines[4][lines[4].Length - 1] - '0';
        this.target.f = lines[5][lines[5].Length - 1] - '0';
    }

    public void IdentifyTargetMonkeys(List<Monkey> m)
    {
        passTestTarget = m[target.p];
        failTestTarget = m[target.f];
    }

    public void InspectAllItems(bool worried, ulong lcm)
    {
        while (items.Count > 0)
        {
            // increase worry level by inspecting item
            InspectedItems++;
            
            ulong n = operation.Operate(items[0]) ;

            items[0] = (worried) ? n : n / 3; // are you worried...

            items[0] = items[0] % lcm;
            // ... no i'm pissed off

            // monkey decides where to throw it
            if (items[0] % testThreshold == 0)
            {
                passTestTarget.ReceiveItem(items[0]);
            }
            else
            {

                failTestTarget.ReceiveItem(items[0]);
            }

            items.RemoveAt(0);
        }
    }
    public void ReceiveItem(ulong i)
    {
        items.Add(i);
    }
}

