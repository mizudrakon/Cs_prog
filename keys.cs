using System;

namespace keys
{
    class MainClass
    {
        static long K = -1;
        static long L = -1;

        //reads only positive integers or 0
        public static long readNum() 
        {
            int c;
            long n;

            while ((c = Console.Read()) < '0' || c > '9')
                ;
            n = c - '0';
            while ((c = Console.Read()) >= '0' && c <= '9') {
                n = 10 * n + (c - '0');
            }
            return n;
        }

        public static bool readInput() 
        {
            string s;
            K = readNum();
            L = readNum();
            while (( s = Console.ReadLine()) != "")
                s = Console.ReadLine();
            if (K != -1 && L != -1)
                return true;
            else return false;
        }

        public static long arraySum(long[] A, long l) 
        {
            long sum = 0;
            for (long i = 0; i < l; i++)
                sum += A[i];
            return sum;
        }

        public static void writeAr(long[] A, long l) 
        {
            for (long i = 0; i < l; i++)
                Console.Write("{0} ", A[i]);
            Console.WriteLine();
        }

        public static long confs(long k, long l) {
            long m;
            long h; //help
            long result;
            //for l = 1 there can be only one configuration
            if (k == 0 || l == 0 || k < 0 || l < 0) return 0;
            if (l == 1) return 1;
            if (k == 1) return l;
            //for l = 2 thre are only 2 possibilities at every point l,k, which adds up to 2^(k-1)
            else if (l == 2) {
                result = 2;
                for (long i = 1; i < k; i++)
                    result *= 2;
                return result;
            }
            //more possible positions
            m = l / 2;
            m = (l % 2 == 1) ? m+1 : m;
            //Console.WriteLine("m = {0}",m);
            //Console.WriteLine("{0} % 2 = {1}", l);
            long[] paths = new long[l];
            //l>2 and k>1
            paths[0] = 2;
            for (long i = 1; i < l-1; i++) 
                paths[i] = 3;
            paths[l - 1] = 2;
            if (k == 2) return arraySum(paths, l);
            //writeAr(paths, l);
            //OK, so, the array will always be symmetric, so I can do this with a single array and do all the computations with only half the elements
            for (long i = 3; i <= k; i++) 
            {
                paths[0] = paths[l - 1] + paths[l - 2];
                for (long j = 1; j < m-1; j++) {
                    paths[j] = paths[l-j] + paths[l - j - 1] + paths[l - j - 2];
                }
                //the routine doesn't work for even L, but this should work generally for the middle point
                if (m > 2)
                    paths[m - 1] = paths[m - 1] + paths[m] + paths[m];
                else paths[m - 1] = paths[m - 1] + paths[m] + paths[l - 1];
                
                if (l % 2 == 0)
                {
                    paths[m] = paths[m-1];
                    h = m;
                }
                else h = m-1;
                for (long j = 1; j <= h-1; j++)
                    paths[h + j] = paths[m - j - 1];
                paths[l - 1] = paths[0];
                writeAr(paths,l);
           
            }

            return arraySum(paths,l);
        }


        public static void Main(string[] args)
        {
            if (readInput())
                Console.WriteLine(confs(K, L));
            else Console.WriteLine("ERROR!");
        }
    }
}
