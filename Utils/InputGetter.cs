using System.IO;
using System.Linq;

namespace AoC2020.Utils
{
    public static class InputGetter
    {
        public static long[] GetFromCsvAsLong(int day)
        {
            return File.ReadAllText(GetFileName(day))
            .Split(",")
            .Select(long.Parse)
            .ToArray();
    }

        public static string[] GetFromCsvAsString(int day)
        {
            return File.ReadAllText(GetFileName(day))
                .Split(",")
                .ToArray();
        }

        public static string[] GetFromLinesAsString(int day)
        {
            return File.ReadAllLines(GetFileName(day));
        }

        public static long[] GetFromLinesAsLong(int day)
        {
            var stringLines = File.ReadAllLines(GetFileName(day));
            return stringLines
                .Select(long.Parse)
                .ToArray();
        }

        private static string GetFileName(int day) => $@"Input/Day{day}.txt";
    }
}
