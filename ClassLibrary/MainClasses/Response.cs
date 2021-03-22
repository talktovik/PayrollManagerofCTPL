using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.MainClasses
{
    /// <summary>
    /// This class take the response from the class with additional informations !
    /// </summary>
    public class Response
    {
        public bool success { get; set; }
        public bool isException { get; set; }
        public string exception { get; set; }
        public object body { set; get; }
    }
}
