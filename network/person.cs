using System;
// 一个人类的接口
public interface IPerson
{
	void Say();
}
// 一个男人的类
public class Man: IPerson
{
	public void Say()
	{
		Console.WriteLine("我是一个男人");
	}
}
// 一个女人的类
public class Woman: IPerson
{
	public void Say()
	{
		Console.WriteLine("我是一个女人");
	}
}
// 一个人的类
public class People
{
	public void Get(IPerson ipn) //接口做为参数传递
	{
		ipn.Say();
	}
}
