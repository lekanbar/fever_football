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
        private int _MatchID;
        private Int64 _GoalID;
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

        public Int64 GoalID
        {
            get { return _GoalID; }
            set { _GoalID = value; }
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
                var assist = (from e in db.FF_Assists
                            where e.ID == this.ID
                            select e).First();

                if (assist != null)
                {
                    LoadedItem = new Assists();
                    LoadedItem.ID = assist.ID;
                    LoadedItem.PlayerID = assist.PlayerID;
                    LoadedItem.MatchID = assist.MatchID;
                    LoadedItem.GoalID = assist.GoalID;
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
                               where e.MatchID == this.MatchID
                               select e);

                Collection = null;
                if (assists.Count() > 0)
                {
                    Collection = new List<Assists>();
                    foreach (var assist in assists)
                    {
                        Assists Item = new Assists();
                        Item.ID = assist.ID;
                        Item.PlayerID = assist.PlayerID;
                        Item.MatchID = assist.MatchID;
                        Item.GoalID = assist.GoalID;

                        Collection.Add(Item);
                    }
                }
            }
        }
    }
}
