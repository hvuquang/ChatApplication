﻿using Spire.PdfViewer.Asp;
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
    public partial class ChatForm : Form
    {
        private int id;
        private int receiver;
        private Timer timer;
        private DataTable lastMessageTable;
        private int pollingInterval = 2000;
        private int countDown = 120;
        private String modeChat = "";
        private int group_id;
        private DataTable lastMessageGroupTable;
        private Images Images;

        public ChatForm()
        {
            InitializeComponent();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = pollingInterval;
            //timer.Tick += Timer_Tick;
        }
        public ChatForm(int id)
        {
            InitializeComponent();
            this.id = id;
            InitializeTimer();
            timer.Start();
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

        private DataTable GetAllGroups()
        {
            DataTable groupTable = new DataTable();
            groupTable.Columns.Add("id_nhom", typeof(int));
            groupTable.Columns.Add("id_nguoi_tao", typeof(int));
            groupTable.Columns.Add("id_thanh_vien", typeof(string));
            groupTable.Columns.Add("hinh_anh", typeof(Image)); // Thay đổi kiểu dữ liệu thành Image
            groupTable.Columns.Add("ten_nhom", typeof(string));

            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT id_nhom, id_nguoi_tao, id_thanh_vien, hinh_anh, ten_nhom FROM [Group] WHERE id_nguoi_tao = @id OR id_thanh_vien LIKE '%' + CAST(@id AS NVARCHAR(MAX)) + '%'";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id_nhom = reader.GetInt32(reader.GetOrdinal("id_nhom"));
                                int id_nguoi_tao = reader.GetInt32(reader.GetOrdinal("id_nguoi_tao"));
                                string id_thanh_vien = reader.GetString(reader.GetOrdinal("id_thanh_vien"));

                                // Chuyển đổi byte[] sang Image
                                byte[] imageBytes = (byte[])reader["hinh_anh"];
                                Image hinh_anh = ConvertByteArrayToImage(imageBytes);

                                string ten_nhom = reader.GetString(reader.GetOrdinal("ten_nhom"));

                                groupTable.Rows.Add(id_nhom, id_nguoi_tao, id_thanh_vien, hinh_anh, ten_nhom);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

            return groupTable;
        }



        private void DisplayUsersInDataGridView()
        {
            DataTable userTable = GetAllUsers();
            dataGridView1.DataSource = userTable;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(249, 91, 61);
            dataGridView1.Columns["id"].Visible = false;
        }

        private void DisplayGroupsInDataGridView()
        {
            DataTable groupTable = GetAllGroups();
            dataGridView2.DataSource = groupTable;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(249, 91, 61);
            dataGridView2.Columns["id_nhom"].Visible = false;
            dataGridView2.Columns["id_nguoi_tao"].Visible = false;
            dataGridView2.Columns["id_thanh_vien"].Visible = false;
            dataGridView2.ClearSelection();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            
        }

        private DataTable GetAllMessages(int senderId, int receiverId)
        {
            DataTable messageTable = new DataTable();

            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT * FROM [Message] WHERE (SenderUsername = @senderId AND ReceiverUsername = @receiverId) OR (SenderUsername = @receiverId AND ReceiverUsername = @senderId) ORDER BY SentTime ASC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@senderId", senderId);
                        command.Parameters.AddWithValue("@receiverId", receiverId);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(messageTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

            return messageTable;
        }

        private DataTable GetAllMessagesInGroup(int group_id)
        {
            DataTable messageTable = new DataTable();

            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            string query = "SELECT * FROM [MessageGroup] WHERE (group_id = @group_id) ORDER BY send_time ASC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@group_id", group_id);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(messageTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

            return messageTable;
        }

        public Image ConvertByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream memoryStream = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(memoryStream);
                return image;
            }
        }

        private void DisplayMessagesInFlowLayoutPanel(DataTable messageTable)
        {
            Images = new Images(messageTable);
            flowLayoutPanel1.Controls.Clear();

            byte[] imageData = null;
            Image image;

            foreach (DataRow row in messageTable.Rows)
            {

                if (modeChat == "single")
                {
                    int senderId = int.Parse(row["SenderUsername"].ToString());
                    if (senderId == id)
                    {
                        string senttime = row["SentTime"].ToString();
                        string messageID = row["ID"].ToString();
                        ChatTime chatTime = new ChatTime(senttime);
                        flowLayoutPanel1.Controls.Add(chatTime);

                        if (row["img"].ToString() != "")
                        {
                            ImageMessage imageMessage = new ImageMessage();
                            imageData = (byte[])row["img"];
                            image = ConvertByteArrayToImage(imageData);
                            imageMessage.pictureBox1.Image = image;
                            flowLayoutPanel1.Controls.Add(imageMessage);
                        }
                        if (row["MessageText"].ToString() != "")
                        {
                            string message = row["MessageText"].ToString();
                            DateTime sentTime = (DateTime)row["SentTime"];
                            Message messageControl = new Message(message, sentTime, messageID);
                            flowLayoutPanel1.Controls.Add(messageControl);
                        }
                        if (row["AudioLink"].ToString() != "")
                        {
                            AudioFile audioFile = new AudioFile(row["AudioLink"].ToString());
                            flowLayoutPanel1.Controls.Add(audioFile);
                        }
                        if (row["FileLink"].ToString() != "")
                        {
                            pdfFile pdfFile = new pdfFile(row["FileLink"].ToString());
                            flowLayoutPanel1.Controls.Add(pdfFile);
                        }
                    }
                    else
                    {
                        string senttime = row["SentTime"].ToString();
                        ChatTime chatTime = new ChatTime(senttime);
                        string messageID = row["ID"].ToString();
                        flowLayoutPanel1.Controls.Add(chatTime);
                        if (row["img"].ToString() != "")
                        {
                            ImageMessage1 imageMessage = new ImageMessage1();
                            imageData = (byte[])row["img"];
                            image = ConvertByteArrayToImage(imageData);
                            imageMessage.pictureBox1.Image = image;
                            flowLayoutPanel1.Controls.Add(imageMessage);
                        }
                        if (row["MessageText"].ToString() != "")
                        {
                            string message = row["MessageText"].ToString();
                            DateTime sentTime = (DateTime)row["SentTime"];
                            Message1 messageControl = new Message1(message, sentTime, messageID);
                            flowLayoutPanel1.Controls.Add(messageControl);
                        }
                        if (row["AudioLink"].ToString() != "")
                        {
                            AudioFile1 audioFile = new AudioFile1(row["AudioLink"].ToString());
                            flowLayoutPanel1.Controls.Add(audioFile);
                        }
                        if (row["FileLink"].ToString() != "")
                        {
                            pdfFile1 pdfFile = new pdfFile1(row["FileLink"].ToString());
                            flowLayoutPanel1.Controls.Add(pdfFile);
                        }
                    }
                }
                else if (modeChat == "multi")
                {
                    MessageBox.Show((row["sender_id"].ToString()));
                    int senderId = int.Parse(row["sender_id"].ToString());
                    if (senderId == id)
                    {
                        //MessageBox.Show()
                        string message = row["content"].ToString();
                        DateTime sentTime = (DateTime)row["send_time"];
                        Message messageControl = new Message(message, sentTime);
                        flowLayoutPanel1.Controls.Add(messageControl);
                    }
                    else
                    {
                        string message = row["content"].ToString();
                        DateTime sentTime = (DateTime)row["send_time"];
                        Message1 messageControl = new Message1(message, sentTime);
                        flowLayoutPanel1.Controls.Add(messageControl);
                    }
                }

            }
        }
        private void ChatForm_Load(object sender, EventArgs e)
        {
            DisplayUsersInDataGridView();
            DisplayGroupsInDataGridView();
        }

        private void LoadLatestMessages()
        {
            DataTable currentMessageTable = GetAllMessages(id, receiver);
            if (lastMessageTable == null || !lastMessageTable.Rows.Cast<DataRow>().SequenceEqual(currentMessageTable.Rows.Cast<DataRow>(), DataRowComparer.Default))
            {
                lastMessageTable = currentMessageTable;
                DisplayMessagesInFlowLayoutPanel(lastMessageTable);
            }
            DataTable currentGroupTable = GetAllMessagesInGroup(group_id);
            if (lastMessageGroupTable == null || !lastMessageGroupTable.Rows.Cast<DataRow>().SequenceEqual(currentGroupTable.Rows.Cast<DataRow>(), DataRowComparer.Default))
            {
                lastMessageGroupTable = currentGroupTable;
                DisplayMessagesInFlowLayoutPanel(lastMessageGroupTable);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            flowLayoutPanel1.SuspendLayout();
            LoadLatestMessages();
            flowLayoutPanel1.ResumeLayout();
            flowLayoutPanel1.PerformLayout();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                modeChat = "single";
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string idString = row.Cells["ID"].Value.ToString();
                if (int.TryParse(idString, out int selectedId))
                {
                    receiver = selectedId;
                }
                DataTable messageTable = GetAllMessages(id, receiver);
                
                DisplayMessagesInFlowLayoutPanel(messageTable);
            }
        }

        private void insertDataReact(SqlConnection connection, int generatedId)
        {
            //SqlCommand command = new SqlCommand(insertMessageQuery, connection);

            int messageId = generatedId;

            //bỏ data vô reactmessage
            string query1 = "INSERT INTO [ReactionMessage] (hearts, likes, laughs, messageID ) VALUES (@hearts, @likes, @laughs, @messageID)";

            SqlCommand command1 = new SqlCommand(query1, connection);

            command1.Parameters.AddWithValue("@hearts", 0);
            command1.Parameters.AddWithValue("@likes", 0);
            command1.Parameters.AddWithValue("@laughs", 0);
            command1.Parameters.AddWithValue("@messageID", messageId);
            command1.ExecuteNonQuery();
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (modeChat == "single")
            {
                string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
                //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True"; 
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO [Message] (SenderUsername, ReceiverUsername, MessageText, SentTime) OUTPUT INSERTED.Id  VALUES (@sender, @receiver, @message, @sentTime)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@sender", id);
                command.Parameters.AddWithValue("@receiver", receiver);
                command.Parameters.AddWithValue("@message", txtMessage.Text);
                command.Parameters.AddWithValue("@sentTime", DateTime.Now);
                //command.ExecuteNonQuery();

                ////lấy id
                int generatedId = 0;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    generatedId = (int)reader["Id"];
                }
                reader.Close();

                insertDataReact(connection, generatedId);

                connection.Close();
                LoadLatestMessages();
                txtMessage.Text = "";
            }
            else if (modeChat == "multi")
            {
                string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
                //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO [MessageGroup] (content, sender_id, group_id , send_time) VALUES (@content, @sender_id, @group_id,@send_time)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@content", txtMessage.Text);
                command.Parameters.AddWithValue("@sender_id", id);
                command.Parameters.AddWithValue("@group_id", group_id);
                command.Parameters.AddWithValue("@send_time", DateTime.Now);
                command.ExecuteNonQuery();
                connection.Close();
                LoadLatestMessages();
                txtMessage.Text = "";
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addGroup add = new addGroup(id);
            add.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set the initial directory and filter for the file dialog
            openFileDialog.InitialDirectory = "C:\\";
            //openFileDialog.Filter = "Image Files (*.jpg; *.png; *.gif)|*.jpg;*.png;*.gif";
            openFileDialog.Filter = "All Files|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Retrieve the selected file path
                string filePath = openFileDialog.FileName;
                
                string fileExtension = Path.GetExtension(filePath);

                // Process the file or save the path to the database
                // ...
                string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
                //string connectionString = "Data Source=VUQUANGHUY\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string query = "INSERT INTO [Message] (SenderUsername, ReceiverUsername, img, SentTime, AudioLink, FileLink) VALUES (@sender, @receiver, CONVERT(varbinary(max), @img), @sentTime, @AudioLink, @FileLink)";
                SqlCommand command = new SqlCommand(query, connection);

                // Read the file as binary data
                byte[] binaryData = null;
                string AudioLink = null;
                string FileLink = null;
                if (fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif")
                {
                    binaryData = File.ReadAllBytes(filePath);
                    command.Parameters.AddWithValue("@sender", id);
                    command.Parameters.AddWithValue("@receiver", receiver);
                    command.Parameters.AddWithValue("@img", binaryData);
                    command.Parameters.AddWithValue("@sentTime", DateTime.Now);
                    command.Parameters.AddWithValue("@AudioLink", DBNull.Value);
                    command.Parameters.AddWithValue("@FileLink", DBNull.Value);
                }
                else if (fileExtension == ".mp3")
                {
                    AudioLink = filePath;
                    //MessageBox.Show(AudioLink);
                    command.Parameters.AddWithValue("@sender", id);
                    command.Parameters.AddWithValue("@receiver", receiver);
                    command.Parameters.AddWithValue("@img", DBNull.Value);
                    command.Parameters.AddWithValue("@sentTime", DateTime.Now);
                    command.Parameters.AddWithValue("@AudioLink", AudioLink);
                    command.Parameters.AddWithValue("@FileLink", DBNull.Value);
                }
                else if (fileExtension == ".pdf")
                {
                    FileLink = filePath;
                    command.Parameters.AddWithValue("@sender", id);
                    command.Parameters.AddWithValue("@receiver", receiver);
                    command.Parameters.AddWithValue("@img", DBNull.Value);
                    command.Parameters.AddWithValue("@sentTime", DateTime.Now);
                    command.Parameters.AddWithValue("@AudioLink", DBNull.Value);
                    command.Parameters.AddWithValue("@FileLink", FileLink);

                    //SettingForm settingForm = new SettingForm(filePath);
                    //settingForm.Show();
                    //settingForm.BringToFront();
                }

                command.ExecuteNonQuery();

                connection.Close();
                LoadLatestMessages();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            

        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void ChatForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                 button1_Click(sender, e);
            }
        }

        private void btnChangeScreen_Click(object sender, EventArgs e)
        {
            //flowLayoutPanel1.BackColor = Color.Black;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "C:\\";
            openFileDialog.Filter = "Image Files (*.jpg; *.png; *.gif)|*.jpg;*.png;*.gif";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                flowLayoutPanel1.BackgroundImage = Image.FromFile(filePath);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            countDown--;
            //MessageBox.Show("Done");
            if (countDown <= 0)
            {
                timer1.Stop();
            }
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Columns[e.ColumnIndex].Name == "hinh_anh" && e.RowIndex >= 0)
            {
                if (e.Value != null && e.Value is Image)
                {
                    Image image = (Image)e.Value;
                    e.Value = ResizeImage(image, 50, 50);
                }
            }
        }
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                modeChat = "multi";
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                string idString = row.Cells["id_nhom"].Value.ToString();
                group_id = Int32.Parse(idString);
                MessageBox.Show(modeChat + "_" + group_id);
                DataTable dt = GetAllMessagesInGroup(group_id);
                DisplayMessagesInFlowLayoutPanel(dt);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Images.Show();
            Images.BringToFront();
        }
    }
}
