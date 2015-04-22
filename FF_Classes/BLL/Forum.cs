using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Forum
    {
        private Forum _LoadedItem;
        private Guid _ForumID;
        private string _Topic;
        private string _Details;
        private Nullable<DateTime> _Date;
        private string _ImageURL;
        private Guid _PostedBy;
        private List<Forum> _ForumCollection;

        #region
        public Forum LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid ForumID
        {
            get { return _ForumID; }
            set { _ForumID = value; }
        }

        public Guid PostedBy
        {
            get { return _PostedBy; }
            set { _PostedBy = value; }
        }

        public string Topic
        {
            get { return _Topic; }
            set { _Topic = value; }
        }

        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }

        public Nullable<DateTime> Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public List<Forum> ForumCollection
        {
            get { return _ForumCollection; }
            set { _ForumCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Forum forum = GetForum();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Forums.InsertOnSubmit(forum);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var forum = db.FF_Forums.Single(u => u.ForumID == this.ForumID);

                if (forum != null)
                {
                    forum.Topic = this.Topic;
                    forum.Details = this.Details;
                    forum.Date = this.Date;
                    forum.ImageURL = this.ImageURL;
                    forum.PostedBy = this.PostedBy;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Forums.Single(u => u.ForumID == this.ForumID);

                if (f != null)
                {
                    db.FF_Forums.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var forum = (from e in db.FF_Forums
                               where e.ForumID == this.ForumID
                               select e).FirstOrDefault();

                if (forum != null)
                {
                    LoadedItem = new Forum();
                    LoadedItem.ForumID = forum.ForumID;
                    LoadedItem.Topic = forum.Topic;
                    LoadedItem.Details = forum.Details;
                    LoadedItem.Date = forum.Date;
                    LoadedItem.ImageURL = forum.ImageURL;
                    LoadedItem.PostedBy = forum.PostedBy;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var forums = (from e in db.FF_Forums
                                select e).DefaultIfEmpty();

                ForumCollection = null;
                if (forums.Count() > 0)
                {
                    ForumCollection = new List<Forum>();
                    foreach (var forum in forums)
                    {
                        Forum Item = new Forum();
                        Item.ForumID = forum.ForumID;
                        Item.Topic = forum.Topic;
                        Item.Details = forum.Details;
                        Item.Date = forum.Date;
                        Item.ImageURL = forum.ImageURL;
                        Item.PostedBy = forum.PostedBy;

                        ForumCollection.Add(Item);
                    }
                }
            }
        }

        public FF_Forum GetForum()
        {
            FF_Forum forum = new FF_Forum();
            forum.ForumID = this.ForumID;
            forum.Topic = this.Topic;
            forum.Details = this.Details;
            forum.Date = this.Date;
            forum.ImageURL = this.ImageURL;
            forum.PostedBy = this.PostedBy;

            return forum;
        }
    }
}
