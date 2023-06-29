using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication_CSharp
{
    public partial class AudioFile : UserControl
    {
        private Mp3Player Mp3Player = new Mp3Player();
        private string AudioLink = "";
        private string name = "";
        public AudioFile(string AudioLink)
        {
            InitializeComponent();
            this.AudioLink = AudioLink;
            Mp3Player.open(AudioLink);
            FileInfo fileInfo = new FileInfo(AudioLink);
            this.name = fileInfo.Name;
            label1.Text = fileInfo.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mp3Player.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mp3Player.stop();
        }
    }
}
