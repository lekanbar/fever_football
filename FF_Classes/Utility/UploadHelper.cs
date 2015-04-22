using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace FF_Classes
{
    ///<summary>
    ///Class to facilitate all File Uploads to the server
    ///</summary>
    public class UploadHelper : System.Web.UI.Page
    {
        ///<summary>
        ///Method to facilitate upload of Pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        public static string UploadPicture(FileUpload file)
        {
            string FileExtension = Path.GetExtension(file.FileName);

            //Generate a unique ID for each file uploaded and generate logo URL
            Guid UniqueID = Guid.NewGuid();
            string Unique = UniqueID.ToString().Substring(0, 8);

            string URL = SavePicture(file, Unique);

            if (!string.IsNullOrEmpty(URL))
            {
                ImageManipulator manipulator = new ImageManipulator();
                bool IsResized = manipulator.ResizePicture(URL, URL, 580, 363);

                return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + FileExtension;
            }
            else
                return null;
        }

        ///<summary>
        ///Method to facilitate upload of Pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        public static string UploadGalleryPicture(FileUpload file)
        {
            string FileExtension = Path.GetExtension(file.FileName);

            //Generate a unique ID for each file uploaded and generate logo URL
            Guid UniqueID = Guid.NewGuid();
            string Unique = UniqueID.ToString().Substring(0, 8);

            string URL = SavePicture(file, Unique);

            if (!string.IsNullOrEmpty(URL))
            {
                ImageManipulator manipulator = new ImageManipulator();
                bool IsResized = manipulator.ResizePicture(URL, URL, 580, 363);

                string ThumbURL = SaveThumbNail(file, Unique);
                IsResized = manipulator.ResizePicture(ThumbURL, ThumbURL, 80, 50);

                return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + FileExtension;
            }
            else
                return null;
        }

        ///<summary>
        ///Method to facilitate saving Client's Gallery picture thumbnail
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        ///<param name="FileName">The name of the file to be saved</param>
        private static string SaveThumbNail(FileUpload file, string FileName)
        {
            if (file.FileName.Equals(""))
            {
                return null;// "No file specified.";
            }
            else
            {
                string FileExtension = Path.GetExtension(file.FileName);

                switch (FileExtension.ToLower())
                {
                    case ".png":
                        break;
                    case ".jpg":
                        break;
                    case ".gif":
                        break;
                    case ".jpeg":
                        break;
                    case ".bmp":
                        break;
                    default:
                        return null;// "File type not supported.";
                }

                string URL = FF_Classes.DomainList.ClientUploadPath() + @"ThumbNail\" + FileName + FileExtension;

                //Save the file or logo
                file.PostedFile.SaveAs(URL);

                return URL;
            }
        }

        ///<summary>
        ///Method to facilitate saving Client's pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        ///<param name="FileName">The name of the file to be saved</param>
        private static string SavePicture(FileUpload file, string FileName)
        {
            if (file.FileName.Equals(""))
            {
                return null;// "No file specified.";
            }
            else
            {
                string FileExtension = Path.GetExtension(file.FileName);

                switch (FileExtension.ToLower())
                {
                    case ".png":
                        break;
                    case ".jpg":
                        break;
                    case ".gif":
                        break;
                    case ".jpeg":
                        break;
                    case ".bmp":
                        break;
                    default:
                        return null;// "File type not supported.";
                }

                string URL = FF_Classes.DomainList.ClientUploadPath() + FileName + FileExtension;                
                
                //Save the file or logo
                file.PostedFile.SaveAs(URL);

                return URL;
            }
        }

        ///<summary>
        ///Method to facilitate upload of Pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        public static string UploadPicture(HttpPostedFile file)
        {
            string FileExtension = Path.GetExtension(file.FileName);

            //Generate a unique ID for each file uploaded and generate logo URL
            Guid UniqueID = Guid.NewGuid();
            string Unique = UniqueID.ToString().Substring(0, 8);

            string URL = SavePicture(file, Unique);

            if (!string.IsNullOrEmpty(URL))
            {
                ImageManipulator manipulator = new ImageManipulator();
                bool IsResized = manipulator.ResizePicture(URL, URL, 580, 363);

                return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + FileExtension;
            }
            else
                return null;
        }

        ///<summary>
        ///Method to facilitate upload of Pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        public static string UploadGalleryPicture(HttpPostedFile file)
        {
            string FileExtension = Path.GetExtension(file.FileName);

            //Generate a unique ID for each file uploaded and generate logo URL
            Guid UniqueID = Guid.NewGuid();
            string Unique = UniqueID.ToString().Substring(0, 8);

            string URL = SavePicture(file, Unique);

            if (!string.IsNullOrEmpty(URL))
            {
                string ThumbURL = FF_Classes.DomainList.ClientUploadPath() + @"\ThumbNail\" + Unique + FileExtension;
                File.Copy(URL, ThumbURL);
                ImageManipulator manipulator = new ImageManipulator();
                ImageManipulator manipulator2 = new ImageManipulator();
                bool IsResized = manipulator2.ResizePicture(ThumbURL, ThumbURL, 80, 50);
                IsResized = manipulator.ResizePicture(URL, URL, 580, 363);
                file.InputStream.Dispose();

                return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + FileExtension;
            }
            else
                return null;
        }

        ///<summary>
        ///Method to facilitate saving Client's Gallery picture thumbnail
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="FileName">The name of the file to be saved</param>
        private static string SaveThumbNail(HttpPostedFile file, string FileName)
        {
            if (file.FileName.Equals(""))
            {
                return null;// "No file specified.";
            }
            else
            {
                string FileExtension = Path.GetExtension(file.FileName);

                switch (FileExtension.ToLower())
                {
                    case ".png":
                        break;
                    case ".jpg":
                        break;
                    case ".gif":
                        break;
                    case ".jpeg":
                        break;
                    case ".bmp":
                        break;
                    default:
                        return null;// "File type not supported.";
                }

                string URL = FF_Classes.DomainList.ClientUploadPath() + @"\ThumbNail\" + FileName + FileExtension;

                //Save the file or logo
                file.SaveAs(URL);
                file.InputStream.Close();
                return URL;
            }
        }

        ///<summary>
        ///Method to facilitate saving Client's pictures
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        ///<param name="UserID">The User's Identification</param>
        ///<param name="FileName">The name of the file to be saved</param>
        private static string SavePicture(HttpPostedFile file, string FileName)
        {
            if (file.FileName.Equals(""))
            {
                return null;// "No file specified.";
            }
            else
            {
                string FileExtension = Path.GetExtension(file.FileName);

                switch (FileExtension.ToLower())
                {
                    case ".png":
                        break;
                    case ".jpg":
                        break;
                    case ".gif":
                        break;
                    case ".jpeg":
                        break;
                    case ".bmp":
                        break;
                    default:
                        return null;// "File type not supported.";
                }

                string URL = FF_Classes.DomainList.ClientUploadPath() + FileName + FileExtension;

                //Save the file or logo
                file.SaveAs(URL);

                return URL;
            }
        }

        ///<summary>
        ///Method to facilitate upload various files
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        public static string UploadFiles(FileUpload file, string[] FileTypes, bool IsClient)
        {
            if (file.FileName.Equals(""))
            {
                return "No file specified.";
            }
            else
            {
                bool fileOkay = false;

                foreach (string fileType in FileTypes)
                {
                    if (Path.GetExtension(file.FileName).Equals(fileType))
                    {
                        fileOkay = true;
                        break;
                    }
                    else if (fileType.Equals("*"))
                    {
                        fileOkay = true;
                        break;
                    }
                }

                if (fileOkay)
                {
                    //Upload the logo
                    //Generate a unique ID for each file uploaded and generate logo URL
                    Guid UniqueID = Guid.NewGuid();
                    string Unique = UniqueID.ToString().Substring(0, 8);
                    string URL = "";

                    URL = FF_Classes.DomainList.ClientUploadPath() + Unique + Path.GetExtension(file.FileName);

                    file.PostedFile.SaveAs(URL);

                    return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + Path.GetExtension(file.FileName);
                    
                }
                else
                {
                    return "File type not supported.";
                }
            }
        }

        ///<summary>
        ///Method to facilitate upload various files
        ///</summary>
        ///<param name="file">The file to be uploaded</param>
        public static string UploadFiles(HttpPostedFile file, string[] FileTypes, bool IsClient)
        {
            if (file.FileName.Equals(""))
            {
                return null; //"No file specified.";
            }
            else
            {
                bool fileOkay = false;

                foreach (string fileType in FileTypes)
                {
                    if (Path.GetExtension(file.FileName).Equals(fileType))
                    {
                        fileOkay = true;
                        break;
                    }
                    else if (fileType.Equals("*"))
                    {
                        fileOkay = true;
                        break;
                    }
                }

                if (fileOkay)
                {
                    //Upload the logo
                    //Generate a unique ID for each file uploaded and generate logo URL
                    Guid UniqueID = Guid.NewGuid();
                    string Unique = UniqueID.ToString().Substring(0, 8);
                    string URL = "";

                    URL = FF_Classes.DomainList.ClientUploadPath() + Unique + Path.GetExtension(file.FileName);

                    file.SaveAs(URL);

                    return FF_Classes.DomainList.ClientSiteURL() + "/Uploads/" + Unique + Path.GetExtension(file.FileName);
                    
                }
                else
                {
                    return null;// "File type not supported.";
                }
            }
        }
    }
}