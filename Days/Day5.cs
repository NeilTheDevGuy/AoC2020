using System;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day5
    {
        public void Run()
        { 
            var input = InputGetter.GetFromLinesAsString(5);
            var seatMap = GetSeatMap(input);
            var result1 = PartOne(seatMap);
            Console.WriteLine(result1);
            var result2 = PartTwo(seatMap);
            Console.WriteLine(result2);
        }

        private int PartOne(List<int> seatMap)
        {
            return seatMap.Max();
        }

        private int PartTwo(List<int> seatMap)
        {
            var orderedMap = seatMap.OrderBy(o => o).ToArray();
            for (int i = 1; i < orderedMap.Length - 1; i++)
            {
                if (orderedMap[i + 1] - orderedMap[i - 1] != 2)
                {
                    return orderedMap[i] + 1;
                }
            }
            return 0;
        }

        private List<int> GetSeatMap(string[] input)
        {
            var seatIds = new List<int>();
            foreach (var line in input)
            {
                var minRow = 0;
                var maxRow = 127;
                var minCol = 0;
                var maxCol = 7;
                var finalRow = 0;
                var finalCol = 0;
                for (var i = 0; i < line.Length; i++)
                {
                    var letter = line[i];
                    if (letter == 'F' || letter == 'B')
                    {
                        var rounding = letter == 'F' ? MidpointRounding.ToZero : MidpointRounding.AwayFromZero;
                        if (line[i + 1] == 'L' || line[i + 1] == 'R')
                        {
                            if (letter == 'F') finalRow = minRow;
                            if (letter == 'B') finalRow = maxRow;
                        }
                        else
                        {
                            (minRow, maxRow) = Parse(letter, minRow, maxRow, rounding);
                        }

                    }
                    if (letter == 'L' || letter == 'R')
                    {
                        var rounding = letter == 'L' ? MidpointRounding.ToZero : MidpointRounding.AwayFromZero;
                        if (i == line.Length - 1)
                        {
                            if (letter == 'L') finalCol = minCol;
                            if (letter == 'R') finalCol = maxCol;
                        }
                        else
                        {
                            (minCol, maxCol) = Parse(letter, minCol, maxCol, rounding);
                        }
                    }
                }
                var id = (finalRow * 8) + finalCol;
                seatIds.Add(id);
            }

            return seatIds;
        }

        private (int, int) Parse(char letter, int min, int max, MidpointRounding rounding)
        {
            var midPoint = GetMidpoint(min, max, rounding);
            switch (letter)
            {
                case 'F':
                case 'L':
                {
                    return (min, midPoint);
                }
                case 'B':
                case 'R':
                {
                    return (midPoint, max);
                }
            }
            return (0,0);
        }

        private int GetMidpoint(decimal from, decimal to, MidpointRounding rounding)
        {
            var total = from + to;
            decimal midPoint = total / 2;
            return (int)decimal.Round(midPoint, rounding);
        }
    }
}
