using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class Votes
    {
        private Votes _LoadedItem;
        private Int64 _ID;
        private string _Option;
        private string _QuestionID;
        private Int64 _Votes;
        private List<Votes> _VotesCollection;

        #region
        public Votes LoadedItem
        {
            get { return _LoadedItem; }
            set { _LoadedItem = value; }
        }

        public Int64 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Int64 VotesCount
        {
            get { return _Votes; }
            set { _Votes = value; }
        }

        public string Option
        {
            get { return _Option; }
            set { _Option = value; }
        }

        public string QuestionID
        {
            get { return _QuestionID; }
            set { _QuestionID = value; }
        }

        public List<Votes> VotesCollection
        {
            get { return _VotesCollection; }
            set { _VotesCollection = value; }
        }
        #endregion

        public void Add()
        {
            FF_Vote vote = new FF_Vote();
            vote.ID = this.ID;
            vote.Option = this.Option;
            vote.QuestionID = this.QuestionID;
            vote.Votes = this.VotesCount;

            using (var db = DatabaseHepler.GetDatabaseData())
            {
                db.FF_Votes.InsertOnSubmit(vote);

                db.SubmitChanges();
            }
        }

        public void Update()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var vote = db.FF_Votes.Single(u => u.QuestionID == this.QuestionID &&
                                                   u.Option == this.Option);

                if (vote != null)
                {
                    vote.Votes = this.VotesCount;

                    db.SubmitChanges();
                }
            }
        }

        public void Delete()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var f = db.FF_Votes.Single(u => u.ID == this.ID);

                if (f != null)
                {
                    db.FF_Votes.DeleteOnSubmit(f);
                    db.SubmitChanges();
                }
            }
        }

        public void Load()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var vote = (from e in db.FF_Votes
                            where e.QuestionID == this.QuestionID && e.Option == this.Option
                            select e).First();

                if (vote != null)
                {
                    LoadedItem = new Votes();
                    LoadedItem.ID = vote.ID;
                    LoadedItem.Option = vote.Option;
                    LoadedItem.QuestionID = vote.QuestionID;
                    LoadedItem.VotesCount = vote.Votes;
                }
                else
                    LoadedItem = null;
            }
        }

        public void GetAll()
        {
            using (var db = DatabaseHepler.GetDatabaseData())
            {
                var votes = (from e in db.FF_Votes
                             where e.QuestionID == this.QuestionID
                             select e);

                VotesCollection = null;
                if (votes.Count() > 0)
                {
                    VotesCollection = new List<Votes>();
                    foreach (var vote in votes)
                    {
                        Votes Item = new Votes();
                        Item = new Votes();
                        Item.ID = vote.ID;
                        Item.Option = vote.Option;
                        Item.QuestionID = vote.QuestionID;
                        Item.VotesCount = vote.Votes;

                        VotesCollection.Add(Item);
                    }
                }
            }
        }
    }
}
