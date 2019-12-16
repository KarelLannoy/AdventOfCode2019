using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day15
    {
        private static List<Direction> _possibleDirections = new List<Direction>() { Direction.North, Direction.South, Direction.West, Direction.East };

        public static int AdventOfCode2019_15_1(List<long> input)
        {
            Dictionary<Point, Tuple<int, int>> grid = new Dictionary<Point, Tuple<int, int>>();
            ExploreNewPositions(new Point(0, 0), Direction.None, input, 0, 0, 0, grid);

            var oxygen = grid.First(kvp => kvp.Value.Item1 == 2);

            return oxygen.Value.Item2;
        }

        public static int AdventOfCode2019_15_2(List<long> input)
        {

            Dictionary<Point, Tuple<int, int>> grid = new Dictionary<Point, Tuple<int, int>>();
            ExploreNewPositions(new Point(0, 0), Direction.None, input, 0, 0, 0, grid);

            Dictionary<Point, Tuple<int, int>> oxygenGrid = new Dictionary<Point, Tuple<int, int>>();
            ExploreNewPositions(new Point(0, 0), Direction.None, OxygenIntCode, OxygenInstructionPointer, OxygenRelativeBase, -1, oxygenGrid);

            return oxygenGrid.Max(o => o.Value.Item2);
        }

        private static List<long> OxygenIntCode = new List<long>();
        private static int OxygenInstructionPointer = 0;
        private static int OxygenRelativeBase = 0;

        private static void ExploreNewPositions(Point newPosition, Direction previousDirection, List<long> IntCodeSequence, int instructionPointer, int relativeBase, int counter, Dictionary<Point, Tuple<int, int>> grid)
        {
            counter++;
            foreach (var direction in _possibleDirections.Where(d => d != GetOppositeDirection(previousDirection)).ToList())
            {
                var x = newPosition.X;
                var y = newPosition.Y;
                switch (direction)
                {
                    case Direction.North:
                        y++;
                        break;
                    case Direction.South:
                        y--;
                        break;
                    case Direction.West:
                        x--;
                        break;
                    case Direction.East:
                        x++;
                        break;
                    default:
                        break;
                }
                var dirCodeSequence = IntCodeSequence.ToArray().ToList();
                var output = AdventOfCode2019_IntCode_Complete(dirCodeSequence, instructionPointer, relativeBase, (long)direction);

                if (!grid.ContainsKey(new Point(x, y)))
                {
                    grid.Add(new Point(x, y), new Tuple<int, int>((int)output.Item1[0], counter));
                    if (output.Item1[0] == 1)
                        ExploreNewPositions(new Point(x, y), direction, dirCodeSequence, output.Item3, output.Item4, counter, grid);
                    if (output.Item1[0] == 2)
                    {
                        OxygenInstructionPointer = output.Item3;
                        OxygenIntCode = dirCodeSequence.ToArray().ToList();
                        OxygenRelativeBase = output.Item4;
                    }
                }

            }
        }

        private static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.South:
                    return Direction.North;
                case Direction.West:
                    return Direction.East;
                case Direction.East:
                    return Direction.West;
                case Direction.None:
                    return Direction.None;
            }
            throw new Exception();
        }

        private enum Direction
        {
            None = 0,
            North = 1,
            South = 2,
            West = 3,
            East = 4
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
                        //if (outputParams.Count == 3)
                        return (outputParams, false, position + 2, relativeBase);
                    //skip = 2;
                    //break;
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
