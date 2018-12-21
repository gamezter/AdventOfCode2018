using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day18
    {
        public static void part1()
        {
            string[] lines = new StreamReader("day18.txt").ReadToEnd().Trim().Split('\n');

            char[][] state = new char[50][];
            for(int y = 0; y < 50; y++)
            {
                state[y] = new char[50];
                for(int x = 0; x < 50; x++)
                {
                    state[y][x] = lines[y][x];
                }
            }

            for(int i = 0; i < 10; i++)
            {
                char[][] nextState = new char[50][];
                for (int y = 0; y < 50; y++)
                {
                    nextState[y] = new char[50];
                    for (int x = 0; x < 50; x++)
                    {
                        int ground = 0, trees = 0, yard = 0;

                        for(int dx = -1; dx < 2; dx++)
                        {
                            for(int dy = -1; dy < 2; dy++)
                            {
                                if (x + dx < 0 || x + dx > 49)
                                    continue;
                                if (y + dy < 0 || y + dy > 49)
                                    continue;
                                if (dx == 0 && dy == 0)
                                    continue;
                                char c = state[y + dy][x + dx];
                                if (c == '.')
                                    ground++;
                                else if (c == '|')
                                    trees++;
                                else
                                    yard++;
                            }
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
                                nextState[y][x] = (yard > 0 && trees > 0) ? '#' : '.';
                                break;

                        }
                    }
                }
                state = nextState;
            }

            int wood = 0, lumberyard = 0;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
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

            char[][] state = new char[50][];
            for (int y = 0; y < 50; y++)
            {
                state[y] = new char[50];
                for (int x = 0; x < 50; x++)
                {
                    state[y][x] = lines[y][x];
                }
            }

            for (int i = 0; i < 1000000000; i++)
            {
                char[][] nextState = new char[50][];
                for (int y = 0; y < 50; y++)
                {
                    nextState[y] = new char[50];
                    for (int x = 0; x < 50; x++)
                    {
                        int trees = 0, yard = 0;

                        for (int dx = -1; dx < 2; dx++)
                        {
                            if (x + dx < 0 || x + dx > 49)
                                continue;
                            for (int dy = -1; dy < 2; dy++)
                            {
                                if (y + dy < 0 || y + dy > 49)
                                    continue;
                                if (dx == 0 && dy == 0)
                                    continue;
                                char c = state[y + dy][x + dx];
                                if (c == '|')
                                    trees++;
                                else if (c == '#')
                                    yard++;
                            }
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
                for (int y = 0; y < 50; y++)
                {
                    Console.WriteLine(new string(nextState[y]));
                }
                Console.Read();
                state = nextState;
            }

            int wood = 0, lumberyard = 0;
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
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
