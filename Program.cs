using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace LogParser
{
    class Program
    {
        static string path = "/Users/dmitrijkovalenko/Downloads/12.19.2022Logs.txt";
        static Regex regexSections = new Regex(@"\w*Section: \w*");
        static Regex regexCategory = new Regex(@"\w*Category: \w*");
        static Regex regexUser = new Regex(@"\w*CurrentUser: \w*");
        static Regex regexNodesId = new Regex(@"\w*Id: \w*");

        static void Main(string[] args)
        {
            string enteredPath = "";
            Console.WriteLine("Enter the path of logFile");
            do
            {
                enteredPath = Console.ReadLine();
                if (!File.Exists(enteredPath)) Console.WriteLine("File does not exist at the given path");
            }
            while (!File.Exists(enteredPath));

            string[] logs = File.ReadAllLines(enteredPath);

            ShowInfo(logs, regexSections, "Sections requests");
            ShowInfo(logs, regexCategory, "Categories requests");
            ShowInfo(logs, regexUser, "Users");
            ShowInfo(logs, regexNodesId, "Nodes requests");
        }

        static private void ShowInfo(string[] logs, Regex reg, string showMessage)
        {
            List<string> resList = new List<string>();

            foreach (var i in logs)
            {
                resList.AddRange(reg.Matches(i).Select(x => x.Value));
            }
            var resDict = ClearList(resList);
            Console.WriteLine(showMessage);
            foreach (var i in resDict)
                Console.WriteLine(String.Format("{0}-{1}", i.Key, i.Value));
            Console.WriteLine();
        }

        static Dictionary<string, int> ClearList(List<string> list)
        {
            Dictionary<string, int> res = new Dictionary<string, int>();

            foreach (var i in list)
            {
                var temp = i.Trim().Replace(" ", "").Split(":").Last();

                if (temp == "") continue;
                else if (res.ContainsKey(temp)) res[temp]++;
                else res.Add(temp, 1);
            }
            return res;
        }
    }
}
