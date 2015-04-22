using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Assists
    {
        private Assists _LoadedItem;
        private Int64 _ID;
        private Guid _PlayerID;
        private string _PlayerName;
        private string _PlayerImage;
        private int _MatchID;
        private Guid _GoalID;
        private int _AssistCount;
        private List<Assists> _Collection;

        #region
        public Assists LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Guid PlayerID
        {
            get { return _PlayerID; }
            set { _PlayerID = value; }
        }

        public string PlayerName
        {
            get { return _PlayerName; }
            set { _PlayerName = value; }
        }

        public string PlayerImage
        {
            get { return _PlayerImage; }
            set { _PlayerImage = value; }
        }
        
        public Guid GoalID
        {
            get { return _GoalID; }
            set { _GoalID = value; }
        }

        public int AssistCount
        {
            get { return _AssistCount; }
            set { _AssistCount = value; }
        }

        public int MatchID
        {
            get { return _MatchID; }
            set { _MatchID = value; }
        }

        public List<Assists> Collection
        {
            get { return _Collection; }
            set { _Collection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Assist assist = new FF_Assist();
            assist.ID = this.ID;
            assist.PlayerID = this.PlayerID;
            assist.MatchID = this.MatchID;
            assist.GoalID = this.GoalID;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Assists.InsertOnSubmit(assist);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var assist = db.FF_Assists.Single(u => u.ID == this.ID);

                if (assist != null)
                {
                    assist.PlayerID = this.PlayerID;
                    assist.MatchID = this.MatchID;
                    assist.GoalID = this.GoalID;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Assists.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_Assists.DeleteOnSubmit(f);
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
                    var assist = (from e in db.FF_Assists
                                  join p1 in db.FF_TeamPlayers on e.PlayerID equals p1.ID
                                  join p2 in db.FF_Players on p1.PlayerID equals p2.PlayerID
                                  where e.ID == this.ID
                                  select new { e, p2.Name }).First();

                    if (assist != null)
                    {
                        LoadedItem = new Assists();
                        LoadedItem.ID = assist.e.ID;
                        LoadedItem.PlayerID = assist.e.PlayerID;
                        LoadedItem.MatchID = assist.e.MatchID;
                        LoadedItem.GoalID = assist.e.GoalID;
                        LoadedItem.PlayerName = assist.Name;
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
                var assists = (from e in db.FF_Assists
                               join p1 in db.FF_TeamPlayers on e.PlayerID equals p1.ID
                               join p2 in db.FF_Players on p1.PlayerID equals p2.PlayerID
                               where e.MatchID == this.MatchID
                               select new { e, p2.Name } );

                Collection = null;
                if (assists.Count() > 0)
                {
                    Collection = new List<Assists>();
                    foreach (var assist in assists)
                    {
                        Assists Item = new Assists();
                        Item.ID = assist.e.ID;
                        Item.PlayerID = assist.e.PlayerID;
                        Item.MatchID = assist.e.MatchID;
                        Item.GoalID = assist.e.GoalID;
                        Item.PlayerName = assist.Name;

                        Collection.Add(Item);
                    }
                }
            }
        }

        public void GetAllForLeague(int LeagueID, Guid SeasonID)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var assists = (from e in db.FF_Assists
                               join p1 in db.FF_TeamPlayers on e.PlayerID equals p1.ID
                               join p2 in db.FF_Players on p1.PlayerID equals p2.PlayerID
                               join t in db.FF_Matches on e.MatchID equals t.ID
                               where t.LeagueID == LeagueID && t.SeasonID == SeasonID
                               orderby t.Date descending
                               select new { e, p2.Name });

                var assistsCount = (from a in assists
                             group a by a.Name into grp
                             orderby grp.Count() descending
                             select new
                             {
                                 PlayerName = grp.Key,
                                 TotalAssists = grp.Count()
                             });

                Collection = null;
                if (assistsCount.Count() > 0)
                {
                    Collection = new List<Assists>();
                    int count = 0;
                    foreach (var assist in assistsCount)
                    {
                        if (count < 11)
                        {
                            var getImage = (from e in db.FF_TeamPlayers
                                            join p1 in db.FF_Players
                                            on e.PlayerID equals p1.PlayerID
                                            where p1.Name == assist.PlayerName
                                            select e.ImageURL);

                            Assists Item = new Assists();
                            Item.PlayerName = assist.PlayerName;
                            Item.AssistCount = assist.TotalAssists;

                            if (getImage.Count() > 0)
                                Item.PlayerImage = getImage.ToList().ElementAt(0);
                            else Item.PlayerImage = null;

                            Collection.Add(Item);
                            count++;
                        }
                    }
                }
            }
        }
    }
}
