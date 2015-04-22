using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Leagues
    {
        private Leagues _LoadedItem;
        private int _LeagueID;
        private string _Name;
        private List<Leagues> _LeagueCollection;

        #region
        public Leagues LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public int LeagueID
        {
            get { return _LeagueID; }
            set { _LeagueID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public List<Leagues> LeagueCollection
        {
            get { return _LeagueCollection; }
            set { _LeagueCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_League league = GetLeague();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Leagues.InsertOnSubmit(league);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var league = db.FF_Leagues.Single(u => u.LeagueID == this.LeagueID);

                if (league != null)
                {
                    league.Name = this.Name;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Leagues.Single(u => u.LeagueID == this.LeagueID);

                if (f != null)
                {
                    db.FF_Leagues.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var league = (from e in db.FF_Leagues
                             where e.LeagueID == this.LeagueID
                             select e).FirstOrDefault();

                if (league != null)
                {
                    LoadedItem = new Leagues();
                    LoadedItem.LeagueID = league.LeagueID;
                    LoadedItem.Name = league.Name;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var leagues = (from e in db.FF_Leagues
                              select e).DefaultIfEmpty();

                LeagueCollection = null;
                if (leagues.Count() > 0)
                {
                    LeagueCollection = new List<Leagues>();
                    foreach (var league in leagues)
                    {
                        Leagues Item = new Leagues();
                        Item.LeagueID = league.LeagueID;
                        Item.Name = league.Name;

                        LeagueCollection.Add(Item);
                    }
                }
            }
        }

        public FF_League GetLeague()
        {
            FF_League league = new FF_League();
            league.Name = this.Name;

            return league;
        }
    }
}
