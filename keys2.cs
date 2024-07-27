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
                continue;
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
            //long m;
            //long h; //help
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
            //m = l / 2;
            //m = (l % 2 == 1) ? m+1 : m;
            //Console.WriteLine("m = {0}",m);
            //Console.WriteLine("{0} % 2 = {1}", l);
            long[] paths = new long[l];
            long[] temp = new long[l];
            //l>2 and k>1
            paths[0] = 2;
            for (long i = 1; i < l-1; i++) 
                paths[i] = 3;
            paths[l - 1] = 2;
            if (k == 2) return arraySum(paths, l);
            //writeAr(paths, l);

            for (long i = 3; i <= k; i++) 
            {
                temp[0] = paths[0] + paths[1];
                for (long j = 1; j <= l - 2; j++) {
                    
                    //Console.WriteLine("temp[j] = {0} + {1} + {2} = {3}", paths[j-1], paths[j], paths[j + 1], paths[j - 1] + paths[j] + paths[j + 1]);
                    temp[j] = paths[j-1] + paths[j] + paths[j + 1];
                }

                temp[l - 1] = temp[0];
                //writeAr(paths,l);
                paths = temp;
                temp = new long[l];
                writeAr(paths, l);
           
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
