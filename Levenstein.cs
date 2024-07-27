using System;

namespace Levenshtein
{
    

    class MainClass
    {
        //reading inpit which is just two lines of practically whatever
        //if not, I'll have to read char by char and check them... but let's have hope
        public static string first = Console.ReadLine();
        public static string second = Console.ReadLine();


        //matrix write out function for debugging...
        public static void printM(int[,] M, int a, int b) 
        {
            for (int i = 0; i <= a; i++)
            {
                for (int j = 0; j <= b; j++)
                {
                    Console.Write("{0} ", M[i, j]);
                }
                Console.WriteLine();
            }
        }


        //using a matrix to determine editation distance between the two strings.
        public static int edit(string A, string B) 
        {
            int a = A.Length;
            int b = B.Length;
            int d, min;
            int[,] matrix = new int[a + 1, b + 1];
            for (int i = 0; i <= a; i++) matrix[i, b] = a - i;
            for (int j = 0; j <= b; j++) matrix[a, j] = b - j;
            //every position will hold the shortest amount of edits given possible from the end.
            for (int i = a - 1; i >= 0; i--)
                for (int j = b - 1; j >= 0; j--) {
                    d = (A[i] == B[j]) ? 0 : 1; //0 -> no edit needed at the [i,j] vs 1 -> edit needed
                    min = d + matrix[i + 1, j + 1]; //position [i+1,j+1] should be kept or changed
                    if (1 + matrix[i + 1, j] < min) min = 1 + matrix[i + 1, j]; //position A[i+1] should be inserted to B
                    if (1 + matrix[i, j + 1] < min) min = 1 + matrix[i, j + 1]; //position B[j+1] should be deleted from B
                    matrix[i, j] = min;
                }
            //printM(matrix, a, b);
            return matrix[0, 0]; // [0,0] gives us the answer.
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(edit(first, second));
        }
    }
}
