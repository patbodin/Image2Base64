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
                dataGridView1.Columns[0].DefaultCellStyle.ForeColor = Color.DarkBlue;
                dataGridView1.Columns[0].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                dataGridView1.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;

                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(new string[] { "Dimension", imgElement.GetImgDimension(), "Width x Height (pixels)" });
                dataGridView1.Rows.Add(new string[] { "Extension", Path.GetExtension(openFileDialog1.FileName).Replace(".", "").ToUpper(), "" });
                dataGridView1.Rows.Add(new string[] { "FileName", imgElement.GetFileName(), "without Path" });
                dataGridView1.Rows.Add(new string[] { "Directory", imgElement.GetDirectory(), "" });
                dataGridView1.Rows.Add(new string[] { "FullName", imgElement.GetFullName(), "" });
                dataGridView1.Rows.Add(new string[] { "FileSize", imgElement.GetFileSize().ToString(), "Byte" });
                dataGridView1.Rows.Add(new string[] { "FileSize (KB)", imgElement.GetFileSize("KByte").ToString("0.##"), "KByte" });
                dataGridView1.Rows.Add(new string[] { "FileSize (MB)", imgElement.GetFileSize("MByte").ToString("0.######"), "MByte" });
                dataGridView1.Rows.Add(new string[] { "FileSize (GB)", imgElement.GetFileSize("GByte").ToString("0.########"), "GByte" });
                dataGridView1.Rows.Add(new string[] { "HResolution", imgElement.GetResolutionH().ToString(), "Horizontal Resolution" });
                dataGridView1.Rows.Add(new string[] { "VResolution", imgElement.GetResolutionV().ToString(), "Vertical Resolution" });
                dataGridView1.Rows.Add(new string[] { "Creation", imgElement.GetCreateDate(), "" });
                dataGridView1.Rows.Add(new string[] { "LastAccess", imgElement.GetLastAccess(), "" });
                dataGridView1.Rows.Add(new string[] { "LastModified", imgElement.GetLastModified(), "" });
                dataGridView1.Rows.Add(new string[] { "IsHidden", imgElement.GetAttribute("hidden"), "Attribute: Hidden" });
                dataGridView1.Rows.Add(new string[] { "IsSystem", imgElement.GetAttribute("system"), "Attribute: System" });
                dataGridView1.Rows.Add(new string[] { "IsReadOnly", imgElement.GetAttribute("readonly"), "Attribute: Read Only" });
                dataGridView1.Rows.Add(new string[] { "IsArchive", imgElement.GetAttribute("archive"), "Attribute: Archive" });
                dataGridView1.Rows.Add(new string[] { "IsCompressed", imgElement.GetAttribute("compressed"), "Attribute: Compressed" });
            }
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {
            //Image image = Image.FromFile(@"C:\ArthroscopyImages\1.jpg");
            //var userComment = Encoding.UTF8.GetString(image.GetPropertyItem(0x9286).Value);
        }
    }
}
