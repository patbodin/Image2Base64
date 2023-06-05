using Image2Base64.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image2Base64
{
    public partial class Form1 : Form
    {
        ImageElement imgElement;
        ImageURLElement imgURLElement;

        public Form1()
        {
            InitializeComponent();
            imgElement = null;
            imgURLElement = null;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult fileResult;

            openFileDialog1.Title = "Browse Image File";
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Filter = @"JPG (*.jpg, *.jpeg)|*.jpg;*.jpeg;
                                        |PNG (*.png)|*.png;
                                        |GIF (*.gif)|*.gif;
                                        |TIFF (*.tif, *.tiff)|*.tif;*.tiff;
                                        |All files (*.jpg,*.jpeg,*.png,*.gif,*.tif,*.tiff)|*.jpg;*.jpeg;*.png;*.gif;*.tif;*.tiff;";
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
                dataGridView1.Rows.Add(new string[] { "Extension", imgElement.GetExtension().ToUpper(), "" });
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

            if (txtFilePath.Text.Trim() != "")
            {
                btnProcess.Enabled = true;
            }
            else
            {
                btnProcess.Enabled = false;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                pictureBox1.Image = Image.FromFile(txtFilePath.Text);
                txtBase64.Text = imgElement.GetBase64Format();
            }
            else
            {
                imgURLElement = new ImageURLElement();
                imgURLElement.ImageURL = txtFilePath.Text.Trim();
                try
                {
                    using (WebClient webClient = new WebClient())
                    {

                        byte[] data = webClient.DownloadData(txtFilePath.Text.Trim());
                        imgURLElement.ByteArray = data;

                        using (MemoryStream mem = new MemoryStream(data))
                        {
                            imgURLElement.Img = Image.FromStream(mem);
                        }
                        pictureBox1.Image = imgURLElement.Img;

                        string base64Output = Convert.ToBase64String(data);
                        imgURLElement.Base64Output = base64Output;
                        txtBase64.Text = base64Output;

                        Uri uri = new Uri(txtFilePath.Text.Trim());

                        dataGridView1.ColumnCount = 3;
                        dataGridView1.Columns[0].DefaultCellStyle.ForeColor = Color.DarkBlue;
                        dataGridView1.Columns[0].DefaultCellStyle.Font = new Font(this.Font, FontStyle.Bold);
                        dataGridView1.Columns[1].DefaultCellStyle.ForeColor = Color.Blue;

                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(new string[] { "Width", imgURLElement.Img.Width.ToString(), "in pixels" });
                        dataGridView1.Rows.Add(new string[] { "Height", imgURLElement.Img.Height.ToString(), "in pixels" });
                        dataGridView1.Rows.Add(new string[] { "Extension", imgURLElement.GetImageType().ToUpper(), "" });
                        dataGridView1.Rows.Add(new string[] { "HResolution", imgURLElement.Img.HorizontalResolution.ToString(), "Horizontal Resolution" });
                        dataGridView1.Rows.Add(new string[] { "VResolution", imgURLElement.Img.VerticalResolution.ToString(), "Vertical Resolution" });
                        dataGridView1.Rows.Add(new string[] { "AbsoluteUri", uri.AbsoluteUri, "" });
                        dataGridView1.Rows.Add(new string[] { "Host", uri.Host, "" });
                        dataGridView1.Rows.Add(new string[] { "AbsolutePath", uri.AbsolutePath, "" });
                        dataGridView1.Rows.Add(new string[] { "QueryString", uri.Query, "" });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void txtBase64_TextChanged(object sender, EventArgs e)
        {
            if (txtBase64.Text.Trim() != "")
            {
                if (!checkBox1.Checked)
                {
                    txtLength.Text = imgElement.GetBase64OutputLength().ToString("N0");
                }
                else
                {
                    txtLength.Text = imgURLElement.Base64Output.Count().ToString("N0");
                }

                btnReady2Use.Enabled = true;
                btnCopyClipboard.Enabled = true;
            }
            else
            {
                txtLength.Text = "";

                btnReady2Use.Enabled = false;
                btnCopyClipboard.Enabled = false;
            }
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            if (txtBase64.Text.Trim() != "")
            {
                txtBase64.SelectAll();
                txtBase64.Copy();
            }
        }

        private void btnReady2Use_Click(object sender, EventArgs e)
        {
            if (txtBase64.Text.Trim() != "")
            {
                if (!checkBox1.Checked)
                {
                    txtBase64.Text = "data:image/" + imgElement.GetExtension().ToLower() + ";base64," + txtBase64.Text;
                }
                else
                {
                    txtBase64.Text = "data:image/" + imgURLElement.GetImageType().ToLower() + ";base64," + txtBase64.Text;
                }    
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                btnBrowse.Enabled = false;
                //txtFilePath.Enabled = true;
                txtFilePath.ReadOnly = false;

                txtFilePath.ForeColor = Color.Crimson;
            }
            else
            {
                btnBrowse.Enabled = true;
                //txtFilePath.Enabled =  false;
                txtFilePath.ReadOnly = true;

                txtFilePath.ForeColor = Color.Black;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtFilePath.Clear();
        }
    }
}
