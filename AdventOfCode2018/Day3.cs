using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day3
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day3.txt").ReadToEnd().Trim().Split('\n');
            int[] fabric = new int[1000 * 1000];
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new[] { '#', '@', ',', ':', 'x' }, StringSplitOptions.RemoveEmptyEntries);
                int sx = int.Parse(parts[1]);
                int sy = int.Parse(parts[2]);
                int width = sx + int.Parse(parts[3]);
                int height = sy + int.Parse(parts[4]);
                for (int x = sx; x < width; x++)
                {
                    for (int y = sy; y < height; y++)
                    {
                        fabric[x + y * 1000]++;
                    }
                }
            }

            int count = 0;
            for(int i = 0; i < fabric.Length; i++)
            {
                if (fabric[i] > 1)
                    count++;
            }
            Console.WriteLine(count);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day3.txt").ReadToEnd().Trim().Split('\n');
            int[] fabric = new int[1000 * 1000];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new[] { '#', '@', ',', ':', 'x' }, StringSplitOptions.RemoveEmptyEntries);
                int sx = int.Parse(parts[1]);
                int sy = int.Parse(parts[2]);
                int width = sx + int.Parse(parts[3]);
                int height = sy + int.Parse(parts[4]);
                for (int x = sx; x < width; x++)
                {
                    for (int y = sy; y < height; y++)
                    {
                        fabric[x + y * 1000]++;
                    }
                }
            }

            int result = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(new[] { '#', '@', ',', ':', 'x' }, StringSplitOptions.RemoveEmptyEntries);
                int sx = int.Parse(parts[1]);
                int sy = int.Parse(parts[2]);
                int width = sx + int.Parse(parts[3]);
                int height = sy + int.Parse(parts[4]);

                bool valid = true;
                for (int x = sx; x < width; x++)
                {
                    for (int y = sy; y < height; y++)
                    {
                        if (fabric[x + y * 1000] != 1)
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (!valid)
                        break;
                }
                if (valid)
                {
                    result = int.Parse(parts[0]);
                    break;
                }  
            }

            Console.WriteLine(result);
            Console.Read();
        }
    }
}
