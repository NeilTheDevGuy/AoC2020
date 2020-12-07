using System;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day6
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(6);
            var result1 = PartOne(input);
            Console.WriteLine(result1);
            var result2 = PartTwo(input);
            Console.WriteLine(result2);
        }

        private long PartOne(string[] input)
        {
            var total = 0;
            var answers = new HashSet<char>();
            for (var i = 0; i < input.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(input[i]))
                {
                    foreach (var answer in input[i])
                    {
                        answers.Add(answer);
                    }
                }
                if (string.IsNullOrWhiteSpace(input[i]) || i == input.Length -1)
                {
                    total += answers.Count;
                    answers.Clear();
                }
            }
            return total;
        }

        private long PartTwo(string[] input)
        {
            var groups = new List<Group>();
            var group = new Group();
            for (var i = 0; i < input.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(input[i]))
                {
                    group.GroupSize++;
                    foreach (var answer in input[i])
                    {
                        if (group.Answers.ContainsKey(answer))
                        {
                            group.Answers[answer]++;
                        }
                        else
                        {
                            group.Answers.Add(answer, 1);
                        }
                    }
                }
                if (string.IsNullOrWhiteSpace(input[i]) || i == input.Length - 1)
                {
                    groups.Add(group);
                    group = new Group();
                }
            }

            var total = 0;
            foreach (var gr in groups)
            {
                var multiAnswers = gr.Answers.Where(a => a.Value == gr.GroupSize);
                var thisTotal = multiAnswers.Count();
                total += thisTotal;
            }

            return total;
        }

        private class Group
        {
            public int GroupSize;
            public Dictionary<char,int> Answers = new Dictionary<char, int>();
        }
    }
}
