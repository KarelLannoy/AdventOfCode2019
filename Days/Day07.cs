using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day07
    {
        public static int AdventOfCode2019_07_1(List<int> input)
        {
            var inputSequence = new List<int>() { 0, 1, 2, 3, 4 };
            var permutations = GetPermutations<int>(inputSequence, 5).ToList();
            var Outputs = new List<Tuple<List<int>, int>>();

            foreach (var sequence in permutations)
            {
                int previousIntOutput = 0;
                foreach (var intInput in sequence.ToList())
                {
                    var inputList = input.ToArray().ToList();
                    previousIntOutput = AdventOfCode2019_07_IntCode(inputList, intInput, previousIntOutput).Last();
                }
                Outputs.Add(new Tuple<List<int>, int>(sequence.ToList(), previousIntOutput));
            }

            return Outputs.OrderBy(o => o.Item2).Last().Item2;
        }

        public static int AdventOfCode2019_07_2(List<int> input)
        {
            var inputSequence = new List<int>() { 5, 6, 7, 8, 9 };
            var permutations = GetPermutations<int>(inputSequence, 5).ToList();
            var Outputs = new List<Tuple<List<int>, int>>();

            foreach (var sequence in permutations)
            {
                var done = false;
                var listOfAmps = new List<Amplifier>()
                {
                    new Amplifier(){ Name = "AMP_A", IntCode = input.ToArray().ToList(), Position = 0 },
                    new Amplifier(){ Name = "AMP_B", IntCode = input.ToArray().ToList(), Position = 0 },
                    new Amplifier(){ Name = "AMP_C", IntCode = input.ToArray().ToList(), Position = 0 },
                    new Amplifier(){ Name = "AMP_D", IntCode = input.ToArray().ToList(), Position = 0 },
                    new Amplifier(){ Name = "AMP_E", IntCode = input.ToArray().ToList(), Position = 0 },
                };

                int ampIndexer = 0;
                int previousIntOutput = 0;
                bool firstRun = true;
                while (!done)
                {
                    var amp = listOfAmps[ampIndexer];
                    (List<int>, bool, int) output;
                    if (firstRun)
                    {
                        output = AdventOfCode2019_07_2_IntCode(amp.IntCode, amp.Position, sequence.ToList()[ampIndexer], previousIntOutput);
                    }
                    else
                    {
                        output = AdventOfCode2019_07_2_IntCode(amp.IntCode, amp.Position, previousIntOutput);
                    }


                    if (output.Item2)
                    {
                        done = true;
                    }
                    else
                    {
                        previousIntOutput = output.Item1.Last();
                        amp.Outputs.Add(output.Item1.Last());
                    }
                    amp.Position = output.Item3;


                    ampIndexer++;
                    if (ampIndexer > 4)
                    {
                        ampIndexer = 0;
                        firstRun = false;
                    }
                }
                Outputs.Add(new Tuple<List<int>, int>(sequence.ToList(), previousIntOutput));
            }

            return Outputs.OrderBy(o => o.Item2).Last().Item2;
        }

        public class Amplifier
        {
            public Amplifier()
            {
                Outputs = new List<int>();
            }
            public List<int> IntCode { get; set; }
            public List<int> Outputs { get; set; }

            public int Position { get; set; }

            public string Name { get; set; }

        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private static List<int> AdventOfCode2019_07_IntCode(List<int> input, params int[] inputParam)
        {
            int paramCounter = 0;
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
                int val1 = 0;
                int val2 = 0;
                int address = 0;


                switch (optcode)
                {
                    case 1:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];

                        input[address] = val1 + val2;
                        skip = 4;
                        break;
                    case 2:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];

                        input[address] = val1 * val2;
                        skip = 4;
                        break;
                    case 3:
                        address = input[position + 1];
                        input[address] = inputParam[paramCounter];
                        paramCounter++;
                        skip = 2;
                        break;
                    case 4:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        outputParams.Add(val1);
                        skip = 2;
                        break;
                    case 5:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        if (val1 != 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 6:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        if (val1 == 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 7:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];
                        input[address] = val1 < val2 ? 1 : 0;
                        skip = 4;
                        break;
                    case 8:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];
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

        private static (List<int>, bool, int) AdventOfCode2019_07_2_IntCode(List<int> input, int position, params int[] inputParam)
        {
            int paramCounter = 0;
            var outputParams = new List<int>();
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
                int val1 = 0;
                int val2 = 0;
                int address = 0;


                switch (optcode)
                {
                    case 1:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];

                        input[address] = val1 + val2;
                        skip = 4;
                        break;
                    case 2:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];

                        input[address] = val1 * val2;
                        skip = 4;
                        break;
                    case 3:
                        address = input[position + 1];
                        input[address] = inputParam[paramCounter];
                        paramCounter++;
                        skip = 2;
                        break;
                    case 4:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        outputParams.Add(val1);
                        return (outputParams, false, position + 2);
                    case 5:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        if (val1 != 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 6:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        if (val1 == 0)
                        {
                            position = val2;
                            skip = 0;
                        }
                        else
                            skip = 3;
                        break;
                    case 7:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];
                        input[address] = val1 < val2 ? 1 : 0;
                        skip = 4;
                        break;
                    case 8:
                        val1 = paramModes[0] == MemoryMode.Position ? input[input[position + 1]] : input[position + 1];
                        val2 = paramModes[1] == MemoryMode.Position ? input[input[position + 2]] : input[position + 2];
                        address = input[position + 3];
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
