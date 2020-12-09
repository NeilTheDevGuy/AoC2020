using System;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day9
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsLong(9);
            var result1 = PartOne(input, 25);
            Console.WriteLine(result1);
            var (min, max) = PartTwo(input, result1);
            Console.WriteLine($"Min: {min}, Max:{max}, Total:{min + max}");
        }

        private long PartOne(long[] input, int preAmbleLength)
        {
            var ptr = preAmbleLength;
            foreach (var number in input.Skip(preAmbleLength))
            {
                var gotMatch = false;
                var preAmble = input.Skip(ptr - preAmbleLength).Take(preAmbleLength).ToArray();
                ptr++;
                for (long i  = 0; i < preAmble.Length; i++)
                {
                    for (long j = 0; j < preAmble.Length; j++)
                    {
                        if (preAmble[i] != preAmble[j] && preAmble[i] + preAmble[j] == number)
                        {
                            gotMatch = true;
                        }
                    }
                }

                if (!gotMatch)
                {
                    return number;
                }
            }
            return 0;
        }

        private (long,long) PartTwo(long[] input, long number)
        {
            var start = 0;
            var counter = 1;
            while (true)
            {
                while (counter < input.Length)
                {
                    var thisSet = input.Skip(start).Take(counter);
                    var sum = thisSet.Sum(t => t);
                    if (sum == number)
                    {
                        return (thisSet.Min(), thisSet.Max());
                    }

                    if (sum > number)
                    {
                        break;
                    }

                    counter++;
                }
                start++;
                counter = 0;
            }
            return (0,0);
        }
    }
}
