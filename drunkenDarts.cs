using System;

namespace DrunkenDarts
{
    static class Input 
    {
        public static int S;
        public static int P;
        public static int[] sectors;
        public static int[] players;
        public static int min = 0, max = 0;
        public static int pmax = 0, pmin = 0;

        public static bool readInput()
        {
            int h, j;

            //reading data about thse sectors
            if (Int32.TryParse(Console.ReadLine(), out S))
                sectors = new int[S];
            else return false;
            j = 0;
            //I'd love to just ignore all the repeated numbers, but I don't want to recheck all the already loaded
            for (int i = 0; i < S; i++)
            {
                if (Int32.TryParse(Console.ReadLine(), out h))
                {
                    if (j == 0)
                    {
                        sectors[j] = h;
                        j++;
                    }
                    else if (h != sectors[j - 1])
                    {
                        sectors[j] = h;
                        j++;
                    }


                    min = (min == 0 || h < min) ? h : min;
                    max = (max == 0 || h > max) ? h : max;

                }

                else return false;
            }

            S = j - 1;

            //reading data about the players
            //it's all marked as unreachable, but it works
            if (Int32.TryParse(Console.ReadLine(), out P))
                players = new int[P];
            for (int i = 0; i < P; i++)
            {
                if (Int32.TryParse(Console.ReadLine(), out players[i]))
                {
                    pmax = (players[i] > pmax) ? players[i] : pmax;
                    pmin = (players[i] < pmin) ? players[i] : pmin;
                    if (players[i] < 2 * min) 
                    {
                        //Console.WriteLine("Lumparna!");
                        return true;
                    }
                }
                else return false;
            }
            P--;


            return true;
        }
    }


    public class Node
    {
        public int key { get; set; }
        public int val { get; set; }
        public Node L = null;
        public Node R = null;

        public Node(int key, int val) 
        {
            this.key = key;
            this.val = val;
        }




    }

    public class Tree 
    {
        Node root { get; set; }

        public Tree(Node root) {
            this.root = root;
        }

        public void add(Node v, Node u)
        {
            if (v.key < u.key)
            {
                if (u.L == null)
                    u.L = v;
                else
                {
                    add(v, u.L);
                }
            }
            else
            {
                if (u.R == null)
                    u.R = v;
                else
                {
                    add(v, u.R);
                }
            }
        }

        public void Show(Node r) 
        {
            if (r.L != null)
                Show(r.L);
            Console.WriteLine(r.val);
            if (r.R != null) Show(r.R);
        }
    }


    public static class Game
    {
        public static int[] sea = new int[1002];

        static void prepare()
        {
            //preparing sea to map the distance of all reachable numbers
            for (int i = 1; i <= 1001; i++)
                sea[i] = -1;//we haven't reach the number yet
            //game finishing numbers reachable with a single trow are a special case
            //we want to fill these positions with 1, because they requre 1 throw
            int h;
            for (int i = 0; i <= Input.S; i++)
            {
                h = Input.sectors[i] * 2;
                if (sea[h] == -1)
                {
                    sea[h] = 1;
                }
                h += Input.sectors[i];
                if (sea[h] == -1)
                {
                    sea[h] = 1;
                }
            }
        }

        //probably not using this
        /*
        public static int check(int num) {
            for (int i = 0; i <= Input.S; i++) 
            {
                if (sea[Input.players[i]] == num) return i + 1;
            }
            return 0;
        }*/

        //this is supposed to fill the sea array
        public static bool swimInNumbers()
        {
            int change = 1;
            int i = 2;
            int h, mh = 1;
            bool first = true;

            prepare();

            while (change > 0)
            {
                
                change = 0;
                for (int j = Input.min; j <= Input.pmax; j++)
                {
                    //Console.WriteLine("i{0} j{1}",i,j);

                    if (sea[j] == i - 1)
                    {
                        if (first == true)
                        {
                            first = false;
                            mh = j;
                        }

                        for (int s = 0; s <= Input.S; s++)
                        {
                            //Console.WriteLine("i{0} j{1} s{2}", i, j, s);
                            h = Input.sectors[s] + j;
                            if (h <= 1001 && sea[h] == -1)
                            {
                                sea[h] = i;
                                change++;

                            }

                            h += Input.sectors[s];
                            if (h <= 1001 && sea[h] == -1)
                            {
                                sea[h] = i;
                                change++;
                            }
                            h += Input.sectors[s];
                            if (h <= 1001 && sea[h] == -1)
                            {
                                sea[h] = i;
                                change++;
                            }
                        }
                    }
                }
                //Console.WriteLine("i{0} max{1}", i, Input.max);
                //Input.max = (3 * Input.max < 1001)? 3*Input.max:1001; didn't work

                Input.min = mh;
                first = true;
                i++;
            }
            return true;
        }//swimInNumbers end

        public static void printSea() 
        {
            for (int i = 1; i <= Input.max; i++)
                Console.Write("{0}:{1} ",i,sea[i]);    
        }



        //using a binary tree to print out the result... hope it's enough...
        public static bool getResult() 
        {
            Node root;
            Node temp;
            Tree R;

            if (Input.P == 0)
            { 
                Console.WriteLine(1);
                return true;
            }
            else if (sea[Input.players[0]] != -1)
                root = new Node(sea[Input.players[0]], 1);
            else return false;
            R = new Tree(root);
            for (int i = 1; i <= Input.P; i++) {
                if (sea[Input.players[i]] == -1)
                {
                    //Console.WriteLine("lumparna!");
                    return false;
                }
                else 
                {
                    temp = new Node(sea[Input.players[i]], i + 1);
                    R.add(temp, root);
                }
            }

            R.Show(root);

            return true;
        }
    }


    class MainClass
    {
        public static void Main(string[] args)
        {
            if (Input.readInput() == false) Console.WriteLine("Lumparna");
            else
            {
                Game.swimInNumbers();

                if (Game.getResult() == false)
                    Console.WriteLine("Lumparna!");
            }

            //Game.printSea();
            /*for (int i = 0; i <= Input.P; i++)
                Console.WriteLine(Game.sea[Input.players[i]]);
                */
        }
    }
}
