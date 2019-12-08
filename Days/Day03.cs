using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day03
    {
        public static int AdventOfCode2019_03_1(List<List<string>> input)
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

        public static int AdventOfCode2019_03_2(List<List<string>> input)
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
    }
}
