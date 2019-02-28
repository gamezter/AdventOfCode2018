using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{

    class Day22
    {
        static int depth = /*8103*/510;
        static int targetX = /*9*/10;
        static int targetY = /*758*/10;

        static int[][] deltas =
        new[]
        {
            new []{ -1, 0 },
            new []{ 0, -1 },
            new []{ 1, 0 },
            new []{ 0, 1 }
        };

        public static void part1()
        {
            int[,] map = new int[targetX + 1, targetY + 1];
            int total = 0;


            for (int x = 0; x <= targetX; x++)
            {
                for(int y = 0; y <= targetY; y++)
                {
                    int geoIndex;
                    if (x == 0 && y == 0 || x == targetX && y == targetY)
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

        struct pos
        {
            public int x;
            public int y;
            public int tool;
            public int time;

            public pos(int x, int y, int t, int time)
            {
                this.x = x;
                this.y = y;
                this.tool = t;
                this.time = time;
            }
        }

        public static void part2()
        {
            int sizeX = targetX * 2;
            int sizeY = targetY * 2;
            
            int[,] map = new int[sizeX, sizeY];
            int[,] times = new int[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    int geoIndex;
                    if (x == 0 && y == 0 || x == targetX && y == targetY)
                        geoIndex = 0;
                    else if (y == 0)
                        geoIndex = x * 16807;
                    else if (x == 0)
                        geoIndex = y * 48271;
                    else
                        geoIndex = map[x - 1, y] * map[x, y - 1];

                    map[x, y] = (geoIndex + depth) % 20183;
                    times[x, y] = int.MaxValue;
                }
            }

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(0, 0, 1, 0));
            times[0, 0] = 0;

            int min = int.MaxValue;

            while (open.Count != 0)
            {
                pos p = open.Dequeue();

                if(p.x == targetX && p.y == targetY)
                {
                    int nTime = p.tool == 1 ? p.time : p.time + 7;
                    if (min > nTime)
                        min = nTime;
                    continue;
                }

                for(int i = 0; i < 4; i++)
                {
                    int nx = p.x + deltas[i][0];
                    int ny = p.y + deltas[i][1];
                    if (nx == -1 || nx == sizeX || ny == -1 || ny == sizeY)
                        continue;

                    float time = times[nx, ny];

                    if (p.tool == map[nx, ny] % 3)
                    {
                        if (time > p.time + 7)
                        {
                            times[nx, ny] = p.time + 8;
                            open.Enqueue(new pos(nx, ny, (p.tool + 1) % 3, p.time + 8));
                            open.Enqueue(new pos(nx, ny, (p.tool + 2) % 3, p.time + 8));
                        }
                    }
                    else
                    {
                        if (time > p.time)
                        {
                            times[nx, ny] = p.time + 1;
                            open.Enqueue(new pos(nx, ny, p.tool, p.time + 1));
                        }
                    }
                }
            }

            Console.Write(min);
            Console.Read();
        }
    }
}
