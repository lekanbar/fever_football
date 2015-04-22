using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class News
    {
        private News _LoadedItem;
        private Guid _NewsID;
        private Nullable<Guid> _SeasonID;
        private string _Title;
        private string _Details;
        private string _ImageURL;
        private string _Excerpt;
        private Nullable<int> _LeagueID;
        private DateTime _Date;
        private List<News> _NewsCollection;

        #region
        public News LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid NewsID
        {
            get { return _NewsID; }
            set { _NewsID = value; }
        }

        public Nullable<Guid> SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
        }

        public Nullable<int> LeagueID
        {
            get { return _LeagueID; }
            set { _LeagueID = value; }
        }

        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
        }

        public string Excerpt
        {
            get { return _Excerpt; }
            set { _Excerpt = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public List<News> NewsCollection
        {
            get { return _NewsCollection; }
            set { _NewsCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_New news = GetNews();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_News.InsertOnSubmit(news);

                db.SubmitChanges();
            }
        }

        public void Update(bool WithFile, string OldImageURL)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var news = db.FF_News.Single(u => u.NewsID == this.NewsID);

                if (news != null)
                {
                    news.Title = this.Title;
                    news.Details = this.Details;
                    news.Date = this.Date;
                    news.ImageURL = this.ImageURL;
                    news.LeagueID = this.LeagueID;
                    news.SeasonID = this.SeasonID;
                    news.Excerpt = this.Excerpt;

                    db.SubmitChanges();

                    if (WithFile && !string.IsNullOrEmpty(OldImageURL))
                    {
                        FileHelper.DeleteFile(OldImageURL, true);
                    }
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_News.Single(u => u.NewsID == this.NewsID);

                if (f != null)
                {
                    //Delete all associated comments
                    DeleteComments(f.NewsID);
                    //Delete all associated comment subscriptions
                    DeleteCommentSubscriptions(f.NewsID);

                    if (System.IO.File.Exists(f.ImageURL)) 
                        FileHelper.DeleteFile(f.ImageURL, true);

                    db.FF_News.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void DeleteComments(Guid ID)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Comments.Where(u => u.OwnerID == ID);

                if (f.Count() > 0)
                {
                    foreach (var v in f)
                    {
                        db.FF_Comments.DeleteOnSubmit(v);
                        db.SubmitChanges();
                    }
                }
            }
        }

        public void DeleteCommentSubscriptions(Guid ID)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_CommentSubscriptions.Where(u => u.OwnerID == ID);

                if (f.Count() > 0)
                {
                    foreach (var v in f)
                    {
                        db.FF_CommentSubscriptions.DeleteOnSubmit(v);
                        db.SubmitChanges();
                    }
                }
            }
        }


        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                try
                {
                    var news = (from e in db.FF_News
                                where e.NewsID == this.NewsID
                                select e).First();

                    if (news != null)
                    {
                        LoadedItem = new News();
                        LoadedItem.NewsID = news.NewsID;
                        LoadedItem.Title = news.Title;
                        LoadedItem.Details = news.Details;
                        LoadedItem.Date = news.Date;
                        LoadedItem.ImageURL = news.ImageURL;
                        LoadedItem.LeagueID = news.LeagueID;
                        LoadedItem.SeasonID = news.SeasonID;
                        LoadedItem.Excerpt = news.Excerpt;
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
                var newss = (from e in db.FF_News
                             orderby e.Date descending
                              select e);

                NewsCollection = null;
                if (newss.Count() > 0)
                {
                    NewsCollection = new List<News>();
                    foreach (var news in newss)
                    {
                        News Item = new News();
                        Item.NewsID = news.NewsID;
                        Item.Title = news.Title;
                        Item.Details = news.Details;
                        Item.Date = news.Date;
                        Item.ImageURL = news.ImageURL;
                        Item.LeagueID = news.LeagueID;
                        Item.SeasonID = news.SeasonID;
                        Item.Excerpt = news.Excerpt;

                        NewsCollection.Add(Item);
                    }
                }
            }
        }

        public void GetLeagueNews()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var newss = (from e in db.FF_News
                             where e.LeagueID == this.LeagueID
                             orderby e.Date descending
                             select e);

                NewsCollection = null;
                if (newss.Count() > 0)
                {
                    NewsCollection = new List<News>();
                    foreach (var news in newss)
                    {
                        News Item = new News();
                        Item.NewsID = news.NewsID;
                        Item.Title = news.Title;
                        Item.Details = news.Details;
                        Item.Date = news.Date;
                        Item.ImageURL = news.ImageURL;
                        Item.LeagueID = news.LeagueID;
                        Item.SeasonID = news.SeasonID;
                        Item.Excerpt = news.Excerpt;

                        NewsCollection.Add(Item);
                    }
                }
            }
        }

        public FF_New GetNews()
        {
            FF_New news = new FF_New();
            news.NewsID = this.NewsID;
            news.Title = this.Title; 
            news.Details = this.Details;
            news.Date = this.Date;
            news.ImageURL = this.ImageURL;
            news.LeagueID = this.LeagueID;
            news.SeasonID = this.SeasonID;
            news.Excerpt = this.Excerpt;

            return news;
        }
    }
}
