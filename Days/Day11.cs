using System;
using System.Collections.Generic;
using System.Linq;
using AoC2020.Utils;

namespace AoC2020.Days
{
    public class Day11
    {
        private string[] _seatMap;
        public void Run()
        {
            _seatMap = InputGetter.GetFromLinesAsString(11);
            var result1 = PartOne();
            Console.WriteLine(result1);
            _seatMap = InputGetter.GetFromLinesAsString(11);
            var result2 = PartTwo();
            Console.WriteLine(result2);
        }

        private int PartOne()
        {
            var maxWidth = _seatMap[0].Length;
            var maxDepth = _seatMap.Length;
            while (true)
            {
                var newSeatMap = (string[])_seatMap.Clone();
                for (var y = 0; y < _seatMap.Length; y++)
                {
                    var line = _seatMap[y];
                    for (var x = 0; x < line.Length; x++)
                    {
                        var nextSeatState = GetNextSeatState(x, y, maxWidth, maxDepth);
                        if (nextSeatState != line[x])
                        {
                            newSeatMap[y] = newSeatMap[y].ReplaceCharAt(x, nextSeatState);
                        }
                    }
                }
                var oldSeatCount = _seatMap.Sum(s => s.Count(a => a == '#'));
                var newSeatCount = newSeatMap.Sum(s => s.Count(a => a == '#'));
                if (newSeatCount == oldSeatCount)
                {
                    return newSeatCount;
                }

                _seatMap = newSeatMap;
            }
        }

        private char GetNextSeatState(int x, int y, int maxWidth, int maxDepth)
        {
            var closeSeats = new List<char>();
            if (IsValidPos(y - 1 , x - 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y - 1][x -1]);
            if (IsValidPos(y - 1, x + 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y - 1][x + 1]);
            if (IsValidPos(y - 1, x, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y - 1][x]);
            if (IsValidPos(y, x - 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y][x - 1]);
            if (IsValidPos(y, x + 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y][x + 1]);
            if (IsValidPos(y + 1, x - 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y + 1][x - 1]);
            if (IsValidPos(y + 1, x + 1, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y + 1][x + 1]);
            if (IsValidPos(y + 1, x, maxWidth, maxDepth)) closeSeats.Add(_seatMap[y + 1][x]);

            if (_seatMap[y][x] == 'L' && !closeSeats.Contains('#')) return '#';
            if (_seatMap[y][x] == '#' && closeSeats.Count(s => s == '#') >= 4) return 'L';
            return _seatMap[y][x];
        }

        private int PartTwo()
        {
            var maxWidth = _seatMap[0].Length;
            var maxDepth = _seatMap.Length;
            while (true)
            {
                var newSeatMap = (string[])_seatMap.Clone();
                for (var y = 0; y < _seatMap.Length; y++)
                {
                    var line = _seatMap[y];
                    for (var x = 0; x < line.Length; x++)
                    {
                        var nextSeatState = GetNextSeatState2(x, y, maxWidth, maxDepth);
                        if (nextSeatState != line[x])
                        {
                            newSeatMap[y] = newSeatMap[y].ReplaceCharAt(x, nextSeatState);
                        }
                    }
                }
                var oldSeatCount = _seatMap.Sum(s => s.Count(a => a == '#'));
                var newSeatCount = newSeatMap.Sum(s => s.Count(a => a == '#'));
                if (newSeatCount == oldSeatCount)
                {
                    return newSeatCount;
                }

                _seatMap = newSeatMap;
            }
        }

        private char GetNextSeatState2(int x, int y, int maxWidth, int maxDepth)
        {
            //MESSY!!! Must be a much better way of doing this
            var visibleSeats = new List<char>();
            var thisX = x;
            var thisY = y;

            while (IsValidPos(--thisY, thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(--thisY, --thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(--thisY, ++thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(++thisY, thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(++thisY, --thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(++thisY, ++thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(thisY, --thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            thisX = x;
            thisY = y;
            while (IsValidPos(thisY, ++thisX, maxWidth, maxDepth))
            {
                if (_seatMap[thisY][thisX] != '.')
                {
                    visibleSeats.Add(_seatMap[thisY][thisX]);
                    break;
                }
            }
            
            if (_seatMap[y][x] == 'L' && !visibleSeats.Contains('#')) return '#';
            if (_seatMap[y][x] == '#' && visibleSeats.Count(s => s == '#') >= 5) return 'L';
            return _seatMap[y][x];
        }

        private bool IsValidPos(int thisY, int thisX, int maxWidth, int maxDepth)
        {
            return thisX >= 0 && thisX < maxWidth
                              && thisY >= 0 && thisY < maxDepth;
        }
    }
}
