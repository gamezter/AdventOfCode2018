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
            new []{ 1, 0 },
            new []{ 0, -1 },
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

        public enum tool { NOTHING, TORCH, CLIMBINGGEAR };
        public enum type { ROCKY = 0, WET = 1, NARROW = 2};

        struct pos
        {
            public int x;
            public int y;
            
            public tool t;
            public int time;
            public pos(int x, int y, tool t, int time)
            {
                this.x = x;
                this.y = y;
                this.t = t;
                this.time = time;
            }
        }

        public static void part2()
        {
            int[,] map = new int[targetX + 1, targetY + 1];
            type[,] mapType = new type[targetX + 1, targetY + 1];

            for (int x = 0; x <= targetX; x++)
            {
                for (int y = 0; y <= targetY; y++)
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
                    mapType[x, y] = (type)(erosionLevel % 3);
                }
            }

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(0, 0, tool.TORCH, 0));

            int[,] times = new int[targetX + 1, targetY + 1];

            while (open.Count != 0)
            {
                pos p = open.Dequeue();
                for(int i = 0; i < 4; i++)
                {
                    int nx = p.x + deltas[i][0];
                    int ny = p.y + deltas[i][1];
                    if (nx == -1 || nx == targetX + 1 || ny == -1 || ny == targetY + 1)
                        continue;

                    type t = mapType[nx, ny];
                    switch (p.t)
                    {
                        case tool.NOTHING:
                            if (t == type.ROCKY)
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 7)
                                {
                                    times[nx, ny] = p.time + 7;
                                    open.Enqueue(new pos(nx, ny, tool.CLIMBINGGEAR, p.time + 7));
                                    open.Enqueue(new pos(nx, ny, tool.TORCH, p.time + 7));
                                }
                            }
                            else
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 1)
                                {
                                    times[nx, ny] = p.time + 1;
                                    open.Enqueue(new pos(nx, ny, tool.NOTHING, p.time + 1));
                                }
                            }

                            break;
                        case tool.TORCH:
                            if (t == type.WET)
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 7)
                                {
                                    times[nx, ny] = p.time + 7;
                                    open.Enqueue(new pos(nx, ny, tool.CLIMBINGGEAR, p.time + 7));
                                    open.Enqueue(new pos(nx, ny, tool.NOTHING, p.time + 7));
                                }
                            }
                            else
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 1)
                                {
                                    times[nx, ny] = p.time + 1;
                                    open.Enqueue(new pos(nx, ny, tool.TORCH, p.time + 1));
                                }
                            }
                            break;
                        case tool.CLIMBINGGEAR:
                            if (t == type.NARROW)
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 7)
                                {
                                    times[nx, ny] = p.time + 7;
                                    open.Enqueue(new pos(nx, ny, tool.NOTHING, p.time + 7));
                                    open.Enqueue(new pos(nx, ny, tool.TORCH, p.time + 7));
                                }
                            }
                            else
                            {
                                if (times[nx, ny] == 0 || times[nx, ny] > p.time + 1)
                                {
                                    times[nx, ny] = p.time + 1;
                                    open.Enqueue(new pos(nx, ny, tool.CLIMBINGGEAR, p.time + 1));
                                }
                            }
                            break;
                    }
                }
            }

            Console.Write(times[targetX, targetY]);
            Console.Read();
        }
    }
}
