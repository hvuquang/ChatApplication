using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication_CSharp
{
    public partial class BigImage : Form
    {
        Image image;
        public BigImage()
        {
            InitializeComponent();
        }

        public BigImage(Image image)
        {
            InitializeComponent();
            this.image = image;
        }

        private void BigImage_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = image;
        }
    }
}
