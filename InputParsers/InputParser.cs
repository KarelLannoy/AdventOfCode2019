using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.InputParsers
{
    public static class InputParser
    {
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
