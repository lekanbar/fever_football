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
        private int _StatusID;
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

        public int StatusID
        {
            get { return _StatusID; }
            set { _StatusID = value; }
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
                var player = (from e in db.FF_TeamPlayers
                              where e.ID == this.ID
                              select e).First();

                if (player != null)
                {
                    LoadedItem = new TeamPlayer();
                    LoadedItem.ID = player.ID;
                    LoadedItem.TeamID = player.TeamID;
                    LoadedItem.PlayerID = player.PlayerID;
                    LoadedItem.StatusID = player.StatusID;
                    LoadedItem.ImageURL = player.ImageURL;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var players = (from e in db.FF_TeamPlayers
                               where e.TeamID == this.TeamID
                               select e);

                PlayerCollection = null;
                if (players.Count() > 0)
                {
                    PlayerCollection = new List<TeamPlayer>();
                    foreach (var player in players)
                    {
                        TeamPlayer Item = new TeamPlayer();
                        Item.ID = player.ID;
                        Item.TeamID = player.TeamID;
                        Item.PlayerID = player.PlayerID;
                        Item.StatusID = player.StatusID;
                        Item.ImageURL = player.ImageURL;

                        PlayerCollection.Add(Item);
                    }
                }
            }
        }
    }
}
