using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class UserComment
    {
        private UserComment _LoadedItem;
        private Guid _ID;
        private int _UserID;
        private string _CommentID;
        private List<UserComment> _CommentsCollection;

        #region
        public UserComment LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public string CommentID
        {
            get { return _CommentID; }
            set { _CommentID = value; }
        }

        public List<UserComment> CommentsCollection
        {
            get { return _CommentsCollection; }
            set { _CommentsCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_UserComment comm = new FF_UserComment();
            comm.ID = this.ID;
            comm.UserID = this.UserID;
            comm.CommentID = this.CommentID;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_UserComments.InsertOnSubmit(comm);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comm = db.FF_UserComments.Single(u => u.ID == this.ID);

                if (comm != null)
                {
                    comm.ID = this.ID;
                    comm.UserID = this.UserID;
                    comm.CommentID = this.CommentID;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_UserComments.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_UserComments.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                try
                {
                var comm = (from e in db.FF_UserComments
                            where e.ID == this.ID
                            select e).First();

                if (comm != null)
                {
                    LoadedItem = new UserComment();
                    LoadedItem.ID = comm.ID;
                    LoadedItem.UserID = comm.UserID;
                    LoadedItem.CommentID = comm.CommentID;
                }
                else
                    LoadedItem = null;
                }
                catch
                {
                    LoadedItem = null;
                }
            }
        }

        public void LoadByComment()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                try
                {
                    var comm = (from e in db.FF_UserComments
                                where e.CommentID == this.CommentID
                                select e).First();

                    if (comm != null)
                    {
                        LoadedItem = new UserComment();
                        LoadedItem.ID = comm.ID;
                        LoadedItem.UserID = comm.UserID;
                        LoadedItem.CommentID = comm.CommentID;
                    }
                    else
                        LoadedItem = null;
                }
                catch
                {
                    LoadedItem = null;
                }
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comms = (from e in db.FF_UserComments
                             where e.CommentID == this.CommentID
                             select e);

                CommentsCollection = null;
                if (comms.Count() > 0)
                {
                    CommentsCollection = new List<UserComment>();
                    foreach (var comm in comms)
                    {
                        UserComment Item = new UserComment();
                        Item.ID = comm.ID;
                        Item.UserID = comm.UserID;
                        Item.CommentID = comm.CommentID;

                        CommentsCollection.Add(Item);
                    }
                }
            }
        }
    }
}
