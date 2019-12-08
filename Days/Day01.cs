using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day01
    {
        public static decimal AdventOfCode2019_01(List<String> input)
        {
            List<decimal> results = new List<decimal>();
            foreach (var inputString in input)
            {
                var number = Convert.ToInt32(inputString);
                var result = Math.Floor(number / (decimal)3) - 2;
                results.Add(result);

                decimal result2 = result;
                while (result2 > 0)
                {
                    result2 = Math.Floor(result2 / (decimal)3) - 2;
                    if (result2 > 0) results.Add(result2);
                }
            }

            return results.Sum();
        }
    }
}
