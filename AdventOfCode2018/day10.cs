using System;
using System.IO;

namespace AdventOfCode2018
{
    class Day10
    {
        public static void getBounds(int[] data, out int minX, out int minY, out int maxX, out int maxY)
        {
            minX = maxX = data[0];
            minY = maxY = data[1];

            for(int i = 4; i < data.Length; i += 4)
            {
                if (data[i] < minX)
                    minX = data[i];
                if (data[i] > maxX)
                    maxX = data[i];
                if (data[i + 1] < minY)
                    minY = data[i + 1];
                if (data[i + 1] > maxY)
                    maxY = data[i + 1];
            }
        }

        public static void part1()
        {
            string[] input = new StreamReader("day10.txt").ReadToEnd().Trim().Split('\n');
            int[] data = new int[input.Length * 4];
            for(int i = 0; i < input.Length; i++)
            {
                string[] line = input[i].Split(new[] { ' ', '<', '>', ',' }, StringSplitOptions.RemoveEmptyEntries); 
                int Index = i * 4;
                data[Index + 0] = int.Parse(line[1]);
                data[Index + 1] = int.Parse(line[2]);
                data[Index + 2] = int.Parse(line[4]);
                data[Index + 3] = int.Parse(line[5]);
            }

            int minX, minY, maxX, maxY;
            getBounds(data, out minX, out minY, out maxX, out maxY);

            while (true)
            {
                for(int i = 0; i < data.Length; i += 4)
                {
                    data[i + 0] += data[i + 2];
                    data[i + 1] += data[i + 3];
                }
                int newMinX, newMinY, newMaxX, newMaxY;
                getBounds(data, out newMinX, out newMinY, out newMaxX, out newMaxY);

                if(newMinX < minX || newMinY < minY || newMaxX > maxX || newMaxY > maxY)
                {
                    int width = maxX - minX + 1;
                    int height = maxY - minY + 1;

                    char[,] graph = new char[width, height];
                    for(int x = 0; x < width; x++)
                    {
                        for(int y = 0; y < height; y++)
                        {
                            graph[x, y] = '.';
                        }
                    }
                    //step back one
                    for (int i = 0; i < data.Length; i += 4)
                    {
                        int x = data[i + 0] - data[i + 2] - minX;
                        int y = data[i + 1] - data[i + 3] - minY;

                        graph[x, y] = '#';
                    }

                    for(int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            Console.Write(graph[x, y]);
                        }
                        Console.WriteLine();
                    }
                    break;

                }
                else
                {
                    minX = newMinX;
                    minY = newMinY;
                    maxX = newMaxX;
                    maxY = newMaxY;
                }
            }
            Console.Read();
        }


        public static void part2()
        {
            string[] input = new StreamReader("day10.txt").ReadToEnd().Trim().Split('\n');
            int[] data = new int[input.Length * 4];
            for (int i = 0; i < input.Length; i++)
            {
                string[] line = input[i].Split(new[] { ' ', '<', '>', ',' }, StringSplitOptions.RemoveEmptyEntries);
                int Index = i * 4;
                data[Index + 0] = int.Parse(line[1]);
                data[Index + 1] = int.Parse(line[2]);
                data[Index + 2] = int.Parse(line[4]);
                data[Index + 3] = int.Parse(line[5]);
            }

            int minX, minY, maxX, maxY;
            getBounds(data, out minX, out minY, out maxX, out maxY);

            int t = 0;

            while (true)
            {
                for (int i = 0; i < data.Length; i += 4)
                {
                    data[i + 0] += data[i + 2];
                    data[i + 1] += data[i + 3];
                }
                t++;
                int newMinX, newMinY, newMaxX, newMaxY;
                getBounds(data, out newMinX, out newMinY, out newMaxX, out newMaxY);

                if (newMinX < minX || newMinY < minY || newMaxX > maxX || newMaxY > maxY)
                {
                    int width = maxX - minX + 1;
                    int height = maxY - minY + 1;

                    char[,] graph = new char[width, height];
                    for (int x = 0; x < width; x++)
                    {
                        for (int y = 0; y < height; y++)
                        {
                            graph[x, y] = '.';
                        }
                    }
                    //step back one
                    for (int i = 0; i < data.Length; i += 4)
                    {
                        int x = data[i + 0] - data[i + 2] - minX;
                        int y = data[i + 1] - data[i + 3] - minY;

                        graph[x, y] = '#';
                    }
                    t--;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            Console.Write(graph[x, y]);
                        }
                        Console.WriteLine();
                    }
                    break;

                }
                else
                {
                    minX = newMinX;
                    minY = newMinY;
                    maxX = newMaxX;
                    maxY = newMaxY;
                }
            }
            Console.WriteLine(t);
            Console.Read();
        }
    }
}
