using System;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day1
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsLong(1);
            var result1 = PartOne(input);
            Console.WriteLine(result1);
            var result2 = PartTwo(input);
            Console.WriteLine(result2);
        }

        private long PartOne(long[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input.Length; j++)
                {
                    var sum = input[i] + input[j];
                    if (sum == 2020)
                    {
                        return input[i] * input[j];
                    }
                }
            }
            return 0;
        }

        private long PartTwo(long[] input)
        {
            for (var i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input.Length; j++)
                {
                    for (var k = 0; k < input.Length; k++)
                    {
                        var sum = input[i] + input[j] + input[k];
                        if (sum == 2020)
                        {
                            return input[i] * input[j] * input[k];
                        }
                    }
                }
            }
            return 0;
        }
    }
}
