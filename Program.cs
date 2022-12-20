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

            GetSections(logs);
            GetCategories(logs);
            GetUsers(logs);
            GetNodes(logs);
        }

        static private void GetSections(string[] logs)
        {
            Regex reg = new Regex(@"\w*Section: \w*");
            MatchCollection res = null;
            List<string> resList = new List<string>();

            foreach (var i in logs)
            {
                resList.AddRange(reg.Matches(i).Select(x => x.Value));
            }
            var resDict = ClearList(resList);
            Console.WriteLine("Sections requests");
            foreach (var i in resDict)
                Console.WriteLine(String.Format("{0}-{1}", i.Key, i.Value));
            Console.WriteLine();
        }

        static private void GetCategories(string[] logs)
        {
            Regex reg = new Regex(@"\w*Category: \w*");
            MatchCollection res = null;
            List<string> resList = new List<string>();

            foreach (var i in logs)
            {
                resList.AddRange(reg.Matches(i).Select(x => x.Value));
            }
            var resDict = ClearList(resList);
            Console.WriteLine("Categories requests");
            foreach (var i in resDict)
                Console.WriteLine(String.Format("{0}-{1}", i.Key, i.Value));
            Console.WriteLine();
        }

        static private void GetUsers(string[] logs)
        {
            Regex reg = new Regex(@"\w*CurrentUser: \w*");
            MatchCollection res = null;
            List<string> resList = new List<string>();

            foreach (var i in logs)
            {
                resList.AddRange(reg.Matches(i).Select(x => x.Value));
            }
            var resDict = ClearList(resList);
            Console.WriteLine("Users");
            foreach (var i in resDict)
                Console.WriteLine(String.Format("{0}-{1}", i.Key, i.Value));
            Console.WriteLine();
        }

        static private void GetNodes(string[] logs)
        {
            Regex reg = new Regex(@"\w*Id: \w*");
            MatchCollection res = null;
            List<string> resList = new List<string>();

            foreach (var i in logs)
            {
                resList.AddRange(reg.Matches(i).Select(x => x.Value));
            }
            var resDict = ClearList(resList);
            Console.WriteLine("Nodes requests");
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
