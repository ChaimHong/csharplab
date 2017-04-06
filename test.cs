using System;

public class HelloWorld
{
	static public void Main (string[] args)
	{
		Console.WriteLine ("Hello Mono World" + args [0]);
		Console.WriteLine (GetData<int> (args [0]));
		Console.WriteLine (GetData<string> (args [1]));
	}

	public static T GetData<T> (string key)
	{
		int k = Convert.ToInt32 (key);
		switch (k) {
		case 0:
			return (T)Convert.ChangeType(1, typeof(T));
		case 1:
			return (T)Convert.ChangeType("str 2", typeof(T));
		}

		return default (T);
	}
}


