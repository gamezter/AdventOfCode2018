using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day6
    {
        public static int findClosest(int x, int y, int[] coord)
        {
            int closest = 0;
            int distance = Math.Abs(coord[0] - x) + Math.Abs(coord[1] - y);
            for(int i = 2; i < coord.Length; i += 2)
            {
                int dx = Math.Abs(x - coord[i]);
                int dy = Math.Abs(y - coord[i + 1]);

                if (dx + dy == distance)
                    closest = -1;
                else if (dx + dy < distance)
                {
                    distance = dx + dy;
                    closest = i / 2;
                }
            }
            return closest;
        }

        public static int findTotalDistancce(int x, int y, int[] coord)
        {
            int total = 0;
            for (int i = 0; i < coord.Length; i += 2)
            {
                int dx = Math.Abs(x - coord[i]);
                int dy = Math.Abs(y - coord[i + 1]);

                total += dx + dy;
            }
            return total;
        }

        public static void part1()
        {
            string[] stringCoordinates = new StreamReader("day6.txt").ReadToEnd().Trim().Split(new[] { '\n', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] coord = new int[stringCoordinates.Length];
            for(int i = 0; i < stringCoordinates.Length; i++)
            {
                coord[i] = int.Parse(stringCoordinates[i]);
            }
            int minX = coord[0], minY = coord[1], maxX = coord[0], maxY = coord[1];
            for(int i = 0; i < coord.Length; i += 2)
            {
                if (coord[i] < minX)
                    minX = coord[i];
                if (coord[i] > maxX)
                    maxX = coord[i];
                if (coord[i + 1] < minY)
                    minY = coord[i + 1];
                if (coord[i + 1] > maxY)
                    maxY = coord[i + 1];
            }

            int width = maxX - minX;
            int height = maxY - minY;
            int[] area = new int[width * height];
            for(int i = 0; i < area.Length; i++)
            {
                int x = i % width + minX;
                int y = i / width + minY;
                area[i] = findClosest(x, y, coord);
            }

            int[] counts = new int[coord.Length / 2];
            for(int i = 0; i < area.Length; i++)
            {
                if(area[i] != -1)
                    counts[area[i]]++;
            }
            for(int i = 0; i < width; i++)
            {
                int topX = i;
                int bottomX = width * (height - 1) + i;
                if (area[topX] != -1)
                    counts[area[topX]] = 0;
                if (area[bottomX] != -1)
                    counts[area[bottomX]] = 0;
            }
            for (int i = 0; i < height; i++)
            {
                int leftY = i * width;
                int rightY = leftY + width - 1;
                if (area[leftY] != -1)
                    counts[area[leftY]] = 0;
                if (area[rightY] != -1)
                    counts[area[rightY]] = 0;
            }
            int max = 0;
            for(int i = 0; i < counts.Length; i++)
            {
                if (counts[i] > max)
                    max = counts[i];
            }
            Console.WriteLine(max);
            Console.Read();
        }

        public static void part2()
        {
            string[] stringCoordinates = new StreamReader("day6.txt").ReadToEnd().Trim().Split(new[] { '\n', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            int[] coord = new int[stringCoordinates.Length];
            for (int i = 0; i < stringCoordinates.Length; i++)
            {
                coord[i] = int.Parse(stringCoordinates[i]);
            }
            int minX = coord[0], minY = coord[1], maxX = coord[0], maxY = coord[1];
            for (int i = 0; i < coord.Length; i += 2)
            {
                if (coord[i] < minX)
                    minX = coord[i];
                if (coord[i] > maxX)
                    maxX = coord[i];
                if (coord[i + 1] < minY)
                    minY = coord[i + 1];
                if (coord[i + 1] > maxY)
                    maxY = coord[i + 1];
            }

            int width = maxX - minX;
            int height = maxY - minY;
            int[] area = new int[width * height];
            for (int i = 0; i < area.Length; i++)
            {
                int x = i % width + minX;
                int y = i / width + minY;
                area[i] = findTotalDistancce(x, y, coord) < 10000 ? 1 : 0;
            }

            int count = 0;
            for (int i = 0; i < area.Length; i++)
            {
                if (area[i] == 1)
                    count++;
            }
            Console.WriteLine(count);
            Console.Read();
        }
    }
}
