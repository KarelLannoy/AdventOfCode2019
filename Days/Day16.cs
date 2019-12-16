using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day16
    {
        public static string AdventOfCode2019_16_1(string input)
        {
            List<int> inputList = input.ToList().Select(i => int.Parse(i.ToString())).ToList();
            List<int> pattern = new List<int>() { 0, 1, 0, -1 };
            List<int> outputList = inputList.ToArray().ToList();
            for (int i = 0; i < 100; i++)
            {
                outputList = FFT(outputList, pattern);
            }
            return outputList.Take(8).Select(o => o.ToString()).Aggregate((a, x) => a += x);
        }

        public static string AdventOfCode2019_16_2(string strings)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 10000; i++)
            {
                stringBuilder.Append(strings);
            }
            strings = stringBuilder.ToString();
            char[] charArray = strings.ToArray();
            int off = int.Parse(strings.Substring(0, 7));
            int numberOfInputs = strings.Count();
            int[] inputInts = new int[numberOfInputs];
            for (int i = 0; i < numberOfInputs; i++)
            {
                inputInts[i] = charArray[i] - 48;
            }
            for (int phase = 0; phase < 100; phase++)
            {
                for (int repeat = numberOfInputs - 2; repeat > off - 5; repeat--)
                {
                    int number = inputInts[repeat + 1] + inputInts[repeat];
                    inputInts[repeat] = Math.Abs(number % 10);
                }
            }
            StringBuilder str = new StringBuilder();
            for (int i = off; i < off + 8; i++)
            {
                str.Append(inputInts[i]);
            }
            return str.ToString();
        }


        private static List<int> FFT(List<int> inputList, List<int> pattern)
        {
            var output = new List<int>();
            for (int i = 0; i < inputList.Count; i++)
            {
                int patternIndex = 0;
                var implementedPattern = GetPatternToImplement(pattern, i);
                List<int> resultList = new List<int>();
                foreach (var inputItem in inputList)
                {
                    var result = inputItem * implementedPattern[patternIndex];
                    resultList.Add(result);
                    patternIndex++;
                    if (patternIndex == implementedPattern.Count)
                    {
                        patternIndex = 0;
                    }
                }
                var resultNumber = resultList.Aggregate((a, x) => a + x);
                output.Add(int.Parse(resultNumber.ToString().Last().ToString()));
            }
            return output;
        }

        private static List<int> GetPatternToImplement(List<int> pattern, int index)
        {
            var stepNr = index + 1;
            List<int> newPattern = new List<int>();
            foreach (var patternItem in pattern)
            {
                for (int i = 0; i < stepNr; i++)
                {
                    newPattern.Add(patternItem);
                }
            }
            var value = newPattern[0];
            newPattern.RemoveAt(0);
            newPattern.Add(value);
            return newPattern;
        }
    }
}
