using System;
using System.IO;


namespace Collection
{
    static class Input 
    {
        static int lastC;
        public static bool readInt(out int num, bool onlyLine)
        {
            if (lastC == '\n') {
                num = -1;
                return false;
            } 
            int c;
            int n = -1, minus = 1;
            while (((c = Console.Read()) < '0' || c > '9') && (c != '\n' && onlyLine)) {
                if (c == '-') minus = -1;
                else minus = 1;

            }
            if (c == '\n' || c == '^'){ num = -1; return false; }
            n = c - '0';
            while ((c = Console.Read()) >= '0' && c <= '9') {
                n = 10 * n + (c - '0');
            }
            lastC = c;
            num = minus * n;
            return true;
        }

        public static int[] readSeq() 
        {
            int[] readingA = new int[10000];
            int[] A;
            int j = 0;
            
            while (readInt(out int num, true)) {
                readingA[j] = num;
                j++;
            }
            A = new int[j];
            for (int i = 0; i < j; i++)
                A[i] = readingA[i];

            return A;
        }

        public static void ArrayWO(int[] A) {
            for (int i = 0; i < A.Length - 1; i++)
                Console.Write("{0} ", A[i]);
            Console.WriteLine(A[A.Length-1]);
        }
    }



    class MainClass
    {
        

        public static void Main(string[] args)
        {
            //Bullshit b = new Bullshit();
            /*
            string order = Console.ReadLine();
            if (order == "merge files") {
                Files.mergeTwoStrings();
            }
            */

            //int[] A = Input.readSeq();
            //Input.ArrayWO(A);
            //Console.WriteLine(A.Length);
            //MergeSort(array)
            /*
            A = DandC.MergeSort(A);
            Input.ArrayWO(A);
            */
            //QuickSort(array, l, r)
            //Quick.QuickSort(A);
            //Console.WriteLine(Quick.pivot(A, 0, 2));
            //Console.WriteLine(Quick.LinearSelect2(A, 5));
            /*
            if (Input.readInt(out int n, true))
                Console.WriteLine(n);
            */
            int x = 5;
            int z = x / 2;
            int y = (int)Math.Ceiling((double)x / 2);
            Console.WriteLine(z);

        }
    }
}
