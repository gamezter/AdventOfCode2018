using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day16
    {

        public static void part1()
        {
            string[] lines = new StreamReader("day16.txt").ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            int total = 0;
            while(lines[i][0] == 'B')
            {
                string[] first = lines[i].Split(new[] { '[', ']', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                string[] second = lines[++i].Split();
                string[] third = lines[++i].Split(new[] { '[', ']', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                i++;
                int[] before = new[] { int.Parse(first[1]), int.Parse(first[2]), int.Parse(first[3]), int.Parse(first[4]) };
                int A = int.Parse(second[1]);
                int B = int.Parse(second[2]);
                int C = int.Parse(second[3]);
                int[] after = new[] { int.Parse(third[1]), int.Parse(third[2]), int.Parse(third[3]), int.Parse(third[4]) };
                int count = 0;
                int[] temp = new int[4];
                { //addr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] + temp[B];
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //addi
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] + B;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //mulr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] * temp[B];
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //muli
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] * B;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //banr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] & temp[B];
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //bani
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] & B;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //borr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] | temp[B];
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //bori
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] | B;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //setr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A];
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //seti
                    before.CopyTo(temp, 0);
                    temp[C] = A;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //gtir
                    before.CopyTo(temp, 0);
                    temp[C] = A > temp[B] ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //gtri
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] > B ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //gtrr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] > temp[B] ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //eqir
                    before.CopyTo(temp, 0);
                    temp[C] = A == temp[B] ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //eqri
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] == B ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }
                { //eqrr
                    before.CopyTo(temp, 0);
                    temp[C] = temp[A] == temp[B] ? 1 : 0;
                    if (temp[0] == after[0] && temp[1] == after[1] && temp[2] == after[2] && temp[3] == after[3])
                        count++;
                }

                if (count > 2)
                    total++;
            }
            Console.WriteLine(total);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day16.txt").ReadToEnd().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int i = 0;
            while (lines[i][0] == 'B')
            {
                i += 3;
            }
            int[] reg = new int[4];

            while (i < lines.Length)
            {
                string[] line = lines[i].Split();
                int op = int.Parse(line[0]);
                int A = int.Parse(line[1]);
                int B = int.Parse(line[2]);
                int C = int.Parse(line[3]);

                switch (op)
                {
                    case 0: //setr
                        reg[C] = reg[A];
                        break;
                    case 1: //eqrr
                        reg[C] = reg[A] == reg[B] ? 1 : 0;
                        break;
                    case 2: //gtri
                        reg[C] = reg[A] > B ? 1 : 0;
                        break;
                    case 3: //muli
                        reg[C] = reg[A] * B;
                        break;
                    case 4: //eqir
                        reg[C] = A == reg[B] ? 1 : 0;
                        break;
                    case 5: //borr
                        reg[C] = reg[A] | reg[B];
                        break;
                    case 6: //bori
                        reg[C] = reg[A] | B;
                        break;
                    case 7: //mulr
                        reg[C] = reg[A] * reg[B];
                        break;
                    case 8: //gtrr
                        reg[C] = reg[A] > reg[B] ? 1 : 0;
                        break;
                    case 9: //seti
                        reg[C] = A;
                        break;
                    case 10: //banr
                        reg[C] = reg[A] & reg[B];
                        break;
                    case 11: //eqri
                        reg[C] = reg[A] == B ? 1 : 0;
                        break;
                    case 12: //addr
                        reg[C] = reg[A] + reg[B];
                        break;
                    case 13: //gtir
                        reg[C] = A > reg[B] ? 1 : 0;
                        break;
                    case 14: //addi
                        reg[C] = reg[A] + B;
                        break;
                    case 15: //bani
                        reg[C] = reg[A] & B;
                        break;
                }
                i++;
            }
            Console.WriteLine(reg[0]);
            Console.Read();
        }
    }
}
