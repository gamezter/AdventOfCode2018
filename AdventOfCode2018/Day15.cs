using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day15
    {
        public struct Unit
        {
            public int x, y, health;
        }

        public struct pos
        {
            public int x, y;

            public pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static void fillDistances(int x, int y, int[,] distances, char[,] map)
        {
            for (int i = 0; i < 32; i++)
                for (int j = 0; j < 32; j++)
                    distances[i, j] = -1;

            distances[x, y] = 0;

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(x, y));
            while(open.Count > 0)
            {
                pos c = open.Dequeue();
                int nextVal = distances[c.x, c.y] + 1;
                if (map[c.x + 1, c.y] == '.')
                {
                    int val = distances[c.x + 1, c.y];
                    if(val == -1 || val > nextVal)
                    {
                        distances[c.x + 1, c.y] = nextVal;
                        open.Enqueue(new pos(c.x + 1, c.y));
                    }                        
                }
                if (map[c.x, c.y + 1] == '.')
                {
                    int val = distances[c.x, c.y + 1];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y + 1] = nextVal;
                        open.Enqueue(new pos(c.x, c.y + 1));
                    }
                }
                if (map[c.x - 1, c.y] == '.')
                {
                    int val = distances[c.x - 1, c.y];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x - 1, c.y] = nextVal;
                        open.Enqueue(new pos(c.x - 1, c.y));
                    }
                }
                if (map[c.x, c.y - 1] == '.')
                {
                    int val = distances[c.x, c.y - 1];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y - 1] = nextVal;
                        open.Enqueue(new pos(c.x, c.y - 1));
                    }
                }
            }
        }

        public static void debugDistances(int[,] distances)
        {
            for(int y = 0; y < 32; y++)
            {
                for(int x = 0; x < 32; x++)
                {
                    if(distances[x, y] < 10 && distances[x,y] != -1)
                        Console.Write(" " + distances[x, y] + " ");
                    else
                        Console.Write(distances[x, y] + " ");
                }
                Console.WriteLine();
            }
            Console.Read();
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day15.txt").ReadToEnd().Trim().Split('\n');
            char[,] map = new char[lines[0].Length, lines.Length];
            List<Unit> goblins = new List<Unit>();
            List<Unit> elves = new List<Unit>();

            for(int x = 0; x < 32; x++)
            {
                for(int y = 0; y < 32; y++)
                {
                    if (lines[y][x] == 'E')
                        elves.Add(new Unit() { x = x, y = y, health = 200 });
                    if (lines[y][x] == 'G')
                        goblins.Add(new Unit() { x = x, y = y, health = 200 });
                    map[x, y] = lines[y][x];
                }
            }
            while (true)
            {
                elves.Sort((e1, e2) => e1.y == e2.y ? e1.x < e2.x ? -1 : 1 : e1.y < e2.y ? -1 : 1);
                int[,] distances = new int[32, 32];
                for(int i = 0; i < elves.Count; i++)
                {
                    fillDistances(elves[i].x, elves[i].y, distances, map);

                    debugDistances(distances);
                }
            }
        }
    }
}
