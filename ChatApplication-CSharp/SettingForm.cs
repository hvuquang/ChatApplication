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
    public partial class SettingForm : Form
    {
        string filePath = "";
        public SettingForm()
        {
            InitializeComponent();
        }

        public SettingForm(string filePath)
        {
            InitializeComponent();
            this.filePath = filePath;
            openFile();
        }

        private void openFile()
        {
            pdfViewer1.LoadFromFile(filePath);
        }
    }
}
