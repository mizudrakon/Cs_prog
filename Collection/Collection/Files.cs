using System;
using System.IO;
namespace Collection
{
    static class Files
    {
        static string file1;
        static string file2;
        static string output;

        static bool input()
        {
            string h, h2;
            bool exists = true;
            Console.Write("\nPlese input the names of two files to merge devided by ',': ");
            string[] infiles = new string[2];
            infiles = Console.ReadLine().Split(',');
            if (!File.Exists(infiles[0]))
            {
                Console.WriteLine(infiles[0] + " doesn't exist!");
                exists = false;
                if (infiles.Length < 2) return false;
            }
            if (!File.Exists(infiles[1]))
            {
                Console.WriteLine(infiles[1] + " doesn't exist!");
                exists = false;
            }
            if (exists == false)
                return false;

            Console.Write("Please input the output file name: ");
            h = Console.ReadLine();
            while (File.Exists(h))
            {
                Console.Write("File: {0} already exists, do you want to rewrite it? (y/n/q) : ", h);

                while ((h2 = Console.ReadLine()) != "y" && h2 != "n" && h2 != "q") ;

                if (h2 == "n")
                {
                    Console.Write("Please input the output file name: ");
                    h = Console.ReadLine();
                }
                else if (h2 == "q")
                    return false;

            }

            file1 = infiles[0];
            file2 = infiles[1];
            output = h;
            return true;
        }

        static bool readNum(StreamReader R, out int num)
        {
            int n;
            int c;
            int minus = 1;
            if (R.EndOfStream)
            {
                num = -1;
                Console.WriteLine("EOS!!!!!!");
                return false;
            }
            while ((c = R.Peek()) < '0' || c > '9')
            {
                if (c == '-')
                {
                    minus = -1;

                }
                else minus = 1;
                if (c == -1)
                {
                    num = -1;
                    return false;
                }
                R.Read();
            }
            R.Read();
            n = minus * (c - '0');
            while ((c = R.Peek()) >= '0' && c <= '9')
            {
                n = 10 * n + (c - '0');
                R.Read();
            }
            num = n;
            return true;


        }

        static bool mergeTwo(string A, string B, string O)
        {
            /*
            if (File.Exists(A) == false || File.Exists(B) == false)
            {
                Console.WriteLine("Files don't exist!");
                return false;
            }
            */
            StreamReader Ar = new StreamReader(A);
            StreamReader Br = new StreamReader(B);
            StreamWriter Ow = new StreamWriter(O);


            int An, Bn;


            bool As = !Ar.EndOfStream;
            bool Bs = !Br.EndOfStream;
            As = readNum(Ar, out An);
            Bs = readNum(Br, out Bn);


            while (As || Bs)
            {

                //we need to get both numbers


                //if An <= Bn: write An and read new An until Bn < An

                //if Bn < An: write Bn and read new Bn until An <= Bn
                if (As && Bs)//there are both numbers, so we compare them
                {
                    if (An <= Bn)
                    {
                        Ow.Write("{0} ", An);
                        //Console.Write("{0} ", An);
                        As = readNum(Ar, out An);

                    }
                    else
                    {
                        Ow.Write("{0} ", Bn);
                        //Console.Write("{0} ", Bn);
                        Bs = readNum(Br, out Bn);

                    }
                }
                else//one or no number is present
                {
                    if (As)
                    {
                        Ow.Write("{0} ", An);
                        //Console.Write("{0} ", An);
                        if (!Ar.EndOfStream) As = readNum(Ar, out An);
                    }
                    if (Bs)
                    {
                        Ow.Write("{0} ", Bn);
                        //Console.Write("{0} ", Bn);

                        if (!Br.EndOfStream) Bs = readNum(Br, out Bn);
                    }
                }
                //Console.WriteLine("\nAs = {0} Bs = {1}", As, Bs);
                //Console.WriteLine("An = {0} Bn = {1}", An, Bn);

                /*
                if (!As && !Bs) {
                    Console.Write("!!!!!!!!!!!!!!");
                }
                */

            }
            Ow.WriteLine();

            Ar.Close();
            Br.Close();
            Ow.Close();

            return true;
        }

        public static void mergeTwoStrings()
        {
            if (input() == false)
                Console.WriteLine("Something went wrong. Please try again!");

            else if (mergeTwo(file1, file2, output))
                Console.WriteLine("Success! {0} created!", output);
            else
                Console.WriteLine("We have failed our master!");
            
        }        
    }
}
