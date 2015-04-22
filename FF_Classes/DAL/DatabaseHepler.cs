using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using FF_Classes;

namespace FF_Classes
{
    /// <summary>
    /// Summary description for DatabaseHepler
    /// </summary>
    public class DatabaseHepler
    {
        public static FFLinqClassDataContext GetDatabaseData()
        {
            var db = new FFLinqClassDataContext();  
            return db;
        }
    }
}