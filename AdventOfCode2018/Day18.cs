using System;
using System.IO;

namespace AdventOfCode2018
{
    class Day18
    {
        public static int[][] offsets = new[]
        {
            new []{ -1, -1 },
            new []{ 0, -1 },
            new []{ 1, -1 },
            new []{ -1, 0 },
            new []{ 1, 0 },
            new []{ -1, 1 },
            new []{ 0, 1 },
            new []{ 1, 1 },
        };


        public static void part1()
        {
            string[] lines = new StreamReader("day18.txt").ReadToEnd().Trim().Split('\n');

            char[][] state = new char[52][];
            state[0] = new char[52];
            state[51] = new char[52];
            for (int y = 0; y < 50; y++)
            {
                state[y + 1] = new char[52];
                for (int x = 0; x < 50; x++)
                {
                    state[y + 1][x + 1] = lines[y][x];
                }
            }

            for (int i = 0; i < 10; i++)
            {
                char[][] nextState = new char[52][];
                nextState[0] = new char[52];
                nextState[51] = new char[52];
                for (int y = 1; y < 51; y++)
                {
                    nextState[y] = new char[52];
                    for (int x = 1; x < 51; x++)
                    {
                        int trees = 0, yard = 0;

                        for (int j = 0; j < 8; j++)
                        {
                            char c = state[y + offsets[j][1]][x + offsets[j][0]];
                            if (c == '|')
                                trees++;
                            else if (c == '#')
                                yard++;
                        }

                        switch (state[y][x])
                        {
                            case '.':
                                nextState[y][x] = trees > 2 ? '|' : '.';
                                break;
                            case '|':
                                nextState[y][x] = yard > 2 ? '#' : '|';
                                break;
                            case '#':
                                nextState[y][x] = (yard != 0 && trees != 0) ? '#' : '.';
                                break;

                        }
                    }
                }
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                for (int y = 0; y < 52; y++)
                {
                    Console.WriteLine(new string(nextState[y]));
                }
                Console.Read();
                state = nextState;
            }

            int wood = 0, lumberyard = 0;
            for (int y = 1; y < 51; y++)
            {
                for (int x = 1; x < 51; x++)
                {
                    char c = state[y][x];
                    if (c == '|')
                        wood++;
                    else if (c == '#')
                        lumberyard++;
                }
            }
            Console.WriteLine(wood * lumberyard);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day18.txt").ReadToEnd().Trim().Split('\n');

            char[][] state = new char[52][];
            state[0] = new char[52];
            state[51] = new char[52];
            for (int y = 0; y < 50; y++)
            {
                state[y + 1] = new char[52];
                for (int x = 0; x < 50; x++)
                {
                    state[y + 1][x + 1] = lines[y][x];
                }
            }

            for (int i = 0; i < 1000000000; i++)
            {
                char[][] nextState = new char[52][];
                nextState[0] = new char[52];
                nextState[51] = new char[52];
                for (int y = 1; y < 51; y++)
                {
                    nextState[y] = new char[52];
                    for (int x = 1; x < 51; x++)
                    {
                        int trees = 0, yard = 0;

                        for(int j = 0; j < 8; j++)
                        {
                            char c = state[y + offsets[j][1]][x + offsets[j][0]];
                            if (c == '|')
                                trees++;
                            else if (c == '#')
                                yard++;
                        }

                        switch (state[y][x])
                        {
                            case '.':
                                nextState[y][x] = trees > 2 ? '|' : '.';
                                break;
                            case '|':
                                nextState[y][x] = yard > 2 ? '#' : '|';
                                break;
                            case '#':
                                nextState[y][x] = (yard != 0 && trees != 0) ? '#' : '.';
                                break;

                        }
                    }
                }
                
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
                for (int y = 0; y < 52; y++)
                {
                    Console.WriteLine(new string(nextState[y]));
                }
                Console.Read();
                state = nextState;
            }

            int wood = 0, lumberyard = 0;
            for (int y = 1; y < 51; y++)
            {
                for (int x = 1; x < 51; x++)
                {
                    char c = state[y][x];
                    if (c == '|')
                        wood++;
                    else if (c == '#')
                        lumberyard++;
                }
            }
            Console.WriteLine(wood * lumberyard);
            Console.Read();
        }
    }
}
