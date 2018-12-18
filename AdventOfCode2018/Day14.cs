using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Day14
    {
        public static void part1()
        {
            int input = 652601;

            List<int> scores = new List<int>();
            scores.Add(3);
            scores.Add(7);
            
            int index1 = 0;
            int index2 = 1;
            while(scores.Count < input + 10)
            {
                int sum = scores[index1] + scores[index2];
                if (sum > 9)
                {
                    scores.Add(1);
                    scores.Add(sum - 10);
                }
                else
                    scores.Add(sum);
                index1 = (index1 + scores[index1] + 1) % scores.Count;
                index2 = (index2 + scores[index2] + 1) % scores.Count;
            }
            for(int i = input; i < input + 10; i++)
            {
                Console.Write(scores[i] + " ");
            }
            Console.WriteLine();
            Console.Read();
        }

        public static void part2()
        {
            int[] input = new[] { 6, 5, 2, 6, 0, 1 };

            List<int> scores = new List<int>();
            scores.Add(3);
            scores.Add(7);

            int index1 = 0;
            int index2 = 1;

            int left = 0;
            while (true)
            {
                int sum = scores[index1] + scores[index2];
                if (sum > 9)
                {
                    scores.Add(1);
                    scores.Add(sum - 10);
                }
                else
                    scores.Add(sum);
                index1 = (index1 + scores[index1] + 1) % scores.Count;
                index2 = (index2 + scores[index2] + 1) % scores.Count;
                if (left + input.Length < scores.Count)
                {
                    bool found = true;
                    for(int i = 0; i < input.Length; i++)
                    {
                        if (scores[left + i] != input[i])
                        {
                            found = false;
                            break;
                        }
                    }
                    if (found)
                    {
                        Console.WriteLine(left);
                        Console.Read();
                    }
                    else
                    {
                        left++;
                    }
                }
            }
        }
    }
}
