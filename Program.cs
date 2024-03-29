﻿using AdventOfCode2019.Days;
using AdventOfCode2019.InputParsers;
using AdventOfCode2019.IntCode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode2019
{
    class Program
    {
        static void Main(string[] args)
        {

            // Round 01
            //List<string> input = InputParser.GetInputLines("input01.txt");
            //var result = Day01.AdventOfCode2019_01(input);
            //Console.WriteLine(result);


            // Round 02
            //List<int> input = InputParser.GetInputCommaSeperated<int>("input02.txt");
            //var result = Day02.AdventOfCode2019_02(input);
            //Console.WriteLine(result);


            // Round 03
            //List<List<string>> input = InputParser.GetInputCommaSeperatedStringByLine("input03.txt");
            //var result1 = Day03.AdventOfCode2019_03_1(input);
            //var result2 = Day03.AdventOfCode2019_03_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 04
            //var result1 = Day04.AdventOfCode2019_04_1(264360, 746325);
            //var result2 = Day04.AdventOfCode2019_04_2(264360, 746325);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 05
            //List<int> input = InputParser.GetInputCommaSeperated<int>("input05.txt");
            //var result = Day05.AdventOfCode2019_05_IntCode(input, 5);
            //Console.WriteLine(result.Last());


            // Round 06
            //var input = InputParser.GetInputLines("input06.txt");
            //var result1 = Day06.AdventOfCode2019_06_1(input);
            //var result2 = Day06.AdventOfCode2019_06_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 7
            //var input = InputParser.GetInputCommaSeperated<int>("Input07.txt");
            //var result1 = Day07.AdventOfCode2019_07_1(input);
            //var result2 = Day07.AdventOfCode2019_07_2(input);
            //Console.WriteLine($"{result1} - {result2}");

            // Round 08
            //var input = InputParser.GetInputText("Input08.txt");
            //var result1 = Day08.AdventOfCode2019_08_1(input);
            //Console.WriteLine(result1);
            //Day08.AdventOfCode2019_08_2(input);


            // Round 09
            //var input = InputParser.GetInputCommaSeperated<long>("input09.txt");
            //var result1 = Day09.AdventOfCode2019_09_1(input);
            //var result2 = Day09.AdventOfCode2019_09_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 10
            //var input = InputParser.GetInputLines("input10.txt");
            //var result1 = Day10.AdventOfCode2019_10_1(input);
            //var result2 = Day10.AdventOfCode2019_10_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 11
            //var input = InputParser.GetInputCommaSeperated<long>("input11.txt");
            //var result1 = Day11.AdventOfCode2019_11_1(input);
            //Console.WriteLine(result1);
            //Day11.AdventOfCode2019_11_2(input);


            // Round 12
            //var input = InputParser.GetInputLines("input12.txt");
            //var result1 = Day12.AdventOfCode2019_12_1(input);
            //var result2 = Day12.AdventOfCode2019_12_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 13
            //var input = InputParser.GetInputCommaSeperated<long>("input13.txt");
            //var result1 = Day13.AdventOfCode2019_13_1(input);
            //var result2 = Day13.AdventOfCode2019_13_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 14
            //var input = InputParser.GetInputLines("input14.txt");
            //var result1 = Day14.AdventOfCode2019_14_1(input);
            //var result2 = Day14.AdventOfCode2019_14_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 15
            //var input = InputParser.GetInputCommaSeperated<long>("input15.txt");
            //var result1 = Day15.AdventOfCode2019_15_1(input);
            //var result2 = Day15.AdventOfCode2019_15_2(input);
            //Console.WriteLine($"{result1} - {result2}");

            // Round 16
            //var input = InputParser.GetInputText("input16.txt");
            //var result1 = Day16.AdventOfCode2019_16_1(input);
            //var result2 = Day16.AdventOfCode2019_16_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 17
            //var input = InputParser.GetInputCommaSeperated<long>("input17.txt");
            //var result1 = Day17.AdventOfCode2019_17_1(input);
            //var result2 = Day17.AdventOfCode2019_17_2(input);
            //Console.WriteLine($"{result1} - {result2}");

            // Round 18
            //var input = InputParser.GetInputLines("input18.txt");
            //var result1 = Day18.AdventOfCode2019_18_1(input);
            //var input2 = InputParser.GetInputLines("input18_2.txt");
            //var result2 = Day18.AdventOfCode2019_18_2(input2);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 19
            var input = InputParser.GetInputCommaSeperated<long>("input19.txt");
            var result1 = Day19.AdventOfCode2019_19_1(input);
            var result2 = Day19.AdventOfCode2019_19_2(input);
            Console.WriteLine($"{result1} - {result2}");

            Console.ReadLine();
        }

        
    }
}
