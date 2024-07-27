
using System;
using System.Collections.Generic;

namespace dijkstra {		//zadavam poc vrch, poc hran, vrch vrch delka,..., vrchol vrchol

	class IntReader {
		private Queue<int> buf=new Queue<int>();
		private bool eof=false;

		public IntReader() {
			FillBuf();
		}

		public void FillBuf() {
			/* read from console. Make sure that either EOF is set, or there's
			 * at least one integer in the buffer */
			while(buf.Count==0 && !eof) {
				string res=Console.ReadLine();
				if(res==null) eof=true; //ReadLine returns null on EOF
				else {
					string[] toks = res.Split(); //split by spaces
					int i;
					foreach(string tok in toks)
						if(Int32.TryParse(tok,out i)) //see if it's an integer
							buf.Enqueue(i); //enqueue it
				}
			}
		}

		public bool isEOF() {
			/* we signal EOF when there's nothing left in the buffer, and
			 * nothing else to read from the console. */
			return (buf.Count==0) && eof;
		}

		public int Read() {
			/* return the next read integer.
			 *
			 * ASSERT: Fails if isEOF is true. */
			int res=buf.Dequeue();
			if(buf.Count==0) FillBuf();
			return res;
		}
	}

	struct Edge {
		public int to, len;

		public Edge(int t, int l) {
			to=t;
			len=l;
		}
	}

	class Graph {
		List<Edge>[] v;

		public Graph(int vertices) {
			v=new List<Edge>[vertices];
			for(int i=0;i<vertices;++i)
				v[i]=new List<Edge>();
		}

		public void AddEdge(int from, int to, int len) {
			v[from].Add(new Edge(to, len));
		}

		public void AddSymEdge(int from, int to, int len) {
			AddEdge(from,to,len);
			AddEdge(to,from,len);
		}
		//new stuff begin
		struct dist_idx:IComparable<dist_idx>
		{
			public int CompareTo(dist_idx d_i){
				if (this.dist > d_i.dist)
					return 1;
				if (this.dist < d_i.dist)
					return -1;
				if (this.v > d_i.v)
					return 1;
				if (this.v < d_i.v)
					return -1;
				return 0;
			}

				
			public int dist, v;
			public dist_idx(int d,int v){
				this.dist = d;
				this.v = v;
			}

		}
		//new stuff end
		public int ShortestDistance(int from, int to) {
			//New stuf begin
			visited = new bool (v.length);
			for (int i = 0; i < visited.length; ++i) 
				visited [i] = false;
			SortedSet<dist_idx> set = new SortedSet<dist_idx>();
			set.Add (new dist_idx (0, from));
			while (set.Count > 0) {
				dist_idx.current = set.Min;
				set.Remove (set.Min);
				if (visited [current.v])
					continue;
				visited [current.v] = true;
				if (current.v == to) {
					return current.dist;
				}
			
				foreach (Edge e in v[current.v]) {
					if (visite [e.to])
						continue;
					set.Add (new dist_idx (e.len + current.dist, e.to));
				}
			}
			//New stuff end

			return -1;
		}
	}

	class main {
		public static void Main() {
			IntReader ir=new IntReader();
			Graph g=new Graph(ir.Read());
			int edges=ir.Read();
			int from,to,len;
			for(int i=0;i<edges;++i) {
				from=ir.Read();
				to=ir.Read();
				len=ir.Read();
				g.AddSymEdge(from,to,len);
			}

			from=ir.Read();
			to=ir.Read();
			Console.WriteLine("Shortest distance from {0} to {1} is {2}",
				from, to, g.ShortestDistance(from,to));
		}
	}
}
