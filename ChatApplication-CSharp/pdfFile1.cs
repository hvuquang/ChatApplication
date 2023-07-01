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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatApplication_CSharp
{

    public partial class pdfFile1 : UserControl
    {
        string fileName = "";
        private string username = "";
        private string mode = "";
        private string messageID;
        private string ID;
        public pdfFile1()
        {
            InitializeComponent();
        }

        public pdfFile1(string fileName)
        {
            InitializeComponent();
            this.fileName = fileName;
            
            FileInfo fileInfo = new FileInfo(fileName);
            string name = fileInfo.Name;
            //fileInfo.Extension();
            long fileSize = fileInfo.Length;
            string value = "";
            if (fileSize > 100000)
            {
                fileSize /= 1024;
                value = fileSize.ToString() + " KB";
            }
            else if (fileSize > 100000000)
            {
                fileSize /= (1024 * 1024);
                value = fileSize.ToString() + " MB";
            }
            else
            {
                value = fileSize.ToString() + " KB";
            }
            btnFileName.Text = name;
            label1.Text = value;
            label1.Location = new Point(91, 66);
        }

        public pdfFile1(string fileName, string mode, string messageID)
        {
            InitializeComponent();
            this.fileName = fileName;
            this.mode = mode;
            this.messageID = messageID;

            FileInfo fileInfo = new FileInfo(fileName);
            string name = fileInfo.Name;
            //fileInfo.Extension();
            long fileSize = fileInfo.Length;
            string value = "";
            if (fileSize > 100000)
            {
                fileSize /= 1024;
                value = fileSize.ToString() + " KB";
            }
            else if (fileSize > 100000000)
            {
                fileSize /= (1024 * 1024);
                value = fileSize.ToString() + " MB";
            }
            else
            {
                value = fileSize.ToString() + " KB";
            }
            btnFileName.Text = name;
            label1.Text = value;
            label1.Location = new Point(91, 66);
            loadUserData();
        }

        private void loadUserData()
        {
            string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string query = "SELECT * FROM [ReactionMessage] WHERE (messageID = @messageID)";
            string querySenderID = "SELECT * FROM [Message] WHERE (ID = @messageID)";
            string queryName = "SELECT * FROM [userTab] WHERE (ID = @ID)";
            //SqlCommand command = new SqlCommand(query, connectionString);
            if (mode == "group")
            {
                //query = "SELECT * FROM [ReactionMessage1] WHERE (messageID = @messageID)";
                querySenderID = "SELECT * FROM [MessageGroup] WHERE (message_id = @messageID)";
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command1 = new SqlCommand(querySenderID, connection);
                command1.Parameters.AddWithValue("@messageID", messageID);
                using (SqlDataReader reader = command1.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Access and process the retrieved data
                        if (mode == "group")
                        {
                            ID = (string)reader["sender_id"].ToString();
                        }
                        else
                            ID = (string)reader["SenderUsername"].ToString();

                    }
                    reader.Close();
                }
                SqlCommand command2 = new SqlCommand(queryName, connection);
                command2.Parameters.AddWithValue("@ID", ID);
                using (SqlDataReader reader = command2.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Access and process the retrieved data
                        username = (string)reader["username"];

                        byte[] imageBytes = (byte[])reader["image"];
                        Image image = ConvertByteArrayToImage(imageBytes);
                        pictureBox2.Image = image;
                        label2.Text = username;
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        void openFile()
        {
            SettingForm settingForm = new SettingForm(fileName);
            settingForm.Show();
            settingForm.BringToFront();
        }

        private void btnFileName_Click(object sender, EventArgs e)
        {
            openFile();
        }

        public Image ConvertByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }
    }
}
