using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode2018
{
    class Day2
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day2.txt").ReadToEnd().Trim().Split('\n');
            int[] counts = new int[26];

            int twos = 0;
            int threes = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for(int j = 0; j < line.Length; j++)
                {
                    counts[line[j] - 97]++;
                }

                bool hasTwo = false;
                bool hasThree = false;
                for(int j = 0; j < counts.Length; j++)
                {
                    hasTwo |= counts[j] == 2;
                    hasThree |= counts[j] == 3;
                    counts[j] = 0;
                }

                if (hasTwo)
                    twos++;
                if (hasThree)
                    threes++;
            }

            Console.WriteLine(twos * threes);
            Console.Read();

        }

        public static void part2()
        {
            string[] lines = new StreamReader("day2.txt").ReadToEnd().Trim().Split('\n');
            int[] counts = new int[26];
            List<string> myList = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    counts[line[j] - 97]++;
                }

                bool hasTwo = false;
                bool hasThree = false;
                for (int j = 0; j < counts.Length; j++)
                {
                    hasTwo |= counts[j] == 2;
                    hasThree |= counts[j] == 3;
                    counts[j] = 0;
                }

                if (hasTwo || hasThree)
                    myList.Add(line);
            }

            HashSet<string> met = new HashSet<string>();
            string newString = "";
            bool found = false;

            for(int i = 0; i < myList[0].Length; i++)
            {
                for(int j = 0; j < myList.Count; j++)
                {
                    newString = myList[j].Substring(0, i) + myList[j].Substring(i + 1);
                    if (!met.Add(newString))
                    {
                        found = true;
                        break;
                    }
                }
                met.Clear();
                if (found)
                    break;
            }

            Console.WriteLine(newString);
            Console.Read();
        }
    }
}
