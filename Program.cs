using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//reference for how C# code even looks like
//everything in a (project) namespace
namespace Application
{
	public class Program //the main class
	{
		//the usual C main function is a static member function of MainClass
		static void Main (string[] args)
		{
			Console.WriteLine("Printing a line for no reason!");
		}
	}
}
//using mono:
//to compile: mcs -out:[name].exe [name].cs
//to run: mono [name].exe