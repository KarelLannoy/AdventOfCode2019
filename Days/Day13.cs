using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day13
    {
        public static int AdventOfCode2019_13_1(List<long> input)
        {
            int instructionPointer = 0;
            int relativeBase = 0;
            List<List<long>> tiles = new List<List<long>>();
            while (true)
            {
                var output = AdventOfCode2019_IntCode_Complete(input, instructionPointer, relativeBase, null);
                instructionPointer = output.Item3;
                relativeBase = output.Item4;
                if (output.Item2)
                {
                    break;
                }
                tiles.Add(output.Item1);
            }
            return tiles.Count(t => t.Last() == 2);
        }

        public static long AdventOfCode2019_13_2(List<long> input)
        {
            input[0] = 2;
            int instructionPointer = 0;
            int relativeBase = 0;
            List<List<long>> tiles = new List<List<long>>();
            long? inputParam = null;
            bool firstRun = true;
            while (true)
            {
                var output = AdventOfCode2019_IntCode_Complete(input, instructionPointer, relativeBase, inputParam);
                inputParam = new Nullable<long>();
                instructionPointer = output.Item3;
                relativeBase = output.Item4;
                if (output.Item2)
                {
                    break;
                }
                if (output.Item1 == null)
                {
                    inputParam = DisplayOutputAndAskForInput(tiles);
                    firstRun = false;
                }
                else
                {
                    if (firstRun)
                        tiles.Add(output.Item1);
                    else
                        tiles.First(t => t[0] == output.Item1[0] && t[1] == output.Item1[1])[2] = output.Item1[2];
                }
            }
            return tiles.First(t => t[0] == -1)[2];
        }

        private static long DisplayOutputAndAskForInput(List<List<long>> tiles)
        {
            //Game Mode
            //Console.Clear();
            //var maxX = tiles.Max(t => t[0]);
            //var maxY = tiles.Max(t => t[1]);

            //for (int y = 0; y <= maxY; y++)
            //{
            //    for (int x = 0; x <= maxX; x++)
            //    {
            //        var tileKind = tiles.First(t => t[0] == x && t[1] == y)[2];
            //        switch (tileKind)
            //        {
            //            case 0:
            //                Console.ForegroundColor = ConsoleColor.Black;
            //                Console.Write("O");
            //                break;
            //            case 1:
            //                Console.ForegroundColor = ConsoleColor.Red;
            //                Console.Write("O");
            //                break;
            //            case 2:
            //                Console.ForegroundColor = ConsoleColor.Green;
            //                Console.Write("O");
            //                break;
            //            case 3:
            //                Console.ForegroundColor = ConsoleColor.White;
            //                Console.Write("O");
            //                break;
            //            case 4:
            //                Console.ForegroundColor = ConsoleColor.Yellow;
            //                Console.Write("X");
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    Console.WriteLine();
            //}
            //var score = tiles.FirstOrDefault(t => t[0] == -1);
            //if (score != null)
            //{
            //    Console.ForegroundColor = ConsoleColor.White;
            //    Console.WriteLine(score[2]);
            //}
            //var input = Console.ReadLine();
            //return Convert.ToInt64(input);

            //CheatMode
            var input = CalculateOptimalPaddlePosition(tiles);
            return input;
        }

        private static long CalculateOptimalPaddlePosition(List<List<long>> tiles)
        {
            var ball = tiles.First(t => t[2] == 4);
            var paddle = tiles.First(t => t[2] == 3);
            if (ball[0] < paddle[0])
                return -1;
            if (ball[0] == paddle[0])
                return 0;
            if (ball[0] > paddle[0])
                return 1;
            return 0;
        }

        private static (List<long>, bool, int, int) AdventOfCode2019_IntCode_Complete(List<long> input, int position, int relativeBase, long? inputParam)
        {
            var outputParams = new List<long>();
            while (position < input.Count)
            {
                var optcodeInstruction = input[position].ToString("D5");
                var optcode = int.Parse(optcodeInstruction.Substring(optcodeInstruction.Length - 2));
                var paramModes = new List<MemoryMode>();
                for (int i = optcodeInstruction.Length - 3; i >= 0; i--)
                    paramModes.Add((MemoryMode)int.Parse(optcodeInstruction[i].ToString()));
                if (optcode == 99)
                    return (outputParams, true, position, relativeBase);
                int skip = 0;
                long val1;
                long val2;
                long address;
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
                        if (!inputParam.HasValue)
                            return (null, false, position, relativeBase);
                        address = IntCode_GetValue_Write(paramModes[0], position + 1, input, relativeBase);
                        IntCode_AddressCheck(address, input);
                        input[(int)address] = inputParam.Value;
                        inputParam = new Nullable<long>();
                        skip = 2;
                        break;
                    case 4:
                        val1 = IntCode_GetValue_Read(paramModes[0], position + 1, input, relativeBase);
                        outputParams.Add(val1);
                        if (outputParams.Count == 3)
                            return (outputParams, false, position + 2, relativeBase);
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
