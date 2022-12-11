using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

public class Day10 : Day
{
    public StringBuilder pixels = new StringBuilder();
    // declare "X", the cpu's single register, which starts at 1, and also it's register
    int X = 1;
    List<int> xRegister = new List<int>();

    public Day10(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }
    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // get the program input
        string[] program = File.ReadAllText(InputData).Split("\n");

        xRegister.Add(X);

        int cycle = 0;

        // loop through the program updating x based on the instructions
        for (int i = 0; i < program.Length; i++)
        {
            // get the command type - first letter gives either "noop" or "addx"
            string command = (program[i][0] == 'n') ? "noop" : "addx";
            // noop takes 1 cycle and does not modify X
            if (command == "noop")
            {
                cycle += 1; 
                updatePixels(cycle);
                xRegister.Add(X); // value of x stored on the register
                continue; // move on to next command
            }
            // addx executes at the end of 2 cycles
            if (command == "addx")
            {
                // first cycle nothing happens
                cycle += 1;
                updatePixels(cycle);
                xRegister.Add(X); // unchaged value of X stored on register
                // second cycle, X is modified by the command
                int change = int.Parse(program[i].Split(" ")[1]);
                cycle += 1;
                updatePixels(cycle);
                X += change; 
                xRegister.Add(X); // modified value of x stored on the register
                continue; // move to next command
            }
        }
        int sumOfSignalStrenght = 0;
        // get the values of X at cycle 20 then every 40 cylces to 220
        for (int c = 20; c <= 220; c+=40)
        {
            sumOfSignalStrenght += xRegister[c-1] * c; // negative 1 because value DURING a cycle is the value logged at the end of the previous.
        }

        displayScreen();

        sw.Stop();
        return (sumOfSignalStrenght.ToString(), "Read the letters...");
    }

    
    public void updatePixels(int cycle)
    {
        int spritePos = xRegister[cycle - 1] ;

        if (cycle % 40 >= spritePos && cycle % 40 <= spritePos +2)
        {
            pixels.Append("#");
        }
        else
        {
            pixels.Append(".");
        }
    }
    
    public void displayScreen()
    {
        string screenData = pixels.ToString();

        for (int i = 0; i < screenData.Length; i+=40)
        {
            Console.WriteLine(screenData.Substring(i, 40));
        }

        Console.WriteLine("\n");
    }
}

