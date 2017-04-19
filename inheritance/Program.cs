using System;

namespace inheritance
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			A.B a = new A.B ();
			a.c = 1;
			Console.WriteLine (a.c);
		}
	}
}
