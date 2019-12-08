using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Days
{
    public static class Day08
    {
        public static int AdventOfCode2019_08_1(string input)
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

        public static void AdventOfCode2019_08_2(string input)
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


            foreach (var line in picture)
            {
                Console.WriteLine();
                foreach (var pixel in line)
                {
                    if (pixel == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("O");
                    }
                    if (pixel == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("O");
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
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
    }
}
