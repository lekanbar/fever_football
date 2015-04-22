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


public partial class EPl : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] == null )
        {
            Response.Redirect("SAPL.aspx");
        }
        else
        {
            if (Misc.ValidateGuid(Request.QueryString["id"]) == false)
                Response.Redirect("SAPL.aspx");
        }

        if (!Page.IsPostBack)
        {
            loadMain();
            loadLeagueTable();
            loadTopScorer();
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


    private void loadMain()
    {
        News item = new News();
        item.NewsID = new Guid(Request.QueryString["id"]);
        item.Load();

        if (item.LoadedItem != null)
        {
            item = item.LoadedItem;
            Image1.ImageUrl = item.ImageURL;
            lblTitle.Text = item.Title.ToUpper() ;
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

    private void loadLeagueTable()
    {
        LeagueTables item = new LeagueTables();
        item.LeagueID = 6;
        item.SeasonID = Season.GetCurrentSeason();
        item.GetAll();

        if (item.TableCollection != null)
        {
            GVLeagueTable.DataSource = item.TableCollection;
            GVLeagueTable.DataBind();
        }
    }

    private void loadTopScorer()
    {
        TopScorers item = new TopScorers();
        item.LeagueID = 6;
        item.SeasonID = Season.GetCurrentSeason();
        item.GetCurrentTopScorer();

        if (item.LoadedItem != null)
        {
            item = item.LoadedItem;
            imgScorer.ImageUrl = item.ImageURL;
            lblName.Text = item.Name;
            lblGoals.Text = item.Goals.ToString();
            lblScorerDetails.Text = item.Details;
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Comments item = new Comments();
        string CommentID = Guid.NewGuid().ToString().Substring(0,8);

        item.ID = CommentID;
        item.AppID = new Guid(Request.QueryString["id"]);
        item.Date = DateTime.Now;
        item.Details = txtComment.Text;
        item.Add();

        if (Session["UserID"] != null)
        {
            UserComment item2 = new UserComment();
            item2.CommentID = CommentID;
            item2.UserID = new Guid(Session["UserID"].ToString());
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

    private string getUsername(Guid UserID)
    {
        UserBase item = new UserBase();
        item.UserID = UserID;
        item.Load();

        return item.LoadedItem.UserName;
    }
}
