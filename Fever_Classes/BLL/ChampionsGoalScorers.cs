using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class ChampionsGoalScorers
    {
        private ChampionsGoalScorers _LoadedItem;
        private Int64 _ID;
        private Guid _TeamID;
        private int _MatchID;
        private string _PlayerName;
        private string _Minute;
        private List<ChampionsGoalScorers> _ScorerCollection;

        #region
        public ChampionsGoalScorers LoadedItem
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

        public string PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

        public string Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }

        public List<ChampionsGoalScorers> ScorerCollection
        {
            get { return _ScorerCollection; }
            set { _ScorerCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_ChampionGoalScorer scorer = new FF_ChampionGoalScorer();
            scorer.TeamID = this.TeamID;
            scorer.MatchID = this.MatchID;
            scorer.PlayerName = this.PlayerName;
            scorer.Minute = this.Minute;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_ChampionGoalScorers.InsertOnSubmit(scorer);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorer = db.FF_ChampionGoalScorers.Single(u => u.ID == this.ID);

                if (scorer != null)
                {
                    scorer.PlayerName = this.PlayerName;
                    scorer.Minute = this.Minute;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_ChampionGoalScorers.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_ChampionGoalScorers.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorer = (from e in db.FF_ChampionGoalScorers
                              where e.ID == this.ID
                              select e).First();

                if (scorer != null)
                {
                    LoadedItem = new ChampionsGoalScorers();
                    LoadedItem.ID = scorer.ID;
                    LoadedItem.TeamID = scorer.TeamID;
                    LoadedItem.MatchID = scorer.MatchID;
                    LoadedItem.PlayerName = scorer.PlayerName;
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
                var scorers = (from e in db.FF_ChampionGoalScorers
                               where e.MatchID == this.MatchID
                               select e);

                ScorerCollection = null;
                if (scorers.Count() > 0)
                {
                    ScorerCollection = new List<ChampionsGoalScorers>();
                    foreach (var scorer in scorers)
                    {
                        ChampionsGoalScorers Item = new ChampionsGoalScorers();
                        Item.ID = scorer.ID;
                        Item.TeamID = scorer.TeamID;
                        Item.MatchID = scorer.MatchID;
                        Item.PlayerName = scorer.PlayerName;
                        Item.Minute = scorer.Minute;

                        ScorerCollection.Add(Item);
                    }
                }
            }
        }
    }
}
