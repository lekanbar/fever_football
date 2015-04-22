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
        if (Request.QueryString["id"] == null)
        {
            Response.Redirect("Articles.aspx");
        }
        else
        {
            if (Misc.ValidateGuid(Request.QueryString["id"]) == false)
                Response.Redirect("Articles.aspx");
        }

        if (!Page.IsPostBack)
        {
            loadArticle();
            loadNews();
            loadPolls();
            Session["QuestionID"] = null;
            loadComments();
            if (Session["UserID"] != null)
            {
                pnlUser1.Visible = true;
                pnlUser2.Visible = false;
                btnSubmit.ValidationGroup = "c2";
            }
            else
            {
                pnlUser1.Visible = false;
                pnlUser2.Visible = true;
                btnSubmit.ValidationGroup = "c1";
            }
        }
    
    }

    private void loadArticle()
    {
        Article item = new Article();
        item.ArticleID = new Guid(Request.QueryString["id"]);
        item.Load();

        if (item.LoadedItem != null)
        {
            item = item.LoadedItem;
            ImageArticle.ImageUrl = item.ImageURL;
            lblTitle.Text = item.Title.ToUpper();
            lblDetails.Text = item.Details;
        }
    }

    private void loadComments()
    {
        Comments item = new Comments();
        item.AppID = new Guid(Request.QueryString["id"]);
        item.GetAll();

        if (item.CommentCollection != null)
        {
            lblCommentCount.Text = item.CommentCollection.Count.ToString();

            GVComments.DataSource = item.CommentCollection;
            GVComments.DataBind();
        }
        else lblCommentCount.Text = "0";
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


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Comments item = new Comments();
        string CommentID = Guid.NewGuid().ToString().Substring(0, 8);

        item.ID = CommentID;
        item.AppID = new Guid(Request.QueryString["id"]);
        item.Date = DateTime.Now;
        item.Details = txtComment.Text;

        if (Session["UserID"] != null)
        {
            UserComment item2 = new UserComment();
            item2.CommentID = CommentID;
            item2.UserID = int.Parse(Session["UserID"].ToString());
            item2.Add();
        }
        else
        {
            UserComment2 item3 = new UserComment2();
            item3.Name = txtName.Text;
            item3.Email = txtEmailAddress.Text;
            item3.CommentID = CommentID;
            item3.Add();
        }

        txtName.Text = "";
        txtEmailAddress.Text = "";
        txtComment.Text = "";
        lblMsg.Text = "Comment Added!";
        loadComments();

    }

    protected string getAuthor(string CommentID)
    {
        UserComment item1 = new UserComment();
        item1.CommentID = CommentID;
        item1.LoadByComment();

        if (item1.LoadedItem != null)
            return getUsername(item1.LoadedItem.UserID);
        else
        {
            UserComment2 item2 = new UserComment2();
            item2.CommentID = CommentID;
            item2.LoadByComment();

            if (item2.LoadedItem != null)
                return item2.LoadedItem.Name;

            else return "Somebody";
        }
    }

    private string getUsername(int UserID)
    {
        return YAF.Core.UserMembershipHelper.GetDisplayNameFromID(UserID);
    }
}
