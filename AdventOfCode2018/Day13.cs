using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day13
    {
        public struct cart
        {
            public int x;
            public int y;
            public int facing;
            public int state;
            public bool crashed;
        }

        public static void part1()
        {
            string[] map = new StreamReader("day13.txt").ReadToEnd().Split('\n');
            List<cart> carts = new List<cart>();
            for(int y = 0; y < map.Length; y++)
            {
                string row = map[y];
                for(int x = 0; x < row.Length; x++)
                {
                    switch (row[x])
                    {
                        case '<':
                            carts.Add(new cart() { x = x, y = y, facing = 0, state = 0 });
                            break;
                        case '^':
                            carts.Add(new cart() { x = x, y = y, facing = 1, state = 0 });
                            break;
                        case '>':
                            carts.Add(new cart() { x = x, y = y, facing = 2, state = 0 });
                            break;
                        case 'v':
                            carts.Add(new cart() { x = x, y = y, facing = 3, state = 0 });
                            break;
                        default:
                            break;
                    }
                }
            }

            while (true)
            {
                carts.Sort((c1, c2) => c1.y == c2.y ? c1.x < c2.x ? -1 : 1 : c1.y < c2.y ? -1 : 1);
                for(int i = 0; i < carts.Count; i++)
                {
                    cart c = carts[i];

                    if (c.facing == 0)
                        c.x--;
                    else if (c.facing == 1)
                        c.y--;
                    else if (c.facing == 2)
                        c.x++;
                    else if (c.facing == 3)
                        c.y++;

                    switch (map[c.y][c.x])
                    {
                        case '\\':
                            c.facing ^= 1;
                            break;
                        case '/':
                            c.facing ^= 3;
                            break;
                        case '+':
                            if (c.state == 0)
                            {
                                if (c.facing == 0)
                                    c.facing = 3;
                                else
                                    c.facing--;
                            }
                            if(c.state == 2)
                                c.facing = (c.facing + 1) % 4;
                            c.state = (c.state + 1) % 3;
                            break;
                    }

                    for(int j = 0; j < carts.Count; j++)
                    {
                        if (j == i)
                            continue;
                        if (carts[j].x == c.x && carts[j].y == c.y)
                        {
                            Console.WriteLine(c.x + ", " + c.y);
                            Console.Read();
                        }
                    }
                    carts[i] = c;
                }
            }
        }

        public static void part2()
        {
            string[] map = new StreamReader("day13.txt").ReadToEnd().Split('\n');
            List<cart> carts = new List<cart>();
            for (int y = 0; y < map.Length; y++)
            {
                string row = map[y];
                for (int x = 0; x < row.Length; x++)
                {
                    switch (row[x])
                    {
                        case '<':
                            carts.Add(new cart() { x = x, y = y, facing = 0, state = 0 });
                            break;
                        case '^':
                            carts.Add(new cart() { x = x, y = y, facing = 1, state = 0 });
                            break;
                        case '>':
                            carts.Add(new cart() { x = x, y = y, facing = 2, state = 0 });
                            break;
                        case 'v':
                            carts.Add(new cart() { x = x, y = y, facing = 3, state = 0 });
                            break;
                        default:
                            break;
                    }
                }
            }

            while (carts.Count != 1)
            {
                carts.Sort((c1, c2) => c1.y == c2.y ? c1.x < c2.x ? -1 : 1 : c1.y < c2.y ? -1 : 1);
                for (int i = 0; i < carts.Count; i++)
                {
                    cart c = carts[i];
                    if (c.crashed)
                        continue;
                    if (c.facing == 0)
                        c.x--;
                    else if (c.facing == 1)
                        c.y--;
                    else if (c.facing == 2)
                        c.x++;
                    else if (c.facing == 3)
                        c.y++;

                    switch (map[c.y][c.x])
                    {
                        case '\\':
                            c.facing ^= 1;
                            break;
                        case '/':
                            c.facing ^= 3;
                            break;
                        case '+':
                            if (c.state == 0)
                            {
                                if (c.facing == 0)
                                    c.facing = 3;
                                else
                                    c.facing--;
                            }
                            if (c.state == 2)
                                c.facing = (c.facing + 1) % 4;
                            c.state = (c.state + 1) % 3;
                            break;
                    }

                    for (int j = 0; j < carts.Count; j++)
                    {
                        if (carts[j].x == c.x && carts[j].y == c.y)
                        {
                            cart a = carts[j];
                            a.crashed = true;
                            carts[j] = a;
                            c.crashed = true;
                            break;
                        }
                    }
                    carts[i] = c;
                }
                carts.RemoveAll(cart => cart.crashed);
            }
            cart last = carts[0];
            Console.WriteLine(last.x + "," + last.y);
            Console.Read();
        }
    }
}
