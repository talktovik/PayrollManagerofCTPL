using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Exception
{

    //The base keyword is used to access members of the base class from within a derived
    //class: Call a method on the base class that has been overridden by another method.
    //Specify which base-class constructor should be called when creating instances of the
    //derived class.
    public class DAOException : System.Exception
    {
        public DAOException(string message) : base(message)
        {
        
        }
    }
}
