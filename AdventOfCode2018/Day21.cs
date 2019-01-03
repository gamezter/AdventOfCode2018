using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day21
    {
        public static void part1()
        {
            int r0 = 0;
            int r4 = 65536;
            int r5 = 3935295;

            while (true)
            {
                r5 = r5 + (r4 & 255);
                r5 = r5 & 16777215;
                r5 = r5 * 65899;
                r5 = r5 & 16777215;
                if (r4 < 256)
                {
                    Console.WriteLine(r5);
                    Console.Read();
                }
                else
                {
                    r4 = r4 >> 8;
                }
            }
        }

        public static void part2()
        {
            int r0 = 0;
            int r4 = 65536;
            int r5 = 3935295;

            while (true)
            {
                r5 = r5 + (r4 & 255);
                r5 = r5 & 16777215;
                r5 = r5 * 65899;
                r5 = r5 & 16777215;
                if (r4 < 256)
                {
                    if (r5 == r0)
                    {
                        Console.WriteLine(r0);
                        Console.Read();
                    }
                    else
                    {
                        r4 = r5 | 65536;
                        r5 = 3935295;
                    }
                }
                else
                {
                    r4 = r4 >> 8;
                }
            }
        }
    }
}
