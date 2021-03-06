﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class GoalScorers
    {
        private GoalScorers _LoadedItem;
        private Int64 _ID;
        private Guid _TeamID;
        private int _MatchID;
        private Guid _PlayerID;
        private string _Minute;
        private List<GoalScorers> _ScorerCollection;

        #region
        public GoalScorers LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Guid TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
        }

        public int MatchID
        {
            get { return _MatchID; }
            set { _MatchID = value; }
        }

        public Guid PlayerID
        {
            get { return _PlayerID; }
            set { _PlayerID = value; }
        }

        public string Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }

        public List<GoalScorers> ScorerCollection
        {
            get { return _ScorerCollection; }
            set { _ScorerCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_GoalScorer scorer = new FF_GoalScorer();
            scorer.TeamID = this.TeamID;
            scorer.MatchID = this.MatchID;
            scorer.PlayerID = this.PlayerID;
            scorer.Minute = this.Minute;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_GoalScorers.InsertOnSubmit(scorer);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorer = db.FF_GoalScorers.Single(u => u.ID == this.ID);

                if (scorer != null)
                {
                    scorer.PlayerID = this.PlayerID;
                    scorer.Minute = this.Minute;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_GoalScorers.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_GoalScorers.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorer = (from e in db.FF_GoalScorers
                              where e.ID == this.ID
                              select e).First();

                if (scorer != null)
                {
                    LoadedItem = new GoalScorers();
                    LoadedItem.ID = scorer.ID;
                    LoadedItem.TeamID = scorer.TeamID;
                    LoadedItem.MatchID = scorer.MatchID;
                    LoadedItem.PlayerID = scorer.PlayerID;
                    LoadedItem.Minute = scorer.Minute;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorers = (from e in db.FF_GoalScorers
                               where e.MatchID == this.MatchID
                               select e);

                ScorerCollection = null;
                if (scorers.Count() > 0)
                {
                    ScorerCollection = new List<GoalScorers>();
                    foreach (var scorer in scorers)
                    {
                        GoalScorers Item = new GoalScorers();
                        Item.ID = scorer.ID;
                        Item.TeamID = scorer.TeamID;
                        Item.MatchID = scorer.MatchID;
                        Item.PlayerID = scorer.PlayerID;
                        Item.Minute = scorer.Minute;

                        ScorerCollection.Add(Item);
                    }
                }
            }
        }
    }
}
