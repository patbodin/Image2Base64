using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image2Base64.Model
{
    class ImageElement
    {
        private Image image;
        private string imageFileName;
        private string imageFilePath;
        private int imageWidth;
        private int imageHeight;
        private ImageFormat imageFormat;

        public ImageElement()
        {
            image = null;
            imageFileName = "";
            imageFilePath = "";
            imageFormat = null;
        }

        public void SetImage(string pathFile)
        {
            image = Image.FromFile(pathFile);
            imageWidth = image.Width;
            imageHeight = image.Height;
            imageFormat = image.RawFormat;
            
        }

        public int GetImgWidth()
        {
            return imageWidth;
        }

        public int GetImgHeight()
        {
            return imageHeight;
        }

        public string GetImgDimension()
        {
            return GetImgWidth() + " x " + GetImgHeight();
        }

        public string GetImgFormat()
        {
            return imageFormat.ToString();
        }
    }
}
