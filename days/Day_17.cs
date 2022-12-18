using AdventOfCode2022.days;
using Grids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day17 : Day
{
    // list of rocks that will fall
    public string[] Rocks = new string[5] { "####", ".#.\n###\n.#.", "..#\n..#\n###", "#\n#\n#\n#", "##\n##" };
    public Point RockOrigin = new Point(3, 0);

    public int TotalRocks = 2022;

    public readonly Point DOWN = new Point(0, 1);
    
    public Day17(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();

        // Draw the chamber
        DynamicGrid chamber= new DynamicGrid();

        string floor = "+-------+";
        string emptyRow = "|.......|";
        // Add the floor to the chamber
        chamber.AddRows(floor);

        // the chamber starts with a hight of three plus the height of the first shape
        string gasJets = File.ReadAllText(InputData);
        int gasTick = 0;

        for (int r = 0; r < TotalRocks; r++)
        {
            int i = (r + Rocks.Length) % Rocks.Length; // index of the rock that will fall
            Shape rock = new Shape(Rocks[i]);
            rock.UpdateShapePosition(RockOrigin);
            // for every new rock resize the chamber
            int rowsToAdd = (rock.Height + 3);
            chamber.AddRows(emptyRow, rowsToAdd);

            while (chamber.IsEmpty(rock))
            {
                // apply gas jet
                int g = (gasTick + gasJets.Length) % gasJets.Length;
                Point gasDirection = GasDirection(gasJets[g]);
                if ( chamber.IsEmpty( rock, gasDirection ) )
                {
                    rock.UpdateShapePosition(gasDirection);
                }

                gasTick++;

                if ( chamber.IsEmpty(rock, DOWN) )
                {
                    rock.UpdateShapePosition(DOWN); // rock can move down
                }
                else
                {
                    chamber.SetShapeValues(rock); // rock cant move down so comes to rest
                }

            }
            // remove empty rows from the top of the chamber
            chamber.RemoveRowsOf(emptyRow);
        }

        int part1 = chamber.Rows - 1;

        sw.Stop();
        return ($"Rock tower is {part1} units tall", "Not done yet...");
    }

    public Point GasDirection(char d) => d switch
    {
        '>' => new Point(1, 0),
        '<' => new Point(-1, 0),
        _ => new Point(0, 0),
    };
}

public class DynamicGrid
{
    
    public List<string> Grid = new List<string>();

    public char EmptySpace;
    public int Rows
    {
        get
        {
            return Grid.Count;
        }
    }
    public int Columns;

    public DynamicGrid(char emptySpace = '.')
    {
        this.EmptySpace = emptySpace;
    }

    public void AddRows(string row, int number = 1, int at = 0)
    {
        for (int i = 0; i < number; i++)
        {
            this.Grid.Insert(at, row);
        }
    }

    public void Draw()
    {
        for (int line = 0; line < Grid.Count; line++)
        {
            Console.WriteLine(Grid[line]);
        }
    }
    
    public int FirstRowContaining(char c)
    {
        for(int i = 0; i < Grid.Count; i++)
        {
            if (Grid[i].Contains(c))
            {
                return i;
            }
        }
        return -1;
    }
    public bool IsEmpty(Shape shape)
    {
        for (int s = 0; s < shape.points.Count; s++)
        {
            if (!IsEmpty(shape.points[s]) && shape.points[s].id != shape.EmptySpace)
            {
                return false;
            }
        }
        return true;
    }
    public bool IsEmpty(Shape shape, Point distance)
    {
        for (int s = 0; s < shape.points.Count; s++)
        {
            if (!IsEmpty(shape.points[s] + distance) && shape.points[s].id != shape.EmptySpace)
            {
                return false;
            }
        }
        return true;
    }
    public bool IsEmpty(Point p)
    {
        return (Grid[p.row][p.column] == EmptySpace);
    }

    public void RemoveRow(int row)
    {
        Grid.RemoveAt(row);
    }

    public void RemoveRowsOf(string row)
    {
        for (int r = Grid.Count - 1; r >= 0; r--)
        {
            if (Grid[r] == row)
            {
                Grid.RemoveAt(r);
            }
        }
    }
    public void SetShapeValues(Shape s)
    {
        for (int p = 0; p < s.points.Count; p++)
        {
            SetPointValue(s.points[p]);
        }
    }
    public void SetPointValue(Point p)
    {
        StringBuilder updatedRow = new StringBuilder(Grid[p.row]);
        updatedRow[p.column] = p.id;
        Grid[p.row] = updatedRow.ToString();
    }
}

public class Shape
{
    public List<Point> points = new List<Point>();

    public char EmptySpace;

    public int Width;
    public int Height;

    public Shape(string shape, char emptySpace = '.')
    {
        this.DefineShape(shape);
        this.EmptySpace = emptySpace;
    }
    public void DefineShape(string s)
    {
        string[] lines = s.Split("\n");

        Height = lines.Length;
        Width = lines[0].Length;

        for (int l = 0; l < lines.Length; l++)
        {
            for(int c = 0; c < lines[l].Length; c++)
            {
                Point p = new Point(c, l, lines[l][c]);
                points.Add(p);
            }
        }
    }
    public void UpdateShapePosition(Point p)
    {
        for(int i = 0; i < points.Count; i++)
        {
            points[i] += p;
        }
    }
}