using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        private FileInfo fi;
        private FileAttributes fa;

        public ImageElement()
        {
            image = null;
            imageFileName = "";
            imageFilePath = "";
            imageFormat = null;
            fi = null;
            //fa = null;
        }

        public void SetImage(string pathFile)
        {
            image = Image.FromFile(pathFile);
            imageWidth = image.Width;
            imageHeight = image.Height;
            imageFormat = image.RawFormat;

            fi = new FileInfo(pathFile);
            fa = File.GetAttributes(pathFile);
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

        public string GetFileName()
        {
            return fi.Name;
        }

        public string GetDirectory()
        {
            return fi.DirectoryName;
        }

        public string GetFullName()
        {
            return fi.FullName;
        }

        public double GetFileSize(string strSize = "Byte")
        {
            long fSize = fi.Length;
            double fResult = 0.0;

            if (strSize.ToLower().Trim() == "kbyte")
            {
                fResult = fSize / 1024.0;
            }
            else if (strSize.ToLower().Trim() == "mbyte")
            {
                fResult = fSize / (1024.0 * 1024.0);
            }
            else if (strSize.ToLower().Trim() == "gbyte")
            {
                fResult = fSize / (1024.0 * 1024.0 * 1024.0);
            }
            else
            {
                fResult = fSize * 1.0;
            }

            return fResult;
        }

        public string GetCreateDate()
        {
            return fi.CreationTime.ToString();
        }

        public float GetResolutionH()
        {
            return image.HorizontalResolution;
        }

        public float GetResolutionV()
        {
            return image.VerticalResolution;
        }

        public string GetLastModified()
        {
            return fi.LastWriteTime.ToString();
        }

        public string GetLastAccess()
        {
            return fi.LastAccessTime.ToString();
        }

        public string GetAttribute(string attr)
        {
            bool bResult = false;

            if (attr.Trim().ToLower() == "hidden")
            {
                bResult = (fa & FileAttributes.Hidden) == FileAttributes.Hidden;
            }
            else if (attr.Trim().ToLower() == "archive")
            {
                bResult = (fa & FileAttributes.Archive) == FileAttributes.Archive;
            }
            else if (attr.Trim().ToLower() == "system")
            {
                bResult = (fa & FileAttributes.System) == FileAttributes.System;
            }
            else if (attr.Trim().ToLower() == "readonly")
            {
                bResult = (fa & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
            }
            else if (attr.Trim().ToLower() == "compressed")
            {
                bResult = (fa & FileAttributes.Compressed) == FileAttributes.Compressed;
            }
            else
            {
                return "Incorrect Attributes!";
            }

            return bResult.ToString();
        }
    }
}
