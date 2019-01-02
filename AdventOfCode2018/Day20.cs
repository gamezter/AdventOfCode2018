using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day20
    {
        public static int[,] o =
        {
            {-1,-1 },
            { 0,-1 },
            { 1,-1 },
            {-1, 0 },
            { 1, 0 },
            {-1, 1 },
            { 0, 1 },
            { 1, 1 },
        };

        public struct pos
        {
            public int x, y;
            public pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static void addWalls(int x, int y, char[,] map)
        {
            for (int i = 0; i < 8; i++)
            {
                char c = map[x + o[i, 0], y + o[i, 1]];
                if (c == 0)
                    map[x + o[i, 0], y + o[i, 1]] = '#';
            }
        }

        public static void debug(char[,] map, int w, int h)
        {
            for(int y = 0; y < h; y++)
            {
                for(int x = 0; x < w; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        public static void part1()
        {
            string line = new StreamReader("day20.txt").ReadToEnd();
            Stack<pos> s = new Stack<pos>();
            char[,] map = new char[300, 300];
            int x = 150, y = 150;
            addWalls(x, y, map);

            for(int i = 1; i < line.Length - 1; i++)
            {
                switch (line[i])
                {
                    case 'N':
                        map[x, --y] = '-';
                        map[x, --y] = '.';
                        addWalls(x, y, map);
                        break;
                    case 'S':
                        map[x, ++y] = '-';
                        map[x, ++y] = '.';
                        addWalls(x, y, map);
                        break;
                    case 'E':
                        map[++x, y] = '|';
                        map[++x, y] = '.';
                        addWalls(x, y, map);
                        break;
                    case 'W':
                        map[--x, y] = '|';
                        map[--x, y] = '.';
                        addWalls(x, y, map);
                        break;
                    case '(':
                        s.Push(new pos(x, y));
                        break;
                    case ')':
                        pos p = s.Pop();
                        x = p.x;
                        y = p.y;
                        break;
                    case '|':
                        x = s.Peek().x;
                        y = s.Peek().y;
                        break;
                }
            }

            //debug(map, 300, 300);

            int[,] distances = new int[300, 300];
            for (int i = 0; i < 300; i++)
                for (int j = 0; j < 300; j++)
                    distances[i, j] = -1;

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(150, 150));
            while(open.Count > 0)
            {
                pos c = open.Dequeue();
                int nextVal = distances[c.x, c.y] + 1;
                if (map[c.x + 1, c.y] == '|')
                {
                    int val = distances[c.x + 2, c.y];

                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x + 2, c.y] = nextVal;
                        open.Enqueue(new pos(c.x + 2, c.y));
                    }
                }
                if (map[c.x - 1, c.y] == '|')
                {
                    int val = distances[c.x - 2, c.y];

                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x - 2, c.y] = nextVal;
                        open.Enqueue(new pos(c.x - 2, c.y));
                    }
                }

                if (map[c.x, c.y + 1] == '-')
                {
                    int val = distances[c.x, c.y + 2];

                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y + 2] = nextVal;
                        open.Enqueue(new pos(c.x, c.y + 2));
                    }
                }
                if (map[c.x, c.y - 1] == '-')
                {
                    int val = distances[c.x, c.y - 2];

                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y - 2] = nextVal;
                        open.Enqueue(new pos(c.x, c.y - 2));
                    }
                }
            }

            int max = 0;
            for (int i = 0; i < 300; i++)
            {
                for (int j = 0; j < 300; j++)
                {
                    if (map[j, i] > max)
                        max = map[j, i];
                }
            }

            Console.WriteLine(max);
            Console.Read();
        }
    }
}
