using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class ChampionsLeagueOther
    {
        private ChampionsLeagueOther _LoadedItem;
        private int _ID;
        private Guid _SeasonID;
        private Guid _Team1ID;
        private Guid _Team2ID;
        private string _Team1Name;
        private string _Team2Name;
        private string _Team1LogoURL;
        private string _Team2LogoURL;
        private string _Team1PlayerImageURL;
        private string _Team2PlayerImageURL;
        private string _Team1Details;
        private string _Team2Details;
        private string _Team1PlayerName;
        private string _Team2PlayerName;
        private int _Team1Score;
        private int _Team2Score;
        private string _Location;
        private DateTime _Date;
        private int _Team1Aggregate;
        private int _Team2Aggregate;
        private int _Leg;
        private List<ChampionsLeagueOther> _MatchCollection;

        #region
        public ChampionsLeagueOther LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Guid SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
        }

        public Guid Team1ID
        {
            get { return _Team1ID; }
            set { _Team1ID = value; }
        }

        public Guid Team2ID
        {
            get { return _Team2ID; }
            set { _Team2ID = value; }
        }

        public string Team1PlayerImageURL
        {
            get { return _Team1PlayerImageURL; }
            set { _Team1PlayerImageURL = value; }
        }

        public string Team2PlayerImageURL
        {
            get { return _Team2PlayerImageURL; }
            set { _Team2PlayerImageURL = value; }
        }

        public string Team1Name
        {
            get { return _Team1Name; }
            set { _Team1Name = value; }
        }

        public string Team2Name
        {
            get { return _Team2Name; }
            set { _Team2Name = value; }
        }

        public string Team1LogoURL
        {
            get { return _Team1LogoURL; }
            set { _Team1LogoURL = value; }
        }

        public string Team2LogoURL
        {
            get { return _Team2LogoURL; }
            set { _Team2LogoURL = value; }
        }

        public string Team1Details
        {
            get { return _Team1Details; }
            set { _Team1Details = value; }
        }

        public string Team2Details
        {
            get { return _Team2Details; }
            set { _Team2Details = value; }
        }

        public string Team1PlayerName
        {
            get { return _Team1PlayerName; }
            set { _Team1PlayerName = value; }
        }

        public string Team2PlayerName
        {
            get { return _Team2PlayerName; }
            set { _Team2PlayerName = value; }
        }

        public int Team1Score
        {
            get { return _Team1Score; }
            set { _Team1Score = value; }
        }

        public int Team2Score
        {
            get { return _Team2Score; }
            set { _Team2Score = value; }
        }

        public int Team1Aggregate
        {
            get { return _Team1Aggregate; }
            set { _Team1Aggregate = value; }
        }

        public int Team2Aggregate
        {
            get { return _Team2Aggregate; }
            set { _Team2Aggregate = value; }
        }

        public int Leg
        {
            get { return _Leg; }
            set { _Leg = value; }
        }

        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public List<ChampionsLeagueOther> MatchCollection
        {
            get { return _MatchCollection; }
            set { _MatchCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_ChampionsOtherStage match = GetMatch();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_ChampionsOtherStages.InsertOnSubmit(match);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var match = db.FF_ChampionsOtherStages.Single(u => u.ID == this.ID);

                if (match != null)
                {
                    match.SeasonID = this.SeasonID;
                    match.Team1ID = this.Team1ID;
                    match.Team2ID = this.Team2ID;
                    match.Team1PlayerImageURL = this.Team1PlayerImageURL;
                    match.Team2PlayerImageURL = this.Team2PlayerImageURL;
                    match.Team1Details = this.Team1Details;
                    match.Team2Details = this.Team2Details;
                    match.Team1PlayerName = this.Team1PlayerName;
                    match.Team2PlayerName = this.Team2PlayerName;
                    match.Team1Score = this.Team1Score;
                    match.Team2Score = this.Team2Score;
                    match.Location = this.Location;
                    match.Date = this.Date;
                    match.Aggregate1 = this.Team1Aggregate;
                    match.Aggregate2 = this.Team2Aggregate;
                    match.Leg = this.Leg;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_ChampionsOtherStages.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_ChampionsOtherStages.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var match = (from e in db.FF_ChampionsOtherStages
                             where e.ID == this.ID
                             select e).FirstOrDefault();

                if (match != null)
                {
                    var team1 = (from e in db.FF_Teams
                                 where e.TeamID == match.Team1ID
                                 select e).FirstOrDefault();

                    var team2 = (from e in db.FF_Teams
                                 where e.TeamID == match.Team2ID
                                 select e).FirstOrDefault();

                    LoadedItem = new ChampionsLeagueOther();
                    LoadedItem.ID = match.ID;
                    LoadedItem.SeasonID = match.SeasonID;
                    LoadedItem.Team1ID = match.Team1ID;
                    LoadedItem.Team2ID = match.Team2ID;
                    LoadedItem.Team1Name = team1.Name;
                    LoadedItem.Team2Name = team2.Name;
                    LoadedItem.Team1LogoURL = team1.LogoURL;
                    LoadedItem.Team2LogoURL = team2.LogoURL;
                    LoadedItem.Team1PlayerImageURL = match.Team1PlayerImageURL;
                    LoadedItem.Team2PlayerImageURL = match.Team2PlayerImageURL;
                    LoadedItem.Team1Details = match.Team1Details;
                    LoadedItem.Team2Details = match.Team2Details;
                    LoadedItem.Team1PlayerName = match.Team1PlayerName;
                    LoadedItem.Team2PlayerName = match.Team2PlayerName;
                    LoadedItem.Team1Score = match.Team1Score;
                    LoadedItem.Team2Score = match.Team2Score;
                    LoadedItem.Location = match.Location;
                    LoadedItem.Date = match.Date;
                    LoadedItem.Team1Aggregate = match.Aggregate1;
                    LoadedItem.Team2Aggregate = match.Aggregate2;
                    LoadedItem.Leg = match.Leg;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var matches = (from e in db.FF_ChampionsOtherStages
                               where e.SeasonID == this.SeasonID
                               orderby e.Date
                               select e).DefaultIfEmpty();

                MatchCollection = null;
                if (matches.Count() > 0)
                {
                    MatchCollection = new List<ChampionsLeagueOther>();
                    foreach (var match in matches)
                    {
                        var team1 = (from e in db.FF_Teams
                                     where e.TeamID == match.Team1ID
                                     select e).FirstOrDefault();

                        var team2 = (from e in db.FF_Teams
                                     where e.TeamID == match.Team2ID
                                     select e).FirstOrDefault();

                        ChampionsLeagueOther Item = new ChampionsLeagueOther();
                        Item.ID = match.ID;
                        Item.SeasonID = match.SeasonID;
                        Item.Team1ID = match.Team1ID;
                        Item.Team2ID = match.Team2ID;
                        Item.Team1Name = team1.Name;
                        Item.Team2Name = team2.Name;
                        Item.Team1LogoURL = team1.LogoURL;
                        Item.Team2LogoURL = team2.LogoURL;
                        Item.Team1PlayerImageURL = match.Team1PlayerImageURL;
                        Item.Team2PlayerImageURL = match.Team2PlayerImageURL;
                        Item.Team1Details = match.Team1Details;
                        Item.Team2Details = match.Team2Details;
                        Item.Team1PlayerName = match.Team1PlayerName;
                        Item.Team2PlayerName = match.Team2PlayerName;
                        Item.Team1Score = match.Team1Score;
                        Item.Team2Score = match.Team2Score;
                        Item.Location = match.Location;
                        Item.Date = match.Date;
                        Item.Team1Aggregate = match.Aggregate1;
                        Item.Team2Aggregate = match.Aggregate2;
                        Item.Leg = match.Leg;

                        MatchCollection.Add(Item);
                    }
                }
            }
        }

        public void GetAllByTeam()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var matches = (from e in db.FF_ChampionsOtherStages
                               where e.SeasonID == this.SeasonID
                                     && (e.Team1ID == this.Team1ID || e.Team2ID == this.Team1ID)
                               orderby e.Date
                               select e).DefaultIfEmpty();

                MatchCollection = null;
                if (matches.Count() > 0)
                {
                    MatchCollection = new List<ChampionsLeagueOther>();

                    foreach (var match in matches)
                    {
                        var team1 = (from e in db.FF_Teams
                                     where e.TeamID == match.Team1ID
                                     select e).FirstOrDefault();

                        var team2 = (from e in db.FF_Teams
                                     where e.TeamID == match.Team2ID
                                     select e).FirstOrDefault();

                        ChampionsLeagueOther Item = new ChampionsLeagueOther();
                        Item.ID = match.ID;
                        Item.SeasonID = match.SeasonID;
                        Item.Team1ID = match.Team1ID;
                        Item.Team2ID = match.Team2ID;
                        Item.Team1Name = team1.Name;
                        Item.Team2Name = team2.Name;
                        Item.Team1LogoURL = team1.LogoURL;
                        Item.Team2LogoURL = team2.LogoURL;
                        Item.Team1PlayerImageURL = match.Team1PlayerImageURL;
                        Item.Team2PlayerImageURL = match.Team2PlayerImageURL;
                        Item.Team1Details = match.Team1Details;
                        Item.Team2Details = match.Team2Details;
                        Item.Team1PlayerName = match.Team1PlayerName;
                        Item.Team2PlayerName = match.Team2PlayerName;
                        Item.Team1Score = match.Team1Score;
                        Item.Team2Score = match.Team2Score;
                        Item.Location = match.Location;
                        Item.Date = match.Date;
                        Item.Team1Aggregate = match.Aggregate1;
                        Item.Team2Aggregate = match.Aggregate2;
                        Item.Leg = match.Leg;

                        MatchCollection.Add(Item);
                    }
                }
            }
        }

        public FF_ChampionsOtherStage GetMatch()
        {
            FF_ChampionsOtherStage match = new FF_ChampionsOtherStage();
            match.SeasonID = this.SeasonID;
            match.Team1ID = this.Team1ID;
            match.Team2ID = this.Team2ID;
            match.Team1PlayerImageURL = this.Team1PlayerImageURL;
            match.Team2PlayerImageURL = this.Team2PlayerImageURL;
            match.Team1Details = this.Team1Details;
            match.Team2Details = this.Team2Details;
            match.Team1PlayerName = this.Team1PlayerName;
            match.Team2PlayerName = this.Team2PlayerName;
            match.Team1Score = this.Team1Score;
            match.Team2Score = this.Team2Score;
            match.Location = this.Location;
            match.Date = this.Date;
            match.Aggregate1 = this.Team1Aggregate;
            match.Aggregate2 = this.Team2Aggregate;
            match.Leg = this.Leg;

            return match;
        }
    }
}
