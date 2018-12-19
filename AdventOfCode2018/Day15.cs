using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2018
{
    class Day15
    {
        public struct Unit
        {
            public int x, y, health;
            public bool isElf;
        }

        public struct pos
        {
            public int x, y, score;

            public pos(int x, int y, int score)
            {
                this.x = x;
                this.y = y;
                this.score = score;
            }
        }

        public static void fillDistances(int x, int y, int width, int height, int[,] distances, char[,] map)
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    distances[i, j] = -1;

            Queue<pos> open = new Queue<pos>();
            distances[x, y] = 0;
            open.Enqueue(new pos(x, y, 0));
            while(open.Count > 0)
            {
                pos c = open.Dequeue();
                int nextVal = c.score + 1;
                if (map[c.x + 1, c.y] == '.')
                {
                    int val = distances[c.x + 1, c.y];
                    if(val == -1 || val > nextVal)
                    {
                        distances[c.x + 1, c.y] = nextVal;
                        open.Enqueue(new pos(c.x + 1, c.y, nextVal));
                    }                        
                }
                if (map[c.x, c.y + 1] == '.')
                {
                    int val = distances[c.x, c.y + 1];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y + 1] = nextVal;
                        open.Enqueue(new pos(c.x, c.y + 1, nextVal));
                    }
                }
                if (map[c.x - 1, c.y] == '.')
                {
                    int val = distances[c.x - 1, c.y];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x - 1, c.y] = nextVal;
                        open.Enqueue(new pos(c.x - 1, c.y, nextVal));
                    }
                }
                if (map[c.x, c.y - 1] == '.')
                {
                    int val = distances[c.x, c.y - 1];
                    if (val == -1 || val > nextVal)
                    {
                        distances[c.x, c.y - 1] = nextVal;
                        open.Enqueue(new pos(c.x, c.y - 1, nextVal));
                    }
                }
            }
        }

        public static void debugMap(char[,] map, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
        }

        public static int sort(pos a, pos b)
        {
            if(a.score == b.score)
            {
                if (a.y == b.y)
                    return a.x < b.x ? -1 : 1;
                return a.y < b.y ? -1 : 1;
            }
            return a.score < b.score ? -1 : 1;            
        }

        public static pos traceBack(pos start, int[,] distances)
        {
            Queue<pos> fronts = new Queue<pos>();
            HashSet<pos> ends = new HashSet<pos>();
            fronts.Enqueue(start);
            while (fronts.Count != 0)
            {
                pos current = fronts.Dequeue();
                if (current.score == 1)
                    ends.Add(current);
                else
                {
                    if (distances[current.x, current.y - 1] == current.score - 1)
                        fronts.Enqueue(new pos(current.x, current.y - 1, current.score - 1));
                    if (distances[current.x + 1, current.y] == current.score - 1)
                        fronts.Enqueue(new pos(current.x + 1, current.y, current.score - 1));
                    if (distances[current.x - 1, current.y] == current.score - 1)
                        fronts.Enqueue(new pos(current.x - 1, current.y, current.score - 1));
                    if (distances[current.x, current.y + 1] == current.score - 1)
                        fronts.Enqueue(new pos(current.x, current.y + 1, current.score - 1));
                }
            }
            List<pos> endsList = ends.ToList();
            endsList.Sort(sort);
            return endsList[0];
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day15.txt").ReadToEnd().Trim().Split('\n');

            int width = lines[0].Length;
            int height = lines.Length;
            char[,] map = new char[width, height];
            List<Unit> units = new List<Unit>();

            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    if (lines[y][x] == 'E')
                        units.Add(new Unit() { x = x, y = y, health = 200, isElf = true });
                    if (lines[y][x] == 'G')
                        units.Add(new Unit() { x = x, y = y, health = 200, isElf = false });
                    map[x, y] = lines[y][x];
                }
            }

            int rounds = 0;
            while (true)
            {
                units.RemoveAll(u => u.health < 1);
                units.Sort((e1, e2) => e1.y == e2.y ? e1.x < e2.x ? -1 : 1 : e1.y < e2.y ? -1 : 1);

                //debug
                {
                    Console.WriteLine();
                    Console.WriteLine(rounds);
                    Console.WriteLine();
                    debugMap(map, width, height);

                    for (int i = 0; i < units.Count; i++)
                    {
                        Console.WriteLine((units[i].isElf ? "E" : "G") + "(" + units[i].health + ")");
                    }
                    //Console.Read();
                }

                int[,] distances = new int[width, height];
                for(int i = 0; i < units.Count; i++)
                {
                    Unit unit = units[i];
                    if (unit.health < 1)
                        continue;

                    {
                        int elves = 0;
                        int goblins = 0;
                        for (int j = 0; j < units.Count; j++)
                        {
                            if (units[j].health < 1)
                                continue;
                            if (units[j].isElf)
                                elves++;
                            else
                                goblins++;
                        }

                        if (elves == 0 || goblins == 0)
                            goto end;
                    }

                    // find adjacent enemies
                    List<int> adjacent = new List<int>();
                    for(int j = 0; j < units.Count; j++)
                    {
                        Unit enemy = units[j];
                        if (enemy.isElf == unit.isElf || enemy.health < 1)
                            continue;
                        if (Math.Abs(enemy.x - unit.x) + Math.Abs(enemy.y - unit.y) == 1)
                            adjacent.Add(j);
                    }

                    if(adjacent.Count == 0)
                    {
                        //move
                        fillDistances(unit.x, unit.y, width, height, distances, map);
                        List<pos> scores = new List<pos>();
                        for (int j = 0; j < units.Count; j++)
                        {
                            Unit enemy = units[j];
                            if (enemy.isElf == unit.isElf || enemy.health < 1)
                                continue;
                            int x = enemy.x;
                            int y = enemy.y;
                            if (distances[x - 1, y] != -1)
                                scores.Add(new pos(x - 1, y, distances[x - 1, y]));
                            if (distances[x + 1, y] != -1)
                                scores.Add(new pos(x + 1, y, distances[x + 1, y]));
                            if (distances[x, y - 1] != -1)
                                scores.Add(new pos(x, y - 1, distances[x, y - 1]));
                            if (distances[x, y + 1] != -1)
                                scores.Add(new pos(x, y + 1, distances[x, y + 1]));
                        }
                        if(scores.Count != 0)
                        {
                            scores.Sort(sort);
                            pos next = traceBack(scores[0], distances);
                            map[unit.x, unit.y] = '.';
                            map[next.x, next.y] = unit.isElf ? 'E' : 'G';
                            unit.x = next.x;
                            unit.y = next.y;
                            units[i] = unit;

                            //find new adjacents;
                            for (int j = 0; j < units.Count; j++)
                            {
                                Unit enemy = units[j];
                                if (enemy.isElf == unit.isElf || enemy.health < 1)
                                    continue;
                                if (Math.Abs(enemy.x - unit.x) + Math.Abs(enemy.y - unit.y) == 1)
                                    adjacent.Add(j);
                            }
                        }
                    }

                    if(adjacent.Count != 0)
                    {
                        //attack
                        int min = 300;
                        int minIndex = -1;
                        for(int j = 0; j < adjacent.Count; j++)
                        {
                            int health = units[adjacent[j]].health;
                            if (health < min && health > 0)
                            {
                                min = health;
                                minIndex = adjacent[j];
                            }
                            if(health == min)
                            {
                                if(units[adjacent[j]].y < units[minIndex].y)
                                {
                                    minIndex = adjacent[j];
                                }
                            }
                        }
                        if(minIndex != -1)
                        {
                            Unit victim = units[minIndex];
                            victim.health -= 3;
                            if (victim.health < 1)
                            {
                                map[victim.x, victim.y] = '.';
                            }
                                
                            units[minIndex] = victim;
                        }
                    }
                }
                rounds++;
            }

end:
            units.RemoveAll(u => u.health < 1);

            //debug
            {
                Console.WriteLine();
                Console.WriteLine(rounds);
                Console.WriteLine();
                debugMap(map, width, height);

                for (int i = 0; i < units.Count; i++)
                {
                    Console.WriteLine((units[i].isElf ? "E" : "G") + "(" + units[i].health + ")");
                }
            }

            int sum = 0;
            for(int i = 0; i < units.Count; i++)
            {
                sum += units[i].health;
            }

            Console.WriteLine(sum + " " + sum * rounds);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day15.txt").ReadToEnd().Trim().Split('\n');

            int width = lines[0].Length;
            int height = lines.Length;
            char[,] map = new char[width, height];
            List<Unit> units = new List<Unit>();

            int elvesPower = 3;

            restart:
            units.Clear();
            int elfCount = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (lines[y][x] == 'E')
                    {
                        units.Add(new Unit() { x = x, y = y, health = 200, isElf = true });
                        elfCount++;
                    } 
                    if (lines[y][x] == 'G')
                        units.Add(new Unit() { x = x, y = y, health = 200, isElf = false });
                    map[x, y] = lines[y][x];
                }
            }


            int rounds = 0;
            while (true)
            {
                units.RemoveAll(u => u.health < 1);
                units.Sort((e1, e2) => e1.y == e2.y ? e1.x < e2.x ? -1 : 1 : e1.y < e2.y ? -1 : 1);

                int[,] distances = new int[width, height];
                for (int i = 0; i < units.Count; i++)
                {
                    Unit unit = units[i];
                    if (unit.health < 1)
                        continue;

                    {
                        int elves = 0;
                        int goblins = 0;
                        for (int j = 0; j < units.Count; j++)
                        {
                            if (units[j].health < 1)
                                continue;
                            if (units[j].isElf)
                                elves++;
                            else
                                goblins++;
                        }

                        if (elves < elfCount)
                        {
                            elvesPower++;
                            goto restart;
                        }
                            
                        if (goblins == 0)
                            goto end;
                    }

                    // find adjacent enemies
                    List<int> adjacent = new List<int>();
                    for (int j = 0; j < units.Count; j++)
                    {
                        Unit enemy = units[j];
                        if (enemy.isElf == unit.isElf || enemy.health < 1)
                            continue;
                        if (Math.Abs(enemy.x - unit.x) + Math.Abs(enemy.y - unit.y) == 1)
                            adjacent.Add(j);
                    }

                    if (adjacent.Count == 0)
                    {
                        //move
                        fillDistances(unit.x, unit.y, width, height, distances, map);
                        List<pos> scores = new List<pos>();
                        for (int j = 0; j < units.Count; j++)
                        {
                            Unit enemy = units[j];
                            if (enemy.isElf == unit.isElf || enemy.health < 1)
                                continue;
                            int x = enemy.x;
                            int y = enemy.y;
                            if (distances[x - 1, y] != -1)
                                scores.Add(new pos(x - 1, y, distances[x - 1, y]));
                            if (distances[x + 1, y] != -1)
                                scores.Add(new pos(x + 1, y, distances[x + 1, y]));
                            if (distances[x, y - 1] != -1)
                                scores.Add(new pos(x, y - 1, distances[x, y - 1]));
                            if (distances[x, y + 1] != -1)
                                scores.Add(new pos(x, y + 1, distances[x, y + 1]));
                        }
                        if (scores.Count != 0)
                        {
                            scores.Sort(sort);
                            pos next = traceBack(scores[0], distances);
                            map[unit.x, unit.y] = '.';
                            map[next.x, next.y] = unit.isElf ? 'E' : 'G';
                            unit.x = next.x;
                            unit.y = next.y;
                            units[i] = unit;

                            //find new adjacents;
                            for (int j = 0; j < units.Count; j++)
                            {
                                Unit enemy = units[j];
                                if (enemy.isElf == unit.isElf || enemy.health < 1)
                                    continue;
                                if (Math.Abs(enemy.x - unit.x) + Math.Abs(enemy.y - unit.y) == 1)
                                    adjacent.Add(j);
                            }
                        }
                    }

                    if (adjacent.Count != 0)
                    {
                        //attack
                        int min = 300;
                        int minIndex = -1;
                        for (int j = 0; j < adjacent.Count; j++)
                        {
                            int health = units[adjacent[j]].health;
                            if (health < min && health > 0)
                            {
                                min = health;
                                minIndex = adjacent[j];
                            }
                            if (health == min)
                            {
                                if (units[adjacent[j]].y < units[minIndex].y)
                                {
                                    minIndex = adjacent[j];
                                }
                            }
                        }
                        if (minIndex != -1)
                        {
                            Unit victim = units[minIndex];
                            victim.health -= unit.isElf ? elvesPower : 3;
                            if (victim.health < 1)
                            {
                                map[victim.x, victim.y] = '.';
                            }

                            units[minIndex] = victim;
                        }
                    }
                }
                rounds++;
            }

            end:
            units.RemoveAll(u => u.health < 1);

            //debug
            {
                Console.WriteLine();
                Console.WriteLine(rounds);
                Console.WriteLine();
                debugMap(map, width, height);

                for (int i = 0; i < units.Count; i++)
                {
                    Console.WriteLine((units[i].isElf ? "E" : "G") + "(" + units[i].health + ")");
                }
            }

            int sum = 0;
            for (int i = 0; i < units.Count; i++)
            {
                sum += units[i].health;
            }

            Console.WriteLine(sum + " " + sum * rounds);
            Console.Read();
        }
    }
}
