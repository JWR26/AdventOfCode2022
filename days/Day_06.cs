using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day06 : Day
{
    public Day06(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }
    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        string data = File.ReadAllText(InputData);

        (int part1, int part2) numbers = (PositionAfterFirstUniqueSubstring(data, 4), PositionAfterFirstUniqueSubstring(data, 14));
        sw.Stop();
        return (numbers.part1.ToString(), numbers.part2.ToString());
    }

    public int PositionAfterFirstUniqueSubstring(string letters, int n)
    {
        for (int i = 0; i < letters.Length - n; i++)
        {
            if (CharactersAreUnique(letters.Substring(i, n)))
            {
                return i + n;
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

