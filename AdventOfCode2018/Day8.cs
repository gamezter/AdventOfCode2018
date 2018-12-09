using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2018
{
    class Day8
    {
        public struct node
        {
            public int nSubnodes;
            public int nMetaData;
            public List<int> childrenValues;
        }

        public static void part1()
        {
            string[] numbers = new StreamReader("day8.txt").ReadToEnd().Trim().Split();

            Stack<node> tree = new Stack<node>();
            int total = 0;

            int i = 0;
            tree.Push(new node() { nSubnodes = int.Parse(numbers[i++]), nMetaData = int.Parse(numbers[i++]) });
            while(i < numbers.Length)
            {
                if(tree.Peek().nSubnodes > 0)
                {
                    tree.Push(new node() { nSubnodes = int.Parse(numbers[i++]), nMetaData = int.Parse(numbers[i++]) });
                }
                else
                {
                    node n = tree.Pop();
                    if(tree.Count != 0)
                    {
                        node parent = tree.Pop();
                        parent.nSubnodes--;
                        tree.Push(parent);
                    }
                    for(int j = 0; j < n.nMetaData; j++)
                    {
                        total += int.Parse(numbers[i++]);
                    }
                }
            }
            Console.WriteLine(total);
            Console.Read();
        }

        public static void part2()
        {
            string[] numbers = new StreamReader("day8.txt").ReadToEnd().Trim().Split();

            Stack<node> tree = new Stack<node>();
            int i = 0;
            tree.Push(new node() { nSubnodes = int.Parse(numbers[i++]), nMetaData = int.Parse(numbers[i++]), childrenValues = new List<int>() });
            int rootValue = 0;
            while (i < numbers.Length)
            {
                int subnodes = tree.Peek().nSubnodes;
                if(subnodes == 0)
                {
                    node n = tree.Pop();
                    int total = 0;
                    for (int j = 0; j < n.nMetaData; j++)
                    {
                        total += int.Parse(numbers[i++]);
                    }
                    tree.Peek().childrenValues.Add(total);
                }
                else
                {
                    if (tree.Peek().childrenValues.Count < subnodes)
                    {
                        tree.Push(new node() { nSubnodes = int.Parse(numbers[i++]), nMetaData = int.Parse(numbers[i++]), childrenValues = new List<int>() });
                    }
                    else
                    {
                        node n = tree.Pop();
                        int total = 0;
                        for (int j = 0; j < n.nMetaData; j++)
                        {
                            int index = int.Parse(numbers[i++]) - 1;
                            if (index < subnodes)
                                total += n.childrenValues[index];
                        }
                        if (tree.Count != 0)
                            tree.Peek().childrenValues.Add(total);
                        else
                            rootValue = total;
                    }
                }
            }
            Console.WriteLine(rootValue);
            Console.Read();
        }
    }
}
