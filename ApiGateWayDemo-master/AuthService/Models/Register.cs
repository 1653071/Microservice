using System;

/// <summary>
/// Summary description for Class1
/// </summary>
public class RegisterRequest
{
	public RegisterRequest()
	{
		//
		// TODO: Add constructor logic here
		//
		[Required]
		public string Email { get; set; }
		[Required]
	    public string Password { get; set; }
		[Required]
		public string ConfirmPassword{ get;set; }

}
}
