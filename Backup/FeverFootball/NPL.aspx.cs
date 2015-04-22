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
        if (!Page.IsPostBack)
        {
            loadMain();
            loadNews();
            loadLeagueTable();
            loadTopScorer();
        }
    }


    private void loadMain()
    {
        News item = new News();
        item.LeagueID = 5;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            Image1.ImageUrl = item.NewsCollection[0].ImageURL;
            lblTitle.Text = item.NewsCollection[0].Title.ToUpper() ;
            lblDetails.Text = shortner( item.NewsCollection[0].Details, 300);
            lnkMain.NavigateUrl = "NPLDetail.aspx?id=" + item.NewsCollection[0].NewsID.ToString();
        }
    }

    private void loadNews()
    {
        News item = new News();
        item.LeagueID = 5;
        item.GetLeagueNews();

        if (item.NewsCollection != null)
        {
            item.NewsCollection.RemoveAt(0);
            GVNews.DataSource = item.NewsCollection;
            GVNews.DataBind();
        }
    }

    private void loadLeagueTable()
    {
        LeagueTables item = new LeagueTables();
        item.LeagueID = 5;
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
        item.LeagueID = 5;
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

    protected string shortner(string input, int length)
    {
        if (input.Length > length)
            return input.PadRight(length, ' ').Substring(0, length) + " ...";
        else return input;
    }

    protected void GVNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVNews.PageIndex = e.NewPageIndex;
        loadNews();
    }
}
