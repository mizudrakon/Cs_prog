using System;

namespace Collection
{
    static class DandC
    {

        //MergeSort
        public static int[] Merge(int[] B1, int[] B2) {

            int n = B1.Length + B2.Length;
            int[] A = new int[n];
            int a = 0, b = 0, i;
            for (i = 0; i < n; i++)
            {
                if (a < B1.Length && b < B2.Length)
                {
                    if (B1[a] < B2[b])
                    {
                        A[i] = B1[a];
                        a++;

                    }
                    else
                    {
                        A[i] = B2[b];
                        b++;
                    }
                }
                else 
                {
                    while (a < B1.Length)
                    {
                        A[i] = B1[a];
                        a++;
                        i++;
                    }
                    while (b < B2.Length)
                    {
                        A[i] = B2[b];
                        b++;
                        i++;
                    }
                }
            }

                    

            return A;

        }
        public static int[] MergeSort(int[] A) 
        {
            
            int[] B1, B2;

            int n = A.Length;

            if (n > 1) 
            {
                B1 = new int[n / 2];
                for (int i = 0; i < n / 2; i++)
                    B1[i] = A[i];
                int j = n / 2;
                B1 = MergeSort(B1);
                B2 = new int[n - j];
                for (int i = 0; i < n - n / 2; i++) {
                    B2[i] = A[j];
                    j++;
                }
                B2 = MergeSort(B2);
                A = Merge(B1, B2);
            }
            return A;
        }


        //Karatsuba - multiplication
        public static string Karatsuba() {
            string X = Console.ReadLine();
            string Y = Console.ReadLine();
            return Karatsuba2(X, Y);
        }

        static string add(string X, string Y) 
        {
            string A = X;
            string B = Y;
            string result = "";
            if (B.Length > A.Length) { A = Y; B = X; }
            int n = A.Length-1, m = B.Length-1;
            int o = 0;
            while (n >= 0 && m >= 0) 
            {
                
            
            }

        }

        static string Karatsuba2(string X, string Y) 
        {
            if (X.Length <= 1 || Y.Length <= 1) return "asd";
        }
    }
}
