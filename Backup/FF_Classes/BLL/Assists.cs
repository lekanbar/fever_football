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
        private string _Name;
        private string _ImageURL;
        private int _Assists;
        private string _Details;
        private Guid _SeasonID;
        private int _LeagueID;
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

        public Guid SeasonID
        {
            get { return _SeasonID; }
            set { _SeasonID = value; }
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

        public int AssistCount
        {
            get { return _Assists; }
            set { _Assists = value; }
        }

        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        public string Details
        {
            get { return _Details; }
            set { _Details = value; }
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
            assist.Name = this.Name;
            assist.ImageURL = this.ImageURL;
            assist.Assists = this.AssistCount;
            assist.SeasonID = this.SeasonID;
            assist.LeagueID = this.LeagueID;
            assist.Details = this.Details;

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
                    assist.Name = this.Name;
                    assist.ImageURL = this.ImageURL;
                    assist.Assists = this.AssistCount;
                    assist.Details = this.Details;

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
                var assist = (from e in db.FF_Assists
                            where e.ID == this.ID
                            select e).FirstOrDefault();

                if (assist != null)
                {
                    LoadedItem = new Assists();
                    LoadedItem.ID = assist.ID;
                    LoadedItem.Name = assist.Name;
                    LoadedItem.ImageURL = assist.ImageURL;
                    LoadedItem.AssistCount = assist.Assists;
                    LoadedItem.SeasonID = assist.SeasonID;
                    LoadedItem.LeagueID = assist.LeagueID;
                    LoadedItem.Details = assist.Details;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var assists = (from e in db.FF_Assists
                             where e.SeasonID == this.SeasonID && e.LeagueID == this.LeagueID
                             select e);

                Collection = null;
                if (assists.Count() > 0)
                {
                    Collection = new List<Assists>();
                    foreach (var assist in assists)
                    {
                        Assists Item = new Assists();
                        Item.ID = assist.ID;
                        Item.Name = assist.Name;
                        Item.ImageURL = assist.ImageURL;
                        Item.AssistCount = assist.Assists;
                        Item.SeasonID = assist.SeasonID;
                        Item.LeagueID = assist.LeagueID;
                        Item.Details = assist.Details;

                        Collection.Add(Item);
                    }
                }
            }
        }
    }
}
