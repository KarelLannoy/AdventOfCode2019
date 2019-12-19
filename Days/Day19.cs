using AdventOfCode2019.IntCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day19
    {
        //private static int AdventOfCode2019_19_1_bis(List<long> input)
        //{
        //    Dictionary<Point, long> result = new Dictionary<Point, long>();

        //    for (int y = 0; y < 50; y++)
        //    {
        //        for (int x = 0; x < 50; x++)
        //        {
        //            IntCodeVM computer = new IntCodeVM(input.ToArray().ToList());
        //            var inputParameters = new List<long>() { x, y };
        //            var output = computer.Run(inputParameters);
        //            result.Add(new Point(x, y), output[0]);
        //            Console.Write(output[0]);
        //        }
        //        Console.WriteLine();
        //    }

        //    return result.Count(kvp => kvp.Value == 1);

        //}

        public static int AdventOfCode2019_19_1(List<long> input)
        {
            Dictionary<Point, long> result = new Dictionary<Point, long>();
            result.Add(new Point(0, 0), 1);

            int x = 0;
            int y = 3;
            int minX = 0;
            var xLength = 1;

            while (y < 120)
            {
                bool start = true;
                bool end = false;
                x = 0;
                var xLengthStart = 0;
                var xLengthEnd = 0;
                while (x < 120)
                {
                    if (x < minX)
                        x = minX;

                    IntCodeVM computer = new IntCodeVM(input.ToArray().ToList());
                    var inputParameters = new List<long>() { x, y };
                    var output = computer.Run(inputParameters);
                    if (output[0] == 1)
                    {
                        if (start)
                        {
                            xLengthStart = x;

                            for (int i = 0; i < xLength - 1; i++)
                            {
                                result.Add(new Point(x, y), output[0]);
                                x++;
                            }
                            start = false;
                            end = true;
                            continue;
                        }
                        else
                            result.Add(new Point(x, y), output[0]);

                    }
                    else
                    {
                        if (start)
                        {
                            minX++;
                        }
                        if (end)
                        {
                            xLengthEnd = x;
                            xLength = xLengthEnd - xLengthStart;
                            break;
                        }
                    }
                    x++;
                }
                y++;
            }

            for (int _y = 0; _y < 50; _y++)
            {
                for (int _x = 0; _x < 50; _x++)
                {
                    if (result.ContainsKey(new Point(_x, _y)))
                        Console.Write(result[new Point(_x, _y)]);
                    else
                        Console.Write("0");
                }
                Console.WriteLine();
            }

            return result.Count(kvp => kvp.Value == 1 && kvp.Key.X < 50);
        }

        public static int AdventOfCode2019_19_2(List<long> input)
        {
            Dictionary<Point, long> result = new Dictionary<Point, long>();
            result.Add(new Point(0, 0), 1);

            int x = 0;
            int y = 3;
            int minX = 0;
            var xLength = 1;

            while (y < 2000)
            {
                bool start = true;
                bool end = false;
                x = 0;
                var xLengthStart = 0;
                var xLengthEnd = 0;
                while (x < 2000)
                {
                    if (x < minX)
                        x = minX;

                    IntCodeVM computer = new IntCodeVM(input.ToArray().ToList());
                    var inputParameters = new List<long>() { x, y };
                    var output = computer.Run(inputParameters);
                    if (output[0] == 1)
                    {
                        if (start)
                        {
                            xLengthStart = x;

                            for (int i = 0; i < xLength - 1; i++)
                            {
                                result.Add(new Point(x, y), output[0]);
                                x++;
                            }
                            start = false;
                            end = true;
                            continue;
                        }
                        else
                            result.Add(new Point(x, y), output[0]);

                    }
                    else
                    {
                        if (start)
                        {
                            minX++;
                        }
                        if (end)
                        {
                            xLengthEnd = x;
                            xLength = xLengthEnd - xLengthStart;
                            break;
                        }
                    }
                    x++;
                }
                y++;
            }

            var lines = result.GroupBy(kvp => kvp.Key.Y).OrderBy(kvp => kvp.Key).ToList();
            for (int i = 0; i < lines.Count(); i++)
            {
                Point beginPoint = new Point(0, 0);
                if (IsThisTheSquareEndLine(lines[i], lines, out beginPoint))
                {
                    return beginPoint.X * 10000 + beginPoint.Y;
                }
            }

            return 0;
        }

        private static bool IsThisTheSquareEndLine(IGrouping<int, KeyValuePair<Point, long>> grouping, List<IGrouping<int, KeyValuePair<Point, long>>> lines, out Point firstPointOfFirstLine)
        {
            var line = grouping.OrderBy(g => g.Key.X).ToList();
            var firstPointOfLastLine = line.ElementAt(0).Key;
            firstPointOfFirstLine = new Point(firstPointOfLastLine.X, firstPointOfLastLine.Y - 99);


            if (!lines.Any(l => l.Key == line[0].Key.Y - 99))
                return false;


            var firstPointOfFirstLineCopy = new Point(firstPointOfLastLine.X, firstPointOfLastLine.Y - 99);

            var lastPointOfFirstLine = new Point(firstPointOfLastLine.X + 99, firstPointOfLastLine.Y - 99);
            var potentialFirstLine = lines.First(l => l.Key == firstPointOfLastLine.Y - 99);
            return potentialFirstLine.Any(l => l.Key == firstPointOfFirstLineCopy) && potentialFirstLine.Any(l => l.Key == lastPointOfFirstLine);
        }
    }
}
