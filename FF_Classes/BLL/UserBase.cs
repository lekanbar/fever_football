using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Security.Cryptography;

namespace FF_Classes
{
    public class UserBase
    {
        private UserBase _LoadedItem;
        private Guid _UserID;
        private int _RoleTypeID;
        private string _Email;
        private string _UserName;
        private string _FirstName;
        private string _LastName;
        private string _Password;
        private string _PhoneNumber;
        private string _Country;
        private bool _IsLockedOut;
        private List<UserBase> _UserCollection;

        #region
        public UserBase LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public int RoleTypeID
        {
            get { return _RoleTypeID; }
            set { _RoleTypeID = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string Password
        {
            get { return _Password; }
            set 
            {
                //Password hashing for security
                HashAlgorithm algorithm = new SHA1Managed();
                ASCIIEncoding encoder = new ASCIIEncoding();
                Byte[] valueByteArray = encoder.GetBytes(value);
                string hashedPassword = "";
                Byte[] hashValueByteArray = algorithm.ComputeHash(valueByteArray);
                foreach (Byte b in hashValueByteArray)
                {
                    hashedPassword += String.Format("{0:x2}", b);
                }

                _Password = hashedPassword;
            }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public bool IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }

        public List<UserBase> UserCollection
        {
            get { return _UserCollection; }
            set { _UserCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_User user = GetUser();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Users.InsertOnSubmit(user);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Users.Single(u => u.UserID == this.UserID);

                if (f != null)
                {
                    f.RoleTypeID = this.RoleTypeID;
                    f.Email = this.Email;
                    f.UserName = this.UserName;
                    f.FirstName = this.FirstName;
                    f.LastName = this.LastName;
                    f.Password = this.Password;
                    f.PhoneNumber = this.PhoneNumber;
                    f.Country = this.Country;
                    f.IsLockedOut = this.IsLockedOut;

                    db.SubmitChanges();
                }
            }
        }

        public void Update2()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Users.Single(u => u.UserID == this.UserID);

                if (f != null)
                {
                    f.RoleTypeID = this.RoleTypeID;
                    f.Email = this.Email;
                    f.UserName = this.UserName;
                    f.FirstName = this.FirstName;
                    f.LastName = this.LastName;
                    f.PhoneNumber = this.PhoneNumber;
                    f.Country = this.Country;
                    f.IsLockedOut = this.IsLockedOut;

                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var user = (from e in db.FF_Users
                            where e.UserID == this.UserID
                            select e).FirstOrDefault();

                if (user != null)
                {
                    LoadedItem = new UserBase();
                    LoadedItem.UserID = user.UserID;
                    LoadedItem.RoleTypeID = user.RoleTypeID;
                    LoadedItem.Email = user.Email;
                    LoadedItem.UserName = user.UserName;
                    LoadedItem.FirstName = user.FirstName;
                    LoadedItem.LastName = user.LastName;
                    LoadedItem.Password = user.Password;
                    LoadedItem.PhoneNumber = user.PhoneNumber;
                    LoadedItem.Country = user.Country;
                    LoadedItem.IsLockedOut = user.IsLockedOut;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var users = (from e in db.FF_Users
                            select e).DefaultIfEmpty();

                UserCollection = null;
                if (users.Count() > 0)
                {
                    UserCollection = new List<UserBase>();
                    foreach (var user in users)
                    {
                        UserBase Item = new UserBase();
                        Item.UserID = user.UserID;
                        Item.RoleTypeID = user.RoleTypeID;
                        Item.Email = user.Email;
                        Item.UserName = user.UserName;
                        Item.FirstName = user.FirstName;
                        Item.LastName = user.LastName;
                        Item.Password = user.Password;
                        Item.PhoneNumber = user.PhoneNumber;
                        Item.Country = user.Country;
                        Item.IsLockedOut = user.IsLockedOut;

                        UserCollection.Add(Item);
                    }
                }
            }
        }

        public FF_User GetUser()
        {
            FF_User user = new FF_User();
            user.UserID = this.UserID;
            user.RoleTypeID = this.RoleTypeID;
            user.Email = this.Email;
            user.UserName = this.UserName;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Password = this.Password;
            user.PhoneNumber = this.PhoneNumber;
            user.Country = this.Country;
            user.IsLockedOut = this.IsLockedOut;

            return user;
        }

        public void Login(int LoginAttempts)
        {
            if (LoginAttempts > 3)
            {
                throw new Exception("Login attempts exceeded.");
            }
            else
            {
                using (var db = DatabaseHepler.GetDatabaseData())
                {
                    var user = (from e in db.FF_Users
                                where e.Email == this.Email
                                where e.Password == this.Password
                                select e).FirstOrDefault();

                    if (user != null)
                    {
                        LoadedItem = new UserBase();
                        LoadedItem.UserID = user.UserID;
                        LoadedItem.RoleTypeID = user.RoleTypeID;
                        LoadedItem.Email = user.Email;
                        LoadedItem.UserName = user.UserName;
                        LoadedItem.FirstName = user.FirstName;
                        LoadedItem.LastName = user.LastName;
                        LoadedItem.Password = user.Password;
                        LoadedItem.PhoneNumber = user.PhoneNumber;
                        LoadedItem.Country = user.Country;
                        LoadedItem.IsLockedOut = user.IsLockedOut;
                    }
                    else
                        LoadedItem = null;
                }
            }
        }

        public void LockAndUnlockUser()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var user = db.FF_Users.Single(u => u.UserID == this.UserID);

                if (user != null)
                {
                    user.IsLockedOut = (user.IsLockedOut ? false : true);

                    db.SubmitChanges();
                }
            }
        }

        public void RecoverPassword()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var user = db.FF_Users.Single(u => u.Email == this.Email);

                if (user != null)
                {
                    string newPassword = Guid.NewGuid().ToString().Substring(0, 8);
                    this.Password = newPassword;

                    user.Password = this.Password;
                    db.SubmitChanges();

                    //Setup Password Recovery Email
                    EmailHelper helper = new EmailHelper();
                    helper.From = "mail@feverfootball.com";
                    helper.Password = "MailOlobe";
                    helper.To = Email;
                    helper.Subject = "FeverFootball.com Password Recovery";

                    helper.MessageBody = "Hello " + user.FirstName + "," +
                                         "<br />Your password has been reset. <br /><br /> Your New Password is >> " +
                                         newPassword +
                                         "<br /><br />Thank you for your patronage, we look forward to working with you all the way." +
                                         "<br /><br /> Please feel free share your thoughts about Fever Football by sending an email to info@feverfootball.com" +
                                         " we would be glad to hear from you." +
                                         "<br /><br />Fever Football Support";

                    //Send the Email
                    string result = helper.Send();
                }
            }
        }
    }
}
