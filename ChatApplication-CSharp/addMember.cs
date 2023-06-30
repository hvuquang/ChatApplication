using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace ChatApplication_CSharp
{
    public partial class addMember : Form
    {
        private int id;
        public string userIDs;
        public string userInGroupWithcomma;
        private string groupID;
        private string temp;
        private List<int> selectedIds = new List<int>();
        public addMember()
        {
            InitializeComponent();
        }

        private DataTable GetAllUsers()
        {
            DataTable userTable = new DataTable();

            //string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT username,id FROM userTab";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.Fill(userTable);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

            return userTable;
        }
        public addMember(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        public addMember(string userID, string userInGroupWithcomma, string groupID)
        {
            InitializeComponent();
            this.userIDs = userID;
            this.userInGroupWithcomma = userInGroupWithcomma;
            this.groupID = groupID;
        }

        private void addGroup_Load(object sender, EventArgs e)
        {
            DataTable userTable = GetAllUsers();
            listView1.View = View.Details;

            // Thêm cột checkbox
            listView1.Columns.Add("Chọn", 80);

            // Thêm cột tên người dùng
            listView1.Columns.Add("Tên người dùng", 120);

            // Duyệt qua từng dòng trong DataTable
            foreach (DataRow row in userTable.Rows)
            {
                bool flag = false;
                for (int i = 0; i < userIDs.Length; i++)
                {
                    if (userIDs[i].ToString() == row["id"].ToString()) flag = true;
                }
                if (flag == true) continue;
                // Lấy id từ dòng hiện tại
                int userId = Convert.ToInt32(row["id"]);

                // Tạo ListViewItem từ dữ liệu của mỗi dòng
                ListViewItem item = new ListViewItem();

                CheckBox checkBox = new CheckBox();
                checkBox.Checked = false;

                item.Tag = userId; // Lưu id vào thuộc tính Tag của ListViewItem
                item.SubItems.Add(row["username"].ToString()); // Thêm tên người dùng vào cột thứ 2

                // Thêm ListViewItem vào ListView
                listView1.Items.Add(item);
            }
        }

        

        private void label1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if(open.ShowDialog()== DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(open.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            connection.Open();
            SqlCommand cmd2 = new SqlCommand("UPDATE [Group] SET id_thanh_vien = @id_thanh_vien_moi where id_nhom = @groupID", connection);
            cmd2.Parameters.AddWithValue("@id_thanh_vien_moi", temp);
            cmd2.Parameters.AddWithValue("@groupID", groupID);
            cmd2.ExecuteNonQuery();
            MessageBox.Show("Thêm thành công");
            this.Close();
            connection.Close();
            return;

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                int userId = (int)selectedItem.Tag;

                if (!selectedIds.Contains(userId))
                {
                    temp = userInGroupWithcomma;
                    temp += "," + userId;
                    selectedIds.Add(userId);
                    selectedItem.BackColor = Color.LightBlue; // Đặt màu nền của dòng đã chọn
                }
                else
                {
                    temp = userInGroupWithcomma;
                    selectedIds.Remove(userId);
                    selectedItem.BackColor = Color.White; // Đặt lại màu nền mặc định cho dòng
                }

                string selectedIdsString = string.Join(",", selectedIds); // Chuyển đổi mảng selectedIds thành chuỗi

                MessageBox.Show("Id của dòng được chọn: " + selectedIdsString);
            }
        }
    }
}
