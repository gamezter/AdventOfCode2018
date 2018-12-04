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

                for(int j = 0; j < counts.Length; j++)
                {
                    if (counts[j] == 2 || counts[j] == 3)
                        myList.Add(line);
                    counts[j] = 0;
                }
            }

            string line1 = "";
            string line2 = "";

            bool found = false;

            for(int i = 0; i < myList.Count - 1; i++)
            {
                line1 = myList[i];
                for(int j = i + 1; j < myList.Count; j++)
                {
                    line2 = myList[j];
                    int diff = 0;
                    for(int k = 0; k < line1.Length; k++)
                    {
                        if(line1[k] != line2[k])
                        {
                            diff++;
                            if (diff == 2)
                                break;
                        }
                    }
                    if (diff == 1)
                    {
                        found = true;
                        break;
                    }
                        
                }
                if (found)
                    break;
            }

            StringBuilder final = new StringBuilder();

            for(int i = 0; i < line1.Length; i++)
            {
                if (line1[i] == line2[i])
                    final.Append(line1[i]);
            }

            Console.WriteLine(final);
            Console.Read();
        }
    }
}
