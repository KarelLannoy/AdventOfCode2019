using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day04
    {
        public static int AdventOfCode2019_04_1(int begin, int end)
        {
            var result = 0;
            for (int i = begin; i <= end; i++)
            {
                if (IsPasswordValid_1(i))
                    result++;
            }
            return result;
        }

        public static int AdventOfCode2019_04_2(int begin, int end)
        {
            var result = 0;
            for (int i = begin; i <= end; i++)
            {
                if (IsPasswordValid_2(i))
                    result++;
            }
            return result;
        }

        private static bool IsPasswordValid_1(int password)
        {
            //contains identical chars
            var chars = password.ToString().ToList();
            if (chars.Count == chars.Distinct().Count())
            {
                return false;
            }
            //never decreases
            for (int i = 0; i < chars.Count - 1; i++)
            {
                if ((int)chars[i + 1] < chars[i])
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsPasswordValid_2(int password)
        {
            //Never decreases
            var chars = password.ToString().ToList();
            for (int i = 0; i < chars.Count - 1; i++)
            {
                if ((int)chars[i + 1] < chars[i])
                {
                    return false;
                }
            }
            //exactly two identical digits
            var groupedChars = chars.GroupBy(c => c);
            if (!groupedChars.Any(g => g.Count() == 2))
                return false;

            return true;
        }
    }
}
