using System;
using System.Collections.Generic;
using System.Drawing;
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

        public ImageElement()
        {
            image = null;
            imageFileName = "";
            imageFilePath = "";
        }

        public void SetImage(string pathFile)
        {
            image = Image.FromFile(pathFile);
        }
    }
}
