using Spire.Pdf.OPC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace ChatApplication_CSharp
{
    public partial class Images : Form
    {
        DataTable data;
        BigImage bigImage;
        public Images()
        {
            InitializeComponent();
        }

        public Images(DataTable data)
        {
            InitializeComponent();
            this.data = data;
        }

        public Image ConvertByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }

        private void loadImages()
        { 
            flowLayoutPanel1.Controls.Clear();
            byte[] imageData = null;
            Image image;

            foreach (DataRow row in data.Rows)
            {
                if (row["img"].ToString() != "")
                {
                    PictureBox imageMessage = new PictureBox();


                    imageData = (byte[])row["img"];
                    image = ConvertByteArrayToImage(imageData);
                    imageMessage.Image = image;
                    imageMessage.Size = new Size(259, 259);
                    BigImage bigImage = new BigImage(image);
                    imageMessage.Click += (sender,e) => ImageMessage_Click(sender,e , bigImage);
                    //bigImage = new BigImage(image);
                    flowLayoutPanel1.Controls.Add(imageMessage);
                }
            }
        }

        private void ImageMessage_Click(object sender, EventArgs e, BigImage bigImage)
        {
            //BigImage bigImage = new BigImage(image);   
            bigImage.Show();
            bigImage.BringToFront();
        }

        private void Images_Load(object sender, EventArgs e)
        {
            loadImages();
        }
    }
}
