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

    public partial class pdfFile : UserControl
    {
        string fileName = "";
        public pdfFile()
        {
            InitializeComponent();
        }

        public pdfFile(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            btnFileName.Text = fileName;
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
