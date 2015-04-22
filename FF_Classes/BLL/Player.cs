using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Player
    {
        private Player _LoadedItem;
        private Guid _PlayerID;
        private string _Name;
        private List<Player> _PlayerCollection;

        #region
        public Player LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }


        public Guid PlayerID
        {
            get { return _PlayerID; }
            set { _PlayerID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public List<Player> PlayerCollection
        {
            get { return _PlayerCollection; }
            set { _PlayerCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Player player = new FF_Player();
            player.PlayerID = this.PlayerID;
            player.Name = this.Name;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Players.InsertOnSubmit(player);

                db.SubmitChanges();
            }
        }

        public void Update( )
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var player = db.FF_Players.Single(u => u.PlayerID == this.PlayerID);

                if (player != null)
                {
                    player.Name = this.Name;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Players.Single(u => u.PlayerID == this.PlayerID);

                if (f != null)
                {
                    db.FF_Players.DeleteOnSubmit(f);
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
                    var player = (from e in db.FF_Players
                                  where e.PlayerID == this.PlayerID
                                  select e).First();

                    if (player != null)
                    {
                        LoadedItem = new Player();
                        LoadedItem.PlayerID = player.PlayerID;
                        LoadedItem.Name = player.Name;
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
                var players = (from e in db.FF_Players
                               orderby e.Name
                               select  e );

                PlayerCollection = null;
                if (players.Count() > 0)
                {
                    PlayerCollection = new List<Player>();
                    foreach (var player in players)
                    {
                        Player Item = new Player();
                        Item.PlayerID = player.PlayerID;
                        Item.Name = player.Name;

                        PlayerCollection.Add(Item);
                    }
                }
            }
        }

        public void Search( string SearchString)
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var players = (from e in db.FF_Players
                               where e.Name.Contains(SearchString)
                               orderby e.Name 
                               select e);

                PlayerCollection = null;
                if (players.Count() > 0)
                {
                    PlayerCollection = new List<Player>();
                    foreach (var player in players)
                    {
                        Player Item = new Player();
                        Item.PlayerID = player.PlayerID;
                        Item.Name = player.Name;

                        PlayerCollection.Add(Item);
                    }
                }
            }
        }
    }
}
