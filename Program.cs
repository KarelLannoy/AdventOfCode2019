using AdventOfCode2019.Days;
using AdventOfCode2019.InputParsers;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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
            var input = InputParser.GetInputText("Input08.txt");
            var result1 = Day08.AdventOfCode2019_08_1(input);
            Console.WriteLine(result1);
            Day08.AdventOfCode2019_08_2(input);
            

            Console.ReadLine();
        }
    }
}
