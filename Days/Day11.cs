using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day11
    {
        public static int AdventOfCode2019_11_1(List<long> intCode)
        {
            Dictionary<Point, int> grid = new Dictionary<Point, int>();

            bool finished = false;
            var xPos = 0;
            var yPos = 0;
            int instructionPointer = 0;
            var relativeBase = 0;
            var direction = "up";
            bool start = true;

            while (!finished)
            {
                var color = grid.Keys.Contains(new Point(xPos, yPos)) ? grid[new Point(xPos, yPos)] : start ? 1 : 0;
                start = false;
                var output = AdventOfCode2019_IntCode_Complete(intCode, instructionPointer, relativeBase, color);

                instructionPointer = output.Item3;
                relativeBase = output.Item4;

                if (output.Item2)
                {
                    finished = true;
                    break;
                }

                if (!grid.Keys.Contains(new Point(xPos, yPos)))
                {
                    grid.Add(new Point(xPos, yPos), (int)output.Item1[0]);
                }
                else
                {
                    grid[new Point(xPos, yPos)] = (int)output.Item1[0];
                }

                direction = GetNextDirection(direction, (int)output.Item1[1]);
                switch (direction)
                {
                    case "up":
                        yPos--;
                        break;
                    case "left":
                        xPos--;
                        break;
                    case "down":
                        yPos++;
                        break;
                    case "right":
                        xPos++;
                        break;
                }
            }

            return grid.Count;

        }

        public static void AdventOfCode2019_11_2(List<long> intCode)
        {
            Dictionary<Point, int> grid = new Dictionary<Point, int>();

            bool finished = false;
            var xPos = 0;
            var yPos = 0;
            int instructionPointer = 0;
            var relativeBase = 0;
            var direction = "up";
            bool start = true;

            while (!finished)
            {
                var color = grid.Keys.Contains(new Point(xPos, yPos)) ? grid[new Point(xPos, yPos)] : start ? 1 : 0;
                start = false;
                var output = AdventOfCode2019_IntCode_Complete(intCode, instructionPointer, relativeBase, color);

                instructionPointer = output.Item3;
                relativeBase = output.Item4;

                if (output.Item2)
                {
                    finished = true;
                    break;
                }

                if (!grid.Keys.Contains(new Point(xPos, yPos)))
                {
                    grid.Add(new Point(xPos, yPos), (int)output.Item1[0]);
                }
                else
                {
                    grid[new Point(xPos, yPos)] = (int)output.Item1[0];
                }

                direction = GetNextDirection(direction, (int)output.Item1[1]);
                switch (direction)
                {
                    case "up":
                        yPos--;
                        break;
                    case "left":
                        xPos--;
                        break;
                    case "down":
                        yPos++;
                        break;
                    case "right":
                        xPos++;
                        break;
                }
            }
            var gridKeys = grid.Keys.ToList();

            var minX = gridKeys.Select(p => p.X).Min();
            var minY = gridKeys.Select(p => p.Y).Min();
            var maxX = gridKeys.Select(p => p.X).Max();
            var maxY = gridKeys.Select(p => p.Y).Max();

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    int color = 0;
                    if (gridKeys.Contains(new Point(x, y)))
                    {
                        color = grid[new Point(x, y)];
                    }

                    switch (color)
                    {
                        case 0:
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }
                    Console.Write("O");
                }
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
            }


        }

        private static string GetNextDirection(string direction, int turnCode)
        {
            if (turnCode == 0)
            {
                switch (direction)
                {
                    case "up":
                        return "left";
                    case "left":
                        return "down";
                    case "down":
                        return "right";
                    case "right":
                        return "up";
                }
            }
            else
            {
                switch (direction)
                {
                    case "up":
                        return "right";
                    case "left":
                        return "up";
                    case "down":
                        return "left";
                    case "right":
                        return "down";
                }
            }
            return "what?";
        }

        private static (List<long>, bool, int, int) AdventOfCode2019_IntCode_Complete(List<long> input, int position, int relativeBase, params long[] inputParam)
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
                    return (outputParams, true, position, relativeBase);
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
                        if (outputParams.Count == 2)
                        {
                            return (outputParams, false, position + 2, relativeBase);
                        }
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
