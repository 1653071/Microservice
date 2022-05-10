using System;
using System.Collections.Generic;
using System.Text;

namespace ModelClassLibrary.iterface
{
    public interface IHashPass
    {
        string hashPass(string pass);
        Boolean checkPass(string hashedPassword, string providedPassword);
    }
}
