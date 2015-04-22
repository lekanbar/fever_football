using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class TopScorers
    {
        private TopScorers _LoadedItem;
        private Int64 _ID;
        private Guid _SeasonID;
        private int _LeagueID;
        private string _Name;
        private int _Goals;
        private string _Details;
        private string _ImageURL;
        private bool _IsCurrent;
        private int _Week;
        private List<TopScorers> _TopScorersCollection;

        #region
        public TopScorers LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int LeagueID
        {
            get { return _LeagueID; }
            set { _LeagueID = value; }
        }

        public int Goals
        {
            get { return _Goals; }
            set { _Goals = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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

        public bool IsCurrent
        {
            get { return _IsCurrent; }
            set { _IsCurrent = value; }
        }

        public int Week
        {
            get { return Week; }
            set { _Week = value; }
        }

        public List<TopScorers> TopScorersCollection
        {
            get { return _TopScorersCollection; }
            set { _TopScorersCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_TopScorer top = new FF_TopScorer();
            top.LeagueID = this.LeagueID;
            top.Name = this.Name;
            top.ImageURL = this.ImageURL;
            top.Details = this.Details;
            top.SeasonID = this.SeasonID;
            top.Goals = this.Goals;
            top.Week = this.Week;
            top.IsCurrent = this.IsCurrent;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_TopScorers.InsertOnSubmit(top);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var top = db.FF_TopScorers.Single(u => u.ID == this.ID);

                if (top != null)
                {
                    top.Name = this.Name;
                    top.ImageURL = this.ImageURL;
                    top.Details = this.Details;
                    top.Goals = this.Goals;
                    top.Week = this.Week;
                    top.IsCurrent = this.IsCurrent;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_TopScorers.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_TopScorers.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var top = (from e in db.FF_TopScorers
                            where e.ID == this.ID
                            select e).FirstOrDefault();

                if (top != null)
                {
                    LoadedItem = new TopScorers();
                    LoadedItem.ID = top.ID;
                    LoadedItem.SeasonID = top.SeasonID;
                    LoadedItem.LeagueID = top.LeagueID;
                    LoadedItem.Name = top.Name;
                    LoadedItem.ImageURL = top.ImageURL;
                    LoadedItem.Goals = top.Goals;
                    LoadedItem.Details = top.Details;
                    LoadedItem.Week = top.Week;
                    LoadedItem.IsCurrent = top.IsCurrent;
                }
                else
                    LoadedItem = null;
            }
        }

        public void LoadByName()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var top = (from e in db.FF_TopScorers
                           where e.Name == this.Name
                           select e).FirstOrDefault();

                if (top != null)
                {
                    LoadedItem = new TopScorers();
                    LoadedItem.ID = top.ID;
                    LoadedItem.SeasonID = top.SeasonID;
                    LoadedItem.LeagueID = top.LeagueID;
                    LoadedItem.Name = top.Name;
                    LoadedItem.ImageURL = top.ImageURL;
                    LoadedItem.Goals = top.Goals;
                    LoadedItem.Details = top.Details;
                    LoadedItem.Week = top.Week;
                    LoadedItem.IsCurrent = top.IsCurrent;
                }
                else
                    LoadedItem = null;
            }
        }


        public void GetCurrentTopScorer()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var top = (from e in db.FF_TopScorers
                           where e.IsCurrent == true && e.LeagueID == this.LeagueID
                           select e).FirstOrDefault();

                if (top != null)
                {
                    LoadedItem = new TopScorers();
                    LoadedItem.ID = top.ID;
                    LoadedItem.SeasonID = top.SeasonID;
                    LoadedItem.LeagueID = top.LeagueID;
                    LoadedItem.Name = top.Name;
                    LoadedItem.ImageURL = top.ImageURL;
                    LoadedItem.Goals = top.Goals;
                    LoadedItem.Details = top.Details;
                    LoadedItem.Week = top.Week;
                    LoadedItem.IsCurrent = top.IsCurrent;
                }
                else
                    LoadedItem = null;
            }
        }
        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var tops = (from e in db.FF_TopScorers
                            where e.SeasonID == this.SeasonID && e.LeagueID == this.LeagueID
                            orderby e.Goals descending
                             select e).DefaultIfEmpty();

                TopScorersCollection = null;
                if (tops.Count() > 0)
                {
                    TopScorersCollection = new List<TopScorers>();
                    foreach (var top in tops)
                    {
                        TopScorers Item = new TopScorers();
                        Item.ID = top.ID;
                        Item.SeasonID = top.SeasonID;
                        Item.LeagueID = top.LeagueID;
                        Item.Name = top.Name;
                        Item.ImageURL = top.ImageURL;
                        Item.Goals = top.Goals;
                        Item.Details = top.Details;
                        Item.Week = top.Week;
                        Item.IsCurrent = top.IsCurrent;

                        TopScorersCollection.Add(Item);
                    }
                }
            }
        }
    }
}
