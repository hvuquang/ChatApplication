using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace ChatApplication_CSharp
{
    public partial class Message1 : UserControl
    {
        private String message;
        private DateTime sentDate;
        public Message1()
        {
            InitializeComponent();
        }
        public Message1(String message, DateTime sentDate)
        {
            InitializeComponent();
            this.message = message;
            lbMessage.Text = message;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            lbMessage.Text = message;
        }
    }
}
