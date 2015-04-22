using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class UserExtras
    {

        public static string getUserIDbyUsername( string username)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.yaf_Users
                               where e.DisplayName  == username
                               select e);

                if (users != null)
                {
                    return users.ToList().ElementAt(0).UserID.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
