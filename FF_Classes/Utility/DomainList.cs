using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF_Classes
{
    public class DomainList
    {
        public static string ClientSiteURL()
        {
            //return "http://localhost:55734/FeverFootball";
            return "http://feverfootball.com";
        }

        public static string HomeSiteURL()
        {
            return "http://feverforum.com";
        }
        public static string ForumURL()
        {
            //return "http://localhost:3157/FeverForum/";
            return "http://feverforum.net";
        }

        public static string ClientUploadPath()
        {
            return HttpContext.Current.Server.MapPath("~/Uploads/");
            //return @"C:\Users\Lekan\Documents\Visual Studio 2008\Websites\ImoHealthAdmin\Uploads\";
        }
    }

}
