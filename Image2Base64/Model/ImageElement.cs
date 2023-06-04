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
        private string imageFullPath;
        private int imageWidth;
        private int imageHeight;
        private ImageFormat imageFormat;
        private FileInfo fi;
        private FileAttributes fa;
        private string strBase64;

        public ImageElement()
        {
            image = null;
            imageFullPath = "";
            imageFormat = null;
            fi = null;
            //fa = null;
            strBase64 = "";
        }

        public void SetImage(string pathFile)
        {
            imageFullPath = pathFile;
            image = Image.FromFile(imageFullPath);
            imageWidth = image.Width;
            imageHeight = image.Height;
            imageFormat = image.RawFormat;

            fi = new FileInfo(imageFullPath);
            fa = File.GetAttributes(imageFullPath);
        }

        public string GetExtension()
        {
            return Path.GetExtension(imageFullPath).Replace(".", "");
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

        public string GetBase64Format()
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(imageFullPath);
            strBase64 = Convert.ToBase64String(imageArray);
            return strBase64;
        }

        public long GetBase64OutputLength()
        {
            return (long)strBase64.Length;
        }
    }
}
