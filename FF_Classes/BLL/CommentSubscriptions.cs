using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class CommentSubscriptions
    {
        private CommentSubscriptions _LoadedItem;
        private Int64 _ID;
        private Guid _AppID;
        private Guid _UserCommentID;
        private List<CommentSubscriptions> _Collection;

        #region
        public CommentSubscriptions LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Guid AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
        }

        public Guid UserCommentID
        {
            get { return _UserCommentID; }
            set { _UserCommentID = value; }
        }

        public List<CommentSubscriptions> Collection
        {
            get { return _Collection; }
            set { _Collection = value; }
        }
        #endregion

        public void Add()
        {
            
            FF_CommentSubscription comment = GetComment();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_CommentSubscriptions.InsertOnSubmit(comment);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_CommentSubscriptions.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    f.UserCommentID = this.UserCommentID;
                    f.OwnerID = this.AppID;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_CommentSubscriptions.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_CommentSubscriptions.DeleteOnSubmit(f);
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
                var comment = (from e in db.FF_CommentSubscriptions
                               where e.ID == this.ID
                               select e).First();

                if (comment != null)
                {
                    LoadedItem = new CommentSubscriptions();
                    LoadedItem.ID = comment.ID;
                    LoadedItem.AppID = comment.OwnerID;
                    LoadedItem.UserCommentID = comment.UserCommentID;
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
                var comments = (from e in db.FF_CommentSubscriptions
                                where e.OwnerID == this.AppID
                                select e);

                Collection = null;
                if (comments.Count() > 0)
                {
                    Collection = new List<CommentSubscriptions>();
                    foreach (var comment in comments)
                    {
                        CommentSubscriptions Item = new CommentSubscriptions();
                        Item.ID = comment.ID;
                        Item.AppID = comment.OwnerID;
                        Item.UserCommentID = comment.UserCommentID;

                        Collection.Add(Item);
                    }
                }
            }
        }

        public FF_CommentSubscription GetComment()
        {
            FF_CommentSubscription comment = new FF_CommentSubscription();
            comment.UserCommentID = this.UserCommentID;
            comment.OwnerID = this.AppID;

            return comment;
        }
    }
}
