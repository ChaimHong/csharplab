using System;

public class Serialization
{
	public void Main ()
	{
	}

	// 序列化包
	public byte[] Encode(string key, object t)
	{
		// 1bit serviceId and 1bit messageId
		int headSize = 2;

		string[] arr = key.Split('.');
		byte serviceId = Convert.ToByte(arr[0]);
		byte messageId = Convert.ToByte(arr[1]);

		List<Field> fields = GetInFields(key);
		int packetSize = GetFieldsSize(fields, t);

		byte[] buffer = new byte[headSize + packetSize];
		using (MemoryStream ms = new MemoryStream(buffer))
		{
			using(BinaryWriter writer = new BinaryWriter(ms))
			{
				// write serviceId byte
				writer.Write(serviceId);

				// write messageId byte
				writer.Write(messageId);

				// write fields
				WriteFields(writer, fields, t);
			}
		}
		return buffer;
	}

	// 反序列化
	public object Decode(string key, BinaryReader reader)
	{
		object fields = GetOutFields(key); 

		// create return data
		LuaTable t = new LuaTable(LuaState.main);

		// read fields
		ReadFields(reader, fields, t);

		return t;
	}
}

