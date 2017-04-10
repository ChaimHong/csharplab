using System;
using System.Collections.Generic;


namespace network
{
	public class Process
	{
		public delegate void ProcessDelegate (IProtoObject content);

		public static  Dictionary<string, ProcessDelegate> Processlist = new Dictionary<string, ProcessDelegate> ();
		public Dictionary<string, IProtoObject> ObjectListIn = new Dictionary<string, IProtoObject> ();
		public Dictionary<string, IProtoObject> ObjectListOut = new Dictionary<string, IProtoObject> ();

		public Process ()
		{
			ObjectListIn.Add ("0.0", new LoginIn ());
			ObjectListIn.Add ("0.1", new RankIn ());

			ObjectListOut.Add ("0.0", new LoginOut ());
			ObjectListOut.Add ("0.1", new RankOut ());

			Processlist.Add ("0.0", LoginOutProcess);
		}


		public void Send (IProtoObject data)
		{
			try {
				byte[] buffer = MarshalData (data);
				MainClass.Logarr (buffer);
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		public  void DoProcess (string key)
		{
			IProtoObject content = ObjectListOut [key];
			byte[] buffer = new byte[content.Size ()];
			content.Unmarshal (buffer, 0);
		
			if (Processlist.ContainsKey (key)) {
				Processlist [key] (content);
			}
		}

		public byte[] MarshalData (IProtoObject data)
		{
			byte[] buffer = new byte[data.Size ()];
			data.Marshal (buffer, 0);
			return buffer;
		}

		public void UnMarshalData (byte[] buffer, IProtoObject data)
		{
			data.Unmarshal (buffer, 0);
		}

		public void LoginOutProcess (IProtoObject content)
		{
			Console.WriteLine ("LoginOutProcess" + (LoginOut)content);
		}

		public string GetProperties<T> (T t)
		{
			string tStr = string.Empty;
			if (t == null) {
				return tStr;
			}
			System.Reflection.PropertyInfo[] properties = t.GetType ().GetProperties (System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

			if (properties.Length <= 0) {
				return tStr;
			}
			foreach (System.Reflection.PropertyInfo item in properties) {
				string name = item.Name;
				object value = item.GetValue (t, null);
				if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith ("String")) {
					tStr += string.Format ("{0}:{1},", name, value);
				} else {
					GetProperties (value);
				}
			}
			return tStr;
		}
	}
}

