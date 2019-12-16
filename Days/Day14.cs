using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day14
    {
        private static Dictionary<string, long> _wareHouse = new Dictionary<string, long>();

        public static int AdventOfCode2019_14_1(List<string> input)
        {

            var reactions = input
                .Select(l => l.Split(new[] { " => " }, 0))
                .Select(a => new { Inputs = a[0].Split(new[] { ", " }, 0), Output = a[1].Split(' ') })
                .Select(t => new Reaction() { Inputs = t.Inputs.Select(i => i.Split(' ')).ToDictionary(a => a[1], a => int.Parse(a[0])), Output = new KeyValuePair<string, int>(t.Output[1], int.Parse(t.Output[0])) })
                .ToDictionary(r => r.Output.Key, r => r);

            var deficits = new Dictionary<string, int> { { "FUEL", 1 } };
            while (HasDeficitsToFill(deficits))
            {
                var deficitToFill = deficits.First(kvp => kvp.Key != "ORE" && kvp.Value > 0);
                var reaction = reactions[deficitToFill.Key];
                deficits[deficitToFill.Key] -= reaction.Output.Value;
                foreach (var reactionInput in reaction.Inputs)
                {
                    if (deficits.ContainsKey(reactionInput.Key))
                    {
                        deficits[reactionInput.Key] += reactionInput.Value;
                    }
                    else
                    {
                        deficits.Add(reactionInput.Key, reactionInput.Value);
                    }
                }
            }

            return deficits["ORE"];
        }

        public static long AdventOfCode2019_14_2(List<string> input)
        {
            var reactions = input
                .Select(l => l.Split(new[] { " => " }, 0))
                .Select(a => new { Inputs = a[0].Split(new[] { ", " }, 0), Output = a[1].Split(' ') })
                .Select(t => new Reaction() { Inputs = t.Inputs.Select(i => i.Split(' ')).ToDictionary(a => a[1], a => int.Parse(a[0])), Output = new KeyValuePair<string, int>(t.Output[1], int.Parse(t.Output[0])) })
                .ToDictionary(r => r.Output.Key, r => r);

            _wareHouse.Add("ORE", 1000000000000);

            var needed = 1000000;
            while (needed > 0)
            {
                while (MakeChemical("FUEL", needed, reactions)) { }
                needed /= 10;
            }

            return _wareHouse["FUEL"];
        }

        private class Reaction
        {
            public Dictionary<string, int> Inputs { get; set; }
            public KeyValuePair<string, int> Output { get; set; }
        }

        private static bool HasDeficitsToFill(Dictionary<string, int> deficits)
        {
            return deficits.Any(kvp => kvp.Key != "ORE" && kvp.Value > 0);
        }

        private static bool MakeChemical(string chemical, long amount, Dictionary<string, Reaction> reactions)
        {
            var reaction = reactions[chemical];

            var howManyToMake = (long)Math.Ceiling(amount / (double)reaction.Output.Value);
            if (reaction.Inputs.Any(input => GetFromWareHouse(input.Key) < howManyToMake * input.Value && input.Key == "ORE"))
            {
                return false;
            }

            var wareHouseCopy = _wareHouse.ToDictionary(a => a.Key, a => a.Value);
            while (reaction.Inputs.Any(input => GetFromWareHouse(input.Key) < howManyToMake * input.Value))
            {
                var chemToMake = reaction.Inputs.First(input => GetFromWareHouse(input.Key) < howManyToMake * input.Value);
                var need = howManyToMake * chemToMake.Value - GetFromWareHouse(chemToMake.Key);
                if (!MakeChemical(chemToMake.Key, need, reactions))
                {
                    _wareHouse = wareHouseCopy;
                    return false;
                }
            }

            foreach (var input in reaction.Inputs)
            {
                StoreInWareHouse(input.Key, (-howManyToMake * input.Value));
            }

            StoreInWareHouse(chemical, (howManyToMake * reaction.Output.Value));

            return true;
        }

        private static long GetFromWareHouse(string chemical)
        {
            return _wareHouse.ContainsKey(chemical) ? _wareHouse[chemical] : 0;
        }

        private static void StoreInWareHouse(string chemical, long amount)
        {
            if (_wareHouse.ContainsKey(chemical))
                _wareHouse[chemical] += amount;
            else _wareHouse.Add(chemical, amount);
        }
    }
}
