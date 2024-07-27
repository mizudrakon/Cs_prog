using System;
namespace Collection
{
    static class Quick
    {
        //swaps two numbers in an array
        static void swap(ref int a, ref int b)
        {
            int h = a;
            a = b;
            b = h;
        }

        //returns index of chosen pivot : median of three
        public static int pivot(int[] P, int l, int r)
        {
            //int r = R - 1;
            int[] ar = { P[l], P[r / 2], P[r] };

            int i = 0;
            while (i < 2)
            {
                for (int j = 1; j <= 2; j++)
                {
                    if (ar[j - 1] > ar[j]) swap(ref ar[j], ref ar[j - 1]);

                }
                i++;
            }
            /*
            int p;
            if (ar[1] == P[l]) p = l;
            else if (ar[1] == P[r]) p = r;
            else p = r / 2;
            */
            return ar[1];
        }


        //QuickSort:
        public static void QuickSort(int[] P) 
        {
            int n = P.Length;
            int l = 0, r = n-1;
            QuickSort2(P, l, r);
            Input.ArrayWO(P);
        }

        //actual program
        static void QuickSort2(int[] P, int l, int r) 
        {
            if (l >= r) return;
            int p = pivot(P, l, r);
            int i = l, j = r;
            while (i <= j) 
            {
                while (P[i] < p) i++;
                while (P[j] > p) j--;
                if (i < j) swap(ref P[i], ref P[j]);
                if (i <= j) { i++;j--; }

            }
            QuickSort2(P, l, j);
            QuickSort2(P, i, r);
        }


        //LinearSelect - gives us k-th smalles element from the given sequence
        //uses itself to get its pivot

        static int max(int[] P) {
            int m = P[0];
            for (int i = 1; i < P.Length; i++)
                if (P[i] > m) m = P[i];
            return m;
        }

        static int Trivial(int[] P, int k) 
        {
            
            int n = P.Length;
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
                A[i] = P[i];
            QuickSort2(A, 0, n-1);
            if (k >= n-1) return A[n-1];
            return A[k];

        }


        public static int LinearSelect2(int[] P, int k) 
        {
        

            int n = P.Length;
            //trivial solution
            if (n <= 5) return Trivial(P, k);
                
            int[] Five = new int[5];
            int h = (n % 5 == 0) ? 0 : 1;
            int[] meds = new int[n/5 + h];
            //I want to go through 5 elements adding them into a field Five
            //get their median and add it to meds
            int j = 0;
            for (int i = 0; i < n; i++) {
                Five[j] = P[i];
                j++;
                //if we hit mod 5 or end
                if (i % 5 == 4 || i == n-1) {
                    while (j < 4) { Five[j] = 0; j++; }
                    meds[i/5] = LinearSelect2(Five, 2);
                    j = 0;
                }

            }
            
            int p = LinearSelect2(meds, n / 10 + h);
            
            //now we divide P into three parts L,E,M
            int[] L = new int[n];//less
            int[] E = new int[n];//equal
            int[] M = new int[n];//more
            int l = 0, e = 0, m = 0;
            for (int i = 0; i < n; i++) {
                if (P[i] < p)
                {
                    L[l] = P[i];
                    l++;
                }
                else if (P[i] == p)
                {
                    E[e] = P[i];
                    e++;
                }
                else {
                    M[m] = P[i];
                    m++;
                }
            }
            //changing the sizes to l,m,e instead of n (that leads to stack overflow otherwise)
            L = fixA(L,l);
            M = fixA(M, m);
            E = fixA(E,e);
        
            if (k <= L.Length) return LinearSelect2(L, k-1);
            if (k <= L.Length + E.Length) return p;
            return LinearSelect2(M, k - L.Length - E.Length - 1);
        }

        //fixes array length (initializing an array that fits the data and copying them)
        static int[] fixA(int[] A, int len) 
        {
            
            int[] H;

            H = new int[len];

            for (int i = 0; i < len; i++)
                H[i] = A[i];
            
            return H;
        }
    }
}
