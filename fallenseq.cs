using System;

namespace Fallenssqgeneral
{

    //class for reading input
    static class Input
    {
        const int MIN = -2147483648;
        const int MAX = 2147483647;
        public static int N;
        public static int K;
        public static int[] s;
        public static bool ReadN()
        {
            if (Int32.TryParse(Console.ReadLine(), out N) && Int32.TryParse(Console.ReadLine(), out K))
            {
                s = new int[N + 2];
                s[0] = MIN;
                for (int i = 1; i <= N; i++)
                {
                    s[i] = ReadNum();
                }
                s[N + 1] = MAX;
                return true;
            }
            else return false;
        }

        public static int ReadNum()
        {
            int c;
            int num = 0;
            int minus = 1;
            while ((c = Console.Read()) == ' ') continue;
            if (c == '-') minus = -1;
            else num = c - '0';
            while ((c = Console.Read()) >= '0' && c <= '9')
            {
                num = 10 * num + (c - '0');
            }
            return minus * num;
        }
    }




    class MainClass
    {

        static int LongestFSS(int[] s, int n, int k)
        {
            int max = 0;
            //this finds the longest increasing subsequence for every element from 1 to N
            //T[i] contains the length of the longest ss from i to N
            int[,] T = new int[n + 1,k+1];
            for (int i = 0; i <= n; i++)
                for (int d = 0; d <= k; d++)
                    T[i, d] = 1;
            
            for (int i = n; i >= 0; i--)
            {
                
                for (int d = 0; d <= k; d++) { 
                    int j = i + 1;
                    while (j <= n)
                    {
                        if (s[i] <= s[j] && T[i, d] < T[j, d] + 1)
                            T[i, d] = T[j, d] + 1;
                        else if (d > 0 && T[i, d] < T[j, d - 1] + 1)
                            T[i, d] = T[j, d - 1] + 1;
                        j++;
                    }
                
                }

            }

            for (int d = 0; d <= k; d++)
                max = (T[0, d] > max) ? T[0, d] : max;

            /*
            for (int d = 0; d <= k; d++)
            {
                for (int i = 0; i <= n; i++)
                {
                    Console.Write("{0} ", T[i,d]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            */


            return max - 1;
       
        }

        public static void Main(string[] args)
        {
            if (Input.ReadN())
                Console.WriteLine(LongestFSS(Input.s, Input.N, Input.K));

        }
    }
}

