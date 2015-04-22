using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Groups
    {
        private Groups _LoadedItem;
        private Guid _GroupID;
        private char _Name;
        private List<Groups> _GroupCollection;

        #region
        public Groups LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid GroupID
        {
            get { return _GroupID; }
            set { _GroupID = value; }
        }

        public char Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public List<Groups> GroupCollection
        {
            get { return _GroupCollection; }
            set { _GroupCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_ChampionsLegaueGroup league = GetGroup();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_ChampionsLegaueGroups.InsertOnSubmit(league);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var league = db.FF_ChampionsLegaueGroups.Single(u => u.ID == this.GroupID);

                if (league != null)
                {
                    league.Group = Convert.ToString(this.Name);

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_ChampionsLegaueGroups.Single(u => u.ID == this.GroupID);

                if (f != null)
                {
                    db.FF_ChampionsLegaueGroups.DeleteOnSubmit(f);
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
                    var league = (from e in db.FF_ChampionsLegaueGroups
                                  where e.ID == this.GroupID
                                  select e).First();

                    if (league != null)
                    {
                        LoadedItem = new Groups();
                        LoadedItem.GroupID = league.ID;
                        LoadedItem.Name = Convert.ToChar(league.Group);
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
                var leagues = (from e in db.FF_ChampionsLegaueGroups
                               select e);

                GroupCollection = null;
                if (leagues.Count() > 0)
                {
                    GroupCollection = new List<Groups>();
                    foreach (var league in leagues)
                    {
                        Groups Item = new Groups();
                        Item.GroupID = league.ID;
                        Item.Name = Convert.ToChar(league.Group);

                        GroupCollection.Add(Item);
                    }
                }
            }
        }

        public FF_ChampionsLegaueGroup GetGroup()
        {
            FF_ChampionsLegaueGroup league = new FF_ChampionsLegaueGroup();
            league.ID = this.GroupID;
            league.Group = Convert.ToString(this.Name);

            return league;
        }
    }
}
