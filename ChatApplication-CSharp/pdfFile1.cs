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

namespace ChatApplication_CSharp
{

    public partial class pdfFile1 : UserControl
    {
        string fileName = "";
        public pdfFile1()
        {
            InitializeComponent();
        }

        public pdfFile1(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            
            FileInfo fileInfo = new FileInfo(fileName);
            string name = fileInfo.Name;
            //fileInfo.Extension();
            long fileSize = fileInfo.Length;
            string value = "";
            if (fileSize > 100000)
            {
                fileSize /= 1024;
                value = fileSize.ToString() + " KB";
            }
            else if (fileSize > 100000000)
            {
                fileSize /= (1024 * 1024);
                value = fileSize.ToString() + " MB";
            }
            else
            {
                value = fileSize.ToString() + " KB";
            }
            btnFileName.Text = name;
            label1.Text = value;
            label1.Location = new Point(89, 55);
        }

        void openFile()
        {
            SettingForm settingForm = new SettingForm(fileName);
            settingForm.Show();
            settingForm.BringToFront();
        }

        private void btnFileName_Click(object sender, EventArgs e)
        {
            openFile();
        }
    }
}
