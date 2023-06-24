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
    public partial class ChatTime : UserControl
    {
        public ChatTime()
        {
            InitializeComponent();
        }

        public ChatTime(string senttime)
        {
            InitializeComponent();
            label1.Text = senttime;
        }
    }
}
