using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day1
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day1.txt").ReadToEnd().Trim().Split('\n');

            int total = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                switch (line[0])
                {
                    case '+':
                        total += int.Parse(line.Substring(1));
                        break;
                    case '-':
                        total -= int.Parse(line.Substring(1));
                        break;
                }
            }

            Console.WriteLine(total);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day1.txt").ReadToEnd().Trim().Split('\n');
            HashSet<int> met = new HashSet<int>();

            int total = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                switch (line[0])
                {
                    case '+':
                        total += int.Parse(line.Substring(1));
                        break;
                    case '-':
                        total -= int.Parse(line.Substring(1));
                        break;
                }
                if (!met.Add(total))
                    break;
                if (i == lines.Length - 1)
                    i = -1;
            }

            Console.WriteLine(total);
            Console.Read();
        }
    }
}
