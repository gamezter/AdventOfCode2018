﻿using System;
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
                if (stack.Count == 0)
                {
                    stack.Push(input[i]);
                    continue;
                }
                    
                int delta = stack.Peek() - input[i];
                if (delta == 32 || delta == -32)
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
            int[] chars = new int[26];
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if (stack.Count == 0)
                    {
                        stack.Push(input[j]);
                        continue;
                    }

                    if (input[j] - 65 == i || input[j] - 97 == i)
                        continue;

                    int delta = stack.Peek() - input[j];
                    if (delta == 32 || delta == -32)
                        stack.Pop();
                    else
                        stack.Push(input[j]);
                }
                chars[i] = stack.Count;
                stack.Clear();
            }

            int min = 0;
            for(int i = 0; i < chars.Length; i++)
            {
                if (chars[i] < chars[min])
                    min = i;
            }
            
            Console.WriteLine(chars[min]);
            Console.Read();
        }
    }
}