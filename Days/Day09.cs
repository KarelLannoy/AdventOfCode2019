using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day09
    {
        public static long AdventOfCode2019_09_1(List<long> input)
        {
            var result = AdventOfCode2019_09_1_IntCode(input, 0, 0, 1);
            return result.Item1.First();
        }

        public static long AdventOfCode2019_09_2(List<long> input)
        {
            var result = AdventOfCode2019_09_1_IntCode(input, 0, 0, 2);
            return result.Item1.First();
        }

        private static (List<long>, bool, long) AdventOfCode2019_09_1_IntCode(List<long> input, int position, int relativeBase, params long[] inputParam)
        {
            int paramCounter = 0;
            var outputParams = new List<long>();
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
                    return (outputParams, true, 0);
                }

                int skip = 0;
                long val1 = 0;
                long val2 = 0;
                long address = 0;


                switch (optcode)
                {
                    case 1:

                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        address = IntCode_GetValue_Write(paramModes[2], position + 3, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = val1 + val2;
                        skip = 4;
                        break;
                    case 2:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        address = IntCode_GetValue_Write(paramModes[2], position + 3, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = val1 * val2;
                        skip = 4;
                        break;
                    case 3:
                        address = IntCode_GetValue_Write(paramModes[0], position + 1, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = inputParam[paramCounter];
                        paramCounter++;
                        skip = 2;
                        break;
                    case 4:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        outputParams.Add(val1);
                        skip = 2;
                        break;
                    case 5:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        if (val1 != 0)
                        {
                            position = (int)val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 6:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        if (val1 == 0)
                        {
                            position = (int)val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 7:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        address = IntCode_GetValue_Write(paramModes[2], position + 3, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = val1 < val2 ? 1 : 0;
                        skip = 4;
                        break;
                    case 8:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        val2 = IntCode_GetValue_Read(paramModes[1], position + 2, input, relativeBase);
                        address = IntCode_GetValue_Write(paramModes[2], position + 3, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = val1 == val2 ? 1 : 0;
                        skip = 4;
                        break;
                    case 9:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        relativeBase += (int)val1;
                        skip = 2;
                        break;
                    default:
                        break;
                }
                position += skip;
            }
            throw new Exception();
        }

        private static long IntCode_GetValue_Read(MemoryMode memoryMode, int position, List<long> input, int relativeBase)
        {
            IntCode_AddressCheck(position, input);

            if (memoryMode == MemoryMode.Position)
            {
                var address = (int)input[position];
                IntCode_AddressCheck(address, input);
                return input[address];
            }
            if (memoryMode == MemoryMode.Immidiate)
            {
                return input[position];
            }
            if (memoryMode == MemoryMode.Relative)
            {
                var address = relativeBase + (int)input[position];
                IntCode_AddressCheck(address, input);
                return input[address];
            }

            throw new Exception();
        }

        private static long IntCode_GetValue_Write(MemoryMode memoryMode, int position, List<long> input, int relativeBase)
        {
            IntCode_AddressCheck(position, input);

            if (memoryMode == MemoryMode.Position)
            {
                var address = (int)input[position];
                IntCode_AddressCheck(address, input);
                return address;
            }
            if (memoryMode == MemoryMode.Immidiate)
            {
                return input[position];
            }
            if (memoryMode == MemoryMode.Relative)
            {
                var address = relativeBase + (int)input[position];
                IntCode_AddressCheck(address, input);
                return address;
            }

            throw new Exception();
        }

        private static void IntCode_AddressCheck(long address, List<long> intcode)
        {
            if (intcode.Count <= (int)address)
            {
                var numberOfAddressSpaces = address - intcode.Count + 1;
                for (int i = 0; i < numberOfAddressSpaces; i++)
                {
                    intcode.Add(0);
                }
            }
        }

        private enum MemoryMode
        {
            Position = 0,
            Immidiate = 1,
            Relative = 2
        }
    }
}
