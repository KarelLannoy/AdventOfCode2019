using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day05
    {
        public static List<int> AdventOfCode2019_05_IntCode(List<int> input, int inputParam = 1)
        {
            var outputParams = new List<int>();
            var position = 0;
            while (position < input.Count)
            {
                var optcodeInstruction = input[position].ToString("D5");
                var optcode = int.Parse(optcodeInstruction.Substring(optcodeInstruction.Length - 2));
                var paramModes = new List<MemoryMode>();
                for (int i = optcodeInstruction.Length - 3; i >= 0; i--)
                {
                    paramModes.Add((MemoryMode)int.Parse(optcodeInstruction[i].ToString()));
                }

                if (optcode == 99)
                {
                    return outputParams;
                }

                int skip = 0;
                int val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                int val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                int address = input[position + 3];


                switch (optcode)
                {
                    case 1:
                        input[address] = val1 + val2;
                        skip = 4;
                        break;
                    case 2:
                        input[address] = val1 * val2;
                        skip = 4;
                        break;
                    case 3:
                        input[address] = inputParam;
                        skip = 2;
                        break;
                    case 4:
                        outputParams.Add(val1);
                        skip = 2;
                        break;
                    case 5:
                        if (val1 != 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 6:
                        if (val1 == 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 7:
                        address = input[position + 3];
                        input[address] = val1 < val2 ? 1 : 0;
                        skip = 4;
                        break;
                    case 8:
                        input[address] = val1 == val2 ? 1 : 0;
                        skip = 4;
                        break;
                    default:
                        break;
                }
                position += skip;
            }
            throw new Exception();
        }

        private enum MemoryMode
        {
            Position = 0,
            Immidiate = 1
        }
    }
}
