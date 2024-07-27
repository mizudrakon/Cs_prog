using System;
using System.Collections.Generic;

namespace Shortest
{
    class MainClass
    {
        public static void error(int i) 
        {
            if (i == 1)
                Console.WriteLine("INPUT ERROR!");
            else
                Console.WriteLine("Something went sooo wrong...");
        }

        //reads a single number from input, returns true if there was a sequence of characters that ended with something that could be considered a positive int
        public static bool readNum(out int n) 
        {
            bool result = false;
            int c;
            while ((c = Console.Read()) < '0' || c > '9' && c != '\n') {continue; }
            if (c >= '0' && c <= '9') result = true;
            n = c - '0';
            while ((c = Console.Read()) >= '0' && c <= '9') {
                n = 10 * n + (c - '0');
            }
            return result;
        }

        //reads the entire input as long as it's correct (with some slight possible deviations)
        public static bool readInput(out int[][] G, out int start,out int dest) 
        {
            bool check = true;
            int N,n;
            string[] line = new string[3];

            //reads the first line, splits it and outs the data if they work
            //decided to switch to readNum() later, I should normally rewrite this to unify the code
            line = Console.ReadLine().Split(' ');
            check = Int32.TryParse(line[0], out N);
            check = (Int32.TryParse(line[1], out start) && check); 
            check = (Int32.TryParse(line[2], out dest) && check);

            if (check == false)
            {
                error(1);
                start = 0;
                dest = 0;
                G = null;
                return false;
            }

            //if we got the first line from the input, we try to get the rest into a jagged array
            G = new int[N+1][];
            G[0] = new int[] { N };
            for (int i = 1; i <= N; i++) {
                if (readNum(out n))
                {
                    G[i] = new int[n+1];
                    G[i][0] = n;
                }
                else 
                {
                    error(1);
                    G = null;
                    return false;
                }
                for (int j = 1; j <= n; j++) 
                {
                    if (readNum(out G[i][j]) == false) 
                    {
                        error(1);
                        G = null;
                        return false;
                    }
                }

                //for debug 
                /*
                for (int j = 1; j < n; j++) {
                    Console.Write("{0} ", G[i][j]);
                }
                Console.WriteLine(G[i][n]);
                */
            }
            //we should have the complete graph in our memory now
            //G[0][0] = N, G[i][0] = degree(i)
            return true;
        }

        public static void BFS(int[][] G, int start, int end) 
        {
            Queue<int> Q = new Queue<int>();
            Stack<int> result = new Stack<int>();
            int[] path = new int[G[0][0] + 1];
            int v, u;
            path[start] = 1;
            Q.Enqueue(start);
            while (Q.Count > 0) 
            {
                u = Q.Dequeue();
                for (int i = 1; i <= G[u][0]; i++) {
                    v = G[u][i];
                    //Console.WriteLine("{0} {1}", v, u);
                    if (path[v] == 0) { 
                        Q.Enqueue(v);
                        path[v] = u;
                    }
                    if (v == end) break;
                }
                if (path[end] != 0) break;
            
            }

            //for debug 
            /*
            for (int j = 1; j < G[0][0]; j++)
            {
                Console.Write("{0} ", path[j]);
            }
            Console.WriteLine(path[G[0][0]]);
            */

            //
            v = end;
            if (path[v] == 0)
            {
                Console.WriteLine("unreachable");

            }
            else
            {
                while (v != start)
                {
                    result.Push(v);
                    v = path[v];
                }
                result.Push(v);
                while (result.Count > 1)
                    Console.Write("{0} ", result.Pop());
                Console.WriteLine("{0}", result.Pop());
            }
        }

        public static void Main(string[] args)
        {
            int S, E;
            int[][] G;
            if (readInput(out G, out S, out E))
            {
                BFS(G, S, E);
            }
            else error(2);
        }
    }
}
