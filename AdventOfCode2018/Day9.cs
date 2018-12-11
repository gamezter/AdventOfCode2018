using System;
using System.IO;

namespace AdventOfCode2018
{
    class number
    {
        public int n;
        public number prev;
        public number next;

        public number(int n)
        {
            this.n = n;
        }

        public void addNext(number n)
        {
            this.next.prev = n;
            n.next = this.next;
            n.prev = this;
            this.next = n;
        }

        public number RemovePrev()
        {
            number toRemove = prev;
            toRemove.prev.next = this;
            prev = toRemove.prev;
            return toRemove;
        }
    }

    class Day9
    {
        public static void part1()
        {
            string[] input = new StreamReader("day9.txt").ReadToEnd().Trim().Split();
            int nPlayers = int.Parse(input[0]);
            int nPoints = int.Parse(input[6]);

            int[] scores = new int[nPlayers];
            number current = new number(0);
            current.next = current;
            current.prev = current;

            int currentPlayer = 0;

            for(int i = 1; i <= nPoints; i++)
            {
                if (i % 23 == 0)
                {
                    scores[currentPlayer] += i;
                    current = current.prev.prev.prev.prev.prev.prev;
                    number removed = current.RemovePrev();
                    scores[currentPlayer] += removed.n;
                }
                else
                {
                    number newest = new number(i);
                    current.next.addNext(newest);
                    current = newest;
                }
                currentPlayer = (currentPlayer + 1) % nPlayers;
            }

            int max = scores[0];
            for(int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > max)
                    max = scores[i];
            }

            Console.WriteLine(max);
            Console.Read();
        }

        public static void part2()
        {
            string[] input = new StreamReader("day9.txt").ReadToEnd().Trim().Split();
            int nPlayers = int.Parse(input[0]);
            int nPoints = int.Parse(input[6]) * 100;

            long[] scores = new long[nPlayers];
            number current = new number(0);
            current.next = current;
            current.prev = current;

            int currentPlayer = 0;

            for (int i = 1; i <= nPoints; i++)
            {
                if (i % 23 == 0)
                {
                    scores[currentPlayer] += i;
                    current = current.prev.prev.prev.prev.prev.prev;
                    number removed = current.RemovePrev();
                    scores[currentPlayer] += removed.n;
                }
                else
                {
                    number newest = new number(i);
                    current.next.addNext(newest);
                    current = newest;
                }
                currentPlayer = (currentPlayer + 1) % nPlayers;
            }

            long max = scores[0];
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > max)
                    max = scores[i];
            }

            Console.WriteLine(max);
            Console.Read();
        }
    }
}
