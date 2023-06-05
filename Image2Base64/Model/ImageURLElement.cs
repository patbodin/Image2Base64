using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image2Base64.Model
{
    class ImageURLElement
    {
        public Image Img { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }
        public string ImageURL { get; set; }
        public string Base64Output { get; set; }
        public byte[] ByteArray { get; set; }

        public ImageURLElement()
        {

        }

        public string GetImageType()
        {
            if (Img.RawFormat.Equals(ImageFormat.Bmp))
            {
                return "Bmp";
            }
            else if (Img.RawFormat.Equals(ImageFormat.MemoryBmp))
            {
                return "BMP";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Emf))
            {
                return "Emf";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Wmf))
            {
                return "Wmf";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Gif))
            {
                return "Gif";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Jpeg))
            {
                return "Jpeg";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Png))
            {
                return "Png";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Tiff))
            {
                return "Tiff";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Exif))
            {
                return "Exif";
            }
            else if (Img.RawFormat.Equals(ImageFormat.Icon))
            {
                return "Ico";
            }

            return "Invalid";

        }
    }
}
