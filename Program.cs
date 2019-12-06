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
            var input = GetInputLines("input06.txt");
            var result1 = AdventOfCode2019_06_1(input);
            var result2 = AdventOfCode2019_06_2(input);
            Console.WriteLine($"{result1} - {result2}");

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
            foreach (var orbit in input)
            {
                var orbitted = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
                var orbittie = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[1];

                if (!orbits.Any(o => o.Name == orbitted))
                    orbits.Add(new OrbitObject() { Name = orbitted });

                if (!orbits.Any(o => o.Name == orbittie))
                    orbits.Add(new OrbitObject() { Name = orbittie, Orbits = orbitted });
                else
                    orbits.First(o => o.Name == orbittie).Orbits = orbitted;
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
            foreach (var orbit in input)
            {
                var orbitted = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[0];
                var orbittie = orbit.Split(")", StringSplitOptions.RemoveEmptyEntries)[1];

                if (!orbits.Any(o => o.Name == orbitted))
                    orbits.Add(new OrbitObject() { Name = orbitted });

                if (!orbits.Any(o => o.Name == orbittie))
                    orbits.Add(new OrbitObject() { Name = orbittie, Orbits = orbitted });
                else
                    orbits.First(o => o.Name == orbittie).Orbits = orbitted;
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

        public static List<string> GetInputLines(string fileName)
        {
            string line;
            var result = new List<string>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"c:\temp\AdventOfCode2019\" + fileName);
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            file.Close();
            // Suspend the screen.  
            return result;
        }

        public static List<T> GetInputCommaSeperated<T>(string fileName)
        {
            var result = new List<T>();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"c:\temp\AdventOfCode2019\" + fileName);
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
                new System.IO.StreamReader(@"c:\temp\AdventOfCode2019\" + fileName);

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
