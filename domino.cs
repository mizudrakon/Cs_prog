using System;
using System.Collections.Generic;
namespace DominoLength
{
    static class Input 
    {
        public static int size { get; set; }
        //ok, this is an array of values from the domino pieces
        //Tile connects the value and the index of the paired value in the array, it could be just an array of pairs...
        public static Tile[] readInput() {
            
            Tile[] tiles = new Tile[size];
            for (int i = 0; i < size; i+=2) {
                tiles[i] = new Tile();
                tiles[i].val = readNum();
                tiles[i + 1] = new Tile();
                tiles[i + 1].val = readNum();
                tiles[i].pair = i + 1;
                tiles[i + 1].pair = i;

            }
            return tiles;
        }

        //number reading function
        static public int readNum()
        {
            int num = 0;
            int c = Console.Read();

            while (c < '0' || c > '9')
            {
                c = Console.Read();
            }

            num = num * 10 + (c - '0');

            while ((c = Console.Read()) >= '0' && c <= '9')
            {
                num = num * 10 + (c - '0');
            }

            return num;
        }
    }

    //This class just connects the domino, every value points to its paired value
    class Tile 
    {
        public int val { get; set; }
        public int pair { get; set; }

    }



    class MainClass
    {

        static Tile[] dominoes; 
        static int[] state;

        //the idea is to recursively try every piece with every piece that fits and remember the maximum recursion depth
        static int recombine(int v) {
            int max = 1;

            int depth = 0;
            //removing values of the last chosen piece
            state[v] = 0;
            state[dominoes[v].pair] = 0;
            int count = 0;
            /*//just for debugging
            for (int i = 0; i < Input.size; i++)
                Console.Write("{0} ", state[i]);
            Console.Write("\n");
            */
            for (int u = 0; u < Input.size; u++)
            {
                //we're only checking matching pieces!!
                if ( dominoes[dominoes[v].pair].val == dominoes[u].val && state[u] == 1)
                {
                    depth = recombine(u)+1;
                    count++;
                }

                if (depth > max) max = depth;
            }
            state[v] = 1;
            state[dominoes[v].pair] = 1;
            if (count == 0) return 1;
            //Console.WriteLine("{0} {1}", depth, max);
            return max;
        }



        public static void Main(string[] args)
        {

            Input.size = 2*Input.readNum(); //the first input number k is a number of domino pieces, but we're using 2k array collecting each single value
            dominoes = new Tile[Input.size];
            dominoes = Input.readInput();
            state = new int[Input.size]; //yes, this is to avoid using Lists... they would have to be added (recodex probably wouldn't mind, but still...)
            for (int s = 0; s < Input.size; s++)
                state[s] = 1;
            //longest vs actual path across dominoes for one specific piece with a value of d
            int longest = 0;
            int path;
            for (int d = 0; d < Input.size; d++)
            {
                path = recombine(d);
                if (longest < path)
                    longest = path;
            }
            Console.WriteLine(longest);

        }
    }
}
