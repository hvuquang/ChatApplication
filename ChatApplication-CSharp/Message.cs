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
    public partial class Message : UserControl
    {
        private String message;
        private DateTime sentDate;
        public Message()
        {
            InitializeComponent();
        }
        public Message(String message , DateTime sentDate)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            label1.Text = message;
            lbTime.Text = sentDate.ToString("dd-MM-yyyy HH:mm");
        }
    }
}
