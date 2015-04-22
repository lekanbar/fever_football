using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YAF.Providers.Membership;
using YAF.Core;
using System.Web.Security;

namespace FF_Classes
{
    public class Comments
    {
        private Comments _LoadedItem;
        private string _ID;
        private Guid _AppID;
        private string _Details;
        private Nullable<DateTime> _Date;
        private List<Comments> _CommentCollection;

        #region
        public Comments LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Guid AppID
        {
            get { return _AppID; }
            set { _AppID = value; }
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

        public List<Comments> CommentCollection
        {
            get { return _CommentCollection; }
            set { _CommentCollection = value; }
        }
        #endregion

        public void Add(bool Subscribe, string PostTitle, string PostURL, Guid UserCommentID)
        {
            FF_Comment comment = GetComment();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Comments.InsertOnSubmit(comment);

                db.SubmitChanges();
            }

            CommentSubscriptions commentsubs = new CommentSubscriptions();
            commentsubs.AppID = this.AppID;

            if (Subscribe)
            {
                commentsubs.UserCommentID = UserCommentID;
                commentsubs.Add();
            }
            
            commentsubs.GetAll();

            foreach (CommentSubscriptions commentsub in commentsubs.Collection)
            {
                
                //UserBase user = new UserBase();
                //user.UserID = commentsub.UserCommentID;
                //user.Load();
                //user = user.LoadedItem;


                UserComment user = new UserComment();
                user.ID = commentsub.UserCommentID;
                user.Load();

                EmailHelper email = new EmailHelper();
                email.Subject = "New Comment on Post [" + PostTitle + "]";
                email.From = "mail@feverfootball.com";

                if (user == null)
                {
                    UserComment2 user2 = new UserComment2();
                    user2.ID = commentsub.UserCommentID;
                    user2.Load();
                    user2 = user2.LoadedItem;

                    if (user2 == null) continue;

                    email.To = user2.Email;
                    email.MessageBody = comment.Details + "<br/><br/>Follow the link below to view post:<br/>" + PostURL;
                    email.Send();
                }
                else
                {
                    MembershipUser robot = UserMembershipHelper.GetMembershipUserById(user.UserID);
                    email.To = robot.Email;
                    email.MessageBody = comment.Details + "<br/><br/>Follow the link below to view post:<br/>" + PostURL;
                    email.Send();
                }
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Comments.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    f.Details = this.Details;
                    f.Date = this.Date;
                    f.OwnerID = this.AppID;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Comments.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_Comments.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comment = (from e in db.FF_Comments
                               where e.ID == this.ID
                               select e).First();

                if (comment != null)
                {
                    LoadedItem = new Comments();
                    LoadedItem.ID = comment.ID;
                    LoadedItem.AppID = comment.OwnerID;
                    LoadedItem.Details = comment.Details;
                    LoadedItem.Date = comment.Date;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var comments = (from e in db.FF_Comments
                                where e.OwnerID == this.AppID
                                select e);

                CommentCollection = null;
                if (comments.Count() > 0)
                {
                    CommentCollection = new List<Comments>();
                    foreach (var comment in comments)
                    {
                        Comments Item = new Comments();
                        Item.ID = comment.ID;
                        Item.AppID = comment.OwnerID;
                        Item.Details = comment.Details;
                        Item.Date = comment.Date;

                        CommentCollection.Add(Item);
                    }
                }
            }
        }

        public FF_Comment GetComment()
        {
            FF_Comment comment = new FF_Comment();
            comment.ID = this.ID;
            comment.OwnerID = this.AppID;
            comment.Details = this.Details;
            comment.Date = this.Date;

            return comment;
        }
    }
}
