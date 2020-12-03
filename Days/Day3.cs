using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks.Dataflow;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day3
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(3);
            var result1 = PartOne(input);
            Console.WriteLine(result1);
            var result2 = PartTwo(input);
            Console.WriteLine(result2);
        }

        public long PartOne(string[] input)
        {
            return RunSlope(input, 3, 1);
        }

        public long PartTwo(string[] input)
        {
            var slopeResults = new List<long>();
            slopeResults.Add(RunSlope(input, 1,1));
            slopeResults.Add(RunSlope(input, 3, 1));
            slopeResults.Add(RunSlope(input, 5,1));
            slopeResults.Add(RunSlope(input, 7, 1));
            slopeResults.Add(RunSlope(input, 1, 2));
            
            long totalTrees = 1;
            foreach (var trees in slopeResults)
            {
                totalTrees *= trees;
            }

            return totalTrees;
        }

        private long RunSlope(string[] input, int xInc, int yInc)
        {
            var treeCount = 0;
            var maxX = input[0].Length - 1;
            var maxY = input.Length - 1;
            int x = 0, y = 0;
            while (y < maxY)
            {
                y += yInc;
                x += xInc;
                if (x > maxX)
                {
                    x = Math.Abs(maxX - x) - 1;
                }
                if (input[y][x] == '#') treeCount++;
            }

            return treeCount;
        }
    }
}
