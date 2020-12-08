using System;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day8
    {
        private int _lastIndex;
        public void Run()
        {
            var input = InputGetter.GetFromLinesAsString(8);
            var result1 = PartOne(input);
            Console.WriteLine(result1);
            var result2 = PartTwo(input);
            Console.WriteLine(result2);
        }

        private int PartOne(string[] input)
        {
            var acc = 0;
            var ptr = 0;
            var instructions = input.Select(i => new Instruction {InstructionValue = i}).ToList();
            while (true)
            {
                var thisIns = instructions[ptr];
                if (ptr >= instructions.Count)
                {
                    return acc;
                }
                if (thisIns.Processed)
                {
                    return acc;
                }

                thisIns.Processed = true;
                var insValue = int.Parse(thisIns.InstructionValue.Substring(4));
                switch (thisIns.InstructionValue.Substring(0, 3))
                {
                    case "nop":
                        ptr++;
                        break;
                    case "acc":
                        acc += insValue;
                        ptr++;
                        break;
                    case "jmp":
                        ptr += insValue;
                        break;
                }
            }
        }

        private int PartTwo(string[] input)
        {
            while (true)
            {
                var acc = 0;
                var ptr = 0;
                var instructions = GetNextAttempt(input);
                while (true)
                {
                    if (ptr >= instructions.Count)
                    {
                        return acc;
                    }

                    var thisIns = instructions[ptr];

                    if (thisIns.Processed)
                    {
                        break; //Infinite loop detected
                    }

                    thisIns.Processed = true;
                    var insValue = int.Parse(thisIns.InstructionValue.Substring(4));
                    switch (thisIns.InstructionValue.Substring(0, 3))
                    {
                        case "nop":
                            ptr++;
                            break;
                        case "acc":
                            acc += insValue;
                            ptr++;
                            break;
                        case "jmp":
                            ptr += insValue;
                            break;
                    }
                }
            }
        }

        private List<Instruction> GetNextAttempt(string[] input)
        {
            _lastIndex++;
            var instructions = input.Select(i => new Instruction { InstructionValue = i }).ToList();
            var remaining = instructions.TakeLast(instructions.Count - _lastIndex);
            var nextToAlter = remaining.First(r =>
                r.InstructionValue.StartsWith("nop") || r.InstructionValue.StartsWith("jmp"));
            if (nextToAlter.InstructionValue.StartsWith("nop"))
            {
                nextToAlter.InstructionValue = nextToAlter.InstructionValue.Replace("nop", "jmp");
            }

            if (nextToAlter.InstructionValue.StartsWith("jmp"))
            {
                nextToAlter.InstructionValue = nextToAlter.InstructionValue.Replace("jmp", "nop");
            }

            _lastIndex = instructions.IndexOf(nextToAlter);
            return instructions;
        } 

        private class Instruction
        {
            public string InstructionValue;
            public bool Processed;
        }
        
    }
}
