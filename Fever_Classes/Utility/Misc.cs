using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace FF_Classes
{
    public class Misc
    {
        public static bool ValidateGuid(string StringToValidate)
        {
            try
            {
                Guid Val = new Guid(StringToValidate);

                return true;
            }
            catch (FormatException fex)
            {
                return false;
            }

        }

        public static string GetThumbNail(string ImageURL)
        {
            int position = ImageURL.IndexOf("ClientFiles") + 49;
            string newURL = ImageURL.Insert(position, "ThumbNail/");
            return newURL;
        }

        public static string GetSiteThumbNail(string ImageURL)
        {
            string newURL = ImageURL.Insert(ImageURL.LastIndexOf('/'), "/ThumbNail/");
            return newURL;
        }

        public static string GetVideoThumbNail(string SourceURL, bool BigSize)
        {
            Match regexMatch = Regex.Match(SourceURL, "^(http://www.youtube.com/v/){1}(.{11}).*",
                        RegexOptions.IgnoreCase);
            if (regexMatch.Success)
            {
                if (BigSize)
                    return "http://img.youtube.com/vi/" + regexMatch.Groups[2].Value + "/0.jpg";
                else
                    return "http://img.youtube.com/vi/" + regexMatch.Groups[2].Value + "/2.jpg";
            }
            else return "~/images/noimage.jpg";
        }

        public static string HTMLDecode(string input)
        {
            StringBuilder result = new StringBuilder(input);

            //plain tags

            //italics
            result.Replace("&lt;i&gt;", "<i>");
            result.Replace("&lt;/i&gt;", "");
            //bold
            result.Replace("&lt;b&gt;", "<b>");
            result.Replace("&lt;/b&gt;", "</b>");
            //underline
            result.Replace("&lt;u&gt;", "<u>");
            result.Replace("&lt;/u&gt;", "</u>");
            //strikethrough
            result.Replace("&lt;strike&gt;", "<strike>");
            result.Replace("&lt;/strike&gt;", "</strike>");
            //quote
            result.Replace("&lt;q&gt;", "<q>");
            result.Replace("&lt;/q&gt;", "</q>");
            //paragraph
            result.Replace("&lt;p&gt;", "<p>");
            result.Replace("&lt;/p&gt;", "");
            //div
            result.Replace("&lt;div&gt;", "<div>");
            result.Replace("&lt;/div&gt;", "</div>");
            //span
            result.Replace("&lt;span&gt;", "<span>");
            result.Replace("&lt;/span&gt;", "</span>");
            //unordered list
            result.Replace("&lt;ul&gt;", "<ul>");
            result.Replace("&lt;/ul&gt;", "</ul>");
            //ordered list
            result.Replace("&lt;ol&gt;", "<ol>");
            result.Replace("&lt;/ol&gt;", "</ol>");
            //list item
            result.Replace("&lt;li&gt;", "<li>");
            result.Replace("&lt;/li&gt;", "</li>");



            //misc

            //space 
            result.Replace("&amp;nbsp;", "&nbsp;");


            //styled tags
            string finalResult = result.ToString(); 
            //string space = "(\\s)*"; //optional whitespace
            //string quote = "[\"\']{1}"; //quotes
            //string openingTag=  "^.*" //random text
            //                    + "(&lt;){1}(a|i|b|u|strike|ul|li|span|div){1}" // allowed tags
            //                    + space
            //                    + "(" //new expr
            //                    + "(style" + space + "=" + space + quote //style tag opens
            //                    + "[^()]*" //content no bracket - script protection
            //                    + quote + ")" //close style tag
            //                    + "|"
            //                    + "(href" + space + "=" + space + quote //href tag opens
            //                    + "[^()]*" //content no bracket - script protection
            //                    + quote + ")" //close href tag
            //                    + ")*" //end new expr
            //                    + space
            //                    + "(&gt;){1}";
            //string closingTag=  "(&lt;){1}/(a|i|b|u|strike|ul|li|span|div){1}(&gt;){1}"  //end tags
            //                    + ".*" ; //more random text
            //Match regexMatch = Regex.Match(SourceURL, openingTag
            //                    RegexOptions.IgnoreCase);
            //if (regexMatch.Success)
            //{
            //    Regex.Replace(finalResult,openingTag,
            //}
            return finalResult; 

        }

        public static bool AuthenticateUser()
        {
            if (HttpContext.Current.Session["UserID"] == null)
            {
                return false;
            }
            return true;
        }

        public static string getURL(int league, Guid ItemID)
        {
            string path = "";

            if (league == 1)
            {
                path = "~/EPLDetail.aspx";
            }
            else if (league == 2)
            {
                path = "~/SPLDetail.aspx";
            }
            else if (league == 3)
            {
                path = "~/IPLDetail.aspx";
            }
            else if (league == 4)
            {
                path = "~/GPLDetail.aspx";
            }
            else if (league == 5)
            {
                path = "~/NPLDetail.aspx";
            }
            else if (league == 6)
            {
                path = "~/SAPLDetail.aspx";
            }
            else if (league == 7)
            {
                path = "~/CLDetail.aspx";
            }
            
            return path + "?id=" + ItemID;
        }
    }
}
