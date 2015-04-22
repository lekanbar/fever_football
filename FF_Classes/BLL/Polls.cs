using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Polls
    {
        private Polls _LoadedItem;
        private string _ID;
        private string _Question;
        private string _Option1;
        private string _Option2;
        private string _Option3;
        private string _Other;
        private bool _Retired;
        private DateTime _Date;
        private List<Polls> _PollsCollection;

        #region
        public Polls LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Question
        {
            get { return _Question; }
            set { _Question = value; }
        }

        public string Option1
        {
            get { return _Option1; }
            set { _Option1 = value; }
        }

        public string Option2
        {
            get { return _Option2; }
            set { _Option2 = value; }
        }

        public string Option3
        {
            get { return _Option3; }
            set { _Option3 = value; }
        }

        public string Other
        {
            get { return _Other; }
            set { _Other = value; }
        }

        public bool Retired
        { 
            get { return _Retired; }
            set { _Retired = value; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public List<Polls> PollsCollection
        {
            get { return _PollsCollection; }
            set { _PollsCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Poll poll = GetPolls();

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Polls.InsertOnSubmit(poll);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var poll = db.FF_Polls.Single(u => u.ID == this.ID);

                if (poll != null)
                {
                    poll.Question = this.Question;
                    poll.Option1 = this.Option1;
                    poll.Option2 = this.Option2;
                    poll.Option3 = this.Option3;
                    poll.Other = this.Other;
                    poll.Date = this.Date;
                    poll.Retired = this.Retired;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Polls.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_Polls.DeleteOnSubmit(f);
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
                    var poll = (from e in db.FF_Polls
                                where e.ID == this.ID
                                select e).First();

                    if (poll != null)
                    {
                        LoadedItem = new Polls();
                        LoadedItem.ID = poll.ID;
                        LoadedItem.Question = poll.Question;
                        LoadedItem.Option1 = poll.Option1;
                        LoadedItem.Option2 = poll.Option2;
                        LoadedItem.Option3 = poll.Option3;
                        LoadedItem.Other = poll.Other;
                        LoadedItem.Date = poll.Date;
                        LoadedItem.Retired = poll.Retired;
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
                var polls = (from e in db.FF_Polls
                             where e.Retired == false
                             select e);

                PollsCollection = null;
                if (polls.Count() > 0)
                {
                    PollsCollection = new List<Polls>();
                    foreach (var poll in polls)
                    {
                        Polls Item = new Polls();
                        Item.ID = poll.ID;
                        Item.Question = poll.Question;
                        Item.Option1 = poll.Option1;
                        Item.Option2 = poll.Option2;
                        Item.Option3 = poll.Option3;
                        Item.Other = poll.Other;
                        Item.Date = poll.Date;
                        Item.Retired = poll.Retired;

                        PollsCollection.Add(Item);
                    }
                }
            }
        }


        public void GetAllForAdmin()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var polls = (from e in db.FF_Polls
                             select e);

                PollsCollection = null;
                if (polls.Count() > 0)
                {
                    PollsCollection = new List<Polls>();
                    foreach (var poll in polls)
                    {
                        Polls Item = new Polls();
                        Item.ID = poll.ID;
                        Item.Question = poll.Question;
                        Item.Option1 = poll.Option1;
                        Item.Option2 = poll.Option2;
                        Item.Option3 = poll.Option3;
                        Item.Other = poll.Other;
                        Item.Date = poll.Date;
                        Item.Retired = poll.Retired;

                        PollsCollection.Add(Item);
                    }
                }
            }
        }

        public FF_Poll GetPolls()
        {
            FF_Poll poll = new FF_Poll();
            poll.ID = this.ID;
            poll.Question = this.Question;
            poll.Option1 = this.Option1;
            poll.Option2 = this.Option2;
            poll.Option3 = this.Option3;
            poll.Other = this.Other;
            poll.Date = this.Date;
            poll.Retired = this.Retired;

            return poll;
        }
    }
}
