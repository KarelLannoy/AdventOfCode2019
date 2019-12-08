using AdventOfCode2019.Days;
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
            //List<string> input = GetInputLines("input01.txt");
            //var result = Day01.AdventOfCode2019_01(input);
            //Console.WriteLine(result);


            // Round 02
            //List<int> input = GetInputCommaSeperated<int>("input02.txt");
            //var result = Day02.AdventOfCode2019_02(input);
            //Console.WriteLine(result);


            // Round 03
            //List<List<string>> input = GetInputCommaSeperatedStringByLine("input03.txt");
            //var result1 = Day03.AdventOfCode2019_03_1(input);
            //var result2 = Day03.AdventOfCode2019_03_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 04
            //var result1 = Day04.AdventOfCode2019_04_1(264360, 746325);
            //var result2 = Day04.AdventOfCode2019_04_2(264360, 746325);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 05
            //List<int> input = GetInputCommaSeperated<int>("input05.txt");
            //var result = Day05.AdventOfCode2019_05_IntCode(input, 5);
            //Console.WriteLine(result.Last());


            // Round 06
            //var input = GetInputLines("input06.txt");
            //var result1 = Day06.AdventOfCode2019_06_1(input);
            //var result2 = Day06.AdventOfCode2019_06_2(input);
            //Console.WriteLine($"{result1} - {result2}");


            // Round 7
            //var input = GetInputCommaSeperated<int>("Input07.txt");
            //var result1 = Day07.AdventOfCode2019_07_1(input);
            //var result2 = Day07.AdventOfCode2019_07_2(input);
            //Console.WriteLine($"{result1} - {result2}");

            // Round 08
            var input = GetInputText("Input08.txt");
            var result1 = Day08.AdventOfCode2019_08_1(input);
            Console.WriteLine(result1);
            Day08.AdventOfCode2019_08_2(input);
            

            Console.ReadLine();
        }




        #region ROUND 05
        

        #endregion


        #region ROUND 06

        

        #endregion


        #region ROUND 07

       

        #endregion


        #region ROUND 08

        

        #endregion


        public static List<string> GetInputLines(string fileName)
        {
            string line;
            var result = new List<string>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            while ((line = file.ReadLine()) != null)
            {
                result.Add(line);
            }

            file.Close();
            // Suspend the screen.  
            return result;
        }

        public static string GetInputText(string fileName)
        {
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            var result = file.ReadToEnd();
            // Suspend the screen.  
            return result;
        }

        public static List<T> GetInputCommaSeperated<T>(string fileName)
        {
            var result = new List<T>();
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);
            var text = file.ReadToEnd();

            file.Close();

            result = text.Split(",", StringSplitOptions.None).Select(i => (T)Convert.ChangeType(i, typeof(T))).ToList();
            // Suspend the screen.  
            return result;
        }

        public static List<List<string>> GetInputCommaSeperatedStringByLine(string fileName)
        {
            string line;
            var result = new List<List<string>>();
            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(@"..\..\..\inputs\" + fileName);

            while ((line = file.ReadLine()) != null)
            {
                result.Add(line.Split(",", StringSplitOptions.None).ToList());
            }

            file.Close();

            // Suspend the screen.  
            return result;
        }
    }
}
