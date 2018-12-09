using System;
using System.IO;

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

            int[] counts = new int[coord.Length / 2];
            for(int i = 0; i < width * height; i++)
            {
                int x = i % width + minX;
                int y = i / width + minY;
                int closest = findClosest(x, y, coord);
                if(closest != -1)
                {
                    if (x == minX || x == maxX || y == minY || y == maxY)
                        counts[closest] = -1;
                    else if (counts[closest] != -1)
                        counts[closest]++;
                }
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
            int count = 0;
            for (int i = 0; i < width * height; i++)
            {
                int x = i % width + minX;
                int y = i / width + minY;
                if(findTotalDistancce(x, y, coord) < 10000)
                    count++;
            }
            Console.WriteLine(count);
            Console.Read();
        }
    }
}
