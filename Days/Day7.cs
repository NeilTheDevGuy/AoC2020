using System;
using System.Collections.Generic;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day7
    {
        Dictionary<string, List<BagRule>> _bagsDict = new Dictionary<string, List<BagRule>>();

        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(7);
            PopulateBagRules(input);
            var result1 = PartOne();
            Console.WriteLine(result1);
            var result2 = PartTwo();
            Console.WriteLine(result2);
        }

        private int PartOne()
        {
            var directBags = new List<string>();
            foreach (var bag in _bagsDict)
            {
                foreach (var rule in bag.Value)
                {
                    if (rule.Colour == "shiny gold")
                    {
                        directBags.Add(bag.Key);
                    }
                }
            }

            var bags = GetBags(directBags, new HashSet<string>());
            return bags.Count;
        }

        private int PartTwo()
        {
            var firstBags = _bagsDict["shiny gold"];
            var bagList = new List<string>();
            foreach (var bag in firstBags)
            {
                bagList.Add(bag.Colour);
            }

            var bags = GetBags2(new List<string>{"shiny gold"}, 0);
            return bags;
        }

        private void PopulateBagRules(string[] input)
        {
            foreach (var line in input)
            {
                var bagCol = line.Split("bags")[0].Trim();
                var requires = line.Split("contain")[1].Split(",");
                _bagsDict.Add(bagCol, new List<BagRule>());
                foreach (var requiredBag in requires)
                {
                    if (!requiredBag.Contains("no other bags"))
                    {
                        var number = int.Parse(requiredBag.Trim().Substring(0, 1));
                        var colour = requiredBag.Replace("bags", "").Replace("bag", "").Replace(".", "").Trim().Substring(1).Trim();
                        _bagsDict[bagCol].Add(new BagRule { Colour = colour, Number = number });
                    }
                }
            }
        }

        private HashSet<string> GetBags(List<string> bagsToCheck, HashSet<string> bagCols)
        {
            foreach (var bagColour in bagsToCheck)
            {
                foreach (var holdingBag in _bagsDict)
                {
                    foreach (var rule in holdingBag.Value)
                    {
                        if (rule.Colour == bagColour)
                        {
                            bagCols = GetBags(new List<string>{holdingBag.Key}, bagCols);
                        }
                    } 
                }
                bagCols.Add(bagColour);
            }

            return bagCols;
        }

        private int GetBags2(List<string> bagsToCheck, int bagCount)
        {
            foreach (var bagColour in bagsToCheck)
            {
                var containingBag = _bagsDict[bagColour];
                foreach (var thisBag in containingBag)
                {
                    for (int i = 0; i < thisBag.Number; i++)
                    {
                        bagCount = GetBags2(new List<string> {thisBag.Colour}, bagCount);
                        bagCount++;
                    }
                }

            }
            return bagCount;
        }

        private class BagRule
        {
            public string Colour;
            public int Number;
        }
    }
}
