﻿using System;
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
    public partial class ChatForm : Form
    {
        private int id;
        private int receiver;
        private Timer timer;
        private DataTable lastMessageTable;
        private int pollingInterval = 2000;
        public ChatForm()
        {
            InitializeComponent();
        }

        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = pollingInterval;
            timer.Tick += Timer_Tick;
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

        private DataTable GetAllMessages(int senderId, int receiverId)
        {
            DataTable messageTable = new DataTable();

            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
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

        private void DisplayMessagesInFlowLayoutPanel(DataTable messageTable)
        {
            flowLayoutPanel1.Controls.Clear();

            foreach (DataRow row in messageTable.Rows)
            {
                int senderId = int.Parse(row["SenderUsername"].ToString());
                
                if(senderId == id)
                {
                    string message = row["MessageText"].ToString();
                    DateTime sentTime = (DateTime)row["SentTime"];
                    Message messageControl = new Message(message, sentTime);
                    flowLayoutPanel1.Controls.Add(messageControl);
                }
                else
                {
                    string message = row["MessageText"].ToString();
                    DateTime sentTime = (DateTime)row["SentTime"];
                    Message1 messageControl = new Message1(message, sentTime);
                    flowLayoutPanel1.Controls.Add(messageControl);
                }
            }
        }
        private void ChatForm_Load(object sender, EventArgs e)
        {
            DisplayUsersInDataGridView();
        }

        private void LoadLatestMessages()
        {
            DataTable currentMessageTable = GetAllMessages(id, receiver);
            if (lastMessageTable == null || !lastMessageTable.Rows.Cast<DataRow>().SequenceEqual(currentMessageTable.Rows.Cast<DataRow>(), DataRowComparer.Default))
            {
                lastMessageTable = currentMessageTable;
                DisplayMessagesInFlowLayoutPanel(lastMessageTable);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=LAPTOP-HFM62E22\\SQLEXPRESS;Initial Catalog=chatDB;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string query = "INSERT INTO [Message] (SenderUsername, ReceiverUsername, MessageText, SentTime) VALUES (@sender, @receiver, @message, @sentTime)";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@sender", id);
            command.Parameters.AddWithValue("@receiver", receiver);
            command.Parameters.AddWithValue("@message", txtMessage.Text);
            command.Parameters.AddWithValue("@sentTime", DateTime.Now);
            command.ExecuteNonQuery();

            connection.Close();
            LoadLatestMessages();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            addGroup add = new addGroup();
            add.ShowDialog();
        }
    }
}