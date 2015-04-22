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

public partial class CGEN : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LeagueTables item = new LeagueTables();
        Random ran = new Random();

        for (int i = 2; i < 8; i++)
        {
            Team tm = new Team();
            tm.LeagueID = i;
            tm.GetAllByLeagueID();
            foreach  (Team t in tm.TeamsCollection )
            {
                item.TeamName = t.Name;
                item.TeamID = t.TeamID;
                item.TeamLogo = t.LogoURL;
                item.LeagueID = i;
                item.SeasonID = new Guid("eb942e1c-a978-40d3-89d6-869097683814");
                item.P = 10;
                item.W = ran.Next(1, 10);
                item.D = ran.Next(1, 10);
                item.L = 10 - (item.W + item.D);
                item.F = ran.Next(0, 20);
                item.A = ran.Next(5, 15);
                item.Points = (item.W * 3) + item.D;
                item.Add();
            }
        }
    }

    private void loadTeams()
    { 
        Team item = new Team();
        for (int i=2; i<8; i++)
        {
            for (int j = 1; j < 6; j++)
            {
                item.Name = "Team" + j.ToString();
                item.TeamID = Guid.NewGuid();
                item.LeagueID = i;
                item.LogoURL = "~/uploads/icons/team" + j.ToString() + ".jpg";
                item.Add();
            }
        }
    
    }

    private void loadNews()
    {
        News item = new News();

        for (int i = 1; i < 8; i++)
        {
            item.NewsID = Guid.NewGuid();
            item.Title = "Pulvinar, augue porttitor vut ac nunc, pid odio nisi?";
            item.Details =
                "Non, augue mus, a elementum! Lorem placerat? Ut! Ultrices adipiscing ac! Et amet et odio sit dolor nisi sit risus! Sit ut pellentesque. Aenean, magnis! Enim amet eu enim, vut sagittis elementum, nunc turpis duis. Enim natoque, adipiscing? Cursus sociis nunc ac nec vel, aliquet est sed. Massa et placerat a ac arcu? Scelerisque urna, cursus ac, rhoncus et, tempor. Cum, nec turpis et sed odio mattis, pulvinar? Lundium aliquam et? Quis montes pid a, parturient, pellentesque! Dapibus risus pulvinar aliquam, odio augue augue in turpis magna vel placerat, mattis placerat dictumst hac. Tincidunt platea, vut augue, in et! Penatibus vut! Nunc aliquet. Vel, enim enim? Ut platea odio, nascetur nunc, in ac aliquet integer elementum mattis arcu diam.";

            item.ImageURL = "~/uploads/image" + i.ToString() + ".jpg";
            item.LeagueID = i;
            item.SeasonID = new Guid("eb942e1c-a978-40d3-89d6-869097683814");

            Random ran = new Random();
            item.Date = DateTime.Now.AddDays(ran.Next(0, 20));
            item.Add();
        }
    }
}
