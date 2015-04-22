using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Season
    {
        private Season _LoadedItem;
        private Guid _SeasonID;
        private string _Period;
        private bool _IsCurrent;
        private List<Season> _SeasonCollection;

        #region
        public Season LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
        }

        public string Period
        {
            get { return _Period; }
            set { _Period = value; }
        }

        public bool IsCurrent
        {
            get { return _IsCurrent; }
            set { _IsCurrent = value; }
        }

        public List<Season> SeasonCollection
        {
            get { return _SeasonCollection; }
            set { _SeasonCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Season season = new FF_Season();
            season.SeasonID = this.SeasonID;
            season.Period = this.Period;
            season.IsCurrent = this.IsCurrent;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Seasons.InsertOnSubmit(season);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var season = db.FF_Seasons.Single(u => u.SeasonID == this.SeasonID);

                if (season != null)
                {
                    season.SeasonID = this.SeasonID;
                    season.Period = this.Period;
                    season.IsCurrent = this.IsCurrent;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Seasons.Single(u => u.SeasonID == this.SeasonID);

                if (f != null)
                {
                    db.FF_Seasons.DeleteOnSubmit(f);
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
                    var season = (from e in db.FF_Seasons
                                  where e.SeasonID == this.SeasonID
                                  select e).First();

                    if (season != null)
                    {
                        LoadedItem = new Season();
                        LoadedItem.SeasonID = season.SeasonID;
                        LoadedItem.Period = season.Period;
                        LoadedItem.IsCurrent = season.IsCurrent;
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

        public static Guid GetCurrentSeason()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                try
                {
                    var season = (from e in db.FF_Seasons
                                  where e.IsCurrent == true
                                  select e).First();

                    if (season != null)
                    {
                        return season.SeasonID;
                    }
                    else
                        return Guid.Empty;
                }
                catch
                {
                    return Guid.Empty;
                }
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var seasons = (from e in db.FF_Seasons
                             select e);

                SeasonCollection = null;
                if (seasons.Count() > 0)
                {
                    SeasonCollection = new List<Season>();
                    foreach (var season in seasons)
                    {
                        Season Item = new Season();
                        Item.SeasonID = season.SeasonID;
                        Item.Period = season.Period;
                        Item.IsCurrent = season.IsCurrent;

                        SeasonCollection.Add(Item);
                    }
                }
            }
        }
    }
}
