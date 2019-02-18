using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day22
    {
        static int depth = 8103;
        static int targetX = 9;
        static int targetY = 758;

        public static void part1()
        {
            int[,] map = new int[targetX + 1, targetY + 1];
            int total = 0;
            for (int x = 0; x <= targetX; x++)
            {
                for(int y = 0; y <= targetY; y++)
                {
                    int geoIndex;
                    if (x == 0 && y == 0)
                        geoIndex = 0;
                    else if (y == 0)
                        geoIndex = x * 16807;
                    else if (x == 0)
                        geoIndex = y * 48271;
                    else
                        geoIndex = map[x - 1, y] * map[x, y - 1];

                    int erosionLevel = (geoIndex + depth) % 20183;
                    map[x, y] = erosionLevel;
                    total += erosionLevel % 3;
                }
            }
            Console.Write(total);
            Console.Read();
        }
    }
}
