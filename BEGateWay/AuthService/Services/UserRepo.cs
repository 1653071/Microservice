using System;
using System.Threading.Tasks;
/// <summary>
/// Summary description for Class1
/// </summary>
public interface IUserRepository	
{
	public IUser()
	{
		Task<> GetByEmail(string email);
		
	}
}
