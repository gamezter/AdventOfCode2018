using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day11
    {
        public static void part1()
        {
            int serial = 8561;

            int bestX = 0;
            int bestY = 0;
            int lvl = 0;

            for (int x = 2; x < 300; x++)
            {
                for(int y = 2; y < 300; y++)
                {
                    int total = 0;
                    total += ((((x +  9) * (y - 1) + serial) * (x +  9) / 100) % 10) - 5;
                    total += ((((x + 10) * (y - 1) + serial) * (x + 10) / 100) % 10) - 5;
                    total += ((((x + 11) * (y - 1) + serial) * (x + 11) / 100) % 10) - 5;
                    total += ((((x +  9) * (y    ) + serial) * (x +  9) / 100) % 10) - 5;
                    total += ((((x + 10) * (y    ) + serial) * (x + 10) / 100) % 10) - 5;
                    total += ((((x + 11) * (y    ) + serial) * (x + 11) / 100) % 10) - 5;
                    total += ((((x +  9) * (y + 1) + serial) * (x +  9) / 100) % 10) - 5;
                    total += ((((x + 10) * (y + 1) + serial) * (x + 10) / 100) % 10) - 5;
                    total += ((((x + 11) * (y + 1) + serial) * (x + 11) / 100) % 10) - 5;

                    if(total > lvl)
                    {
                        bestX = x;
                        bestY = y;
                        lvl = total;
                    }
                }
            }

            Console.WriteLine((bestX - 1) + "," + (bestY - 1));
            Console.Read();
        }

        public static void part2()
        {
            int serial = 8561;

            int bestX = 0;
            int bestY = 0;
            int bestS = 1;
            int lvl = 0;

            int[,] sums = new int[301, 301];
            for (int x = 1; x <= 300; x++)
            {
                for (int y = 1; y <= 300; y++)
                {
                    int p = ((((x + 10) * y + serial) * (x + 10) / 100) % 10) - 5;
                    sums[x, y] = p + sums[x, y - 1] + sums[x - 1, y] - sums[x - 1, y - 1]; // everything higher and to the left
                }
            }

            for(int size = 1; size <= 300; size++)
            {
                for (int x = 0; x <= 300 - size; x++)
                {
                    for (int y = 0; y <= 300 - size; y++)
                    {
                        int total = sums[x + size, y + size] - sums[x, y + size] - sums[x + size, y] + sums[x, y];

                        if (total >= lvl)
                        {
                            bestX = x;
                            bestY = y;
                            bestS = size;
                            lvl = total;
                        }
                    }
                }
            }

            Console.WriteLine((bestX + 1) + "," + (bestY + 1) + "," + bestS);
            Console.Read();
        }
    }
}
