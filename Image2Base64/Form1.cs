using Image2Base64.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image2Base64
{
    public partial class Form1 : Form
    {
        ImageElement imgElement;

        public Form1()
        {
            InitializeComponent();
            imgElement = null;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult fileResult;

            openFileDialog1.Title = "Browse Image File";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = @"JPG (*.jpg, *.jpeg)|*.jpg;*.jpeg
                                        |PNG (*.png)|*.png
                                        |GIF (*.gif)|*.gif
                                        |TIFF (*.tif, *.tiff)|*.tif;*.tiff
                                        |All files (*.jpg,*.jpeg,*.png,*.gif,*.tif,*.tiff)|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "Select a file";

            fileResult = openFileDialog1.ShowDialog();

            if (fileResult == DialogResult.OK)
            {
                txtFilePath.Text = openFileDialog1.FileName;

                imgElement = new ImageElement();
                imgElement.SetImage(openFileDialog1.FileName);
                //txtImgDimension.Text = imgElement.GetImgDimension();
                //txtImgFormat.Text = Path.GetExtension(openFileDialog1.FileName).Replace(".", "").ToUpper();

                dataGridView1.ColumnCount = 3;
                dataGridView1.Rows.Add(new string[] { "Dimension", imgElement.GetImgDimension(), "Width x Height (pixels)" });
                dataGridView1.Rows.Add(new string[] { "Extension", Path.GetExtension(openFileDialog1.FileName).Replace(".", "").ToUpper(), "" });
            }
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            //Image image = Image.FromFile(@"C:\ArthroscopyImages\1.jpg");
            //var userComment = Encoding.UTF8.GetString(image.GetPropertyItem(0x9286).Value);
        }
    }
}
