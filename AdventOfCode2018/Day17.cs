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
            int xMax = 0, yMax = 0, xMin = 500000, yMin = 500000;
            for(int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                int axis = int.Parse(line[1]);
                int min = int.Parse(line[3]);
                int max = int.Parse(line[4]);

                if (line[0] == "x")
                {
                    if (axis < xMin)
                        xMin = axis;
                    if (axis > xMax)
                        xMax = axis;
                    if (min < yMin)
                        yMin = min;
                    if (max > yMax)
                        yMax = max;
                }
                else
                {
                    if (axis < yMin)
                        yMin = axis;
                    if (axis > yMax)
                        yMax = axis;
                    if (min < xMin)
                        xMin = min;
                    if (max > xMax)
                        xMax = max;
                }
            }
            xMin--;//space for water falling
            xMax++;

            char[,] map = new char[xMax + 1 - xMin, yMax + 1 - yMin];
            // populate map
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (line[0] == "x")
                {
                    int x = int.Parse(line[1]) - xMin;
                    int y0 = int.Parse(line[3]) - yMin;
                    int y1 = int.Parse(line[4]) - yMin;
                    for(int y = y0; y <= y1; y++)
                        map[x, y] = '#';
                }
                else
                {
                    int y = int.Parse(line[1]) - yMin;
                    int x0 = int.Parse(line[3]) - xMin;
                    int x1 = int.Parse(line[4]) - xMin;
                    for (int x = x0; x <= x1; x++)
                        map[x, y] = '#';
                }
            }

            Queue<Pos> spouts = new Queue<Pos>();
            spouts.Enqueue(new Pos(500 - xMin, 0));
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
                    if (spout.y > yMax - yMin)
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
                        char below = map[x, y + 1];
                        if (below != 0 && below != '|')
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
                        char below = map[x, y + 1];
                        if (below != 0 && below != '|')
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
            for(int x = 0; x < xMax + 1 - xMin; x++)
            {
                for(int y = 0; y < yMax + 1 - yMin; y++)
                {
                    char c = map[x, y];
                    if (c == '|' || c == '~')
                        count++;
                }
            }
            Console.WriteLine(count);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day17.txt").ReadToEnd().Trim().Split('\n');
            int xMax = 0, yMax = 0, xMin = 500000, yMin = 500000;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                int axis = int.Parse(line[1]);
                int min = int.Parse(line[3]);
                int max = int.Parse(line[4]);

                if (line[0] == "x")
                {
                    if (axis < xMin)
                        xMin = axis;
                    if (axis > xMax)
                        xMax = axis;
                    if (min < yMin)
                        yMin = min;
                    if (max > yMax)
                        yMax = max;
                }
                else
                {
                    if (axis < yMin)
                        yMin = axis;
                    if (axis > yMax)
                        yMax = axis;
                    if (min < xMin)
                        xMin = min;
                    if (max > xMax)
                        xMax = max;
                }
            }
            xMin--;//space for water falling
            xMax++;

            char[,] map = new char[xMax + 1 - xMin, yMax + 1 - yMin];
            // populate map
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(new[] { '=', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (line[0] == "x")
                {
                    int x = int.Parse(line[1]) - xMin;
                    int y0 = int.Parse(line[3]) - yMin;
                    int y1 = int.Parse(line[4]) - yMin;
                    for (int y = y0; y <= y1; y++)
                        map[x, y] = '#';
                }
                else
                {
                    int y = int.Parse(line[1]) - yMin;
                    int x0 = int.Parse(line[3]) - xMin;
                    int x1 = int.Parse(line[4]) - xMin;
                    for (int x = x0; x <= x1; x++)
                        map[x, y] = '#';
                }
            }

            Queue<Pos> spouts = new Queue<Pos>();
            spouts.Enqueue(new Pos(500 - xMin, 0));
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
                    if (spout.y > yMax - yMin)
                        goto skip;
                }
                // fill phase
                bool spill = false;
                int y = spout.y - 1;
                while (!spill)
                {
                    map[spout.x, y] = '~';
                    int leftX, rightX;

                    for (leftX = spout.x - 1; map[leftX, y] != '#'; leftX--)
                    {
                        char below = map[leftX, y + 1];
                        if (below != 0 && below != '|')
                        {
                            map[leftX, y] = '~';
                        }
                        else
                        {
                            spouts.Enqueue(new Pos(leftX, y));
                            spill = true;
                            break;
                        }
                    }

                    for (rightX = spout.x + 1; map[rightX, y] != '#'; rightX++)
                    {
                        char below = map[rightX, y + 1];
                        if (below != 0 && below != '|')
                        {
                            map[rightX, y] = '~';
                        }
                        else
                        {
                            spouts.Enqueue(new Pos(rightX, y));
                            spill = true;
                            break;
                        }
                    }

                    if (spill)
                    {
                        for(int x = leftX + 1; x < rightX; x++)
                        {
                            map[x, y] = '|';
                        }
                    }
                    //drawAround(spout.x, y, map);
                    y--;
                }
            }
            int count = 0;
            for (int x = 0; x < xMax + 1 - xMin; x++)
            {
                for (int y = 0; y < yMax + 1 - yMin; y++)
                {
                    char c = map[x, y];
                    if (c == '~')
                        count++;
                }
            }
            Console.WriteLine(count);
            Console.Read();
        }
    }
}
