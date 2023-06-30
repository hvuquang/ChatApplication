using System;
using System.CodeDom.Compiler;
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
using System.Xml.Linq;

namespace ChatApplication_CSharp
{
    public partial class GroupDetail : Form
    {
        private string groupID;
        private string userID;
        private string[] substrings;
        private List<string> listOfUser = new List<string>();
        private string userInGroupWithcomma;
        private string idFromChatForm;
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
        }

        public GroupDetail(string groupID, string id)
        {
            InitializeComponent();
            this.groupID = groupID;
            this.idFromChatForm = id;
            dataGridView1.Columns["Delete"].Visible = false;
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
            //userID = userID.Replace(",", "");
            substrings = userID.Split(',');
        }
        private void readUserData()
        {
            
            groupDataTable = new DataTable();
            groupDataTable.Columns.Add("image", typeof(Image));
            groupDataTable.Columns.Add("username", typeof(string));
            groupDataTable.Columns.Add("id", typeof(int));
            groupDataTable.Columns.Add("email", typeof(string));
            groupDataTable.Columns.Add("delete", typeof(bool));
            //MessageBox.Show(userID.Length.ToString());
            for (int i =0; i< substrings.Length;i++)
            {
                SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
                connection.Open();
                SqlCommand cmd1 = new SqlCommand("select * from [userTab] where (ID = @userID)", connection);
                cmd1.Parameters.AddWithValue("@userID", substrings[i]);
                using (SqlDataReader reader = cmd1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string username = "";
                        string email = "";
                        byte[] image;
                        bool delete = false;
                        int id = (int)reader["ID"];
                        username = (string)reader["username"];
                        email = (string)reader["email"];
                        image = (byte[])reader["image"];
                        Image im = ConvertByteArrayToImage(image);
                        //groupDataTable.Rows.Add(im, username, email);
                        dataGridView1.Rows.Add(id, im, username, email, delete);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            //dataGridView1.DataSource = groupDataTable;
            //dataGridView1.Rows.Add()
            dataGridView1.ClearSelection();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
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
            addMember member = new addMember(substrings, userInGroupWithcomma, groupID);
            member.Show();
        }

        private void UpdateIdThanhVien(int nhomId, int id)
        {
            //string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True"; // Thay thế bằng chuỗi kết nối của bạn
            string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT id_thanh_vien FROM [Group] WHERE id_nhom = @nhomId";
                SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@nhomId", nhomId);
                SqlDataReader reader = selectCommand.ExecuteReader();

                if (reader.Read())
                {
                    string idThanhVien = reader["id_thanh_vien"].ToString();

                    // Tách các giá trị trong id_thanh_vien thành mảng
                    string[] idThanhVienArray = idThanhVien.Split(',');

                    // Tạo danh sách mới chứa các giá trị khác id
                    List<string> newIdThanhVienList = new List<string>();
                    foreach (string value in idThanhVienArray)
                    {
                        if (value != id.ToString())
                        {
                            newIdThanhVienList.Add(value);
                        }
                    }
                    reader.Close();
                    // Cập nhật lại trường id_thanh_vien trong bảng Group
                    string newIdThanhVien = string.Join(",", newIdThanhVienList.ToArray());
                    string updateQuery = "UPDATE [Group] SET id_thanh_vien = @newIdThanhVien WHERE id_nhom = @nhomId";
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@newIdThanhVien", newIdThanhVien);
                    updateCommand.Parameters.AddWithValue("@nhomId", nhomId);
                    updateCommand.ExecuteNonQuery();
                }

                reader.Close();
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            UpdateIdThanhVien(Int32.Parse(groupID), Int32.Parse(idFromChatForm));
            this.Close();
            //ChatForm chat = new ChatForm();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(listOfUser.Count > 0)
            {
                //MessageBox.Show(listOfUser[0]);
                for (int i = 0; i < listOfUser.Count; i++)
                {
                    UpdateIdThanhVien(Int32.Parse(groupID), Int32.Parse(listOfUser[i]));
                }
                return;
            }
            using (SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True"))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM [Group] WHERE id_nhom = @GroupID";

                using (SqlCommand command = new SqlCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@GroupID", groupID);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Row deleted successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No rows deleted.");
                    }
                }
            }

        }

        private void GroupDetail_Load(object sender, EventArgs e)
        {
            loadUserInGroup();
            getUserInGroup();
            readUserData();
            int val = 0;
            SqlConnection connection = new SqlConnection("Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True");
            connection.Open();
            SqlCommand cmd1 = new SqlCommand("select * from [Group] where (id_nhom = @groupID)", connection);
            cmd1.Parameters.AddWithValue("@groupID", groupID);
            using (SqlDataReader reader = cmd1.ExecuteReader())
            {
                while (reader.Read())
                {
                    val = (int)reader["id_nguoi_tao"];
                }
                reader.Close();
            }
            connection.Close();
            if (val.ToString() == idFromChatForm)
            {
                btnXoa.Visible = true;
                dataGridView1.Columns["Delete"].Visible = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { 
            if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
               
                object checkboxCell = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                bool isChecked = (bool)checkboxCell;
                if (isChecked == true)
                {
                    isChecked = false;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = false;
                }
                else
                {
                    isChecked = true;
                    dataGridView1.Rows[e.RowIndex].Cells[4].Value = true;
                }
                object userid = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                string id = "";
                id = userid.ToString();
                // Perform actions based on the checked state
                if (isChecked == true)
                {
                    listOfUser.Add(id);
                }
                else
                {
                    listOfUser.RemoveAt(listOfUser.Count - 1);
                }
            }
        }
    }
}
