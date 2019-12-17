using AdventOfCode2019.IntCode;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public class Day17
    {
        public static int AdventOfCode2019_17_1(List<long> input)
        {
            var intCodeVM = new IntCodeVM(input.ToArray().ToList());
            var output = intCodeVM.Run(null);
            var x = 0;
            var y = 0;

            var grid = new Dictionary<Point, char>();
            for (int i = 0; i < output.Count; i++)
            {
                switch (output[i])
                {
                    case 10:
                        x = -1;
                        y++;
                        break;
                    default:
                        grid.Add(new Point(x, y), Convert.ToChar(output[i]));
                        break;
                }
                x++;
            }

            for (int _y = 0; _y <= grid.Keys.Max(k => k.Y); _y++)
            {
                for (int _x = 0; _x <= grid.Keys.Max(k => k.X); _x++)
                {
                    Console.Write(grid[new Point(_x, _y)]);
                }
                Console.WriteLine();
            }

            List<Point> intersections = new List<Point>();
            foreach (var point in grid)
            {
                if (point.Value == '#' && IsIntersection(point.Key, grid))
                    intersections.Add(point.Key);
            }
            var total = 0;
            foreach (var point in intersections)
            {
                total += point.X * point.Y;
            }

            return total;
        }

        public static long AdventOfCode2019_17_2(List<long> input)
        {
            var enabledInput = input.ToArray().ToList();
            enabledInput[0] = 2;

            var inputParameters = new List<long>();

            //Insert MethodChain
            inputParameters.AddRange("A,B,A,B,C,B,C,A,C,C".ToArray().ToList().Select(c => (int)c).Select(i => (long)i).ToList());
            inputParameters.Add(10);

            ////Insert Method A         R12 L10 L10  
            inputParameters.AddRange("R,12,L,10,L,10".ToArray().ToList().Select(c => (int)c).Select(i => (long)i).ToList());
            inputParameters.Add(10);

            ////Insert Method B         L6 L12 R12 L4 
            inputParameters.AddRange("L,6,L,12,R,12,L,4".ToArray().ToList().Select(c => (int)c).Select(i => (long)i).ToList());
            inputParameters.Add(10);


            //Insert Method C  L12 R12 L6 
            inputParameters.AddRange("L,12,R,12,L,6".ToArray().ToList().Select(c => (int)c).Select(i => (long)i).ToList());
            inputParameters.Add(10);

            //Insert continuous video feed
            inputParameters.Add('n');
            inputParameters.Add(10);

            var intCodeVM = new IntCodeVM(enabledInput);

            var output = intCodeVM.Run(inputParameters);

            //Route puzzling
            //R12 L10 L10                   A
            //L6 L12 R12 L4                 B
            //R12 L10 L10                   A
            //L6 L12 R12 L4                 B
            //L12 R12 L6                    C
            //L6 L12 R12 L4                 B
            //L12 R12 L6                    C
            //R12 L10 L10                   A
            //L12 R12 L6                    C
            //L12 R12 L6                    C

            return output.Last();
        }

        private static bool IsIntersection(Point p, Dictionary<Point, char> grid)
        {
            List<Point> intersectionPoints = new List<Point>() { new Point(p.X - 1, p.Y), new Point(p.X + 1, p.Y), new Point(p.X, p.Y - 1), new Point(p.X, p.Y + 1) };
            var pointsOnGrid = grid.Where(g => intersectionPoints.Contains(g.Key)).ToList();
            return pointsOnGrid.Count == 4 && pointsOnGrid.All(p => p.Value == '#');
        }
    }
}
