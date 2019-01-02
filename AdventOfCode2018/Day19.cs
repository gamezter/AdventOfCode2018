using System;
using System.IO;

namespace AdventOfCode2018
{
    class Day19
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day19.txt").ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int ipr = int.Parse(lines[0].Split()[1]);
            int ip = 0;
            int[] reg = new int[6];

            while (ip < lines.Length - 1)
            {
                reg[ipr] = ip;
                string[] line = lines[ip + 1].Split();

                string op = line[0];
                int A = int.Parse(line[1]);
                int B = int.Parse(line[2]);
                int C = int.Parse(line[3]);

                switch (op)
                {
                    case "setr":
                        reg[C] = reg[A];
                        break;
                    case "eqrr":
                        reg[C] = reg[A] == reg[B] ? 1 : 0;
                        break;
                    case "gtri":
                        reg[C] = reg[A] > B ? 1 : 0;
                        break;
                    case "muli":
                        reg[C] = reg[A] * B;
                        break;
                    case "eqir":
                        reg[C] = A == reg[B] ? 1 : 0;
                        break;
                    case "borr":
                        reg[C] = reg[A] | reg[B];
                        break;
                    case "bori":
                        reg[C] = reg[A] | B;
                        break;
                    case "mulr":
                        reg[C] = reg[A] * reg[B];
                        break;
                    case "gtrr":
                        reg[C] = reg[A] > reg[B] ? 1 : 0;
                        break;
                    case "seti":
                        reg[C] = A;
                        break;
                    case "banr":
                        reg[C] = reg[A] & reg[B];
                        break;
                    case "eqri":
                        reg[C] = reg[A] == B ? 1 : 0;
                        break;
                    case "addr":
                        reg[C] = reg[A] + reg[B];
                        break;
                    case "gtir":
                        reg[C] = A > reg[B] ? 1 : 0;
                        break;
                    case "addi":
                        reg[C] = reg[A] + B;
                        break;
                    case "bani":
                        reg[C] = reg[A] & B;
                        break;
                }
                ip = reg[ipr];
                ip++;
            }
            Console.WriteLine(reg[0]);
            Console.Read();
        }

        public static void part2()
        {
            int sumFactors = 0;
            int n = 10551430;

            for(int i = 1; i <= n; i++)
            {
                if (n % i == 0)
                    sumFactors += i;
            }

            Console.WriteLine(sumFactors);
            Console.Read();
        }
    }
}
