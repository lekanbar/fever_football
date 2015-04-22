using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class GoalScorers
    {
        private GoalScorers _LoadedItem;
        private Guid _ID;
        private Guid _TeamID;
        private int _MatchID;
        private Guid _PlayerID;
        private string _Minute;
        private string _ScoredBy;
        protected string _PlayerImage;
        private string _AssistedBy;
        private Int64 _AssistID;
        private List<GoalScorers> _ScorerCollection;
        private List<GoalKeep> _GoalCollection ;

        #region
        public GoalScorers LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid ID
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

        public string ScoredBy
        {
            get { return _ScoredBy; }
            set { _ScoredBy = value; }
        }
        
        public string PlayerImage
        {
            get { return _PlayerImage; }
            set { _PlayerImage = value; }
        }

        public string AssistedBy
        {
            get { return _AssistedBy; }
            set { _AssistedBy = value; }
        }

        public Int64 AssistID
        { 
            get { return _AssistID ; }
            set { _AssistID = value; }
        }

        public List<GoalScorers> ScorerCollection
        {
            get { return _ScorerCollection; }
            set { _ScorerCollection = value; }
        }

        public List<GoalKeep> GoalCollection
        {
            get { return _GoalCollection; }
            set { _GoalCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_GoalScorer scorer = new FF_GoalScorer();
            scorer.ID = this.ID;
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
                //var scorer = (from e in db.FF_GoalScorers
                //              where e.ID == this.ID
                //              select e).First();
                try
                {
                    var scorer = (from e in db.FF_GoalScorers
                                  join tp1 in db.FF_TeamPlayers
                                  on e.PlayerID equals tp1.ID
                                  join p1 in db.FF_Players
                                  on tp1.PlayerID equals p1.PlayerID
                                  where e.ID == this.ID
                                  select new { e, Scorer = p1.Name, ScorerImage = tp1.ImageURL }).First();

                    if (scorer != null)
                    {
                        var assists = (from ass in db.FF_Assists
                                       join play in db.FF_TeamPlayers
                                       on ass.PlayerID equals play.ID
                                       join play2 in db.FF_Players
                                       on play.PlayerID equals play2.PlayerID
                                       where ass.GoalID == this.ID
                                       select new { ass.ID, play2.Name });

                        LoadedItem = new GoalScorers();
                        LoadedItem.ID = scorer.e.ID;
                        LoadedItem.TeamID = scorer.e.TeamID;
                        LoadedItem.MatchID = scorer.e.MatchID;
                        LoadedItem.PlayerID = scorer.e.PlayerID;
                        LoadedItem.Minute = scorer.e.Minute;
                        LoadedItem.ScoredBy = scorer.Scorer;
                        LoadedItem.PlayerImage = scorer.ScorerImage;

                        if (assists.Count() > 0)
                        {
                            LoadedItem.AssistedBy = assists.ToList().ElementAt(0).Name;
                            LoadedItem.AssistID = assists.ToList().ElementAt(0).ID;
                        }
                        else
                        {
                            LoadedItem.AssistedBy = "not applicable";
                        }

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
                var scorers = (from e in db.FF_GoalScorers
                               join tp1 in db.FF_TeamPlayers
                               on e.PlayerID equals tp1.ID
                               join p1 in db.FF_Players
                               on tp1.PlayerID equals p1.PlayerID
                               where e.MatchID == this.MatchID && e.TeamID == this.TeamID
                               select new { e, Scorer = p1.Name, ScorerImage = tp1.ImageURL });

                ScorerCollection = null;
                if (scorers.Count() > 0)
                {
                    ScorerCollection = new List<GoalScorers>();
                    foreach (var scorer in scorers)
                    {
                        var assists = (from ass in db.FF_Assists
                                       join play in db.FF_TeamPlayers
                                       on ass.PlayerID equals play.ID
                                       join play2 in db.FF_Players
                                       on play.PlayerID equals play2.PlayerID
                                       where ass.GoalID == scorer.e.ID
                                       select new { ass.ID, play2.Name });

                        GoalScorers Item = new GoalScorers();
                        Item.ID = scorer.e.ID;
                        Item.TeamID = scorer.e.TeamID;
                        Item.MatchID = scorer.e.MatchID;
                        Item.PlayerID = scorer.e.PlayerID;
                        Item.Minute = scorer.e.Minute;
                        Item.ScoredBy = scorer.Scorer;
                        Item.PlayerImage = scorer.ScorerImage;

                        if (assists.Count() > 0)
                        {
                            Item.AssistedBy = assists.ToList().ElementAt(0).Name;
                            Item.AssistID = assists.ToList().ElementAt(0).ID;
                        }
                        else
                        {
                            Item.AssistedBy = "not applicable";
                        }
                        ScorerCollection.Add(Item);
                    }
                }
            }
        }

        public void GetAllByLeague(int LeagueID, Guid SeasonID )
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var scorers = (from e in db.FF_GoalScorers
                               join p1 in db.FF_TeamPlayers
                               on e.PlayerID equals p1.ID
                               join p2 in db.FF_Players
                               on p1.PlayerID equals p2.PlayerID
                               join m in db.FF_Matches
                               on e.MatchID equals m.ID
                               where m.LeagueID == LeagueID && m.SeasonID == SeasonID
                               orderby m.Date ascending
                               select new { e, p2.Name, p1.ImageURL }
                                   );

                var goals  = ( from s in scorers
                              group s by s.Name into grp
                              orderby grp.Count() descending
                              select new { PlayerName = grp.Key, 
                                            TotalGoals = grp.Count()
                              } );

                GoalCollection = null;
                if (goals.Count() > 0)
                {
                    GoalCollection = new List<GoalKeep>();
                    int count = 0;

                    foreach (var goal in goals)
                    {
                        if (count < 11)
                        {
                            var getImage = (from e in db.FF_TeamPlayers
                                            join p1 in db.FF_Players
                                            on e.PlayerID equals p1.PlayerID
                                            where p1.Name == goal.PlayerName
                                            select e.ImageURL);

                            GoalKeep Item = new GoalKeep();
                            Item.PlayerName = goal.PlayerName;
                            Item.GoalCount = goal.TotalGoals;

                            if (getImage.Count() > 0)
                                Item.PlayerImage = getImage.ToList().ElementAt(0);
                            else Item.PlayerImage = null;

                            GoalCollection.Add(Item);
                            count++;
                        }
                    }
                }
            }
        }

        public class GoalKeep
        { 
            private string _PlayerName;
            private string _PlayerImage;
            private int _GoalCount;

            public string PlayerName
            {
                get { return _PlayerName; }
                set { _PlayerName = value; }
            }

            public int GoalCount
            {
                get { return _GoalCount; }
                set { _GoalCount = value; }
            }

            public string PlayerImage
            {
                get { return _PlayerImage; }
                set { _PlayerImage = value; }
            }
        }
    }

    
}
