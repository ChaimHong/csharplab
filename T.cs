using System;
using System.Collections.Generic;
public class  LoginGroup {
	public const sbyte LOGIN_GROUP_D = 1;
	public const sbyte LOGIN_GROUP_E = 2;
	public const sbyte LOGIN_GROUP_F = 3;
	public const sbyte LOGIN_GROUP_G = 4;
}
public class  LoginStatus {
	public const sbyte LOGIN_STATUS_SUCCEED = 1;
	public const sbyte LOGIN_STATUS_FIRST_TIME = 2;
}
class NotifyCloseSessionOut {
	public int Size() {
		int size = 0;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		return n;
	}
}
class LoginIn {
	public byte[] User;
	public LoginGroup Group;
	public int Size() {
		int size = 0;
		size += Gobuf.UvarintSize((ulong)this.User.Length) + this.User.Length;
		size += 1;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		Gobuf.WriteBytes(this.User, b, ref n);
		b[n++] = (byte)this.Group;
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		this.User = Gobuf.ReadBytes(b, ref n);
		this.Group = (LoginGroup)b[n++];
		return n;
	}
}
class LoginOut {
	public LoginStatus Status;
	public long PlayerId;
	public long LastDistance;
	public long MaxDistance;
	public int Size() {
		int size = 0;
		size += 1;
		size += 8;
		size += 8;
		size += 8;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		b[n++] = (byte)this.Status;
		Gobuf.WriteUint64((ulong)this.PlayerId, b, ref n);
		Gobuf.WriteUint64((ulong)this.LastDistance, b, ref n);
		Gobuf.WriteUint64((ulong)this.MaxDistance, b, ref n);
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		this.Status = (LoginStatus)b[n++];
		this.PlayerId = (long)Gobuf.ReadUint64(b, ref n);
		this.LastDistance = (long)Gobuf.ReadUint64(b, ref n);
		this.MaxDistance = (long)Gobuf.ReadUint64(b, ref n);
		return n;
	}
}
class RankPlayer {
	public LoginGroup BestGroup;
	public string User;
	public int Num;
	public long Distance;
	public int BestAliveTime;
	public int BestGolds;
	public int Size() {
		int size = 0;
		size += 1;
		size += Gobuf.StringSize(this.User);
		size += 4;
		size += 8;
		size += 4;
		size += 4;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		b[n++] = (byte)this.BestGroup;
		Gobuf.WriteString(this.User, b, ref n);
		Gobuf.WriteUint32((uint)this.Num, b, ref n);
		Gobuf.WriteUint64((ulong)this.Distance, b, ref n);
		Gobuf.WriteUint32((uint)this.BestAliveTime, b, ref n);
		Gobuf.WriteUint32((uint)this.BestGolds, b, ref n);
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		this.BestGroup = (LoginGroup)b[n++];
		this.User = Gobuf.ReadString(b, ref n);
		this.Num = (int)Gobuf.ReadUint32(b, ref n);
		this.Distance = (long)Gobuf.ReadUint64(b, ref n);
		this.BestAliveTime = (int)Gobuf.ReadUint32(b, ref n);
		this.BestGolds = (int)Gobuf.ReadUint32(b, ref n);
		return n;
	}
}
class RankIn {
	public int Size() {
		int size = 0;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		return n;
	}
}
class RankOut {
	public List<RankPlayer> List = new List<RankPlayer>();
	public int Rank;
	public int Size() {
		int size = 0;
		size += Gobuf.UvarintSize((ulong)this.List.Count);
		for (var i1 = 0; i1 < this.List.Count; i1 ++) {
			size += this.List[i1].Size();
		}
		size += 4;
		return size;
	}
	public int Marshal(byte[] b, int n) {
		Gobuf.WriteUvarint((ulong)this.List.Count, b, ref n);
		for (var i1 = 0; i1 < this.List.Count; i1 ++) {
			n = this.List[i1].Marshal(b, n);
		}
		Gobuf.WriteUint32((uint)this.Rank, b, ref n);
		return n;
	}
	public int Unmarshal(byte[] b, int n) {
		{
			this.List = new List<RankPlayer>((int)Gobuf.ReadUvarint(b, ref n));
			for (var i1 = 0; i1 < this.List.Capacity; i1 ++) {
				RankPlayer v1;
				v1 = new RankPlayer();
				n = v1.Unmarshal(b, n);
				this.List.Add(v1);
			}
		}
		this.Rank = (int)Gobuf.ReadUint32(b, ref n);
		return n;
	}
}

class TT {
	public void Send<T>(string key, T data)
    {
		try {
			byte[] buffer = proto.Encode(key, data);
			conn.Send(buffer);
		} catch (Exception ex) {
			Debug.Log(ex.Message);
			conn.Close();
		}
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

	public object GetDataObject(int key){
		return (object)"1";
	}

	public static void DoProcess(int key){

		// decode packet
		object content = proto.Decode(key, reader);

		
		object content = GetDataObject(int);
		if (Processlist.ContainsKey (key)) {
			Processlist[key] (content);
		}
	}
}