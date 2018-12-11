using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Day5
    {
        public static void part1()
        {
            string input = new StreamReader("day5.txt").ReadToEnd().Trim();
            Stack<char> stack = new Stack<char>();
            for(int i = 0; i < input.Length; i++)
            {
                if (stack.Count != 0 && (stack.Peek() ^ input[i]) == 32)
                    stack.Pop();
                else
                    stack.Push(input[i]);
            }
            Console.WriteLine(stack.Count);
            Console.Read();
        }

        public static void part2()
        {
            string input = new StreamReader("day5.txt").ReadToEnd().Trim();
            Stack<char> stack = new Stack<char>();
            int min = int.MaxValue;

            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j] - i == 'a' || input[j] - i == 'A')
                        continue;

                    if (stack.Count != 0 && (stack.Peek() ^ input[j]) == 32)
                        stack.Pop();
                    else
                        stack.Push(input[j]);
                }
                if (stack.Count < min)
                    min = stack.Count;
                stack.Clear();
            }

            Console.WriteLine(min);
            Console.Read();
        }
    }
}
