using System;

namespace Coffe
{
    interface PriorityQueue<T>
    {
        int count { get; }         // number of elements currently in the queue
        void add(T elem, double priority);
        Tuple<T, double> extractMax();  // remove and return element with highest priority
    }

    public class Guest 
    {
        public string name { get; set; }
        public double key { get; set; }

        public Guest(string name, double key) {
            this.name = name;
            this.key = key;
        }
    }

    class PQueue : PriorityQueue<Guest> 
    {
        public int count { get; set; }
        public int capacity { get; set; }
        Guest[] guestList;


        public PQueue() 
        {
            Int32.TryParse(Console.ReadLine(), out int n);
            capacity = n;
            guestList = new Guest[n+1];
            count = 0;
        }


        //inserts the new guest at the bottom of the heap and bubbles her up as far as she can go
        public void add(Guest elem, double priority) 
        {
            
            int i = ++count;
            int p;

            while (i > 1 && elem.key > guestList[p = i / 2].key && p != 0) { //object reference not set up to an instant of an object
                guestList[i] = guestList[p];
                i = p;
            }
            guestList[i] = elem;
        }

        //finds the bigger key in daughter elements and returns index of that element
        int findMax(int v) 
        {
            int u = 2 * v + 1;
            if (u > capacity || guestList[u] == null) return 2 * v;
            if (guestList[2 * v].key >= guestList[u].key) return 2 * v;
            return u;
        }

        //returns the first element from the heap, fixes the heap after its removal
        public Tuple<Guest, double> extractMax() 
        {
            int v = 1;
            int u = 1;
            Guest result = guestList[v];

            Guest helper = new Guest(guestList[count].name, guestList[count].key);

            guestList[count] = null;
            count--;
            while (2*v <= capacity && guestList[2*v] != null && guestList[u = findMax(v)].key > helper.key) {
                guestList[v] = guestList[u];
                v = u;
            }
            guestList[v] = helper;

            return Tuple.Create(result, result.key);
        }
    }

    class MainClass
    {
        //the heap of a specific capacity, construction produces this capacity as PQueue.capacity
        public static PQueue coffeQ = new PQueue();


        //reads a single new line from standard input, returns false if there's no line or if the input isnt [string]' '[double]
        //if the input's right, it's added into the heap
        public static bool readInput() 
        {
            Guest g;
            string[] input = new string[2];
            string h;
            double key;
            if ((h = Console.ReadLine()) == null) return false;

            input = h.Split(' ');
            if (h == "") 
            {
                //Console.WriteLine("empty line!");
                return false;
            }
            if (input.Length < 2) 
            {
                //Console.WriteLine("Wrong input!");
                return false; 
            
            }
            if (Double.TryParse(input[1], out key) && key >= 0 && key <= 100)
                g = new Guest(input[0], key);
            else 
            {
                //Console.WriteLine("Not a number!");
                return false; 
            }

            coffeQ.add(g, g.key);

            return true;
        }

        public static void writeElement() 
        {
            Tuple<Guest, double> t;
            t = coffeQ.extractMax();
            Console.WriteLine("{0} {1:F1}", t.Item1.name, t.Item2);
        }

        public static void Main(string[] args)
        {
            int i;

            while (readInput() != false) 
            {
                if (coffeQ.count == coffeQ.capacity)
                {
                    writeElement();
                }
            }
            while ((i = coffeQ.count) > 0) {
                writeElement();
                i--;
            }
        }
    }
}
