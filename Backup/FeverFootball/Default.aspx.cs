using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using FF_Classes;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadSlideShow();
            loadC1();
            loadC2();
            loadC3();
            loadC4();
            loadC5();
            loadC6();
            loadArticles();
            loadPolls();
        }
    
    }


    private void loadSlideShow()
    {
        News item = new News();
        item.GetAll();

        if (item != null)
        {             
            List<News> items = new List<News>();
            items = item.NewsCollection;

            for (int i = 0; i < 4; i++)
            {
                if (i == 0)
                {
                    GImage1.ImageUrl = items[i].ImageURL;
                    GTImage1.ImageUrl = getThumbnail(items[i].ImageURL);
                    GTLabel1.Text = shortner(items[i].Title, 50);
                    GCLabel1.Text = shortner(items[i].Title, 35);
                    GDLabel1.Text = shortner(items[i].Details, 108); 
                    GDHyperLink1.NavigateUrl = Misc.getURL( items[i].LeagueID.Value, items[i].NewsID);
                }
                if (i == 1)
                {
                    GImage2.ImageUrl = items[i].ImageURL;
                    GTImage2.ImageUrl = getThumbnail(items[i].ImageURL);
                    GTLabel2.Text = shortner(items[i].Title, 50);
                    GCLabel2.Text = shortner(items[i].Title, 35);
                    GDLabel2.Text = shortner(items[i].Details, 108);
                    GDHyperLink2.NavigateUrl = Misc.getURL(items[i].LeagueID.Value, items[i].NewsID);
                }
                if (i == 2)
                {
                    GImage3.ImageUrl = items[i].ImageURL;
                    GTImage3.ImageUrl = getThumbnail(items[i].ImageURL);
                    GTLabel3.Text = shortner(items[i].Title, 50);
                    GCLabel3.Text = shortner(items[i].Title, 35);
                    GDLabel3.Text = shortner(items[i].Details, 108);
                    GDHyperLink3.NavigateUrl = Misc.getURL(items[i].LeagueID.Value, items[i].NewsID);
                }
                if (i == 3)
                {
                    GImage4.ImageUrl = items[i].ImageURL;
                    GTImage4.ImageUrl = getThumbnail(items[i].ImageURL);
                    GTLabel4.Text = shortner( items[i].Title,50);
                    GCLabel4.Text = shortner( items[i].Title, 35);
                    GDLabel4.Text = shortner(items[i].Details, 108);
                    GDHyperLink4.NavigateUrl = Misc.getURL(items[i].LeagueID.Value, items[i].NewsID);
                }
            }
        }
    }

    private void loadC1()
    {
        News item = new News();
        item.LeagueID = 1;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C1Image.ImageUrl = items[0].ImageURL;
            C1LabelTitle.Text = items[0].Title;
            C1LabelDetails.Text = shortner(items[0].Details, 150);
            C1Recommend.NavigateUrl = "EPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C1HyperLink1.Text = items[1].Title;
                C1HyperLink1.NavigateUrl = "EPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C1HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C1HyperLink2.Text = items[2].Title;
                C1HyperLink2.NavigateUrl = "EPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C1HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C1HyperLink3.Text = items[3].Title;
                C1HyperLink3.NavigateUrl = "EPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C1HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C1HyperLink4.Text = items[4].Title;
                C1HyperLink4.NavigateUrl = "EPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C1HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C1HyperLink5.Text = items[5].Title;
                C1HyperLink5.NavigateUrl = "EPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C1HyperLink5.Visible = false;
        }
    }

    private void loadC2()
    {
        News item = new News();
        item.LeagueID = 2;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C2Image.ImageUrl = items[0].ImageURL;
            C2LabelTitle.Text = items[0].Title;
            C2LabelDetails.Text = shortner(items[0].Details, 150);
            C2Recommend.NavigateUrl = "SPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C2HyperLink2.Text = items[1].Title;
                C2HyperLink2.NavigateUrl = "SPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C2HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C2HyperLink2.Text = items[2].Title;
                C2HyperLink2.NavigateUrl = "SPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C1HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C2HyperLink3.Text = items[3].Title;
                C2HyperLink3.NavigateUrl = "SPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C2HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C2HyperLink4.Text = items[4].Title;
                C2HyperLink4.NavigateUrl = "SPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C2HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C2HyperLink5.Text = items[5].Title;
                C2HyperLink5.NavigateUrl = "SPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C2HyperLink5.Visible = false;
        }
    }

    private void loadC3()
    {
        News item = new News();
        item.LeagueID = 3;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C3Image.ImageUrl = items[0].ImageURL;
            C3LabelTitle.Text = items[0].Title;
            C3LabelDetails.Text = shortner(items[0].Details, 150);
            C3Recommend.NavigateUrl = "IPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C3HyperLink2.Text = items[1].Title;
                C3HyperLink2.NavigateUrl = "IPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C3HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C3HyperLink2.Text = items[2].Title;
                C3HyperLink2.NavigateUrl = "IPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C3HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C3HyperLink3.Text = items[3].Title;
                C3HyperLink3.NavigateUrl = "IPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C3HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C3HyperLink4.Text = items[4].Title;
                C3HyperLink4.NavigateUrl = "IPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C3HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C3HyperLink5.Text = items[5].Title;
                C3HyperLink5.NavigateUrl = "IPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C3HyperLink5.Visible = false;
        }
    }

    private void loadC4()
    {
        News item = new News();
        item.LeagueID = 4;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C4Image.ImageUrl = items[0].ImageURL;
            C4LabelTitle.Text = items[0].Title;
            C4LabelDetails.Text = shortner(items[0].Details, 150);
            C4Recommend.NavigateUrl = "IPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C4HyperLink2.Text = items[1].Title;
                C4HyperLink2.NavigateUrl = "IPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C4HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C4HyperLink2.Text = items[2].Title;
                C4HyperLink2.NavigateUrl = "IPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C4HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C4HyperLink3.Text = items[3].Title;
                C4HyperLink3.NavigateUrl = "IPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C4HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C4HyperLink4.Text = items[4].Title;
                C4HyperLink4.NavigateUrl = "IPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C4HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C4HyperLink5.Text = items[5].Title;
                C4HyperLink5.NavigateUrl = "IPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C4HyperLink5.Visible = false;
        }
    }

    private void loadC5()
    {
        News item = new News();
        item.LeagueID = 5;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C5Image.ImageUrl = items[0].ImageURL;
            C5LabelTitle.Text = items[0].Title;
            C5LabelDetails.Text = shortner(items[0].Details, 150);
            C5Recommend.NavigateUrl = "NPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C5HyperLink2.Text = items[1].Title;
                C5HyperLink2.NavigateUrl = "NPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C5HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C5HyperLink2.Text = items[2].Title;
                C5HyperLink2.NavigateUrl = "NPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C5HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C5HyperLink3.Text = items[3].Title;
                C5HyperLink3.NavigateUrl = "NPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C5HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C5HyperLink4.Text = items[4].Title;
                C5HyperLink4.NavigateUrl = "NPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C5HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C5HyperLink5.Text = items[5].Title;
                C5HyperLink5.NavigateUrl = "NPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C5HyperLink5.Visible = false;
        }
    }

    private void loadC6()
    {
        News item = new News();
        item.LeagueID = 6;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            List<News> items = new List<News>();
            items = item.NewsCollection;

            C6Image.ImageUrl = items[0].ImageURL;
            C6LabelTitle.Text = items[0].Title;
            C6LabelDetails.Text = shortner(items[0].Details, 150);
            C6Recommend.NavigateUrl = "SAPLDetail.aspx?id=" + items[0].NewsID.ToString();

            if (items.Count >= 2)
            {
                C6HyperLink2.Text = items[1].Title;
                C6HyperLink2.NavigateUrl = "SAPLDetail.aspx?id=" + items[1].NewsID.ToString();
            }
            else C6HyperLink1.Visible = false;


            if (items.Count >= 3)
            {
                C6HyperLink2.Text = items[2].Title;
                C6HyperLink2.NavigateUrl = "SAPLDetail.aspx?id=" + items[2].NewsID.ToString();
            }
            else C6HyperLink2.Visible = false;

            if (items.Count >= 4)
            {
                C6HyperLink3.Text = items[3].Title;
                C6HyperLink3.NavigateUrl = "SAPLDetail.aspx?id=" + items[3].NewsID.ToString();
            }
            else C6HyperLink3.Visible = false;

            if (items.Count >= 5)
            {
                C6HyperLink4.Text = items[4].Title;
                C6HyperLink4.NavigateUrl = "SAPLDetail.aspx?id=" + items[4].NewsID.ToString();
            }
            else C6HyperLink4.Visible = false;

            if (items.Count >= 6)
            {
                C6HyperLink5.Text = items[5].Title;
                C6HyperLink5.NavigateUrl = "SAPLDetail.aspx?id=" + items[5].NewsID.ToString();
            }
            else C6HyperLink5.Visible = false;
        }
    }



    private void loadArticles()
    {
        Article item = new Article();
        item.GetAll();

        if (item.ArticleCollection != null)
        {
            GVArticles.DataSource = item.ArticleCollection;
            GVArticles.DataBind();
        }
    }

    private void loadPolls()
    {
        
        Polls poll = new Polls();
        poll.GetAll();

        Random ran = new Random();
        int dexin = ran.Next(0, poll.PollsCollection.Count);

        poll = poll.PollsCollection.ElementAt(dexin);

        while( CheckCookie(poll.ID))
        {
            dexin = ran.Next(0, poll.PollsCollection.Count);
            poll = poll.PollsCollection.ElementAt(dexin);        
        }

        lblQuestion.Text = poll.Question;
        RadioButtonList1.Items.Add(poll.Option1);
        RadioButtonList1.Items.Add(poll.Option2);
        RadioButtonList1.Items.Add(poll.Option3);
        Session["QuestionID"] = poll.ID;
    }

    public bool CheckCookie(string QuestionID)
    {
        HttpCookie cookie = Request.Cookies["rec_ff_ques"];

        if (cookie == null)
        {
            cookie = new HttpCookie("rec_ff_ques");
            cookie["rec_ff_id"] = QuestionID;
            cookie.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(cookie);

            return false;
        }
        else
        {
            if (cookie["rec_ff_id"].Equals(QuestionID))
                return true;
            else
                return false;
        }
    }

    protected void BtnPollSubmit_Click(object sender, EventArgs e)
    {
        bool CookieExist = CheckCookie(Session["QuestionID"].ToString());

        if (!CookieExist)
        {
            lblResult.Visible = true;
            Votes vote = new Votes();
            vote.QuestionID = Session["QuestionID"].ToString();
            vote.Option = RadioButtonList1.SelectedItem.Text;
            vote.Load();
            vote = vote.LoadedItem;

            vote.VotesCount += 1;
            vote.Update();

            vote.GetAll();

            BulletedList1.Items.Clear();
            foreach (Votes v in vote.VotesCollection)
            {
                BulletedList1.Items.Add(v.Option + " -- " + v.VotesCount + " votes");
            }

            RadioButtonList1.Items.Clear();
            loadPolls();
        }
        else
        {
            lblResult.Text = "You have previously answered this question!";
            loadPolls();
        }
    }

    private string shortner (string input, int length )
    {
        if (input.Length > length)
            return input.PadRight(length, ' ').Substring(0, length) + " ...";
        else return input;
    }

    private string getThumbnail(string path)
    {
        return path.Insert(path.IndexOf("uploads/")+8, "thumbnail/");
    }

    
}
