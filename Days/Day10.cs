using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day10
    {
        public static int AdventOfCode2019_10_1(List<string> input)
        {
            Dictionary<Point, int> asteroidWithView = new Dictionary<Point, int>();
            List<Point> asteroids = ParseInputToAsteroids(input);
            foreach (var asteroid in asteroids)
            {
                var nrOfAsteroids = CalculateNumberOfAsteroidsInView(asteroid, asteroids);
                asteroidWithView.Add(asteroid, nrOfAsteroids);
            }

            var bestMatch = asteroidWithView.OrderByDescending(kvp => kvp.Value).First();
            return bestMatch.Value;
        }

        public static int AdventOfCode2019_10_2(List<string> input)
        {
            Dictionary<Point, int> asteroidWithView = new Dictionary<Point, int>();
            List<Point> asteroids = ParseInputToAsteroids(input);
            foreach (var asteroid in asteroids)
            {
                var nrOfAsteroids = CalculateNumberOfAsteroidsInView(asteroid, asteroids);
                asteroidWithView.Add(asteroid, nrOfAsteroids);
            }

            var bestMatch = asteroidWithView.OrderByDescending(kvp => kvp.Value).First();


            var allLineOfSights = GetAllLineOfSights(bestMatch.Key, asteroids).OrderBy(l => l.Item2).ToList();

            int blasted = 0;
            var startAngle = -90;
            var indexer = allLineOfSights.IndexOf(allLineOfSights.FirstOrDefault(s => s.Item2 == startAngle));
            Point blast = new Point();
            while (blasted < 200)
            {
                if (allLineOfSights[indexer].Item1.Count > 0)
                {

                    blast = allLineOfSights[indexer].Item1.OrderBy(p => GetDistance(bestMatch.Key, p)).First();
                    allLineOfSights[indexer].Item1.Remove(blast);
                    blasted++;
                }
                indexer++;
                if (indexer >= allLineOfSights.Count)
                {
                    indexer = 0;
                }
            }

            return blast.X * 100 + blast.Y;
        }

        private static List<Tuple<List<Point>, double>> GetAllLineOfSights(Point home, List<Point> asteroids)
        {
            var result = new List<Tuple<List<Point>, double>>();
            var skip = new List<Point>();
            foreach (var asteroid in asteroids)
            {
                if (skip.Contains(asteroid))
                {
                    continue;
                }
                float xDiff = asteroid.X - home.X;
                float yDiff = asteroid.Y - home.Y;
                var angle = Math.Atan2(yDiff, xDiff) * 180.0 / Math.PI;

                var items = result.FirstOrDefault(r => r.Item2 == angle);
                if (items != null) items.Item1.Add(asteroid);
                else
                {
                    var lineOfSight = LineOfSight(home, asteroid);
                    lineOfSight.Remove(home);
                    var common = lineOfSight.Intersect(asteroids).ToList();
                    skip.AddRange(common);
                    result.Add(new Tuple<List<Point>, double>(common, angle));
                }
            }
            return result;
        }

        private static double GetDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }

        private static int CalculateNumberOfAsteroidsInView(Point asteroid, List<Point> asteroids)
        {
            int counter = 0;
            foreach (var otherAsteroid in asteroids)
            {
                if (asteroid == otherAsteroid)
                {
                    continue;
                }
                var lineOfSight = LineOfSight(asteroid, otherAsteroid);
                lineOfSight.Remove(asteroid);
                lineOfSight.Remove(otherAsteroid);
                if (!lineOfSight.Intersect(asteroids).Any())
                {
                    counter++;
                }

            }
            return counter;
        }

        private static List<Point> LineOfSight(Point asteroid, Point otherAsteroid)
        {
            var points = GetPoints(asteroid, otherAsteroid);
            var returnValue = points.Distinct().ToList();
            return returnValue;
        }


        private static List<Point> ParseInputToAsteroids(List<string> input)
        {
            List<Point> points = new List<Point>();
            int y = 0;
            foreach (var line in input)
            {
                int x = 0;
                foreach (var c in line)
                {
                    if (c == '#')
                    {
                        points.Add(new Point(x, y));
                    }
                    x++;
                }
                y++;
            }
            return points;
        }

        public static List<Point> GetPoints(Point p1, Point p2)
        {
            List<Point> points = new List<Point>();

            var pointMap = new List<Point>() { p1, p2 };
            var minX = pointMap.Select(p => p.X).Min();
            var minY = pointMap.Select(p => p.Y).Min();
            var maxX = pointMap.Select(p => p.X).Max();
            var maxY = pointMap.Select(p => p.Y).Max();
            // no slope (vertical line)
            if (p1.X == p2.X)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    Point p = new Point(p1.X, y);
                    points.Add(p);
                }
                return points;
            }
            // horizontal line
            if (p1.Y == p2.Y)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Point p = new Point(x, p1.Y);
                    points.Add(p);
                }
                return points;
            }

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    if (LineContainsPoint(p1, p2, new Point(x, y)))
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

            return points;
        }

        public static bool LineContainsPoint(Point a, Point b, Point c)
        {
            var crossproduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);
            if (Math.Abs(crossproduct) != 0) return false;
            var dotproduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct < 0) return false;
            var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            if (dotproduct > squaredlengthba)
                return false;
            return true;
        }
    }
}
