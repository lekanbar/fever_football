using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF_Classes
{
    public class UserExtras
    {

        public static string getUserIDbyUsername(string username)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.yaf_Users
                             where e.DisplayName == username
                             select e.UserID);

                if (users.Count() > 0 )
                {
                    return users.ToList().ElementAt(0).ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static string getUsernamebyUserID(int userid)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.yaf_Users
                             where e.UserID == userid
                             select e.DisplayName);

                if (users.Count() > 0)
                {
                    return users.ToList().ElementAt(0).ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        public static DataTable  getAvatar(int userid)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.yaf_Users
                             where e.UserID == userid
                             select new { e.AvatarImage, e.AvatarImageType});


                if (users.Count() > 0)
                {
                    if (users.ToList().ElementAt(0).AvatarImage != null)
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("1", typeof(object));
                        table.Columns.Add("2", typeof(string));

                        DataRow row = table.NewRow();
                        row[0] = (object)users.ToList().ElementAt(0).AvatarImage.ToArray();
                        row[1] = users.ToList().ElementAt(0).AvatarImageType.ToString();
                        table.Rows.Add(row);

                        return table;
                    }
                    else return null;
                }
                else
                {
                    return null;
                }
            }
        
        }


        public static bool hasAvatar(int userid)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.yaf_Users
                             where e.UserID == userid
                             select new { e.AvatarImage, e.AvatarImageType });


                if (users.Count() > 0)
                {
                    if (users.ToList().ElementAt(0).AvatarImage != null)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }

        }


        public static bool Login (string UserName, string password)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.FF_Admins
                             where e.UserName ==  UserName && e.Password == password
                             select e);

                if (users.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }



        public void UpdatePassword( Guid UserID, string Password)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var user = db.FF_Admins.Single(u => u.UserID == UserID);

                if (user != null)
                {
                    user.Password = Password;

                    db.SubmitChanges();
                }
            }
        }


    }
}
