using AdventOfCode2022.days;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public class Day21 : Day
{

    Dictionary<string, long> YellingMonkeys = new Dictionary<string, long>();
    List<string> WaitingMonkeys = new List<string>();

    Dictionary<string, long> Yelling = new Dictionary<string, long>();
    List<string> Waiting = new List<string>();
    List<string> CriticalPath = new List<string>();

    public Day21(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        string[] monkeys = File.ReadAllLines(this.InputData);

        for (int m = 0; m < monkeys.Length; m++)
        {

            if (Char.IsDigit(monkeys[m][6]))
            {
                string name = monkeys[m].Substring(0, 4);
                long number = long.Parse(monkeys[m].Substring(6));
                YellingMonkeys.Add(name, number);
                if ( name == "humn" )
                {
                    CriticalPath.Add(monkeys[m]);
                }
                else
                {
                    Yelling.Add(name, number);
                }
            }
            else
            {
                WaitingMonkeys.Add(monkeys[m]);
                Waiting.Add(monkeys[m]);
            }
        }

        long part1 = StartYelling();

        FindCriticalPath();

        long part2 = FindHumanNumber();

        sw.Stop();
        return ($"Root will yell {part1}", $"I need to yell {part2}");
    }

    public long StartYelling()
    {
        while (!YellingMonkeys.ContainsKey("root"))
        {
            for (int i = 0; i < WaitingMonkeys.Count; i++)
            {
                // look for the two monkies that are yelling number
                string monkey1 = WaitingMonkeys[i].Substring(6, 4);
                string monkey2 = WaitingMonkeys[i].Substring(13);

                if (YellingMonkeys.ContainsKey(monkey1) && YellingMonkeys.ContainsKey(monkey2))
                {
                    long number = MathOperation(WaitingMonkeys[i][11], YellingMonkeys[monkey1], YellingMonkeys[monkey2]);
                    string name = WaitingMonkeys[i].Substring(0, 4);
                    YellingMonkeys.Add(name, number);
                    WaitingMonkeys.RemoveAt(i);
                }
            }
        }

        return YellingMonkeys["root"];
    }

    public long MathOperation(char o, long a, long b) => o switch
    {
        '+' => a + b,
        '-' => a - b,
        '*' => a * b,
        '/' => a / b,
        _ => 0
    };


    public void FindCriticalPath()
    {
        while (Waiting.Count > 0)
        {
            for (int i = 0; i < Waiting.Count; i++)
            {
                // look for the two monkies that are yelling number
                string monkey1 = Waiting[i].Substring(6, 4);
                string monkey2 = Waiting[i].Substring(13);

                if (Yelling.ContainsKey(monkey1) && Yelling.ContainsKey(monkey2))
                {
                    long number = MathOperation(Waiting[i][11], Yelling[monkey1], Yelling[monkey2]);
                    string name = Waiting[i].Substring(0, 4);
                    Yelling.Add(name, number);
                    Waiting.RemoveAt(i);
                    continue;
                }
                // check to see if one of the monkeys is on the critical path
                for (int m = 0; m < CriticalPath.Count; m++)
                {
                    if (CriticalPath[m].Contains(monkey1) || CriticalPath[m].Contains(monkey2))
                    {
                        Console.WriteLine(Waiting[i]);
                        CriticalPath.Add(Waiting[i]);
                        Waiting.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }

    public long FindHumanNumber()
    {
        // look for the monkey giving root a number
        string monkey1 = CriticalPath[CriticalPath.Count - 1].Substring(6, 4);
        string monkey2 = CriticalPath[CriticalPath.Count - 1].Substring(13);

        if (Yelling.ContainsKey(monkey1))
        {
            long number = Yelling[monkey1];
            Console.WriteLine(number);
            Yelling[monkey2] = number;
        }
        else if (Yelling.ContainsKey(monkey2))
        {
            long number = Yelling[monkey2];
            Console.WriteLine(number);
            Yelling[monkey1] = number;
        }
        else
        {
            Console.WriteLine("ERROR!");
        }
        // remove the root from the critical path
        CriticalPath.Remove(CriticalPath[CriticalPath.Count - 1]);

        while (CriticalPath.Count > 1)
        {
            // get the last item on the critical path
            string m1 = CriticalPath[CriticalPath.Count - 1].Substring(6, 4);
            string m2 = CriticalPath[CriticalPath.Count - 1].Substring(13);

            long n = Yelling[CriticalPath[CriticalPath.Count - 1].Substring(0, 4)];

            char op = CriticalPath[CriticalPath.Count - 1][11];

            Console.WriteLine("{0}: {1} {2} {3}", n, m1, op, m2);
            // check for the known number
            if (Yelling.ContainsKey(m1))
            {
                long number = Yelling[m1];
                Console.WriteLine(m1 + " = " + number);
                if (op == '+' || op == '*')
                {
                    Yelling[m2] = MathOperation(GetInverseOperator(op), n, number);
                }
                else if (op == '-')
                {
                    Yelling[m2] = MathOperation(op, number, n);
                }
                else if (op == '/')
                {
                    Yelling[m2] = MathOperation(op, number, n);
                }
                else
                {
                    Console.WriteLine("ERROR!");
                }
            }

            else if (Yelling.ContainsKey(m2) )
            {
                long number = Yelling[m2];
                Console.WriteLine(m2 + " = " + number);
                Yelling[m1] = MathOperation(GetInverseOperator(op), n, number);
            }
            else
            {
                Console.WriteLine("ERROR!");
            }

            CriticalPath.Remove(CriticalPath[CriticalPath.Count - 1]);
        }


        return Yelling["humn"];
    }

    public char GetInverseOperator(char c) => c switch
    {
        '-' => '+',
        '*' => '/',
        '+' => '-',
        '/' => '*',
        _ => '='
    };
}
