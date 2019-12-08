using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day06
    {
        public static int AdventOfCode2019_06_1(List<string> input)
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
        public static int AdventOfCode2019_06_2(List<string> input)
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
    }
}
