﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class LeagueTables
    {
        private LeagueTables _LoadedItem;
        private int _ID;
        private int _LeagueID;
        private Guid _SeasonID;
        private Guid _TeamID;
        private string _TeamName;
        private string _TeamLogo;
        private int _P;
        private int _W;
        private int _D;
        private int _L;
        private int _F;
        private int _A;
        private int _Diff;
        private int _Points;
        private List<LeagueTables> _TableCollection;

        #region
        public LeagueTables LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int LeagueID
        {
            get { return _LeagueID; }
            set { _LeagueID = value; }
        }

        public Guid SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
        }

        public Guid TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
        }

        public string TeamName
        {
            get { return _TeamName; }
            set { _TeamName = value; }
        }

        public string TeamLogo
        {
            get { return _TeamLogo; }
            set { _TeamLogo = value; }
        }

        public int P
        {
            get { return _P; }
            set { _P = value; }
        }

        public int W
        {
            get { return _W; }
            set { _W = value; }
        }

        public int D
        {
            get { return _D; }
            set { _D = value; }
        }

        public int L
        {
            get { return _L; }
            set { _L = value; }
        }

        public int F
        {
            get { return _F; }
            set { _F = value; }
        }

        public int A
        {
            get { return _A; }
            set { _A = value; }
        }

        public int Diff
        {
            get { return _Diff; }
            set { _Diff = value; }
        }

        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }

        public List<LeagueTables> TableCollection
        {
            get { return _TableCollection; }
            set { _TableCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_LeagueTable table = GetTable();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_LeagueTables.InsertOnSubmit(table);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var table = db.FF_LeagueTables.Single(u => u.ID == this.ID);

                if (table != null)
                {
                    table.P = this.P;
                    table.W = this.W;
                    table.D = this.D;
                    table.L = this.L;
                    table.F = this.F;
                    table.A = this.A;
                    table.Diff = this.Diff;
                    table.Points = this.Points;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = (from e in db.FF_LeagueTables
                              where e.TeamID == this.TeamID
                              select e).First();

                if (f != null)
                {
                    db.FF_LeagueTables.DeleteOnSubmit(f);
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
                    var table = (from e in db.FF_LeagueTables
                                 where e.TeamID == this.TeamID && e.SeasonID == this.SeasonID
                                 select e).First();

                    if (table != null)
                    {
                        LoadedItem = new LeagueTables();

                        var team = (from t in db.FF_Teams
                                    where t.TeamID == table.TeamID
                                    select t).Single();

                        LoadedItem.ID = table.ID;
                        LoadedItem.TeamID = table.TeamID;
                        LoadedItem.TeamName = team.Name;
                        LoadedItem.TeamLogo = team.LogoURL;
                        LoadedItem.LeagueID = table.LeagueID;
                        LoadedItem.SeasonID = table.SeasonID;
                        LoadedItem.P = table.P;
                        LoadedItem.W = table.W;
                        LoadedItem.D = table.D;
                        LoadedItem.L = table.L;
                        LoadedItem.F = table.F;
                        LoadedItem.A = table.A;
                        LoadedItem.Diff = table.Diff;
                        LoadedItem.Points = table.Points;
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
                var tables = (from e in db.FF_LeagueTables
                              where e.LeagueID == this.LeagueID && e.SeasonID == this.SeasonID
                              orderby e.W descending
                              orderby e.L ascending
                              orderby e.P descending
                              orderby e.Diff descending
                              orderby e.Points descending
                              select e);

                TableCollection = null;
                if (tables.Count() > 0)
                {
                    TableCollection = new List<LeagueTables>();
                    foreach (var table in tables)
                    {
                        LeagueTables Item = new LeagueTables();

                        var team = (from t in db.FF_Teams
                                    where t.TeamID == table.TeamID
                                    select t).Single();

                        Item.ID = table.ID;
                        Item.TeamID = table.TeamID;
                        Item.TeamName = team.Name;
                        Item.TeamLogo = team.LogoURL;
                        Item.LeagueID = table.LeagueID;
                        Item.SeasonID = table.SeasonID;
                        Item.P = table.P;
                        Item.W = table.W;
                        Item.D = table.D;
                        Item.L = table.L;
                        Item.F = table.F;
                        Item.A = table.A;
                        Item.Diff = table.Diff;
                        Item.Points = table.Points;

                        TableCollection.Add(Item);
                    }
                }
            }
        }

        public FF_LeagueTable GetTable()
        {
            FF_LeagueTable table = new FF_LeagueTable();
            table.TeamID = this.TeamID;
            table.LeagueID = this.LeagueID;
            table.SeasonID = this.SeasonID;
            table.P = this.P;
            table.W = this.W;
            table.D = this.D;
            table.L = this.L;
            table.F = this.F;
            table.A = this.A;
            table.Diff = this.Diff;
            table.Points = this.Points;

            return table;
        }
    }
}
