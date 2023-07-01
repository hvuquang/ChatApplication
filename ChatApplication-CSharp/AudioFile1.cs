using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication_CSharp
{
    public partial class AudioFile1 : UserControl
    {
        private Mp3Player Mp3Player = new Mp3Player();
        private string AudioLink = "";
        private string name = "";
        private string username = "";
        private string mode = "";
        private string messageID;
        private string ID;
        public AudioFile1(string AudioLink)
        {
            InitializeComponent();
            this.AudioLink = AudioLink;
            Mp3Player.open(AudioLink);
            FileInfo fileInfo = new FileInfo(AudioLink);
            this.name = fileInfo.Name;
            label1.Text = fileInfo.Name;
        }

        public AudioFile1(string AudioLink, string mode, string messageID)
        {
            InitializeComponent();
            this.messageID = messageID;
            this.AudioLink = AudioLink;
            Mp3Player.open(AudioLink);
            FileInfo fileInfo = new FileInfo(AudioLink);
            this.name = fileInfo.Name;
            label1.Text = fileInfo.Name;
            this.mode = mode;
            loadUserData();
            //pictureBox1.Image = 
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
                        pictureBox1.Image = image;
                        label2.Text = username;
                    }
                    reader.Close();
                }
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mp3Player.play();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Mp3Player.stop();
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
