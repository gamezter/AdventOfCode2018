using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2018
{
    class Day7
    {
        public struct step
        {
            public char name;
            public HashSet<char> dependencies;
            public int timeLeft;
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day7.txt").ReadToEnd().Trim().Split('\n');
            step[] steps = new step[26];
            for(int c = 'A'; c <= 'Z'; c++)
            {
                steps[c - 'A'] = new step()
                {
                    name = (char)c,
                    dependencies = new HashSet<char>()
                };
            }

            char[] rules = new char[lines.Length * 2];
            for(int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split();
                char pre = parts[1][0];
                char post = parts[7][0];
                steps[post - 'A'].dependencies.Add(pre);
            }

            HashSet<char> visited = new HashSet<char>();

            char[] order = new char[26];
            for(int i = 0; i < order.Length; i++)
            {
                //the next one will always be the first element in the array that has no dependencies and hasnt been visited;
                for(int j = 0; j < 26; j++)
                {
                    if (steps[j].dependencies.Count == 0 && !visited.Contains(steps[j].name))
                    {
                        order[i] = steps[j].name;
                        visited.Add(steps[j].name);
                        break;
                    }
                }

                //now remove order[i] from all remaining step dependencies;
                for (int j = 0; j < 26; j++)
                {
                    steps[j].dependencies.Remove(order[i]);
                }
            }

            Console.WriteLine(new string(order));
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day7.txt").ReadToEnd().Trim().Split('\n');
            step[] steps = new step[26];
            for (int c = 0; c < steps.Length; c++)
            {
                steps[c] = new step()
                {
                    name = (char)(c + 'A'),
                    dependencies = new HashSet<char>(),
                    timeLeft = 61 + c
                };
            }

            char[] rules = new char[lines.Length * 2];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split();
                char pre = parts[1][0];
                char post = parts[7][0];
                steps[post - 'A'].dependencies.Add(pre);
            }

            int completed = 0;
            int totalTime = 0;
            while (completed != 26)
            {
                totalTime++;
                int count = 5;
                List<char> toRemove = new List<char>();
                for(int i = 0; i < steps.Length; i++)
                {
                    step s = steps[i];
                    if(s.dependencies.Count == 0 && s.timeLeft > 0 && count > 0)
                    {
                        s.timeLeft--;
                        count--;
                        steps[i] = s;
                        if (s.timeLeft == 0)
                        {
                            toRemove.Add(s.name);
                            completed++;
                        }
                    }
                }
                for(int i = 0; i < toRemove.Count; i++)
                {
                    for(int j = 0; j < steps.Length; j++)
                    {
                        steps[j].dependencies.Remove(toRemove[i]);
                    }
                }
                toRemove.Clear();
            }

            Console.WriteLine(totalTime);
            Console.Read();
        }
    }
}
