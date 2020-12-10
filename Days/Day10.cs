using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day10
    {

        private Dictionary<long, long> attempts = new Dictionary<long, long>();

        public void Run()
        {
            var input = InputGetter.GetFromLinesAsLong(10);
            var sortedInput = input.ToList();
            sortedInput.Sort();
            sortedInput.Add(sortedInput.Max() + 3); //device
            sortedInput.Insert(0,0); //charging port
            var result1 = PartOne(sortedInput);
            Console.WriteLine(result1);
            var result2 = PartTwo(sortedInput);
            Console.WriteLine(result2);
        }


        private long PartOne(List<long> input)
        {
            var dict = new Dictionary<long, long>();

            long currentJoltage = 0;
            long maxJoltage = input.Max();
            long deviceJoltage = maxJoltage + 3;
            
            for (int i = 1; i <= input.Count; i++)
            {
                var candidateAdapter = input[i];
                var difference = candidateAdapter - currentJoltage;
                if (dict.ContainsKey(difference))
                {
                    dict[difference]++;
                }
                else
                {
                    dict.Add(difference, 1);
                }
                currentJoltage += difference;
                if (deviceJoltage - currentJoltage == 3)
                {
                    return dict[1] * dict[3];
                }
            }

            return 0;
        }

        private long PartTwo(List<long> input)
        {
            attempts[input.Count - 1] = 0;
            attempts[input.Count - 2] = 1;
            var result = FindAdapterPath(0, input);
            return attempts[0];
        }

        private long FindAdapterPath(int startPoint, List<long> input)
        {
            long counter = 0;

            if (attempts.ContainsKey(startPoint))
            {
                return attempts[startPoint];
            }

            for (var i = 1; i <= 3; i++)
            {
                if (startPoint + i < input.Count && input[startPoint + i] - input[startPoint] <= 3)
                {
                    counter += FindAdapterPath(startPoint + i, input);
                }
            }

            attempts[startPoint] = counter;
            return counter;
        }
    }
}
