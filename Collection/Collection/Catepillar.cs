using System;
namespace Collection
{
    static class Catepillar
    {

        static bool minus = false;
        //read number


        static string flippedAdd(string X, string Y, int shift)
        {
            string preanswer = "";

            while (shift > 0)
            {
                Y += 0;
                shift--;
            }

            int n = X.Length - 1;
            int m = Y.Length - 1;
            int c, o = 0;//o for overflow
            int h;


            while (n >= 0 || m >= 0)
            {

                if (n >= 0 && m >= 0)
                {

                    c = (X[n] + Y[m] - 2 * '0');
                    h = c + o;
                    preanswer += h % 10;
                    o = h / 10;
                    n--; m--;
                }
                else if (n < 0 && m >= 0)
                {
                    c = Y[m] - '0';
                    h = c + o;
                    preanswer += h % 10;
                    o = h / 10;
                    m--;
                }
                else if (n >= 0 && m < 0)
                {
                    c = X[n] - '0';
                    h = c + o;
                    preanswer += h % 10 + o;
                    o = h / 10;
                    n--;
                }
                if (n < 0 && m < 0 && o > 0)
                    preanswer += o;


            }

            return preanswer;

        }

        static string add(string X, string Y, int shift)
        {
            string answer = "";
            string preanswer = flippedAdd(X, Y, shift);
            for (int i = preanswer.Length - 1; i >= 0; i--)
            {
                answer += preanswer[i];
            }
            return answer;
        }


        static string multiStep(string X, int a)
        {
            string answer = "";
            string preanswer = "";
            int n = X.Length - 1;
            int c, o = 0;
            int h;
            for (int i = n; i >= 0; i--)
            {
                c = a * (X[i] - '0');
                h = c + o;

                preanswer += h % 10;
                o = h / 10;
            }
            if (o > 0)
            {
                preanswer += o;
                //Console.WriteLine("change"); 
            }
            for (int i = preanswer.Length - 1; i >= 0; i--)
            {
                answer += preanswer[i];
            }
            //Console.WriteLine("{0} : {1}", a, answer);
            return answer;
        }


        static string multi(string X, string Y)
        {
            string A = X;
            string B = Y;

            if (X.Length < Y.Length)
            {
                A = Y;
                B = X;
            }
            //int n = A.Length - 1;
            int m = B.Length - 1;
            string answer = "0";
            int j = 0;
            for (int i = m; i >= 0; i--)
            {
                answer = add(answer, multiStep(A, B[i] - '0'), j);
                j++;
            }


            return answer;
        }

        static void minusChange()
        {
            if (minus) minus = false;
            else minus = true;
        }

        static string fix(string X)
        {
            int n = X.Length - 1;
            string A = "";
            int i = 0;
            while (X[i] < '0' || X[i] > '9')
                i++;
            //Console.WriteLine("j is {0}", i);
            //Console.WriteLine(X[i - 1]);
            if (i > 0)
            {
                if ((char)X[i - 1] == '-') minusChange();
                for (; i <= n; i++)
                {
                    if (X[i] < '0' || X[i] > '9')
                        return A;
                    A += X[i];
                }
                //minusChange();
                return A;
            }
            return X;
        }
    }
}
