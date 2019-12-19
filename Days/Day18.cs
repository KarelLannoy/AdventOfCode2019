using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day18
    {
        public static int AdventOfCode2019_18_1(List<string> input)
        {
            var lines = input.ToArray();
            var keys = lines.SelectMany(_ => _.Where(char.IsLower)).ToList();
            var dictionary = new Dictionary<P, List<ReachableKey>>();
            dictionary[FindPositionOf('@', lines)] = ReachableKeys(lines, FindPositionOf('@', lines), string.Empty);
            foreach (var k in keys)
            {
                dictionary[FindPositionOf(k, lines)] = ReachableKeys(lines, FindPositionOf(k, lines), string.Empty);
            }
            var minimumSteps = CollectKeys(lines, dictionary, GetPositions(lines, keys), new[] { '@' });
            return minimumSteps;
        }

        public static int AdventOfCode2019_18_2(List<string> input)
        {
            var lines = input.ToArray();
            var keys = lines.SelectMany(_ => _.Where(char.IsLower)).ToList();
            var dictionary = new Dictionary<P, List<ReachableKey>>();
            for (var i = '1'; i <= '4'; i++)
            {
                dictionary[FindPositionOf(i, lines)] = ReachableKeys(lines, FindPositionOf(i, lines), string.Empty);
            }
            foreach (var k in keys)
            {
                dictionary[FindPositionOf(k, lines)] = ReachableKeys(lines, FindPositionOf(k, lines), string.Empty);
            }
            var minimumSteps = CollectKeys(lines, dictionary, GetPositions(lines, keys), new[] { '1', '2', '3', '4' });
            return minimumSteps;
        }

        private static Dictionary<char, P> GetPositions(string[] map, List<char> keys)
        {
            var dict = new Dictionary<char, P>();

            foreach (var k in keys)
            {
                dict.Add(k, FindPositionOf(k, map));
            }

            return dict;
        }


        private static int CollectKeys(string[] map, Dictionary<P, List<ReachableKey>> keyPaths, Dictionary<char, P> positions, char[] robots)
        {
            var pos = robots.Select(c => FindPositionOf(c, map)).ToArray();
            var currentMinimum = int.MaxValue;

            var startingSet = new PSet();
            for (var index = 0; index < pos.Length; index++)
            {
                var p = pos[index];
                startingSet[index + 1] = p;
            }

            var q = new Queue<State>();
            q.Enqueue(new State { Positions = startingSet, OwnedKeys = 0 });

            var visited = new Dictionary<(PSet, int), int>();
            var finishValue = 0;
            for (var i = 0; i < positions.Count; ++i)
            {
                finishValue |= (int)Math.Pow(2, i);
            }

            while (q.Any())
            {
                var state = q.Dequeue();

                var valueTuple = (state.Positions, state.OwnedKeys);
                if (visited.TryGetValue(valueTuple, out var steps))
                {
                    if (steps <= state.Steps)
                    {
                        continue;
                    }

                    // this is the crucial bit
                    // if the current state is a better path to a known state, update -
                    // this will cull more future paths, leading to faster convergence
                    visited[valueTuple] = state.Steps;
                }
                else
                {
                    visited.Add(valueTuple, state.Steps);
                }

                if (state.OwnedKeys == finishValue)
                {
                    currentMinimum = Math.Min(currentMinimum, state.Steps);
                    continue;
                }

                for (int i = 1; i <= robots.Length; i++)
                {
                    foreach (var k in keyPaths[state.Positions[i]])
                    {
                        var ki = (int)Math.Pow(2, k.Key - 'a');
                        if ((state.OwnedKeys & ki) == ki || (k.Obstacles & state.OwnedKeys) != k.Obstacles)
                        {
                            continue;
                        }

                        var newOwned = state.OwnedKeys | ki;

                        var newPos = state.Positions.Clone();
                        newPos[i] = positions[k.Key];
                        q.Enqueue(new State
                        {
                            Positions = newPos,
                            OwnedKeys = newOwned,
                            Steps = state.Steps + k.Distance
                        });
                    }
                }
            }

            return currentMinimum;
        }

        private struct State
        {
            public PSet Positions { get; set; }

            public int OwnedKeys { get; set; }

            public int Steps { get; set; }
        }

        private static P FindPositionOf(char c, string[] map)
        {
            var startingLine = map.Single(_ => _.Contains(c));
            var startingColumn = startingLine.IndexOf(c);
            var startingRow = Array.IndexOf(map, startingLine);
            return new P { X = startingColumn, Y = startingRow };
        }

        private static List<ReachableKey> ReachableKeys(string[] map, P start, string currentKeys)
        {
            var list = new List<ReachableKey>();
            var visited = new HashSet<P>();

            var q = new Queue<P>();
            var s = new Queue<int>();
            var o = new Queue<int>();
            q.Enqueue(start);
            s.Enqueue(0);
            o.Enqueue(0);

            while (q.Any())
            {
                var pos = q.Dequeue();
                var dist = s.Dequeue();
                var obst = o.Dequeue();
                if (visited.Contains(pos))
                {
                    continue;
                }
                visited.Add(pos);
                var c = map[pos.Y][pos.X];

                if (c == '@' || c == '1' || c == '2' || c == '3' || c == '4')
                {
                    c = '.';
                }
                if (char.IsLower(c))
                {
                    list.Add(new ReachableKey { Distance = dist, Key = c, Obstacles = obst });

                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst);
                    }
                }
                else if (char.IsUpper(c))
                {
                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst |= (int)Math.Pow(2, (char.ToLower(c) - 'a')));
                    }
                }
                else if (c == '.')
                {
                    foreach (var p in pos.Around())
                    {
                        q.Enqueue(p);
                        s.Enqueue(dist + 1);
                        o.Enqueue(obst);
                    }
                }
            }
            return list;
        }

        public class ReachableKey
        {
            public char Key { get; set; }

            public int Distance { get; set; }

            public int Obstacles { get; set; }
        }

        public struct PSet : IEquatable<PSet>
        {
            public P P1 { get; set; }
            public P P2 { get; set; }
            public P P3 { get; set; }
            public P P4 { get; set; }
            public P this[int index]
            {
                get
                {
                    return index switch
                    {
                        1 => P1,
                        2 => P2,
                        3 => P3,
                        4 => P4
                    };
                }
                set
                {
                    switch (index)
                    {
                        case 1:
                            P1 = value;
                            break;
                        case 2:
                            P2 = value;
                            break;
                        case 3:
                            P3 = value;
                            break;
                        case 4:
                            P4 = value;
                            break;
                    }
                }
            }
            public PSet Clone()
            {
                return new PSet
                {
                    P1 = P1,
                    P2 = P2,
                    P3 = P3,
                    P4 = P4
                };
            }
            public bool Equals(PSet other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Equals(P1, other.P1) && Equals(P2, other.P2) && Equals(P3, other.P3) && Equals(P4, other.P4);
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((PSet)obj);
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(P1, P2, P3, P4);
            }
        }
        public struct P : IEquatable<P>
        {
            public int X { get; set; }
            public int Y { get; set; }
            public bool Equals(P other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return X == other.X && Y == other.Y;
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((P)obj);
            }
            public override int GetHashCode()
            {
                return HashCode.Combine(X, Y);
            }
            public IEnumerable<P> Around()
            {
                yield return new P { X = X, Y = Y - 1 };
                yield return new P { X = X - 1, Y = Y };
                yield return new P { X = X + 1, Y = Y };
                yield return new P { X = X, Y = Y + 1 };
            }
        }
    }
}
