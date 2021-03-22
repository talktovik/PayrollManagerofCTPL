using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace ClassLibrary.Database
{
    /// <summary>
    /// This Class would give the oleDbConnection object to the caller. Useful for the connection of database.
    /// </summary>
    public class DatabaseConnection
    {
        public static OleDbConnection GetConnection() 
        {
            return new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=.\main.mdb");
        }
    }
}
