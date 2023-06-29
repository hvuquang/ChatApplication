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

namespace ChatApplication_CSharp
{
    public partial class Message : UserControl
    {
        private String message;
        private DateTime sentDate;
        private string messageID;
        private int hearts;
        private int likes;
        private int laughs;
        private string mode;
        public Message()
        {
            InitializeComponent();
        }
        public Message(String message , DateTime sentDate)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
        }

        public Message(String message, DateTime sentDate, string messageID)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
            this.messageID = messageID;
        }

        public Message(String message, DateTime sentDate, string messageID, string mode)
        {
            InitializeComponent();
            this.message = message;
            this.sentDate = sentDate;
            this.messageID = messageID;
            this.mode = mode;
        }

        private void Message_Load(object sender, EventArgs e)
        {
            loadData();
            label1.Text = message;
            lbTime.Text = sentDate.ToString("dd-MM-yyyy HH:mm");
            ReactMessage reactMessage = new ReactMessage(hearts, likes, laughs, messageID, mode);
            reactMessage.Location = new Point(146, 76);
            reactMessage.BringToFront();
            this.Controls.Add(reactMessage);
        }

        private void loadData()
        {
            if (messageID == null)
            {
                return;
            }
            string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT * FROM [ReactionMessage] WHERE (messageID = @messageID)";
            if (mode == "group")
            {
                query = "SELECT * FROM [ReactionMessage1] WHERE (messageID = @messageID)";
            }
            //SqlCommand command = new SqlCommand(query, connectionString);


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
                            // Example: int id = (int)reader["Id"];
                            hearts = (int)reader["hearts"];
                            likes = (int)reader["likes"];
                            laughs = (int)reader["laughs"];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        //code mới
        private void Message_MouseClick(object sender, MouseEventArgs e)
        {
            //ReactMessage reactMessage1 = new ReactMessage(hearts, likes, 10);
            //reactMessage1.Location = new Point(146, 61);
            reactMessage1.Visible = true;
            reactMessage1.lbHeart.Text = hearts.ToString();
            reactMessage1.lbLaugh.Text = laughs.ToString();
            reactMessage1.lbLike.Text = likes.ToString();
        }
    }
}
