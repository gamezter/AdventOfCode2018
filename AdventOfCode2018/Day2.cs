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
            Dictionary<char, int> counts = new Dictionary<char, int>();

            int twos = 0;
            int threes = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for(int j = 0; j < line.Length; j++)
                {
                    if (counts.ContainsKey(line[j]))
                        counts[line[j]]++;
                    else
                        counts[line[j]] = 1;
                }

                bool hasTwo = false;
                bool hasThree = false;
                foreach(var kvp in counts)
                {
                    int val = kvp.Value;
                    if(val == 2)
                        hasTwo = true;
                    else if(val == 3)
                        hasThree = true;
                }

                if (hasTwo)
                    twos++;
                if (hasThree)
                    threes++;

                counts.Clear();
            }

            Console.WriteLine(twos * threes);
            Console.Read();

        }

        public static void part2()
        {
            string[] lines = new StreamReader("day2.txt").ReadToEnd().Trim().Split('\n');
            Dictionary<char, int> counts = new Dictionary<char, int>();
            List<string> myList = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (counts.ContainsKey(line[j]))
                        counts[line[j]]++;
                    else
                        counts[line[j]] = 1;
                }

                foreach (var kvp in counts)
                {
                    int val = kvp.Value;
                    if (val == 2 || val == 3)
                        myList.Add(line);
                }

                counts.Clear();
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
