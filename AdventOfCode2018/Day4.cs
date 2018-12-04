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
        public struct entry
        {
            public int year;
            public int month;
            public int day;
            public int hour;
            public int minute;
            public int id;
            public enum State { START, AWAKE, ASLEEP };
            public State state; 
        }

        public static int entrySort(entry a, entry b)
        {
            if (a.year != b.year)
                return a.year < b.year ? -1 : 1;
            if (a.month != b.month)
                return a.month < b.month ? -1 : 1;
            if (a.day != b.day)
                return a.day < b.day ? -1 : 1;
            if (a.hour != b.hour)
                return a.hour < b.hour ? -1 : 1;
            if (a.minute != b.minute)
                return a.minute < b.minute ? -1 : 1;
            return 0;
        }

        public static void part1()
        {
            string[] lines = new StreamReader("day4.txt").ReadToEnd().Trim().Split('\n');
            List<entry> entries = new List<entry>(lines.Length);

            for(int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = lines[i].Split(new[] { '[', '-', ' ', ':', '#', ']' }, StringSplitOptions.RemoveEmptyEntries);
                entry e = new entry();
                e.year = int.Parse(values[0]);
                e.month = int.Parse(values[1]);
                e.day = int.Parse(values[2]);
                e.hour = int.Parse(values[3]);
                e.minute = int.Parse(values[4]);
                switch (values[5])
                {
                    case "wakes":
                        e.id = -1;
                        e.state = entry.State.AWAKE;
                        break;
                    case "falls":
                        e.id = -1;
                        e.state = entry.State.ASLEEP;
                        break;
                    case "Guard":
                        e.id = int.Parse(values[6]);
                        e.state = entry.State.START;
                        break;

                }
                entries.Add(e);
            }

            entries.Sort(entrySort);

            Dictionary<int, int[]> guardScores = new Dictionary<int, int[]>();

            int currentGuard = -1;
            int asleepTime = -1;
            for(int i = 0; i < entries.Count; i++)
            {
                entry e = entries[i];
                switch (e.state)
                {
                    case entry.State.START:
                        currentGuard = e.id;
                        if (!guardScores.ContainsKey(currentGuard))
                            guardScores[currentGuard] = new int[61];
                        break;
                    case entry.State.ASLEEP:
                        asleepTime = e.minute;
                        break;
                    case entry.State.AWAKE:
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
            List<entry> entries = new List<entry>(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = lines[i].Split(new[] { '[', '-', ' ', ':', '#', ']' }, StringSplitOptions.RemoveEmptyEntries);
                entry e = new entry();
                e.year = int.Parse(values[0]);
                e.month = int.Parse(values[1]);
                e.day = int.Parse(values[2]);
                e.hour = int.Parse(values[3]);
                e.minute = int.Parse(values[4]);
                switch (values[5])
                {
                    case "wakes":
                        e.id = -1;
                        e.state = entry.State.AWAKE;
                        break;
                    case "falls":
                        e.id = -1;
                        e.state = entry.State.ASLEEP;
                        break;
                    case "Guard":
                        e.id = int.Parse(values[6]);
                        e.state = entry.State.START;
                        break;

                }
                entries.Add(e);
            }

            entries.Sort(entrySort);

            Dictionary<int, int[]> guardScores = new Dictionary<int, int[]>();

            int currentGuard = -1;
            int asleepTime = -1;
            for (int i = 0; i < entries.Count; i++)
            {
                entry e = entries[i];
                switch (e.state)
                {
                    case entry.State.START:
                        currentGuard = e.id;
                        if (!guardScores.ContainsKey(currentGuard))
                            guardScores[currentGuard] = new int[60];
                        break;
                    case entry.State.ASLEEP:
                        asleepTime = e.minute;
                        break;
                    case entry.State.AWAKE:
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
