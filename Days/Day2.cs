using System;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day2
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(2);
            var result1 = PartOne(input);
            Console.WriteLine(result1);
            var result2 = PartTwo(input);
            Console.WriteLine(result2);
        }

        public int PartOne(string[] input)
        {
            var matches = 0;
            foreach (var line in input)
            {
                var splitLine = line.Split(":");
                var password = splitLine[1].Trim();
                var requirements = splitLine[0].Split(" ");
                var requiredCharacter = requirements[1][0];
                var requiredOccurances = requirements[0].Split("-");
                var minRequired = int.Parse(requiredOccurances[0]);
                var maxRequired = int.Parse(requiredOccurances[1]);
                var occuranceCount = password.Count(x => x == requiredCharacter);
                
                if (occuranceCount >= minRequired && occuranceCount <= maxRequired)
                {
                    matches++;
                }
            }

            return matches;
        }

        public int PartTwo(string[] input)
        {
            var matches = 0;
            foreach (var line in input)
            {
                var splitLine = line.Split(":");
                var password = splitLine[1].Trim();
                var requirements = splitLine[0].Split(" ");
                var requiredCharacter = requirements[1].ToCharArray()[0];
                var requiredOccurances = requirements[0].Split("-");
                var char1Pos = int.Parse(requiredOccurances[0]);
                var char2Pos = int.Parse(requiredOccurances[1]);

                var char1Matches = false;
                var char2Matches = false;
                
                char1Matches = password[char1Pos - 1] == requiredCharacter;
                char2Matches = password[char2Pos - 1] == requiredCharacter;

                if ((char1Matches && !char2Matches) || (!char1Matches && char2Matches))
                {
                    matches++;
                }
            }

            return matches;
        }
    }
}
