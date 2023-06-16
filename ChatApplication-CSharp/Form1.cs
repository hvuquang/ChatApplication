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
    public partial class Form1 : Form
    {
        private void addLoginUC(LoginUC login)
        {
            login.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(login);
            login.BringToFront();
        }
        private void addRegisterUC(RegisterUC register)
        {
            register.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(register);
            register.BringToFront();
        }
        public Form1()
        {
            InitializeComponent();
            LoginUC login = new LoginUC();
            addLoginUC(login);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            panel2.BackColor = Color.FromArgb(70, 84, 163);
            label1.ForeColor = Color.White;
            panel1.BackColor = Color.White;
            label2.ForeColor = Color.FromArgb(70, 84, 163);
            RegisterUC register = new RegisterUC();
            addRegisterUC(register);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(70, 84, 163);
            label2.ForeColor = Color.White;
            panel2.BackColor = Color.White;
            label1.ForeColor = Color.FromArgb(70, 84, 163);
            LoginUC login = new LoginUC();
            addLoginUC(login);
        }
    }
}
