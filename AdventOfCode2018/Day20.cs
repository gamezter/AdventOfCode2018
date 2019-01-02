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
        public static void debug2(int[,] map, int w, int h)
        {
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (map[x, y] != -1)
                        Console.Write(map[x, y] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void part1()
        {
            int w = 211;
            int h = 205;

            string line = new StreamReader("day20.txt").ReadToEnd();
            Stack<pos> s = new Stack<pos>();
            char[,] map = new char[w, h];
            int x = w / 2, y = h / 2;
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

            //debug(map, w, h);

            int[,] distances = new int[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    distances[i, j] = -1;
            distances[w / 2, h / 2] = 0;

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(w / 2, h / 2));
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
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (distances[j, i] > max)
                        max = distances[j, i];
                }
            }

            Console.WriteLine(max);
            Console.Read();
        }

        public static void part2()
        {
            int w = 211;
            int h = 205;

            string line = new StreamReader("day20.txt").ReadToEnd();
            Stack<pos> s = new Stack<pos>();
            char[,] map = new char[w, h];
            int x = w / 2, y = h / 2;
            addWalls(x, y, map);

            for (int i = 1; i < line.Length - 1; i++)
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

            //debug(map, w, h);

            int[,] distances = new int[w, h];
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    distances[i, j] = -1;
            distances[w / 2, h / 2] = 0;

            Queue<pos> open = new Queue<pos>();
            open.Enqueue(new pos(w / 2, h / 2));
            while (open.Count > 0)
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

            int count = 0;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (distances[j, i] >= 1000)
                        count++;
                }
            }

            Console.WriteLine(count);
            Console.Read();
        }
    }
}
