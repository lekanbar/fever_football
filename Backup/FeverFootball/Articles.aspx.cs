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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            loadArticles();
            loadNews();
            loadPolls();
            Session["QuestionID"] = null;
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

    private void loadNews()
    {
        News item = new News();
        item.GetAll();

        if (item.NewsCollection != null)
        {
            GVNews.DataSource = item.NewsCollection;
            GVNews.DataBind();
        }
    }

    private void loadPolls()
    {

        Polls poll = new Polls();
        poll.GetAll();

        Random ran = new Random();
        int dexin = ran.Next(0, poll.PollsCollection.Count);

        poll = poll.PollsCollection.ElementAt(dexin);

        while (CheckCookie(poll.ID))
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


    protected string shortner(string input, int length)
    {
        if (input.Length > length)
            return input.PadRight(length, ' ').Substring(0, length) + " ...";
        else return input;
    }

}
