using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Weighted
{
    
    public class Element
    {
        
        
        public int name { get; set; }
        public double key { get; set; }
        //public int p { get; set; }


        public Element(int name, double key) 
        {
            this.name = name;
            this.key = key;
        }

        public Element(int name)
        {
            this.name = name;
            key = Graph.MAX;
            //p = 0;
        }
    }

    public class Graph
    {
        public const double MAX = Double.MaxValue;
        public int V { get; set; } //number of vertices
        public List<Element>[] G;
        public double[,] edge;
        public PQueue graph;


        public Graph(int V)
        {
            Element e;

            this.V = V;
            G = new List<Element>[V + 1];
            edge = new double[V + 1, V + 1];
            graph = new PQueue(V);
            graph.add(e = new Element(1, 0));
            for (int i = 2; i <= V; i++) {
                e = new Element(i);
                graph.add(e);
            }
            //W = new double[V + 1][];
        }

        //deals with a single line of input
        bool readEdge(out int v, out int w, out double l) 
        {
            bool check = true;
            string line = Console.ReadLine();
            string[] vars = new string[3];

            if (line == null)
            {
                v = -1;
                w = -1;
                l = -1;
                return false;
            }

            vars = line.Split(' ');


            if (vars.Length == 3)
            {
                check = (Int32.TryParse(vars[0], out v));
                if (Int32.TryParse(vars[1], out w) == false || check == false) check = false;
                if (Double.TryParse(vars[2], out l) == false || check == false) check = false;
                if (check == false)
                {
                    v = -1;
                    w = -1;
                    l = -1;
                    Useful.error(1);
                    return false;
                }
            }
            else 
            {
                v = -1;
                w = -1;
                l = -1;
                Useful.error(1);
                return false; 
            }

            return true;
        }

        //fills in the graph
        public void readGraph()
        {
            //int[] edges = new int[2*V+1];
            //double[] weights = new double[2 * V+1];
            int v, w;
            double l;
            int i = 1;
            G[i] = new List<Element>();
            //Element e;
            //edges[0] = 0;
            while (readEdge(out v, out w, out l)) {
                if (v != i) {
                    i = v;
                    G[i] = new List<Element>();
                }
                edge[i, w] = l;

                G[i].Add(graph.Heap[w]);
            }
        }

    }

    //error messages and simple functions for reading numbers and so on...
    static class Useful 
    {
        public static void error(int i)
        {
            if (i == 1)
                Console.WriteLine("INPUT ERROR!");
            if (i == 2)
                Console.WriteLine("Something went sooo wrong...");
            if (i == 3)
                Console.WriteLine("Element isn't in the dictionary!");
            else
                Console.WriteLine("Something else failed...");
        }

    }

    interface PriorityQueue<T>
        {
            int count { get; }
            void add(T elem);
            T extractMin();
            void decrease(T elem, double priority);
        }


    //minimal priority queue
    public class PQueue : PriorityQueue<Element>
    {
        public int count { get; set; }
        public int capacity { get; set; }
        public Element[] Heap;
        public Dictionary<Element, int> ElDict = new Dictionary<Element, int>();


        public PQueue(int V)
        {
            //Int32.TryParse(Console.ReadLine(), out int n);
            capacity = V;
            Heap = new Element[capacity + 1];
            count = 0;
        }


        //inserts the new guest at the bottom of the heap and bubbles her up as far as she can go                                                                                                           
        public void add(Element elem)
        {

            int i = ++count;
            int p;

            while (i > 1 && elem.key < Heap[p = i / 2].key && p != 0)
            { //object reference not set up to an instant of an object                                                                       
                Heap[i] = Heap[p];
                ElDict[Heap[i]] = i;
                i = p;
            }
            Heap[i] = elem;
            ElDict.Add(elem, i); //adding to the dictionary
            //Console.WriteLine("dict:{0} heap:{1}",ElDict[elem], Heap[ElDict[elem]].key );
        }

        //finds the bigger key in daughter elements and returns index of that element                                                                                                                       
        int findMin(int v)
        {
            int u = 2 * v; //left daughter                                                                                                                                                                             
            if (u > capacity || Heap[u] == null) return v; //there is no index 2*v in the heap, nor is 2*v+1, v is still the smalles one
            if (u + 1 <= capacity && Heap[u+1] != null && Heap[u + 1].key <= Heap[u].key) return u + 1; //there is 2*v, if 2*v+1 key is smaller, return it 
            return u; //else return u                                                                             
        }

        //returns the first element from the heap, fixes the heap after its removal                                                                                                                         
        public Element extractMin()
        {
            int v = 1;
            int u = 1;
            Element result = Heap[v];
            ElDict.Remove(result);
            Element helper = Heap[count];

            Heap[count] = null;
            count--;
            while (2 * v <= capacity && Heap[2 * v] != null && Heap[u = findMin(v)].key < helper.key)
            {
                Heap[v] = Heap[u];
                ElDict[Heap[u]] = v;
                v = u;
            }
            Heap[v] = helper;
            ElDict[helper] = v;

            return result;
        }

        //needs to find and decrease priority of a specified element
        public void decrease(Element elem, double priority)
        {
            int i = ElDict[elem];
            int p;
            Element helper;
            Heap[i].key = priority;
            while ((p = i /2) > 0 && priority < Heap[p].key) {
                helper = Heap[p];
                Heap[p] = Heap[i];
                ElDict[Heap[p]] = p;
                Heap[i] = helper;
                ElDict[Heap[i]] = i;
                i = p;

            }
        }

    }

    class MainClass
    {
        public const double MAX = Graph.MAX;
        //dijkstra
        public static Tuple<int[], double[]> dijkstra(Graph G) 
        {
            double[] dist = new double[G.V+1];
            int[] p = new int[G.V + 1];
            //p[1] = 1;
            double d;
            //int father = 0;
            Element v;

            while (G.graph.count > 0) {
                v = G.graph.extractMin();
                if (G.G[v.name] != null)
                {
                    foreach (Element u in G.G[v.name])
                    {
                        if (u.key > (d = v.key + G.edge[v.name, u.name]))
                        {
                            G.graph.decrease(u, d);
                            p[u.name] = v.name;
                            dist[u.name] = d;
                        }
                    }
                }
                dist[v.name] = v.key;

            }
                
                

            return Tuple.Create(p, dist);
        }

        //debugging write out
        public static void writeOut(int[] a) 
        {
            int l = a.Length;
            for (int i = 1; i <= l - 2; i++) {
                Console.Write("{0}:{1} ", i, a[i]);
            }
            Console.WriteLine("{0}:{1}",l-1,a[l-1]);
        }

        public static void writeOut(double[] a)
        {
            int l = a.Length;
            for (int i = 1; i <= l - 2; i++)
            {
                Console.Write("{0}:{1} ", i, a[i]);
            }
            Console.WriteLine("{0}:{1} ", l-1, a[l-1]);
        }

        //writes out a single line specifying end vertex i distance d[i]: sequence specifying the path to i 
        public static bool iResult(int i, int[] p, double[] d) 
        {
            if (d[i] == MAX) return false;
            Stack<int> path = new Stack<int>();

            Console.Write("{0} {1:0.0}: ", i, d[i]);

            while (i > 0) 
            {
                path.Push(i);
                i = p[i];
            }
            while (path.Count > 1) {
                Console.Write("{0} ", path.Pop());
            }
            Console.WriteLine(path.Pop());
            return true;

        }


        public static void Main(string[] args)
        {
            Graph g;
            Tuple<int[], double[]> t;
            if (Int32.TryParse(Console.ReadLine(), out int N))
            {
                g = new Graph(N);
                g.readGraph();
                t = dijkstra(g);

                for (int i = 1; i <= N; i++) {
                    iResult(i, t.Item1, t.Item2);
                }

                //writeOut(t.Item1);
                //writeOut(t.Item2);
            }
            else { Console.WriteLine("I've got nothing!"); }   
        }
    }
}
