using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

/// <summary>
/// Summary description for Class1
/// </summary>
public class ImageManipulator
{
	public ImageManipulator()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public  bool ResizePicture(string path, string outputPath, int width, int height)
    {
        Size size  =  new Size(width , height);
        Image image = resizeImage(path, size);
        string realPath = outputPath;
        //string realPath = HttpContext.Current.Server.MapPath(outputPath);
        Bitmap img = new Bitmap(image);
        saveJpeg(realPath, img, 100);
        img.Dispose();
        image.Dispose();
        return true;
    }

    public bool ResizePicture(string path, string outputPath, int width, int height, bool IsFavicon)
    {
        Size size = new Size(width, height);
        Image image = resizeImage(path, size, true);
        string realPath = outputPath;
        Bitmap img = new Bitmap(image);
        saveJpeg(realPath, img, 100);
        img.Dispose();
        image.Dispose();
        return true;
    }

    private static Image resizeImage(string Path, Size size, bool IsFav)
    {
        Image imgToResize;
        string RealPath = Path;

        if (!File.Exists(RealPath))
            throw new Exception("Invalid image path");
        else imgToResize = Image.FromFile(RealPath);

        int sourceWidth = size.Width;
        int sourceHeight = size.Height;

        Bitmap b = new Bitmap(sourceWidth, sourceHeight);
        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, sourceWidth, sourceHeight);
        g.Dispose();
        imgToResize.Dispose();

        return (Image)b;
    }

    private static Image resizeImage(string Path, Size size)
    {

        Image imgToResize;
        string RealPath = Path;
        //string RealPath = HttpContext.Current.Server.MapPath(Path);
        if (!File.Exists(RealPath))
            throw new Exception("Invalid image path");
        else imgToResize = Image.FromFile(RealPath);
        int sourceWidth = imgToResize.Width;
        int sourceHeight = imgToResize.Height;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)size.Width / (float)sourceWidth);
        nPercentH = ((float)size.Height / (float)sourceHeight);

        if (nPercentH < nPercentW)
            nPercent = nPercentH;
        else
            nPercent = nPercentW;

        if (nPercent > 1)
        {
            nPercent = 1;
        }
        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap b = new Bitmap(destWidth, destHeight);
        Graphics g = Graphics.FromImage((Image)b);
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

        g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
        g.Dispose();
        imgToResize.Dispose();

        return (Image)b;
    }

    private void saveJpeg(string path, Bitmap img, long quality)
    {
        // Encoder parameter for image quality
        EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, quality);

        // Jpeg image codec
        ImageCodecInfo jpegCodec = this.getEncoderInfo("image/jpeg");

        if (jpegCodec == null)
            return;

        EncoderParameters encoderParams = new EncoderParameters(1);
        encoderParams.Param[0] = qualityParam;

        if(File.Exists(path))
        {
            File.Delete(path);
        }

        img.Save(path, jpegCodec, encoderParams);
        img.Dispose();
        
    }

    private ImageCodecInfo getEncoderInfo(string mimeType)
    {
        // Get image codecs for all image formats
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

        // Find the correct image codec
        for (int i = 0; i < codecs.Length; i++)
            if (codecs[i].MimeType == mimeType)
                return codecs[i];
        return null;
    }


}
