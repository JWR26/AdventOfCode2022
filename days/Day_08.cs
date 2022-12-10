using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode2022.days;

public class Day08 : Day
{
    public Day08(int d, string inputFile) : base(d)
    {
        this.InputData = String.Format("{0}{1}", filepath, inputFile);
    }

    public override (string a, string b) CalculateAnswer()
    {
        sw.Start();
        // turn patch given in file into rows
        string[] patch = File.ReadAllText(InputData).Split("\n");

        // grid giving each trees visibility. Initial values are false
        bool[,] treeVisible = new bool[patch.Length, patch[0].Length];
        int numberOfTreesVisible = 0;
        // For the following iterations, skip over trees that are already visible.

        // Iteration 1 - iterate from left to right then right to left for each row
        for (int row = 0; row < patch[0].Length; row++)
        {
            // delcare the highest tree as the one on the left
            int highestTree = patch[row][0] - '0';
            // start moving along the row one tree at a time
            for (int col = 0; col < patch[row].Length; col++)
            {
                // firstly mark a tree as visible if it is on an edge
                if ( row == 0 || col == 0 || row == patch[row].Length - 1 || col == patch.Length - 1)
                {
                    // if the tree is not marked as visible, mark it so and update the count
                    if (!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // compare the current tree to the highest tree. If it is higher it is visible from the side.
                if (patch[row][col] - '0' > highestTree)
                {
                    highestTree = patch[row][col] - '0';
                    // if the tree is not marked as visible, mark it so and update the count
                    if (!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // once a tree of height 9 is seen, no further trees are visible so stop iterating
                if (highestTree == 9)
                {
                    break;
                }
            }
            // delcare the highest tree as the one on the right
            highestTree = patch[row][patch[row].Length-1];
            // now move along the row from right to left (no need to go to left edge though on way back)
            for (int col = patch[row].Length - 1; col > 0; col--)
            {
                // compare the current tree to the highest tree. If it is higher it is visible from the side.
                if (patch[row][col] > highestTree)
                {
                    highestTree = patch[row][col];
                    // if the tree is not marked as visible, mark it so and update the count
                    if (!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // once a tree of height 9 is seen, no further trees are visible so stop iterating
                if (highestTree == 9)
                {
                    break;
                }
            }
        }

        // Iteration 2 - iterate from top to bottom then bottom to tope for each row
        for (int col = 0; col < patch[0].Length; col++)
        {
            // detclare the highest tree as the one at the top
            int highestTree = patch[0][col] - '0';
            // start moving down the columns one tree at a time
            for (int row = 0; row < patch[0].Length; row++)
            {
                // firstly mark a tree as visible if it is on an edge
                if (row == 0 || col == 0 || row == patch[row].Length - 1 || col == patch.Length - 1)
                {
                    // if the tree is not marked as visible, mark it so and update the count
                    if (!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // compare the current tree to the highest tree. If it is higher it is visible from the top.
                if (patch[row][col] - '0' > highestTree)
                {
                    highestTree = patch[row][col] - '0';
                    // if the tree is not marked as visible, mark it so and update the count
                    if (!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // once a tree of height 9 is seen, no further trees are visible so stop iterating
                if (highestTree == 9)
                {
                    break;
                }
            }
            // declare the highest tree as the one at the bottom
            highestTree = patch[patch.Length - 1][col] - '0';
            // now move along the column from bottom to top (no need to go to the top on the way back)
            for (int row = patch.Length - 1; row > 0; row--)
            {
                // compare the current tree to the highest tree. If it is higher it is visible from the side.
                if (patch[row][col] - '0' > highestTree)
                {
                    highestTree = patch[row][col] - '0';
                    // if the tree is not marked as visible, mark it so and update the count
                    if(!treeVisible[row, col])
                    {
                        treeVisible[row, col] = true;
                        numberOfTreesVisible++;
                    }
                }
                // once a tree of height 9 is seen, no further trees are visible so stop iterating
                if (highestTree == 9)
                {
                    break;
                }
            }
        }


        // Part 2 - most scenic tree
        int scenicScore = 0;
        // for each tree on the patch
        for (int row = 0; row < patch.Length; row++)
        {
            for (int col = 0; col < patch[0].Length; col++)
            {
                int treeHeight = patch[row][col] - '0';
                // declare the number of trees visible in each direction
                int left = 0, right = 0, down = 0 , up = 0;
                // check left
                for (int l = col - 1; l >= 0; l--)
                {
                    left++;
                    if ( patch[row][l] - '0' >= treeHeight)
                    {
                        break;
                    }
                }
                // check right
                for (int r = col + 1; r < patch[0].Length ; r++)
                {
                    right++;
                    if (patch[row][r] - '0' >= treeHeight)
                    {
                        break;
                    }
                }
                // check down
                for (int d = row + 1; d < patch.Length; d++)
                {
                    down++;
                    if(patch[d][col] - '0' >= treeHeight)
                    {
                        break;
                    }
                }
                // check up
                for (int u = row - 1; u >= 0; u--)
                {
                    up++;
                    if (patch[u][col] - '0' >= treeHeight)
                    {
                        break;
                    }
                }
                // the scenic score is updated if the product of visible trees is greater than the current score
                scenicScore = ( left * right * down * up > scenicScore) ? left * right * down * up : scenicScore;
            }
        }
        sw.Stop();
        return (numberOfTreesVisible.ToString(), scenicScore.ToString());
    }
}
