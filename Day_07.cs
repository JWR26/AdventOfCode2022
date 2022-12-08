using AdventOfCode2022;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day07 : Day
{
    public string[] terminalOutput;

    public Day07(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // terminal actions are one per line.
        terminalOutput = File.ReadAllText(InputData).Split("\n");

        // construct a list of directories based on the terminal output

        List<Directory> directoryList = new List<Directory>();

        // directory we are currently in.
        Directory current = new Directory("/", null);
        directoryList.Add(current);
        //This is the first line from the terminal it is going to be our "root" directory and starting point//

        // start from the second line of the terminal output and perform all of the relevant actions
        for (int i = 1; i < terminalOutput.Length; i++)
        {
            /* 
            If the string starts "$ cd":
                & followed by " .." set the current directorys parent as the current directory
                & follewed by " /" switches to the root (ie first directory in the list, our starting point)
                & followed by ls continue on, no action required
                & followed by at least 1 letter at index 5, set to this directory from the list.
            */

            if (terminalOutput[i][0] == '$')
            {
                if (terminalOutput[i] == "$ cd ..")
                {
                    current = (current.ParentDirectory != null) ? current.ParentDirectory : current;
                    continue;
                }
                else if (terminalOutput[i] == "$ cd /")
                {
                    current = directoryList[0];
                    continue;
                }

                else if (terminalOutput[i] == "$ ls")
                {
                    continue;
                }

                else
                {
                    string nextDirectory = terminalOutput[i].Split(" ")[2];
                    for(int j = 0; j < current.subDirectories.Count; j++)
                    {
                        if(current.subDirectories[j].Name == nextDirectory)
                        {
                            current = current.subDirectories[j];
                            break;
                        }
                    }
                }
                continue;
            }

            // if the string starts with a number it is a file. add it to the current directory
            if ( Char.IsDigit(terminalOutput[i], 0) )
            {
                current.addFile(systemFileFromString(terminalOutput[i]));
                continue;
            }

            // if the string starts with "dir" a directory of the given name needs 1) creating, 2) adding to the directory list & 3) adding to the current directory's list of sub directories.
            if ( terminalOutput[i][0] == 'd' )
            {
                string directoryName = terminalOutput[i].Split(" ")[1];
                Directory dir = new Directory(directoryName, current);
                directoryList.Add(dir);
                current.addDirectory(dir);
                continue;
            }
        }
        // for each directory in the directory list, sum the sizes of all directories less than a threshold
        const int THRESHOLD = 100000;
        int sumOfSizes = 0;
        // space available on disk
        int space = 70000000 - directoryList[0].GetSize();
        // space required to be freed up
        int target = 30000000 - space;

        List<int> choices = new List<int>();

        for (int d = 0; d < directoryList.Count; d++)
        {
            int s = directoryList[d].GetSize();
            if ( s <= THRESHOLD )
            {
                sumOfSizes += s;
            }
            if ( s >= target)
            {
                choices.Add(s);
            }
        }

        int[] choiceArray = choices.ToArray();
        Array.Sort(choiceArray);
        int smallest = choiceArray[0];

        sw.Stop();
        return (sumOfSizes.ToString(), smallest.ToString());
    }

    // returns a SystemFile based on a string. Expected string format will be "12345 abc.xyz"
    public SystemFile systemFileFromString(string s)
    {
        string[] data = s.Split(" ");
        
        return new SystemFile(data[1], int.Parse(data[0]));
    }
}

public class Directory
{
    public List<Directory> subDirectories;
    public List<SystemFile> files;
    
    public Directory? ParentDirectory { get; private set; }
    public string Name { get; private set; }
    public Directory(string n, Directory? d) // directory constructor
    {
        this.Name = n;
        this.ParentDirectory = d;
        // initialise the list of sub-directories and files contained in the directory
        this.subDirectories = new List<Directory>();
        this.files = new List<SystemFile>();
    }

    public int GetSize()
    {
        int size = 0;

        for (int d = 0; d < subDirectories.Count; d++)
        {
            size += subDirectories[d].GetSize();
        }

        for (int f = 0; f < files.Count; f++)
        {
            size += files[f].Size;
        }

        return size;
    }

    public void addDirectory(Directory dir)
    {
        subDirectories.Add(dir);
    }

    public void addFile(SystemFile systemFile)
    {
        files.Add(systemFile);
    }
}

public class SystemFile
{
    public string Name { get; private set; }
    public int Size { get; private set; }

    public SystemFile(string n, int s)
    {
        this.Name = n;
        this.Size = s;
    }
}
