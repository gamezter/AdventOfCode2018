using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Day12
    {
        public static void part1()
        {
            string[] input = new StreamReader("day12.txt").ReadToEnd().Trim().Split('\n');
            string initialState = input[0].Split()[2];
            List<int> plantState = new List<int>();
            HashSet<int> rules = new HashSet<int>();
            for(int i = 0; i < initialState.Length; i++)
            {
                if (initialState[i] == '#')
                    plantState.Add(i);
            }

            for(int i = 2; i < input.Length; i++)
            {
                string[] rule = input[i].Split();
                if (rule[2][0] == '.')
                    continue;
                int hash = 0;
                for(int j = 0; j < 5; j++)
                {
                    if (rule[0][j] == '#')
                        hash |= 1 << j;
                }
                
                rules.Add(hash);
            }

            for(int i = 0; i < 20; i++)
            {
                int minIndex = plantState[0] - 2;
                int maxIndex = plantState[plantState.Count - 1] + 2;
                List<int> newState = new List<int>();

                for(int j = minIndex; j <= maxIndex; j++)
                {
                    int currentVal = 0;
                    for (int k = j - 2; k <= j + 2; k++)
                    {
                        if (plantState.Contains(k))
                            currentVal |= 1 << (k - j + 2);
                    }
                    if (rules.Contains(currentVal))
                        newState.Add(j);
                        
                }
                plantState = newState;
            }

            int sum = 0;
            for(int i = 0; i < plantState.Count; i++)
            {
                sum += plantState[i];
            }
            Console.WriteLine(sum);
            Console.Read();
        }

        struct state
        {
            public long iteration;
            public long minIndex;
        }

        public static void part2()
        {
            string[] input = new StreamReader("day12.txt").ReadToEnd().Trim().Split('\n');
            string initialState = input[0].Split()[2];
            List<int> plantState = new List<int>();
            HashSet<int> rules = new HashSet<int>();
            for (int i = 0; i < initialState.Length; i++)
            {
                if (initialState[i] == '#')
                    plantState.Add(i);
            }

            for (int i = 2; i < input.Length; i++)
            {
                string[] rule = input[i].Split();
                if (rule[2][0] == '.')
                    continue;
                int hash = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (rule[0][j] == '#')
                        hash |= 1 << j;
                }

                rules.Add(hash);
            }

            Dictionary<long, state> met = new Dictionary<long, state>();

            long sum = 0;

            for (long i = 0; i < 50000000000; i++)
            {
                int minIndex = plantState[0] - 2;
                int maxIndex = plantState[plantState.Count - 1] + 2;
                List<int> newState = new List<int>();

                for (int j = minIndex; j <= maxIndex; j++)
                {
                    int currentVal = 0;
                    for (int k = j - 2; k <= j + 2; k++)
                    {
                        if (plantState.Contains(k))
                            currentVal |= 1 << (k - j + 2);
                    }
                    if (rules.Contains(currentVal))
                        newState.Add(j);
                }
                plantState = newState;
                {
                    long hash = 0;
                    for(int k = 0; k < newState.Count; k++)
                    {
                        hash += (newState[k] - newState[0]) << k;
                    }
                    if (met.ContainsKey(hash))
                    {
                        long cycle = i - met[hash].iteration;
                        long offset = minIndex - met[hash].minIndex;
                        long remaining = 49999999999 - i;
                        for(int k = 0; k < newState.Count; k++)
                        {
                            sum += newState[k] + offset * remaining;
                        }
                        break;
                    }
                    else
                    {
                        met.Add(hash, new state() { iteration = i, minIndex = minIndex});
                    }
                }
            }

            Console.WriteLine(sum);
            Console.Read();
        }
    }
}
