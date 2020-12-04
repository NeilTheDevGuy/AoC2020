using System;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day4
    {
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(4);
            var (result1, result2) = PartsOneAndTwo(input);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }

        private (int,int) PartsOneAndTwo(string[] input)
        {
            var validPassports = 0;
            var validPassportsWithData = 0;
            var currentLine = 0;
            while (currentLine < input.Length)
            {
                var passportDict = new Dictionary<string, string>();
                while (currentLine < input.Length && !string.IsNullOrWhiteSpace(input[currentLine]))
                {
                    var line = input[currentLine].Split(" ");
                    foreach (var item in line)
                    {
                        var kv = item.Split(":");
                        passportDict.Add(kv[0], kv[1]);
                    }
                    currentLine++;
                }
                currentLine++;
                var isValid = IsValidPassport(passportDict, true);
                var isValidPassportWithData = IsValidPassportWithData(passportDict, true);
                if (isValid) validPassports++;
                if (isValidPassportWithData) validPassportsWithData++;
            }
            return (validPassports, validPassportsWithData);
        }

        private bool IsValidPassport(Dictionary<string, string> passportData, bool ignoreCid)
        {
            var requiredFields = new List<string> {"byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};
            if (!ignoreCid)
            {
                requiredFields.Add("cid");
            }

            return requiredFields.All(passportData.ContainsKey);
        }

        private bool IsValidPassportWithData(Dictionary<string, string> passportData, bool ignoreCid)
        {
            var requiredFields = new List<string> { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            if (!ignoreCid)
            {
                requiredFields.Add("cid");
            }

            //If not all fields are present then don't use
            if (!requiredFields.All(passportData.ContainsKey)) return false;

            foreach (var field in passportData)
            {
                var isValid = field.Key switch
                {
                    "byr" => int.Parse(field.Value) >= 1920 && int.Parse(field.Value) <= 2002,
                    "iyr" => int.Parse(field.Value) >= 2010 && int.Parse(field.Value) <= 2020,
                    "eyr" => int.Parse(field.Value) >= 2020 && int.Parse(field.Value) <= 2030,
                    "hgt" => CheckHeight(field.Value),
                    "hcl" => CheckHairColour(field.Value),
                    "ecl" => (new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }).Contains(field.Value),
                    "pid" => int.TryParse(field.Value, out _) && field.Value.Length == 9,
                    _ => true
                };
                if (!isValid)
                    return false;
            }

            return true;
        }

        private bool CheckHairColour(string colour)
        {
            if (colour[0] != '#') return false;
            if (colour.Length > 7) return false;
            var allowedChars = new [] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            foreach (var c in colour.Substring(1))
            {
                if (!allowedChars.Contains(c))
                    return false;
            }

            return true;
        }

        private bool CheckHeight(string height)
        {
            var units = height.Substring(height.Length - 2);
            if (!(units == "cm" || units == "in"))
                return false;

            var number = height.Replace("cm", "").Replace("in", "");
            if (!int.TryParse(number, out _))
                return false;

            switch (units)
            {
                case "cm" when int.Parse(number) < 150 || int.Parse(number) > 193:
                case "in" when int.Parse(number) < 59 || int.Parse(number) > 76:
                    return false;
                default:
                    return true;
            }
        }
    }
}
