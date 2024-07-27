using System;

namespace Joseph
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            long N, i=1;
            if (Int64.TryParse(Console.ReadLine(), out N) == false || N <= 0)
            {
                Console.WriteLine("ERROR");
            }
            else if (N == 1) Console.WriteLine(1);
            else
            {
                for (long c = 2; c <= N; c++)
                {
                    i += 2;
                    if (i != c)
                        i = i % c;
                    
                }
                Console.WriteLine(i);
            }
           
        }
    }
}
