using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class TeamPlayer
    {
        private TeamPlayer _LoadedItem;
        private Guid _TeamID;
        private Guid _ID;
        private Guid _PlayerID;
        private string _PlayerName;
        private int _StatusID;
        private string _Status;
        private string _ImageURL;
        private List<TeamPlayer> _PlayerCollection;

        #region
        public TeamPlayer LoadedItem
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
        
        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value;}
        }
        
        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public List<TeamPlayer> PlayerCollection
        {
            get { return _PlayerCollection; }
            set { _PlayerCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_TeamPlayer player = new FF_TeamPlayer();
            player.ID = this.ID;
            player.TeamID = this.TeamID;
            player.PlayerID = this.PlayerID;
            player.StatusID = this.StatusID;
            player.ImageURL = this.ImageURL;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_TeamPlayers.InsertOnSubmit(player);

                db.SubmitChanges();
            }
        }

        public void Update( bool isWithFile, string oldImageURL)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var player = db.FF_TeamPlayers.Single(u => u.ID == this.ID);

                if (player != null)
                {
                    player.PlayerID = this.PlayerID;
                    player.StatusID = this.StatusID;
                    player.ImageURL = this.ImageURL;

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
                var f = db.FF_TeamPlayers.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_TeamPlayers.DeleteOnSubmit(f);
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
                    var players = (from e in db.FF_TeamPlayers
                                   join p in db.FF_Players
                                   on e.PlayerID equals p.PlayerID
                                   join s in db.FF_PlayerStatus
                                   on e.StatusID equals s.StatusID
                                   where e.ID == this.ID
                                   select new { e, p.Name, s.Description });

                    if (players.Count() > 0)
                    {
                        LoadedItem = new TeamPlayer();
                        var player = players.ToList().ElementAt(0);

                        LoadedItem.ID = player.e.ID;
                        LoadedItem.TeamID = player.e.TeamID;
                        LoadedItem.PlayerID = player.e.PlayerID;
                        LoadedItem.StatusID = player.e.StatusID;
                        LoadedItem.Status = player.Description;
                        LoadedItem.ImageURL = player.e.ImageURL;
                        LoadedItem.PlayerName = player.Name;

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
                var players = (from e in db.FF_TeamPlayers
                               join p in db.FF_Players
                               on e.PlayerID equals p.PlayerID
                               join s in db.FF_PlayerStatus
                                on e.StatusID equals s.StatusID
                               where e.TeamID == this.TeamID
                               orderby e.StatusID ascending, p.Name ascending
                               select new { e, p.Name, s.Description });

                PlayerCollection = null;
                if (players.Count() > 0)
                {
                    PlayerCollection = new List<TeamPlayer>();
                    foreach (var player in players)
                    {
                        TeamPlayer Item = new TeamPlayer();
                        Item.ID = player.e.ID;
                        Item.TeamID = player.e.TeamID;
                        Item.PlayerID = player.e.PlayerID;
                        Item.StatusID = player.e.StatusID;
                        Item.Status = player.Description;
                        Item.ImageURL = player.e.ImageURL;
                        Item.PlayerName = player.Name;

                        PlayerCollection.Add(Item);
                    }
                }
            }
        }
    }
}
