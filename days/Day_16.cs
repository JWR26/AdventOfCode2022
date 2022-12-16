using AdventOfCode2022.days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Day16 : Day
{
    List<Valve> ValveList = new List<Valve>();

    List<int> MaxPossiblePressures = new List<int>();

    public Day16(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        // first step is to parse the input to create some valves
        string[] valveReport = File.ReadAllText(this.InputData).Split("\n");

        // start a list containing all closed valves
        List<Valve> closed = new List<Valve>();

        // create the valves and add them to the list of valves
        for (int l = 0; l < valveReport.Length; l++)
        {
            string vID = valveReport[l].Substring("Valve ".Length, 2);

            int flowrateindex = valveReport[l].IndexOf("rate=") + "rate=".Length;
            int flowrateend = valveReport[l].IndexOf(';');
            int vFlowRate = int.Parse(valveReport[l].Substring(flowrateindex, flowrateend - flowrateindex));

            Valve v = new Valve(vFlowRate, vID);

            ValveList.Add(v);
        }

        // add the valve to the closed list if it has a flow rate above 0 & set its neighbours
        for (int v = 0; v < ValveList.Count; v++)
        {
            if (ValveList[v].FlowRate > 0)
            {
                closed.Add(ValveList[v]);
            }
                        
            string[] neighbours = valveReport[v].Substring(valveReport[v].IndexOf("valves ") + "valves ".Length).Trim('\r').Split(", ");
            for (int n = 0; n < neighbours.Length; n++)
            {
                Valve? neighbour = ValveList.Find(x => x.ID == neighbours[n]);
                if (neighbour != null)
                {
                    ValveList[v].AddNeighbour(neighbour);
                }
            }
        }

        Valve AA = ValveList.Find(x => x.ID == "AA");

        PressureFromPaths(AA, closed, 30, 0);

        return base.CalculateAnswer();
    }

    public void PressureFromPaths(Valve start, List<Valve> closed, int minutes, int pressure)
    {
        Valve current = start;

        Console.WriteLine(closed.Count);

        for (int i = 0; i < closed.Count; i++)
        {
            BreadthFirstSearch bfs = new BreadthFirstSearch();

            int time = bfs.FindShortestDistance(current, closed[i]) + 1;
            Console.WriteLine(time);

            if ( time < minutes )
            {
                current = closed[i];

                closed.Remove(current);

                int valvePressureReleased = current.TotalPressureReleased(minutes - time);

                PressureFromPaths(current, closed, minutes - time, pressure + valvePressureReleased);
            }
            // if can't get to the next closed valve in the time left add this as a path to max possible pressures
            else
            {
                MaxPossiblePressures.Add(pressure);
            }
        }
    }
}


public class BreadthFirstSearch
{
    /// <summary>
    /// Returns the shortest distance between a start and end valve. It assumes all distances are 1. -1 will be returned if no path is found.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public int FindShortestDistance(Valve start, Valve end)
    {
        // remove parent of start node
        start.Parent= null;
        
        // A last in first out queue of valves.
        Queue<Valve> queue = new Queue<Valve>();
        List<Valve> explored = new List<Valve>();

        Console.WriteLine($"Searching for {end.ID} from {start.ID}");

        queue.Enqueue(start);

        while(queue.Count > 0)
        {
            Valve current = queue.Dequeue();
            Console.WriteLine($"Searching for {end.ID} from {current.ID}");
            if (current.Equals(end))
            {
                List<Valve> path = new List<Valve>();
                Console.WriteLine($"Found path");
                while (current.Parent != null)
                {
                    path.Add(current);
                    Console.WriteLine($"parent of {current.ID} is: {current.Parent.ID}");
                    current = current.Parent;
                }

                return path.Count;
            }

            for (int n = 0; n < current.Neighbours.Count; n++)
            {
                if (!explored.Contains(current.Neighbours[n]))
                {
                    current.Neighbours[n].Parent = current;
                    queue.Enqueue(current.Neighbours[n]);
                }
            }

            explored.Add(current);
        }
        
        return -1;
    }
}

public class Valve
{
    /// <summary>
    /// The ID of a valve, typically two consecutive capitals.
    /// </summary>
    public string ID;
    /// <summary>
    /// Flowrate of a valve in pressure per miniute
    /// </summary>
    public int FlowRate;
    /// <summary>
    /// List of ajoining valves.
    /// </summary>
    public List<Valve> Neighbours = new List<Valve>();
    /// <summary>
    /// The Parent of a valve for pathfinding. Is nullable by default so valves can be the "start" of a path.
    /// </summary>
    public Valve? Parent;
    /// <summary>
    /// Creates a new valve with an ID and Flowrate.
    /// </summary>
    /// <param name="flowRate"></param>
    /// <param name="id"></param>
    public Valve(int flowRate, string id)
    {
        this.FlowRate = flowRate;
        this.ID = id;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        Valve valve = obj as Valve;
        if (valve == null)
        {
            return false;
        }

        return Equals(valve);
    }

    public bool Equals(Valve other)
    {
        return ( this.ID == other.ID );
    }

    public void AddNeighbour(Valve valve)
    {
        Neighbours.Add(valve);
    }

    public int TotalPressureReleased(int minutes) => FlowRate * minutes;

}
