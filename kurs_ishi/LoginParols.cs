using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;



namespace kurs_ishi
{
    public partial class LoginParols : Form
    {
        private static string Host = "localhost";
        private static string User = "postgres";
        private static string DBname = "algoritm_kurs_ishi";
        private static string Password = "shavkat17#";
        private static string Port = "5432";

        string connString =
              String.Format(
                  "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                  Host,
                  User,
                  DBname,
                  Port,
                  Password);
        public LoginParols()
        {
            InitializeComponent();
        }
        public int kas_soni()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("select * from kassa_login_parols ", conn))
                {

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        k++;
                    }
                    reader.Close();
                }
            }
            return k;
        }
        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form1 kirish = new Form1();
            kirish.Show();
            this.Close();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    if (guna2TextBox4.Text.Equals(guna2TextBox5.Text))
                    {
                        using (var command = new NpgsqlCommand("update admin_login_parol set parol='" + guna2TextBox4.Text + "' where  login='" + login + "'", conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        guna2TextBox5.Text = ""; label10.Text = "Parollar bir xil emas !!!";
                        label10.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    if (guna2TextBox4.Text.Equals(guna2TextBox5.Text))
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("insert into admin_login_parol(login, parol) values(@login, @parol)", conn))
                        {
                            cmd.Parameters.AddWithValue("@login", guna2TextBox3.Text);
                            cmd.Parameters.AddWithValue("@parol", guna2TextBox4.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        guna2TextBox5.Text = ""; label10.Text = "Parollar bir xil emas !!!";
                        label10.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
           
            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                guna2DataGridView1.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 4]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM kassa_login_parols", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3]};
                    guna2DataGridView1.Rows.Add(row);

                }
            }
            guna2Button5.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    if (guna2TextBox4.Text.Equals(guna2TextBox5.Text))
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("insert into kassa_login_parols(nomi,login, parol, holat) values(@nomi, @login, @parol, @holat)", conn))
                        {
                            MessageBox.Show(kas_soni().ToString()+"\t"+ guna2DataGridView1.Rows.Count);
                            cmd.Parameters.AddWithValue("@nomi", "Kassa № "+guna2DataGridView1.Rows.Count+"");
                            cmd.Parameters.AddWithValue("@login", guna2TextBox3.Text);
                            cmd.Parameters.AddWithValue("@parol", guna2TextBox4.Text);
                            cmd.Parameters.AddWithValue("@holat", false);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        guna2TextBox5.Text = "";
                        label10.Visible = true; label10.Text = "Parollar bir xil emas !!!";
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;                    
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }
            }
            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                guna2DataGridView1.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 4]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM kassa_login_parols", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3] };
                    guna2DataGridView1.Rows.Add(row);

                }
            }
        }

        private void LoginParols_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            guna2Button1.Enabled = false;
            guna2Button4.Enabled = false;
            guna2Button2.Enabled = false;
            label6.Text = "Maydonni to'ldiring !!!";
            label7.Text = "Maydonni to'ldiring !!!";
            label8.Text = "Maydonni to'ldiring !!!";
            label9.Text = "Maydonni to'ldiring !!!";
            label10.Text ="Maydonni to'ldiring !!!";
        }
        char[] belgi = { ' ', '/', '*', '+', '-', '\'', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '`', '~', '.'};
        int del = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int k = 0;
            if (!guna2TextBox1.Text.Equals(belgi) && guna2TextBox1.Text!="") { k++; label6.Visible = false; } else label6.Visible = true;
            if (!guna2TextBox2.Text.Equals(belgi) && guna2TextBox2.Text != "") { k++; label7.Visible = false; } else label7.Visible = true; 
            if (!guna2TextBox3.Text.Equals(belgi) && guna2TextBox3.Text != "") { k++; label8.Visible = false; } else label8.Visible = true;
            if (!guna2TextBox4.Text.Equals(belgi) && guna2TextBox4.Text != "") { k++; label9.Visible = false; } else label9.Visible = true; 
            if (!guna2TextBox5.Text.Equals(belgi) && guna2TextBox5.Text != "") { k++; label10.Visible = false; } else label10.Visible = true;
            if (k > 4)
            {
                guna2Button1.Enabled = true;
                guna2Button2.Enabled = true;
                guna2Button4.Enabled = true;
                guna2Button6.Enabled = true;
                guna2Button7.Enabled = true;
                guna2Button8.Enabled = true;
              

            }
        }
       
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            guna2TextBox3.Text= guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            guna2TextBox4.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            guna2TextBox5.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            del++;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {

            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            string login1 = guna2TextBox3.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    if (guna2TextBox4.Text.Equals(guna2TextBox5.Text))
                    {
                        using (var command = new NpgsqlCommand("update kassa_login_parols set parol='" + guna2TextBox4.Text + "' where  login='" + login1 + "'", conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        guna2TextBox5.Text = ""; label10.Text = "Parollar bir xil emas !!!";
                        label10.Visible = true;
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }
            }
            del = 0;
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            string login1 = guna2TextBox3.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    using (var command = new NpgsqlCommand("DELETE FROM kassa_login_parols where nomi='" + guna2TextBox3.Text + "'", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }

                conn.Close();
            }

            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                guna2DataGridView1.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 4]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM kassa_login_parols", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3] };
                    guna2DataGridView1.Rows.Add(row);

                }
                conn.Close();
            }
            guna2Button5.Enabled = false;
            del = 0;
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            string login = guna2TextBox1.Text;
            string parol = guna2TextBox2.Text;
            string login1 = guna2TextBox3.Text;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol where login='" + login + "' and parol='" + parol + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }

                if (count != 0)
                {
                    using (var command = new NpgsqlCommand("DELETE FROM admin_login_parol where parol='" + guna2TextBox4.Text + "'", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label6.Text = "Parol yoki login xato !!!";
                    label7.Text = "Parol yoki login xato !!!";
                    label6.Visible = true;
                    label7.Visible = true;
                    guna2TextBox1.Text = "";
                    guna2TextBox2.Text = "";
                }

                conn.Close();
            }

            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                guna2DataGridView1.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 4]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3] };
                    guna2DataGridView1.Rows.Add(row);

                }
                conn.Close();
            }
            guna2Button5.Enabled = false;
            del = 0;
            guna2TextBox1.Text = "";
            guna2TextBox2.Text = "";
            guna2TextBox3.Text = "";
            guna2TextBox4.Text = "";
            guna2TextBox5.Text = "";
        }
    }
}
