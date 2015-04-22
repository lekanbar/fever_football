using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class UserComment2
    {
        private UserComment2 _LoadedItem;
        private Guid _ID;
        private string _Name;
        private string _CommentID;
        private string _Email;
        private List<UserComment2> _CommentsCollection;

        #region
        public UserComment2 LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public string CommentID
        {
            get { return _CommentID; }
            set { _CommentID = value; }
        }

        public List<UserComment2> CommentsCollection
        {
            get { return _CommentsCollection; }
            set { _CommentsCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_UserComment2 comm = new FF_UserComment2();
            comm.ID = this.ID;
            comm.Name = this.Name;
            comm.CommentID = this.CommentID;
            comm.Email = this.Email;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_UserComment2s.InsertOnSubmit(comm);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comm = db.FF_UserComment2s.Single(u => u.ID == this.ID);

                if (comm != null)
                {
                    comm.Name = this.Name;
                    comm.CommentID = this.CommentID;
                    comm.Email = this.Email;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_UserComment2s.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_UserComment2s.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comm = (from e in db.FF_UserComment2s
                            where e.ID == this.ID
                            select e).First();

                if (comm != null)
                {
                    LoadedItem = new UserComment2();
                    LoadedItem.ID = comm.ID;
                    LoadedItem.Name = comm.Name;
                    LoadedItem.CommentID = comm.CommentID;
                    LoadedItem.Email = comm.Email;
                }
                else
                    LoadedItem = null;
            }
        }

        public void LoadByComment()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comm = (from e in db.FF_UserComment2s
                            where e.CommentID== this.CommentID
                            select e).First();

                if (comm != null)
                {
                    LoadedItem = new UserComment2();
                    LoadedItem.ID = comm.ID;
                    LoadedItem.Name = comm.Name;
                    LoadedItem.CommentID = comm.CommentID;
                    LoadedItem.Email = comm.Email;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comms = (from e in db.FF_UserComment2s
                             where e.CommentID == this.CommentID
                             select e);

                CommentsCollection = null;
                if (comms.Count() > 0)
                {
                    CommentsCollection = new List<UserComment2>();
                    foreach (var comm in comms)
                    {
                        UserComment2 Item = new UserComment2();
                        Item.ID = comm.ID;
                        Item.Name = comm.Name;
                        Item.CommentID = comm.CommentID;
                        Item.Email = comm.Email;

                        CommentsCollection.Add(Item);
                    }
                }
            }
        }
    }
}
