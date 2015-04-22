using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using System.IO;

namespace FF_Classes
{
    public class FileHelper
    {
        public static bool DeleteFile(string _FilePath)
        {
            string FileName = _FilePath.Substring(_FilePath.LastIndexOf("/") + 1);
            string FilePath = HttpContext.Current.Server.MapPath("~/Uploads/" + FileName);

            try
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public static bool DeleteFile(string _FilePath, bool HasThumbNail)
        {
            string FileName = _FilePath.Substring(_FilePath.LastIndexOf("/") + 1);
            string FilePath = HttpContext.Current.Server.MapPath("~/Uploads/" + FileName);

            try
            {
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);

                    if (HasThumbNail)
                    {
                        FilePath = FilePath.Insert(FilePath.LastIndexOf("\\"), "\\ThumbNail");
                        if (File.Exists(FilePath))
                        {
                            File.Delete(FilePath);
                        }
                    }

                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public static bool DeleteMultipleFiles(string[] FilePaths)
        {
            bool IsValid = false;
            foreach (string _FilePath in FilePaths)
            {
                string FileName = _FilePath.Substring(_FilePath.LastIndexOf("/") + 1);
                string FilePath = HttpContext.Current.Server.MapPath("~/Uploads/" + FileName);

                try
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);

                        IsValid = true;
                    }
                    else
                        IsValid = false;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                    continue;
                }
            }
            return IsValid;
        }

        public static bool DeleteMultipleFiles(string[] FilePaths, bool HasThumbNail)
        {
            bool IsValid = false;
            foreach (string _FilePath in FilePaths)
            {
                string FileName = _FilePath.Substring(_FilePath.LastIndexOf("/") + 1);
                string FilePath = HttpContext.Current.Server.MapPath("~/Uploads/" + FileName);

                try
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);

                        if (HasThumbNail)
                        {
                            FilePath = FilePath.Insert(FilePath.LastIndexOf("\\"), "\\ThumbNail");
                            if (File.Exists(FilePath))
                            {
                                File.Delete(FilePath);
                            }
                        }

                        IsValid = true;
                    }
                    else
                        IsValid = false;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                    continue;
                }
            }
            return IsValid;
        }
    }
}