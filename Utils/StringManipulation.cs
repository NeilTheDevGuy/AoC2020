using System;
using System.Collections.Generic;
using System.Text;

namespace AoC2020.Utils
{
    public static class StringManipulation
    {
        public static string ReplaceCharAt(this string input, int index, char newChar)
        {
            char[] chars = input.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
