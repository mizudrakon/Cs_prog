using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanoi
{
/*
	Towers of Hanoi
	-tehre are 64 disks on rod1
	-input: number -> number of disks to be moved from rod1 to rod2
	-output: Kotouc [number-how far form top] z [1-3] na [1-3]\n (has to be in czech

	how to do it?
	with recursion...
	n - number of disks to be moved
	
	if (n == 1):
	   move(1,1,2) - 1st from rod 1 moves on top rod 2
	else:
	   move(n-1,1,3)
	   move(1,1,2)
	   move(n-1,3,2)
*/




	public class Program
	{
	
		static void move(int n, int a, int b, int c)
		{
			if (n == 1){
				Console.WriteLine("Kotouc {0} z {1} na {2}",n,a,b);
			} else {
				move(n-1,a,c,b);
				Console.WriteLine("Kotouc {0} z {1} na {2}",n,a,b);
				move(n-1,c,b,a);
			}

		}

		static void Main (string[] args)
		{
			int r1 = 1;
			int r2 = 2;
			int r3 = 3;
			int n = Int32.Parse(Console.ReadLine());
			if (n > 64) n = 64;
			move(n,r1,r2,r3);		

		}
	}
}

