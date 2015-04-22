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
            return "http://localhost:3157/FeverForum";
        }

        public static string HomeSiteURL()
        {
            return "http://feverforum.com";
        }

        public static string ClientUploadPath()
        {
            return HttpContext.Current.Server.MapPath("~/Uploads/");
            //return @"C:\Users\Lekan\Documents\Visual Studio 2008\Websites\ImoHealthAdmin\Uploads\";
        }
    }

}
