using System;
using System.Collections.Generic;

public class HelloWorld
{
	
	public delegate void ProcessDelegate(object content);
	public static  Dictionary<int, ProcessDelegate> Processlist = new Dictionary<int, ProcessDelegate>();

	static public void Main (string[] args)
	{
		Console.WriteLine ("Hello Mono World" + args [0]);
		Console.WriteLine (GetData<int> (args [0]));
		Console.WriteLine (GetData<string> (args [1]));
		AddProcess (1, A);
		AddProcess (2, B);

		DoProcess (1);
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

	public static void DoProcess(int key){
		object content = "1";
		if (Processlist.ContainsKey (key)) {
			Processlist[key] (content);
		}
	}

	public static void AddProcess(int key, ProcessDelegate f){
		Processlist.Add (key, f);
	}

	public static void A(object content){
		int content1 = Convert.ToInt32 (content);
		Console.WriteLine ("A int" + Convert.ToString (content1));
	}

	public static void B(object content){
		string content1 = Convert.ToString (content);
		Console.WriteLine ("B string" + content1);
	}
}


