using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
        private string messageID;
        private int hearts;
        private int likes;
        private int laughs;
        private string mode;
        private string ID;
        private string name;
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

        public Message1(string message, DateTime sentDate, string messageID)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
            this.messageID = messageID;
        }

        public Message1(string message, DateTime sentDate, string messageID, string mode)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
            this.messageID = messageID;
            this.mode = mode;
        }

        private void loadData()
        {
            if (messageID == null)
            {
                return;
            }
            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT * FROM [ReactionMessage] WHERE (messageID = @messageID)";
            string querySenderID = "SELECT * FROM [Message] WHERE (ID = @messageID)";
            string queryName = "SELECT * FROM [userTab] WHERE (ID = @ID)";
            //SqlCommand command = new SqlCommand(query, connectionString);
            if (mode == "group")
            {
                query = "SELECT * FROM [ReactionMessage1] WHERE (messageID = @messageID)";
                querySenderID = "SELECT * FROM [MessageGroup] WHERE (message_id = @messageID)";
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@messageID", messageID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access and process the retrieved data

                            hearts = (int)reader["hearts"];
                            likes = (int)reader["likes"];
                            laughs = (int)reader["laughs"];
                        }
                        reader.Close();
                    }
                    SqlCommand command1 = new SqlCommand(querySenderID, connection);
                    command1.Parameters.AddWithValue("@messageID", messageID);
                    using (SqlDataReader reader = command1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Access and process the retrieved data
                            if(mode == "group")
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
                            name = (string)reader["username"];
                            
                        }
                        reader.Close();
                    }


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void lbMessage_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void lbMessage_Click(object sender, EventArgs e)
        {
        }


        //code mới
        private void Message1_Click(object sender, EventArgs e)
        {
            reactMessage1.Visible = true;
        }

        private void Message1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Message1_Load(object sender, EventArgs e)
        {

            loadData();
            //lbTime.Text = sentDate.ToString("dd-MM-yyyy HH:mm");
            ReactMessage reactMessage = new ReactMessage(hearts, likes, laughs, messageID, mode);
            reactMessage.Location = new Point(575, 78);
            reactMessage.BringToFront();
            this.Controls.Add(reactMessage);
            lbMessage.Text = message;
            label1.Text = name;
        }
    }
}
