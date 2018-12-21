using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day17
    {
        public struct Pos
        {
            public int x, y;
            public Pos(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public static void drawAround(int px, int py, char[,] map)
        {
            for (int y = py - 10 < 0 ? 0 : py - 10; y < py + 10; y++)
            {
                for (int x = px - 10 < 0 ? 0 : px - 10; x < px + 10; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            Console.Read();
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day17.txt").ReadToEnd().Trim().Split('\n');
            int xMax = 0, yMax = 0, yMin = 5000;
            for(int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if(line[0] == "x")
                {
                    int x = int.Parse(line[1]);
                    if (x > xMax)
                        xMax = x;
                    int y = int.Parse(line[4]);
                    if (y > yMax)
                        yMax = y;
                    if (y < yMin)
                        yMin = y;
                }
                else
                {
                    int y = int.Parse(line[1]);
                    if (y > yMax)
                        yMax = y;
                    int x = int.Parse(line[4]);
                    if (x > xMax)
                        xMax = x;
                    if (y < yMin)
                        yMin = y;
                }
            }

            char[,] map = new char[xMax + 10, yMax + 10];
            // populate map
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (line[0] == "x")
                {
                    int x = int.Parse(line[1]);
                    int y0 = int.Parse(line[3]);
                    int y1 = int.Parse(line[4]);
                    for(int y = y0; y <= y1; y++)
                        map[x, y] = '#';
                }
                else
                {
                    int y = int.Parse(line[1]);
                    int x0 = int.Parse(line[3]);
                    int x1 = int.Parse(line[4]);
                    for (int x = x0; x <= x1; x++)
                        map[x, y] = '#';
                }
            }

            Queue<Pos> spouts = new Queue<Pos>();
            spouts.Enqueue(new Pos(500, 0));
            skip:
            while (spouts.Count > 0)
            {
                Pos spout = spouts.Dequeue();
                if (map[spout.x, spout.y] == '~' || map[spout.x, spout.y] == '|')
                    continue;
                // fall phase
                while (map[spout.x, spout.y] != '#')
                {
                    map[spout.x, spout.y] = '|';
                    spout.y++;
                    if (spout.y > yMax)
                        goto skip;
                }
                // fill phase
                bool spill = false;
                int y = spout.y - 1;
                while (!spill)
                {
                    int x = spout.x;
                    map[x, y] = '~';
                    for (x = spout.x - 1;  map[x, y] != '#'; x--)
                    {
                        if(map[x, y + 1] != 0)
                        {
                            map[x, y] = '~';
                        }
                        else
                        {
                            spouts.Enqueue(new Pos(x, y));
                            spill = true;
                            break;
                        }

                    }
                    for (x = spout.x + 1;  map[x, y] != '#'; x++)
                    {
                        if(map[x, y + 1] != 0)
                        {
                            map[x, y] = '~';
                        }
                        else
                        {
                            spouts.Enqueue(new Pos(x, y));
                            spill = true;
                            break;
                        }
                        
                    }
                    y--;
                }
            }
            int count = 0;
            for(int x = 0; x < xMax + 2; x++)
            {
                for(int y = yMin; y <= yMax; y++)
                {
                    char c = map[x, y];
                    if (c == '|' || c == '~')
                        count++;
                }
            }
            Console.WriteLine(count);
            Console.Read();
        }
    }
}
