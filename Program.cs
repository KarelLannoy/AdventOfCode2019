using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {

            // Round 01
            //List<string> input = GetInputLines("input01.txt");
            //var result = AdventOfCode2019_01(input);
            //Console.WriteLine(result);


            // Round 02
            //List<int> input = GetInputCommaSeperated<int>("input02.txt");
            //var result = AdventOfCode2019_02(input);
            //Console.WriteLine(result);


            // Round 03
            //List<List<string>> input = GetInputCommaSeperatedStringByLine("input03.txt");
            //var result1 = AdventOfCode2019_03_1(input);
            //var result2 = AdventOfCode2019_03_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 04
            //var result1 = AdventOfCode2019_04_1(264360, 746325);
            //var result2 = AdventOfCode2019_04_2(264360, 746325);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 05
            //List<int> input = GetInputCommaSeperated<int>("input05.txt");
            //var result = AdventOfCode2019_05_IntCode(input, 5);
            //Console.WriteLine(result.Last());


            // Round 06
            //var input = GetInputLines("input06.txt");
            //var result1 = AdventOfCode2019_06_1(input);
            //var result2 = AdventOfCode2019_06_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 7
            //var input = GetInputCommaSeperated<int>("Input07.txt");
            //var result1 = AdventOfCode2019_07_2(input);
            //Console.WriteLine(result1);

            // Round 08
            var input = GetInputText("Input08.txt");
            var result1 = AdventOfCode2019_08_1(input);
            var result2 = AdventOfCode2019_08_2(input);
            Console.WriteLine(result1);
            foreach (var line in result2)
            {
                Console.WriteLine();
                foreach (var pixel in line)
                {
                    if (pixel == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("O");
                    }
                    if (pixel == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("O");
                    }
                }
            }

            Console.ReadLine();
        }


        #region ROUND 01
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

        #endregion


        #region ROUND 02
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

        #endregion


        #region ROUND 03

        private static int AdventOfCode2019_03_1(List<List<string>> input)
        {
            var line1 = PlotLine(input[0]);
            var line2 = PlotLine(input[1]);
            line1.Remove(new Point());
            line2.Remove(new Point());

            var set1 = new HashSet<Point>(line1);
            var set2 = new HashSet<Point>(line2);
            set1.IntersectWith(set2);
            var minvalue = set1.Min(p => Math.Abs(p.X) + Math.Abs(p.Y));
            return minvalue;
        }

        private static int AdventOfCode2019_03_2(List<List<string>> input)
        {
            var line1 = PlotLine(input[0]);
            var line2 = PlotLine(input[1]);
            line1.Remove(new Point());
            line2.Remove(new Point());

            var set1 = new HashSet<Point>(line1);
            var set2 = new HashSet<Point>(line2);
            set1.IntersectWith(set2);

            List<int> steps = new List<int>();
            foreach (var intersection in set1)
            {
                steps.Add(line1.IndexOf(intersection) + 1 + line2.IndexOf(intersection) + 1);
            }

            var minvalue = steps.Min();
            return minvalue;
        }

        private static List<Point> PlotLine(List<string> line)
        {
            List<Point> plottedLine = new List<Point>();
            Point begin = new Point();
            plottedLine.Add(begin);
            foreach (var point in line)
            {
                Point previousPoint = plottedLine.Last();

                var operation = point[0];
                var movement = int.Parse(point.Substring(1));

                for (int i = 1; i <= movement; i++)
                {
                    Point newPoint = new Point() { X = previousPoint.X, Y = previousPoint.Y };
                    switch (operation)
                    {
                        case 'U':
                            newPoint.Y += i;
                            break;
                        case 'R':
                            newPoint.X += i;
                            break;
                        case 'D':
                            newPoint.Y -= i;
                            break;
                        case 'L':
                            newPoint.X -= i;
                            break;
                    }
                    plottedLine.Add(newPoint);
                }

            }
            return plottedLine;
        }

        public class Point : IEquatable<Point>
        {
            public int X { get; set; }
            public int Y { get; set; }
            public override bool Equals(object obj)
            {
                if (obj is Point)
                {
                    var point = obj as Point;
                    return point.X == this.X && point.Y == this.Y;
                }
                return false;
            }
            public bool Equals([AllowNull] Point other)
            {

                return other != null && other.X == this.X && other.Y == this.Y;
            }
            public override int GetHashCode()
            {
                return this.ToString().GetHashCode();
            }
            public override string ToString()
            {
                return $"{X}|{Y}";
            }
        }

        #endregion


        #region ROUND 04

        private static int AdventOfCode2019_04_1(int begin, int end)
        {
            var result = 0;
            for (int i = begin; i <= end; i++)
            {
                if (IsPasswordValid_1(i))
                    result++;
            }
            return result;
        }

        private static int AdventOfCode2019_04_2(int begin, int end)
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

        #endregion


        #region ROUND 05
        private static List<int> AdventOfCode2019_05_IntCode(List<int> input, int inputParam = 1)
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

        #endregion


        #region ROUND 06

        private static int AdventOfCode2019_06_1(List<string> input)
        {
            var diction = new Dictionary<string, List<string>>();
            List<OrbitObject> orbits = new List<OrbitObject>();
            orbits.Add(new OrbitObject() { Name = "COM" });
            foreach (var orbit in input)
            {
                var orbitted = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
                var orbittie = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[1];
                orbits.Add(new OrbitObject() { Name = orbittie, Orbits = orbitted });
            }

            //Find StartPoint
            var start = orbits.First(o => o.Name == "COM");
            FindWhoOrbitsThis(start, orbits);

            return orbits.Sum(o => o.InderectlyOrbits.Count) + orbits.Count(o => o.Orbits != null);
        }
        private static int AdventOfCode2019_06_2(List<string> input)
        {
            var diction = new Dictionary<string, List<string>>();
            List<OrbitObject> orbits = new List<OrbitObject>();
            orbits.Add(new OrbitObject() { Name = "COM" });
            foreach (var orbit in input)
            {
                var orbitted = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
                var orbittie = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[1];
                orbits.Add(new OrbitObject() { Name = orbittie, Orbits = orbitted });
            }

            //Find StartPoint
            var start = orbits.First(o => o.Name == "COM");
            FindWhoOrbitsThis(start, orbits);

            var you = orbits.First(o => o.Name == "YOU");
            var san = orbits.First(o => o.Name == "SAN");

            return FindMinimumNumberOfStepsBetween(you.InderectlyOrbits, san.InderectlyOrbits);
        }

        private static int FindMinimumNumberOfStepsBetween(List<string> inderectlyOrbits1, List<string> inderectlyOrbits2)
        {
            var matchingNodes = inderectlyOrbits1.Intersect(inderectlyOrbits2).ToList();
            List<Tuple<string, int>> matchingIndexes = new List<Tuple<string, int>>();
            foreach (var node in matchingNodes)
            {
                // Add two for the direct orbits
                var indexNumber = inderectlyOrbits1.IndexOf(node) + inderectlyOrbits2.IndexOf(node) + 2;
                matchingIndexes.Add(new Tuple<string, int>(node, indexNumber));
            }
            return matchingIndexes.OrderBy(kvp => kvp.Item2).ToList().First().Item2;
        }

        private static void FindWhoOrbitsThis(OrbitObject orbitObject, List<OrbitObject> orbits)
        {
            orbits.Where(o => o.Orbits == orbitObject.Name).ToList().ForEach(o =>
            {
                if (orbitObject.Orbits != null) o.InderectlyOrbits.Add(orbitObject.Orbits);
                o.InderectlyOrbits.AddRange(orbitObject.InderectlyOrbits);
                FindWhoOrbitsThis(o, orbits);
            });
        }

        public class OrbitObject
        {
            public OrbitObject()
            {
                InderectlyOrbits = new List<string>();
            }
            public string Name { get; set; }
            public string Orbits { get; set; }
            public List<string> InderectlyOrbits { get; set; }
        }

        #endregion


        #region ROUND 07

        private static int AdventOfCode2019_07_1(List<int> input)
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

        private static int AdventOfCode2019_07_2(List<int> input)
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

        #endregion


        #region ROUND 08

        private static int AdventOfCode2019_08_1(string input)
        {
            var wide = 25;
            var tall = 6;

            List<List<string>> layers = new List<List<string>>();
            int tallCounter = 0;
            while (tallCounter < input.Length - 1)
            {
                var layer = new List<string>();
                for (int i = 0; i < tall; i++)
                {
                    layer.Add(input.Substring(tallCounter, wide));
                    tallCounter += wide;
                }
                layers.Add(layer);
            }

            var minLayer = layers.OrderBy(l => l.Sum(lin => lin.ToList().Count(c => c == '0'))).First();
            var numberOf1Digits = minLayer.Sum(l => l.Count(c => c == '1'));
            var numberOf2Digits = minLayer.Sum(l => l.Count(c => c == '2'));


            return numberOf1Digits * numberOf2Digits;
        }

        private static List<List<int>> AdventOfCode2019_08_2(string input)
        {
            var wide = 25;
            var tall = 6;

            List<List<string>> layers = new List<List<string>>();
            int tallCounter = 0;
            while (tallCounter < input.Length - 1)
            {
                var layer = new List<string>();
                for (int i = 0; i < tall; i++)
                {
                    layer.Add(input.Substring(tallCounter, wide));
                    tallCounter += wide;
                }
                layers.Add(layer);
            }

            List<List<int>> picture = new List<List<int>>();

            for (int t = 0; t < tall; t++)
            {
                var pictureLine = new List<int>();
                for (int w = 0; w < wide; w++)
                {
                    pictureLine.Add(CalculatePixel(w, t, layers));
                }
                picture.Add(pictureLine);
            }



            return picture;
        }

        private static int CalculatePixel(int wide, int tall, List<List<string>> layers)
        {
            foreach (var layer in layers)
            {
                var color = int.Parse(layer[tall][wide].ToString());
                if (color == 0 || color == 1)
                {
                    return color;
                }
            }
            return 2;
        }

        #endregion


        public static List<string> GetInputLines(string fileName)
        {
            string line;
            var result = new List<string>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            file.Close();
            // Suspend the screen.  
            return result;
        }

        public static string GetInputText(string fileName)
        {
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            var result = file.ReadToEnd();
            // Suspend the screen.  
            return result;
        }

        public static List<T> GetInputCommaSeperated<T>(string fileName)
        {
            var result = new List<T>();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            var text = file.ReadToEnd();

            file.Close();

            result = text.Split(",", StringSplitOptions.None).Select(i => (T)Convert.ChangeType(i, typeof(T))).ToList();
            // Suspend the screen.  
            return result;
        }

        public static List<List<string>> GetInputCommaSeperatedStringByLine(string fileName)
        {
            string line;
            var result = new List<List<string>>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);

            while ((line = file.ReadLine()) != null)
            {
                result.Add(line.Split(",", StringSplitOptions.None).ToList());
            }

            file.Close();

            // Suspend the screen.  
            return result;
        }
    }
}
