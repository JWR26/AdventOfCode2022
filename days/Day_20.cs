using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

public class Day20 : Day
{
    public const long KEY = 811589153;
    public Day20(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        // convert input data to a list of file numbers
        string[] numbers = File.ReadAllLines(this.InputData);

        // Part 1 
        // create list, mix it and sum the releveant indexes
        List<FileNumber> List1 = CreateList(numbers);

        MixList(List1, 1);
        long part1 = SumIndicesFromZero(List1);
        // Part 2
        // Apply the Decryption Key, then mix the list 10 times
        List<FileNumber> List2 = CreateList(numbers, KEY);
        MixList(List2, 10);
        long part2 = SumIndicesFromZero(List2);

        sw.Stop();
        return ($"Sum of three numbers is {part1}", $"Using the Decryption Key gives {part2}");
    }

    public List<FileNumber> CreateList(string[] numbers, long multiplier = 1)
    {
        // The encrypted file is a list of integers
        List<FileNumber> EncryptedFile = new List<FileNumber>(numbers.Length);

        for (int n = 0; n < numbers.Length; n++)
        {
            FileNumber FN = new FileNumber(n, numbers[n], multiplier);
            EncryptedFile.Add(FN);
        }

        // create the links between file numbers
        for (int f = 0; f < EncryptedFile.Count; f++)
        {

            if (f == 0)
            {
                EncryptedFile[f].previous = EncryptedFile[EncryptedFile.Count - 1]; // previous of first is last in list
            }
            else
            {
                EncryptedFile[f].previous = EncryptedFile[f - 1];
            }
            if (f == EncryptedFile.Count - 1)
            {
                EncryptedFile[f].next = EncryptedFile[0]; // next of last is first
            }
            else
            {
                EncryptedFile[f].next = EncryptedFile[f + 1];
            }
        }

        return EncryptedFile;
    }

    public void MixList(List<FileNumber> list, int mixes)
    {
        // move each number from the original file based on their orignial order.
        for (int m = 0; m < mixes; m++)
        {
            for (int n = 0; n < list.Count; n++)
            {
                FileNumber? f = list[n];
                int newIndex = (int)(f.Value % (list.Count - 1));

                // move "forward" around the list inserting the number before its future "next"
                if (newIndex > 0)
                {
                    FileNumber before = f.GetNext(newIndex);
                    f.RemoveSelf();
                    f.previous = before.previous;
                    f.next = before;
                    before.previous.next = f;
                    before.previous = f;

                }
                // move backwards, again inserting the number before its future "next"
                else if (newIndex < 0)
                {
                    FileNumber before = f.GetPrevious(newIndex);
                    f.RemoveSelf();
                    f.next = before;
                    f.previous = before.previous;
                    before.previous.next = f;
                    before.previous = f;
                }
                else
                {
                    continue; // this is for a 0 that will not move
                }
            }
        }
    }

    public long SumIndicesFromZero(List<FileNumber> list)
    {
        FileNumber zeroFile = list.Find(f => f.Value == 0);

        long sum = 0;
        for (int i = 1; i < 4; i++)
        {
            int num = (1000 * i) % list.Count;
            long value = zeroFile.GetNumberValue(num);
            Console.WriteLine($"{i * 1000}: {value}");
            sum += value;
        }

        return sum;
    }
}
/// <summary>
/// Represents a unique number in an encrypted file
/// </summary>
public class FileNumber
{
    public FileNumber next;
    public FileNumber previous;
    public int InitialIndex { get; }
    public long Value { get; }
    /// <summary>
    /// Takes the index of the number in the file and string representation of it's value.
    /// </summary>
    /// <param name="initialIndex"></param>
    /// <param name="value"></param>
    /// <param name="multiplier"></param>
    public FileNumber(int initialIndex, string value, long multiplier = 1)
    { 
        this.InitialIndex = initialIndex;
        this.Value = long.Parse(value) * multiplier;
    }

    /// <summary>
    /// Method to be called by the FileNumber on itself to return the previous FileNumber in the list for a relative index, with zero reffering to itself.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public FileNumber GetPrevious(int index)
    {
        if (index == 0)
        {
            return previous;
        }
        return previous.GetPrevious(index + 1);
    }

    public FileNumber GetNext(int index)
    {
        if (index == 0) 
        {
            return next;
        }
        return next.GetNext(index - 1);
    }

    public long GetNumberValue(int at)
    {
        if ( at == 0)
        {
            return Value;
        }
        return next.GetNumberValue(at-1);
    }

    public void RemoveSelf()
    {
        next.previous = previous;
        previous.next = next;
    }
}
