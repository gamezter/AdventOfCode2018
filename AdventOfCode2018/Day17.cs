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
                for (int x = px - 30 < 0 ? 0 : px - 30; x < px + 30; x++)
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
            int xMax = 0, yMax = 0;
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
                }
                else
                {
                    int y = int.Parse(line[1]);
                    if (y > yMax)
                        yMax = y;
                    int x = int.Parse(line[4]);
                    if (x > xMax)
                        xMax = x;
                }
            }

            char[,] map = new char[xMax + 1, yMax + 1];
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

            int fx = 0, fy = 0;

            while(spouts.Count > 0)
            {
                Pos spout = spouts.Dequeue();
                map[spout.x, spout.y] = '|';
                // fall phase
                while (map[spout.x, spout.y + 1] != '#')
                {
                    map[spout.x, spout.y + 1] = '|';
                    spout.y++;
                    if (spout.y + 1> yMax)
                    {
                        fx = spout.x;
                        fy = spout.y;
                        goto end;
                    }
                }
                // fill phase
                //find left
                int origX = spout.x;
                bool spill = false;
                do
                {
                    spout.x = origX;
                    map[spout.x, spout.y] = '~';
                    while (map[spout.x - 1, spout.y] != '#' && (map[spout.x, spout.y + 1] != 0 && map[spout.x, spout.y + 1] != '|'))
                    {
                        map[spout.x - 1, spout.y] = '~';
                        spout.x--;
                    }
                    if (map[spout.x, spout.y + 1] == 0)
                    {
                        spouts.Enqueue(new Pos(spout.x, spout.y + 1));
                        spill = true;
                    }

                    // find right
                    spout.x = origX;
                    while (map[spout.x + 1, spout.y] != '#' && (map[spout.x, spout.y + 1] != 0 && map[spout.x, spout.y + 1] != '|'))
                    {
                        map[spout.x + 1, spout.y] = '~';
                        spout.x++;
                    }
                    if (map[spout.x, spout.y + 1] == 0)
                    {
                        spouts.Enqueue(new Pos(spout.x, spout.y + 1));
                        spill = true;
                    }

                    if (!spill)
                        spout.y--;
                    drawAround(spout.x, spout.y, map);
                } while (!spill);
            }
            end:
            int count = 0;
            for(int x = 0; x < xMax; x++)
            {
                for(int y = 0; y < yMax; y++)
                {
                    char c = map[x, y];
                    if (x == '|' | x == '~')
                        count++;
                }
            }
            Console.WriteLine(count);
            Console.Read();
        }
    }
}
