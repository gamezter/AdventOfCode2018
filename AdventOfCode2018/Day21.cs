using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    class Day21
    {
        public static void part1()
        {
            int r4 = 65536;
            int r5 = 3935295;

            while (true)
            {
                r5 = r5 + (r4 & 255);
                r5 = r5 & 16777215;
                r5 = r5 * 65899;
                r5 = r5 & 16777215;
                if (r4 < 256)
                {
                    Console.WriteLine(r5);
                    Console.Read();
                }
                else
                {
                    r4 = r4 >> 8;
                }
            }
        }

        public static void part2()
        {
            HashSet<int> met = new HashSet<int>();
            

            int r4 = 65536;
            int r5 = 6577493;
            int last = r5;
            while (true)
            {
                if (r4 < 256)
                {
                    if (!met.Add(r5))
                    {
                        Console.WriteLine(last);
                        Console.Read();
                    }
                    last = r5;
                    r4 = r5 | 65536;
                    r5 = 3935295;
                }
                else
                {
                    r4 >>= 8;
                }
                r5 += r4 & 255;
                r5 &= 16777215;
                r5 *= 65899;
                r5 &= 16777215;
            }
        }

        public static void temp()
        {
            int r0 = 0;
            int r1 = 0;
            int r2 = 0;
            int r3 = 0;
            int r4 = 0;
            int r5 = 0;
            /*0*/r5 = 123;                  //seti 123 0 5
n1:         /*1*/r5 = r5 & 456;             //bani 5 456 5
            /*2*/r5 = r5 == 72 ? 1 : 0;     //eqri 5 72 5
            /*3*/r1 = r5 + r1;              //addr 5 1 1        !! 4 / 5
n4:         /*4*/r1 = 0;                    //seti 0 0 1        !! 1
n5:         /*5*/r5 = 0;                    //seti 0 2 5
n6:         /*6*/r4 = r5 | 65536;           //bori 5 65536 4
            /*7*/r5 = 3935295;              //seti 3935295 1 5
n8:         /*8*/r2 = r4 & 255;             //bani 4 255 2
            /*9*/r5 = r5 + r2;              //addr 5 2 5
            /*10*/r5 = r5 & 16777215;       //bani 5 16777215 5
            /*11*/r5 = r5 * 65899;          //muli 5 65899 5
            /*12*/r5 = r5 & 16777215;       //bani 5 16777215 5
            /*13*/r2 = 256 > r4 ? 1 : 0;    //gtir 256 4 2
            /*14*/r1 = r2 + r1;             //addr 2 1 1        !! 15 / 16
n15:        /*15*/r1 = r1 + 1;              //addi 1 1 1        !! 17
n16:        /*16*/r1 = 27;                  //seti 27 1 1       !! 28
n17:        /*17*/r2 = 0;                   //seti 0 5 2
n18:        /*18*/r3 = r2 + 1;              //addi 2 1 3
            /*19*/r3 = r3 * 256;            //muli 3 256 3
            /*20*/r3 = r3 > r4 ? 1 : 0;     //gtrr 3 4 3
            /*21*/r1 = r3 + r1;             //addr 3 1 1        !! 22 / 23
n22:        /*22*/r1 = r1 + 1;              //addi 1 1 1        !! 24
n23:        /*23*/r1 = 25;                  //seti 25 0 1       !! 26
n24:        /*24*/r2 = r2 + 1;              //addi 2 1 2
            /*25*/r1 = 17;                  //seti 17 7 1       !! 18
n26:        /*26*/r4 = r2;                  //setr 2 2 4
            /*27*/r1 = 7;                   //seti 7 6 1        !! 8
n28:        /*28*/r2 = r5 == r0 ? 1 : 0;    //eqrr 5 0 2
            /*29*/r1 = r2 + r1;             //addr 2 1 1        !! 30 / 31
n30:        /*30*/r1 = 5;                   //seti 5 4 1        !! 6
n31:        Console.WriteLine(r0);
            Console.Read();
        }
    }
}
