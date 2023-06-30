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
    public partial class GroupDetail : Form
    {
        private string groupID;
        private string userID;
        private string userInGroupWithcomma;
        private DataTable groupDataTable;

        public GroupDetail()
        {
            InitializeComponent();
        }

        public GroupDetail(string groupID)
        {
            InitializeComponent();
            this.groupID = groupID;
            loadUserInGroup();
            getUserInGroup();
            readUserData();
            label1.Text = userID;
        }

        private void loadUserInGroup()
        {
            dataGridView1.Rows.Clear();
            SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select * from [Group] where (id_nhom = @groupID)", connection);
            cmd1.Parameters.AddWithValue("@groupID", groupID);
            using (SqlDataReader reader = cmd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    userID = (string)reader["id_thanh_vien"];
                }
                reader.Close();
            }
            connection.Close();
        }
        private void getUserInGroup()
        {
            userInGroupWithcomma = userID;
            userID = userID.Replace(",", "");
        }
        private void readUserData()
        {
            groupDataTable = new DataTable();
            groupDataTable.Columns.Add("image", typeof(Image));
            groupDataTable.Columns.Add("username", typeof(string));
            groupDataTable.Columns.Add("email", typeof(string));
            //MessageBox.Show(userID.Length.ToString());
            for (int i =0; i<userID.Length;i++)
            {
                SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("select * from [userTab] where (ID = @userID)", connection);
                cmd1.Parameters.AddWithValue("@userID", userID[i]);
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string username = "";
                        string email = "";
                        byte[] image;
                        username = (string)reader["username"];
                        email = (string)reader["email"];
                        image = (byte[])reader["image"];
                        Image im = ConvertByteArrayToImage(image);
                        groupDataTable.Rows.Add(im, username, email);
                        dataGridView1.Rows.Add(im, username, email);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            //dataGridView1.DataSource = groupDataTable;
            //dataGridView1.Rows.Add()
        }
        public Image ConvertByteArrayToImage(byte[] byteArray)
        {

                //MessageBox.Show(byteArray.ToString());
                using (MemoryStream memoryStream = new MemoryStream(byteArray))
                {
                    Image image = Image.FromStream(memoryStream);
                    return image;
                }

        }

        private void btnAddMember_Click(object sender, EventArgs e)
        {
            addMember member = new addMember(userID, userInGroupWithcomma, groupID);
            member.Show();
        }
    }
}
