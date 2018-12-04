using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day4
    {
        public struct Entry
        {
            public int month;
            public int day;
            public int hour;
            public int minute;
            public int id;
            public enum State { START, AWAKE, ASLEEP };
            public State state;
        }

        public class EntrySort : IComparer<Entry>
        {
            public int Compare(Entry x, Entry y)
            {
                if (x.month != y.month)
                    return x.month < y.month ? -1 : 1;
                if (x.day != y.day)
                    return x.day < y.day ? -1 : 1;
                if (x.hour != y.hour)
                    return x.hour < y.hour ? -1 : 1;
                if (x.minute != y.minute)
                    return x.minute < y.minute ? -1 : 1;
                return 0;
            }
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day4.txt").ReadToEnd().Trim().Split('\n');
            SortedSet<Entry> entries = new SortedSet<Entry>(new EntrySort());

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = lines[i].Split(new[] { '[', '-', ' ', ':', '#', ']' }, StringSplitOptions.RemoveEmptyEntries);
                Entry e = new Entry
                {
                    month = int.Parse(values[1]),
                    day = int.Parse(values[2]),
                    hour = int.Parse(values[3]),
                    minute = int.Parse(values[4])
                };
                switch (values[5])
                {
                    case "wakes":
                        e.id = -1;
                        e.state = Entry.State.AWAKE;
                        break;
                    case "falls":
                        e.id = -1;
                        e.state = Entry.State.ASLEEP;
                        break;
                    case "Guard":
                        e.id = int.Parse(values[6]);
                        e.state = Entry.State.START;
                        break;

                }
                entries.Add(e);
            }

            Dictionary<int, int[]> guardScores = new Dictionary<int, int[]>();

            int currentGuard = -1;
            int asleepTime = -1;
            foreach (Entry e in entries)
            {
                switch (e.state)
                {
                    case Entry.State.START:
                        currentGuard = e.id;
                        if (!guardScores.ContainsKey(currentGuard))
                            guardScores[currentGuard] = new int[61];
                        break;
                    case Entry.State.ASLEEP:
                        asleepTime = e.minute;
                        break;
                    case Entry.State.AWAKE:
                        int[] guardScore = guardScores[currentGuard];
                        for (int j = asleepTime; j < e.minute; j++)
                        {
                            guardScore[j]++;
                        }
                        guardScore[60] += e.minute - asleepTime;
                        guardScores[currentGuard] = guardScore;
                        break;
                }
            }

            int maxGuard = -1;
            int maxValue = -1;
            foreach(var kvp in guardScores)
            {
                int sleepTime = kvp.Value[60];
                if (sleepTime > maxValue)
                {
                    maxValue = sleepTime;
                    maxGuard = kvp.Key;
                }
            }

            int maxMinute = 0;
            int[] guardTimes = guardScores[maxGuard];
            for(int i = 0; i < 60; i++)
            {
                if (guardTimes[i] > guardTimes[maxMinute])
                    maxMinute = i;
            }

            Console.WriteLine(maxGuard * maxMinute);
            Console.Read();
        }

        public static void part2()
        {
            string[] lines = new StreamReader("day4.txt").ReadToEnd().Trim().Split('\n');
            SortedSet<Entry> entries = new SortedSet<Entry>(new EntrySort());

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = lines[i].Split(new[] { '[', '-', ' ', ':', '#', ']' }, StringSplitOptions.RemoveEmptyEntries);
                Entry e = new Entry
                {
                    month = int.Parse(values[1]),
                    day = int.Parse(values[2]),
                    hour = int.Parse(values[3]),
                    minute = int.Parse(values[4])
                };
                switch (values[5])
                {
                    case "wakes":
                        e.id = -1;
                        e.state = Entry.State.AWAKE;
                        break;
                    case "falls":
                        e.id = -1;
                        e.state = Entry.State.ASLEEP;
                        break;
                    case "Guard":
                        e.id = int.Parse(values[6]);
                        e.state = Entry.State.START;
                        break;

                }
                entries.Add(e);
            }

            Dictionary<int, int[]> guardScores = new Dictionary<int, int[]>();

            int currentGuard = -1;
            int asleepTime = -1;
            foreach(Entry e in entries)
            {
                switch (e.state)
                {
                    case Entry.State.START:
                        currentGuard = e.id;
                        if (!guardScores.ContainsKey(currentGuard))
                            guardScores[currentGuard] = new int[60];
                        break;
                    case Entry.State.ASLEEP:
                        asleepTime = e.minute;
                        break;
                    case Entry.State.AWAKE:
                        int[] guardScore = guardScores[currentGuard];
                        for (int j = asleepTime; j < e.minute; j++)
                        {
                            guardScore[j]++;
                        }
                        guardScores[currentGuard] = guardScore;
                        break;
                }
            }

            int maxGuard = -1;
            int maxMinute = -1;
            int maxValue = -1;
            foreach (var kvp in guardScores)
            {
                int[] guardScore = kvp.Value;
                for(int i = 0; i < guardScore.Length; i++)
                {
                    if(guardScore[i] > maxValue)
                    {
                        maxGuard = kvp.Key;
                        maxMinute = i;
                        maxValue = guardScore[i];
                    }
                }
            }

            Console.WriteLine(maxGuard * maxMinute);
            Console.Read();
        }
    }
}
