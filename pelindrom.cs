using System;
using System.Threading;

namespace Pelindrom
{
    

    class MainClass
    {
        static char[] input;
        static int[,] cache;
        static char[,] change;
        static char[,] directions;
        static int len;
        //reads input as an array of chars (could be string, but do it yourself...)
        public static int readInput() 
        {
            input = new char[101];
            int c;
            while ((c = Console.Read()) == ' ' || c == '\n')
                return 0;
            input[1] = (char)c;
            int i = 2;
            while ((c = Console.Read()) != ' ' && c != '\n')
            { 
                input[i] = (char)c;
                i++;
            }
            //printTable(input,i-1);
            return i-1;
        }


        //Table write out methods for debugging
        public static void printSeq(char[] T,int l) {
            for (int i = 1; i <= l; i++) {
                Console.Write("{0} ", T[i]);
            }
            Console.WriteLine();
        }


        public static void printTable(char[,] T, int l)
        {
            for (int i = 1; i <= l; i++)
            {
                for (int j = 1; j <= l; j++)
                    Console.Write("{0}  ", T[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public static void printTable(int[,] T, int l)
        {
            for (int i = 1; i <= l; i++)
            {
                for (int j = 1; j <= l; j++)
                    Console.Write("{0}  ", T[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public static int absolute(int x) {
            if (x < 0) return 0 - x;
            return x;
        }

        //computes the diffecrence between two strings (arrays of chars)
        //first string is input, second is the reverse of imput
        //also fills in a table which keeps tabs on what changes were made
        public static void distance()
        {
            len = readInput();
            int t;
            int penalty = 1;
            cache = new int[len + 2, len + 2];
            change = new char[len + 2, len + 2];
            directions = new char[len + 2, len + 2];

            for (int i = 1; i <= len + 1; i++)
            {
                t = len - i + 1;
                cache[i, len + 1] = t;
                cache[len + 1, i] = t;
            }

            int rrow;
            int min;
            char ch, dir;
            int del, ins, keep;
            for (int row = len; row >= 1; row--)
            {
                for (int col = len; col >= 1; col--)
                {
                    rrow = len - row + 1;
                    if (input[rrow] == input[col]) t = 0;
                    else t = penalty;
                    //t = (input[row] == input[rcol]) ? 0 : 3;
                    //Console.WriteLine("{0} =? {1} : t = {2}", input[row], input[rcol], t);
                    del = cache[row + 1, col];
                    keep = cache[row + 1, col + 1];
                    ins = cache[row, col + 1];


                    min = keep + t;
                    ch = (t == penalty) ? 'R' : 'K';
                    dir = 'M';
                    if (min > del + penalty)
                    {
                        min = del + penalty;
                        ch = 'D';
                        dir = 'D';
                    }
                    if (min > ins + penalty)
                    {
                        min = ins + penalty;
                        ch = 'I';
                        dir = 'R';
                    }




                    change[row, col] = ch;
                    directions[row, col] = dir;
                    cache[row, col] = min;

                    //Console.WriteLine("{0} <- {1},{2},{3} : t = {4} <- {5}:{6}", cache[row, col], keep + t, del + 2, ins + 2, t, input[rrow], input[col]);
                }


            }
            printTable(cache, len + 1);
            printTable(directions, len);
            printTable(change, len);
        }

        //goes through the table of changes and counts Ks (keepers) 
        public static int path()
        {
            //char last = 'N';
            
            
            int i = 1;
            int j = 1;
            
            
            int keep = 0;
            while (i != len + 1 && j != len + 1)
            {

                //Console.Write("{0} ", change[i, j]);
                if (change[i, j] == 'K') {
                    keep++;
                    Console.Write(input[j]);
                }


                switch (directions[i, j]) { 
                    case 'R':
                        j++;
                        break;
                    case 'M':
                        i++;
                        j++;
                        break;
                    case 'D':
                        i++;
                        break;
                    default:
                        break;
                            
             
                }

            }
            
            
            return keep;
        }



        public static void Main(string[] args)
        {
            

            distance();

            Console.WriteLine(path());
        }
    }
}
