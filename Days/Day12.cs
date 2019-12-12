using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day12
    {
        public static int AdventOfCode2019_12_1(List<string> input)
        {
            List<Point3D[]> moons = MapInputToMoons(input);

            for (int i = 0; i < 1000; i++)
            {
                UpdateVelocityForGravity(moons);
                UpdatePosition(moons);
            }

            var energy = CalculateEnergy(moons);

            return energy;
        }

        public static long AdventOfCode2019_12_2(List<string> input)
        {
            List<Point3D[]> moons = MapInputToMoons(input);
            int times = 0;
            long happendXTimes = 0;
            long happendYTimes = 0;
            long happendZTimes = 0;

            string moonXZero = PrintMoonsToString(moons, "X");
            string moonYZero = PrintMoonsToString(moons, "Y");
            string moonZZero = PrintMoonsToString(moons, "Z");


            while (happendXTimes == 0 || happendYTimes == 0 || happendZTimes == 0)
            {
                times++;
                UpdateVelocityForGravity(moons);
                UpdatePosition(moons);
                var moonStringX = PrintMoonsToString(moons, "X");
                var moonStringY = PrintMoonsToString(moons, "Y");
                var moonStringZ = PrintMoonsToString(moons, "Z");

                if (happendXTimes == 0 && moonXZero == moonStringX)
                {
                    happendXTimes = times;
                }
                if (happendYTimes == 0 && moonYZero == moonStringY)
                {
                    happendYTimes = times;
                }
                if (happendZTimes == 0 && moonZZero == moonStringZ)
                {
                    happendZTimes = times;
                }
            }

            return LCM(happendXTimes, happendYTimes, happendZTimes);
        }

        private static long LCM(params long[] numbers)
        {
            return numbers.Aggregate(lcm);
        }
        private static long lcm(long a, long b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }
        private static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        private static string PrintMoonsToString(List<Point3D[]> moons, string property)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var moon in moons)
            {
                builder.Append(moon[0].ToString(property));
                builder.Append(moon[1].ToString(property));
            }
            return builder.ToString();
        }

        private static int CalculateEnergy(List<Point3D[]> moons)
        {
            int total = 0;
            foreach (var moon in moons)
            {
                total += ((Math.Abs(moon[0].X) + Math.Abs(moon[0].Y) + Math.Abs(moon[0].Z)) * (Math.Abs(moon[1].X) + Math.Abs(moon[1].Y) + Math.Abs(moon[1].Z)));
            }
            return total;
        }

        private static void UpdatePosition(List<Point3D[]> moons)
        {
            foreach (var moon in moons)
            {
                moon[0].X += moon[1].X;
                moon[0].Y += moon[1].Y;
                moon[0].Z += moon[1].Z;
            }
        }

        private static void UpdateVelocityForGravity(List<Point3D[]> moons)
        {
            foreach (var moon in moons)
            {
                foreach (var moon2 in moons)
                {
                    if (moon == moon2)
                    {
                        continue;
                    }
                    moon[1].X += (moon[0].X > moon2[0].X) ? -1 : (moon[0].X == moon2[0].X) ? 0 : 1;
                    moon[1].Y += (moon[0].Y > moon2[0].Y) ? -1 : (moon[0].Y == moon2[0].Y) ? 0 : 1;
                    moon[1].Z += (moon[0].Z > moon2[0].Z) ? -1 : (moon[0].Z == moon2[0].Z) ? 0 : 1;
                }
            }
        }

        private static List<Point3D[]> MapInputToMoons(List<string> input)
        {
            var result = new List<Point3D[]>();
            foreach (var line in input)
            {
                var coordStrings = line.Split(",");
                int x = int.Parse(coordStrings[0].Substring(coordStrings[0].IndexOf("=") + 1));
                int y = int.Parse(coordStrings[1].Substring(coordStrings[1].IndexOf("=") + 1));
                int z = int.Parse(coordStrings[2].Substring(coordStrings[2].IndexOf("=") + 1).Substring(0, coordStrings[2].Substring(coordStrings[2].IndexOf("=") + 1).IndexOf(">")));
                result.Add(new List<Point3D>() { new Point3D() { X = x, Y = y, Z = z }, new Point3D() { X = 0, Y = 0, Z = 0 } }.ToArray());
            }
            return result;
        }

        private struct Point3D
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }

            public string ToString(string property)
            {
                if (string.IsNullOrEmpty(property))
                {
                    return $"{X} | {Y} | {Z}";
                }
                switch (property)
                {
                    case "X":
                        return $"{X}";
                    case "Y":
                        return $"{Y}";
                    case "Z":
                        return $"{Z}";
                    default:
                        return $"{X} | {Y} | {Z}"; ;
                }
            }
        }
    }
}
