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
    public partial class addGroup : Form
    {
        private int id;
        private List<int> selectedIds = new List<int>();
        public addGroup()
        {
            InitializeComponent();
        }

        private DataTable GetAllUsers()
        {
            DataTable userTable = new DataTable();

            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
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
        public addGroup(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void addGroup_Load(object sender, EventArgs e)
        {
            DataTable userTable = GetAllUsers();
            listView1.View = View.Details;

            // Thêm cột checkbox
            listView1.Columns.Add("Checkbox", 80);

            // Thêm cột tên người dùng
            listView1.Columns.Add("Tên người dùng", 120);

            // Duyệt qua từng dòng trong DataTable
            foreach (DataRow row in userTable.Rows)
            {
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

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int userId = (int)listView1.SelectedItems[0].Tag;

                if (!selectedIds.Contains(userId))
                {
                    selectedIds.Add(userId);
                }
                else
                {
                    selectedIds.Remove(userId);
                }

                string selectedIdsString = string.Join(",", selectedIds); // Chuyển đổi mảng selectedIds thành chuỗi

                MessageBox.Show("Id của dòng được chọn: " + selectedIdsString);
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
            byte[] b = ImageToByteArray(pictureBox1.Image);
            string selectedIdsString = string.Join(",", selectedIds);
            SqlConnection connection = new SqlConnection("Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            //SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            connection.Open();
            SqlCommand cmd = new SqlCommand("insert into [Group] values (@id_nguoi_tao , @id_thanh_vien , @hinh_anh , @ten_nhom)", connection);
            cmd.Parameters.AddWithValue("@id_nguoi_tao", id);
            cmd.Parameters.AddWithValue("@id_thanh_vien",selectedIdsString);
            cmd.Parameters.AddWithValue("@hinh_anh", b);
            cmd.Parameters.AddWithValue("@ten_nhom", txtTenNhom.Text);
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Thêm nhóm thành công");
        }

        byte[] ImageToByteArray(Image img)
        {
            MemoryStream m = new MemoryStream();
            img.Save(m, System.Drawing.Imaging.ImageFormat.Png);
            return m.ToArray();
        }
    }
}
