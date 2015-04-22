using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Team
    {
        private Team _LoadedItem;
        private Guid _TeamID;
        private int _LeagueID;
        private string _Name;
        private string _LogoURL;
        private List<Team> _TeamsCollection;

        #region
        public Team LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Guid TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
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

        public string LogoURL
        {
            get { return _LogoURL; }
            set { _LogoURL = value; }
        }

        public List<Team> TeamsCollection
        {
            get { return _TeamsCollection; }
            set { _TeamsCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Team team = new FF_Team();
            team.TeamID = this.TeamID;
            team.LeagueID = this.LeagueID;
            team.Name = this.Name;
            team.LogoURL = this.LogoURL;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Teams.InsertOnSubmit(team);

                db.SubmitChanges();
            }
        }

        public void Update( bool isWithFile, string oldImageURL)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var team = db.FF_Teams.Single(u => u.TeamID == this.TeamID);

                if (team != null)
                {
                    team.LeagueID = this.LeagueID;
                    team.Name = this.Name;
                    team.LogoURL = this.LogoURL;

                    db.SubmitChanges();

                    if (isWithFile)
                        try
                        {
                            FileHelper.DeleteFile(oldImageURL);
                        }
                        catch { }
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Teams.Single(u => u.TeamID == this.TeamID);

                if (f != null)
                {
                    db.FF_Teams.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var team = (from e in db.FF_Teams
                              where e.TeamID == this.TeamID
                              select e).First();

                if (team != null)
                {
                    LoadedItem = new Team();
                    LoadedItem.TeamID = team.TeamID;
                    LoadedItem.LeagueID = team.LeagueID;
                    LoadedItem.Name = team.Name;
                    LoadedItem.LogoURL = team.LogoURL;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var teams = (from e in db.FF_Teams
                               select e);

                TeamsCollection = null;
                if (teams.Count() > 0)
                {
                    TeamsCollection = new List<Team>();
                    foreach (var team in teams)
                    {
                        Team Item = new Team();
                        Item.TeamID = team.TeamID;
                        Item.LeagueID = team.LeagueID;
                        Item.Name = team.Name;
                        Item.LogoURL = team.LogoURL;

                        TeamsCollection.Add(Item);
                    }
                }
            }
        }

        public void GetAllByLeagueID()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var teams = (from e in db.FF_Teams
                             where e.LeagueID == this.LeagueID 
                             select e);

                TeamsCollection = null;
                if (teams.Count() > 0)
                {
                    TeamsCollection = new List<Team>();
                    foreach (var team in teams)
                    {
                        Team Item = new Team();
                        Item.TeamID = team.TeamID;
                        Item.LeagueID = team.LeagueID;
                        Item.Name = team.Name;
                        Item.LogoURL = team.LogoURL;

                        TeamsCollection.Add(Item);
                    }
                }
            }
        }
    }
}
