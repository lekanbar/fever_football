using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Article
    {
        private Article _LoadedItem;
        private Guid _ArticleID;
        private int _UserID;
        private string _Title;
        private string _Details;
        private string _ImageURL;
        private DateTime _Date;
        private List<Article> _ArticleCollection;

        #region
        public Article LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid ArticleID
        {
            get { return _ArticleID; }
            set { _ArticleID = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public List<Article> ArticleCollection
        {
            get { return _ArticleCollection; }
            set { _ArticleCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Article user = GetArticle();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Articles.InsertOnSubmit(user);

                db.SubmitChanges();
            }
        }

        public void Update( bool WithFile, string OldImageURL)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Articles.Single(u => u.ArticleID == this.ArticleID);

                if (f != null)
                {
                    f.UserID = this.UserID;
                    f.Title = this.Title;
                    f.Details = this.Details;
                    f.ImageURL = this.ImageURL;
                    f.Date = this.Date;

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
                var f = db.FF_Articles.Single(u => u.ArticleID == this.ArticleID);

                if (f != null)
                {
                    if (System.IO.File.Exists(f.ImageURL))
                        FileHelper.DeleteFile(f.ImageURL, true);

                    db.FF_Articles.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var article = (from e in db.FF_Articles
                            where e.ArticleID == this.ArticleID
                            select e).First();

                if (article != null)
                {
                    LoadedItem = new Article();
                    LoadedItem.UserID = article.UserID;
                    LoadedItem.ArticleID = article.ArticleID;
                    LoadedItem.Title = article.Title;
                    LoadedItem.Details = article.Details;
                    LoadedItem.ImageURL = article.ImageURL;
                    LoadedItem.Date = article.Date;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var articles = (from e in db.FF_Articles
                                orderby e.Date descending
                             select e);

                ArticleCollection = null;
                if (articles.Count() > 0)
                {
                    ArticleCollection = new List<Article>();
                    foreach (var article in articles)
                    {
                        Article Item = new Article();
                        Item.UserID = article.UserID;
                        Item.ArticleID = article.ArticleID;
                        Item.Title = article.Title;
                        Item.Details = article.Details;
                        Item.ImageURL = article.ImageURL;
                        Item.Date = article.Date;

                        ArticleCollection.Add(Item);
                    }
                }
            }
        }

        public FF_Article GetArticle()
        {
            FF_Article article = new FF_Article();
            article.UserID = this.UserID;
            article.ArticleID = this.ArticleID;
            article.Title = this.Title;
            article.Details = this.Details;
            article.ImageURL = this.ImageURL;
            article.Date = this.Date;

            return article;
        }
    }
}
