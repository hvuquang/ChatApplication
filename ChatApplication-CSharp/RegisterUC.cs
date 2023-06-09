﻿using System;
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
    public partial class RegisterUC : UserControl
    {
        public RegisterUC()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlConnection connection = new SqlConnection("Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            byte[] b = ImageToByteArray(pictureBox1.Image);
            connection.Open();
            SqlCommand cmd = new SqlCommand("insert into [userTab] values (@username , @password , @email , @image)", connection);
            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPass.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);
            cmd.Parameters.AddWithValue("@image", b);
            cmd.ExecuteNonQuery();
            connection.Close();

            txtUsername.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtConfirm.Text = "";

            MessageBox.Show("Tạo tài khoản thành công");
        }
        byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }
    }
}
