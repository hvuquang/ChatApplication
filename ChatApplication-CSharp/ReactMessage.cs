using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatApplication_CSharp
{
    public partial class ReactMessage : UserControl
    {
        private int heart = 0;
        private int like = 0;
        private int laugh = 0;
        private string messageID = "";
        public ReactMessage()
        {
            InitializeComponent();
        }

        public ReactMessage(int hearts, int likes, int laughs, string messageID)
        {
            InitializeComponent();
            this.heart = hearts;
            this.like = likes;
            this.laugh = laughs;
            this.messageID = messageID;
            lbHeart.Text = heart.ToString();
            lbLaugh.Text = laugh.ToString();
            lbLike.Text = like.ToString();
        }

        private void updateData()
        {
            string updateQuery = "UPDATE [ReactionMessage] SET hearts = @heart, likes = @like, laughs = @laugh WHERE messageID = @messageID";
            String connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@heart", heart);
                    updateCommand.Parameters.AddWithValue("@like", like);
                    updateCommand.Parameters.AddWithValue("@laugh", laugh);
                    updateCommand.Parameters.AddWithValue("@messageID", messageID);
                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnHeart_Click(object sender, EventArgs e)
        {
            heart++;
            lbHeart.Text = heart.ToString();
            updateData();
        }

        private void btnLike_Click(object sender, EventArgs e)
        {
            like++;
            lbLike.Text = like.ToString();
            updateData();
        }

        private void btnLaugh_Click(object sender, EventArgs e)
        {
            laugh++;
            lbLaugh.Text = laugh.ToString();
            updateData();
        }
    }
}
