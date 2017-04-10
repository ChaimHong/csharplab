using System;
using System.Collections.Generic;
using System.Reflection;


namespace network
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!");
			Process p = new Process ();


			LoginIn li = new LoginIn ();
			li.User = System.Text.Encoding.Default.GetBytes ("abc");
			li.Group = LoginGroup2.LOGIN_GROUP_E;

			byte[] bytes = p.MarshalData (li);
			Logarr (bytes);

			IProtoObject uobject = new LoginIn ();
			p.UnMarshalData (bytes, uobject);
			Console.WriteLine (((LoginIn)uobject).Group);

//			IProtoObject v = p.ObjectListIn ["0.0"];
//			LoginIn v2 = (LoginIn)v;
//			Console.WriteLine ("abc" + v2);
//			Console.WriteLine("abc"+ p.GetProperties<LoginIn> (v));
		}

		public static void Logarr (byte[] bytes)
		{
			Console.Write ("(" + bytes.Length + ")");
			for (int i = 0; i < bytes.Length; i++) {
				Console.Write (bytes [i] + " ");
			}
			Console.WriteLine ("");
		}




	}
}
