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
        private string _Paragraph2;
        private string _ImageURL2;
        private string _Paragraph3;
        private string _ImageURL3;
        private string _Paragraph4;
        private string _ImageURL4;
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

        public string Paragragh2
        {
            get { return _Paragraph2; }
            set { _Paragraph2 = value; }
        }

        public string ImageURL2
        {
            get { return _ImageURL2; }
            set { _ImageURL2 = value; }
        }

        public string Paragragh3
        {
            get { return _Paragraph3; }
            set { _Paragraph3 = value; }
        }

        public string ImageURL3
        {
            get { return _ImageURL3; }
            set { _ImageURL3 = value; }
        }

        public string Paragragh4
        {
            get { return _Paragraph4; }
            set { _Paragraph4 = value; }
        }

        public string ImageURL4
        {
            get { return _ImageURL4; }
            set { _ImageURL4 = value; }
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

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Articles.Single(u => u.ArticleID == this.ArticleID);

                if (f != null)
                {
                    Article item = new Article();
                    item.ArticleID = this.ArticleID;

                    item.Load();
                    item = item.LoadedItem;

                    f.Title = this.Title;
                    f.UserID = this.UserID;
                    f.Details = this.Details;
                    f.ImageURL = this.ImageURL;
                    f.Paragraph2 = this.Paragragh2;
                    f.ImageURL2 = this.ImageURL2;
                    f.Paragraph3 = this.Paragragh3;
                    f.ImageURL3 = this.ImageURL3;
                    f.Paragraph4 = this.Paragragh4;
                    f.ImageURL4 = this.ImageURL4;

                    if (this.ImageURL != item.ImageURL && !string.IsNullOrEmpty(this.ImageURL))
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL))
                        FileHelper.DeleteFile(item.ImageURL, true);
                    }

                    if (this.ImageURL2 != item.ImageURL2 && !string.IsNullOrEmpty(this.ImageURL2))
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL2))
                        FileHelper.DeleteFile(item.ImageURL2, true);
                    }

                    if (this.ImageURL3 != item.ImageURL3 && !string.IsNullOrEmpty(this.ImageURL3))
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL3))
                        FileHelper.DeleteFile(item.ImageURL3, true);
                    }

                    if (this.ImageURL4 != item.ImageURL4 && !string.IsNullOrEmpty(this.ImageURL4))
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL4))
                        FileHelper.DeleteFile(item.ImageURL4, true);
                    }

                    db.SubmitChanges();
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
                    //Delete all associated comments
                    DeleteComments(f.ArticleID);
                    //Delete all associated comment subscriptions
                    DeleteCommentSubscriptions(f.ArticleID);

                    if (System.IO.File.Exists(f.ImageURL))
                        FileHelper.DeleteFile(f.ImageURL, true);

                    db.FF_Articles.DeleteOnSubmit(f);
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
                        LoadedItem.Paragragh2 = article.Paragraph2;
                        LoadedItem.ImageURL2 = article.ImageURL2;
                        LoadedItem.Paragragh3 = article.Paragraph3;
                        LoadedItem.ImageURL3 = article.ImageURL3;
                        LoadedItem.Paragragh4 = article.Paragraph4;
                        LoadedItem.ImageURL4 = article.ImageURL4;
                        LoadedItem.Date = article.Date;
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
                        Item.Paragragh2 = article.Paragraph2;
                        Item.ImageURL2 = article.ImageURL2;
                        Item.Paragragh3 = article.Paragraph3;
                        Item.ImageURL3 = article.ImageURL3;
                        Item.Paragragh4 = article.Paragraph4;
                        Item.ImageURL4 = article.ImageURL4;

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
            article.Paragraph2 = this.Paragragh2;
            article.ImageURL2 = this.ImageURL2;
            article.Paragraph3 = this.Paragragh3;
            article.ImageURL3 = this.ImageURL3;
            article.Paragraph4 = this.Paragragh4;
            article.ImageURL4 = this.ImageURL4;
            
            return article;
        }
    }
}
