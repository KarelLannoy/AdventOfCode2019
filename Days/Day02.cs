using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day02
    {
        public static int AdventOfCode2019_02(List<int> input)
        {
            int result = 0;
            int noun = 0;
            int verb = 0;
            while (verb < 100)
            {
                noun = 0;
                while (noun < 100)
                {
                    var newArray = input.ToArray();
                    var resultList = AdventOfCode2019_02_Recursive(newArray.ToList(), noun, verb);
                    result = resultList[0];
                    if (result == 19690720)
                    {
                        return (100 * noun) + verb;
                    }
                    noun++;
                }
                verb++;
            }
            throw new Exception();
        }

        private static List<int> AdventOfCode2019_02_Recursive(List<int> input, int noun, int verb)
        {
            input[1] = noun;
            input[2] = verb;

            var position = 0;
            while (position < input.Count)
            {
                var action = input[position];

                if (action == 99)
                {
                    return input;
                }

                var val1 = input[position + 1];
                var val2 = input[position + 2];
                var address = input[position + 3];

                switch (action)
                {
                    case 1:
                        input[address] = input[val1] + input[val2];
                        break;
                    case 2:
                        input[address] = input[val1] * input[val2];
                        break;
                    default:
                        break;
                }

                position += 4;
            }

            throw new Exception();
        }
    }
}
