using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
	public class Program
	{
	    public static long[] kthPermutation(long n, long k){
		long[] nums = new long[] {1, 2, 3};
		long[] factorial = new long[n+1];

		factorial[0] = 1;
		factorial[1] = 1;
		//nums[0] = 1;
	
		for (int i = 2; i <= n; i++) {
		    //nums[i-1] = i;
		    factorial[i] = i*factorial[i - 1];
		}
	
		if(k <= 1){
		    return nums;
		}
		if(k >= factorial[n]){
		    reverse(nums, 0, n-1);
		    return nums;
		}
	
		k -= 1;//0-based 
		for(long i = 0; i < n-1; i++){
		    long fact = factorial[n-i-1];
		    //index of the element in the rest of the input set
		    //to put at i position (note, index is offset by i)
		    long index = (k/fact);
		    //put the element at index (offset by i) element at position i 
		    //and shift the rest on the right of i
		    shiftRight(nums, i, i+index);
		    //decrement k by fact*index as we can have fact number of 
		    //permutations for each element at position less than index
		    k = k - fact*index;
		}
	
		return nums;
	    }

	    private static void shiftRight(long[] a, long s, long e){
		long temp = a[e];
		for(long i = e; i > s; i--){
		    a[i] = a[i-1];
		}
		a[s] = temp;
	    }

	    public static void reverse(long[] A, long i, long j){
		while(i < j){
		    swap(A, i, j);
		    i++;
		    j--;
		}
	    }

	    public static void swap(long[] a, long i, long j){
		long temp;
		temp = a[i];
		a[i] = a[j];
		a[j] = temp;
	    }
	    
	    static void Main (string[] args)
	    {
		long N = 3;
		long K = 3;
		long[] numbers = new long[N];
		numbers = kthPermutation(N, K+1);
		for (long i = 0; i < N; i++) {
		    Console.Write("{0} ", numbers[i]);
		}
		Console.WriteLine();
	    }
	}
}

//to compile: mcs -out:[name].exe [name].cs
//to run: mono [name].exe
