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
using System.Timers;

namespace kurs_ishi
{
    public partial class Form3 : Form
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
        public Form3()
        {
            InitializeComponent();
        }
        bool bolean;
        public int mah_soni()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("select * from maxsulotlar ", conn))
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
        public int kas_soni()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("select * from kunlik_foyda ", conn))
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

        public int mah_soni1()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("select * from  muddati_utgan_maxsulotlar", conn))
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

        public void kassa_hisob(String kassa_nomer)
        {
            for (int j = 0; j < dataGridView3.RowCount; j++)
            {
                dataGridView3.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 5]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM kunlik_foyda", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        mass[i, 4] = reader.GetString(4);
                        i++;
                    }
                    reader.Close();
                }
                double sum = 0;
                double foyda = 0;
                string[] sana = monthCalendar1.SelectionStart.ToString().Split(' ');
                for (int j = 0; j < kas_soni(); j++)
                {
                    if (mass[j, 2].Equals(sana[0]))
                    {
                        sum += Convert.ToDouble(mass[j, 3]);
                        foyda += Convert.ToDouble(mass[j, 4]);
                    }
                }
                string[,] kassa1 = new string[3, 5];
                for (int k = 0; k < i; k++)
                {
                    if (mass[k, 1].Equals(kassa_nomer.ToLower()) && mass[k, 2].Equals(sana[0]))
                    {
                        kassa1[0, 0] = mass[k, 1];
                        kassa1[0, 1] = mass[k, 2];
                        kassa1[0, 2] = mass[k, 3];
                        kassa1[0, 3] = mass[k, 4];
                    }
                }
                kassa1[1, 0] = "";
                kassa1[1, 1] = "";
                kassa1[1, 2] = "";
                kassa1[1, 3] = "";
                kassa1[2, 0] = "Kunlik umumiy hisob";
                kassa1[2, 1] = DateTime.Now.ToString("d");
                kassa1[2, 2] = sum.ToString();
                kassa1[2, 3] = foyda.ToString();

                string[] row = new string[4];

                for (int j = 0; j < 3; j++)
                {
                    row = new string[] { kassa1[j, 0], kassa1[j, 1], kassa1[j, 2], kassa1[j, 3] };
                    dataGridView3.Rows.Add(row);
                }
            }
        }
      
        int mud_ut_soni = 0, bosildi = 0;   
        int satr = 0;
    
        char[] belgi = { ' ', '/', '*', '+', '-', '\'', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '`', '~','.' };
        private void Form3_Load(object sender, EventArgs e)
        {
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label12.Visible = false;
            label11.Text = DateTime.Now.ToString("d"); 
            for(int i = 1; i < kas_soni()-2; i++)
            {
                comboBox1.Items.Add(i);
            }
        }
         private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(belgi)) label5.Visible = false;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!textBox2.Text.Equals(belgi)) label6.Visible = false;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (!textBox3.Text.Equals(belgi)) label7.Visible = false;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!textBox4.Text.Equals(belgi)) label8.Visible = false;
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!textBox6.Text.Equals(belgi)) label12.Visible = false;

        }
        string[,] mass1 = new string[2000, 7];
        private void button5_Click(object sender, EventArgs e)
        {
            int nowyear = int.Parse(DateTime.Now.ToString("yyyy"));
            int nowmounth = int.Parse(DateTime.Now.ToString("MM"));
            int nowday = int.Parse(DateTime.Now.ToString("dd"));
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int k = 0, l = 0;
                string[,] idisi = new string[mah_soni(), 7];
                using (var command = new NpgsqlCommand("SELECT * FROM maxsulotlar", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string[] sanasi = reader.GetString(5).Split(belgi);
                        int yili = int.Parse(sanasi[2]);
                        int oy = int.Parse(sanasi[1]);
                        int kun = int.Parse(sanasi[0]);
                        DateTime start = new DateTime(yili, oy, kun);                       
                        DateTime end = new DateTime(nowyear, nowmounth, nowday);                      
                        TimeSpan difference = start - end;                       
                        int kuni = int.Parse(textBox5.Text);
                        if (difference.Days < kuni)
                        {
                            idisi[l, 0] = reader.GetInt32(0).ToString();
                            idisi[l, 1] = reader.GetString(1);
                            idisi[l, 2] = reader.GetString(2);
                            idisi[l, 3] = reader.GetString(3);
                            idisi[l, 4] = reader.GetInt32(4).ToString();
                            idisi[l, 5] = reader.GetString(5);
                            idisi[l, 6] = reader.GetString(6);
                            l++;
                        }
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                satr = dataGridView2.Rows.Count;
                for (int j = 0; j < l; j++)
                {
                    row = new string[] { idisi[j, 0], idisi[j, 1], idisi[j, 2], idisi[j, 3], idisi[j, 4], idisi[j, 5],idisi[j, 6]};
                    dataGridView2.Rows.Add(row);                    
                }
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int j = 0; j <dataGridView2.RowCount; j++)
            {
                dataGridView2.Rows.Clear();
            }
            string[,] mass = new string[mah_soni1(), 7]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM muddati_utgan_maxsulotlar", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        mass[i, 4] = reader.GetInt32(4).ToString();
                        mass[i, 5] = reader.GetString(5);
                        mass[i, 6] = reader.GetString(6);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                satr = dataGridView2.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3], mass[j, 4], mass[j, 5], mass[j, 6] };
                    dataGridView2.Rows.Add(row);
                    
                }
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!textBox5.Text.Equals(belgi))
            {
                button5.Enabled = true;
            }
            else
            {
                button5.Enabled = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 obj = new Form1();
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < dataGridView3.RowCount; j++)
            {
                dataGridView3.Rows.Clear();
            }
            string[,] mass = new string[kas_soni(), 5]; int i = 0;
            string[] sana = monthCalendar1.SelectionStart.ToString().Split(' ');
            int soni;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM kunlik_foyda where sana='" + sana[0] + "'", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        mass[i, 4] = reader.GetString(4);
                        i++;
                    }
                    soni = i;
                    reader.Close();
                }
                double sum = 0;
                double foyda = 0;

                for (int j = 0; j < soni; j++)
                {
                    sum += Convert.ToDouble(mass[j, 3]);
                    foyda += Convert.ToDouble(mass[j, 4]);
                }
                string[,] kassa1 = new string[3, 5];
                for (int k = 0; k < i; k++)
                {
                    if (mass[k, 1].Equals(label15.Text + " " + comboBox1.SelectedItem.ToString()))
                    {
                        kassa1[0, 0] = mass[k, 1];
                        kassa1[0, 1] = mass[k, 2];
                        kassa1[0, 2] = mass[k, 3];
                        kassa1[0, 3] = mass[k, 4];
                    }
                }
                kassa1[1, 0] = "";
                kassa1[1, 1] = "";
                kassa1[1, 2] = "";
                kassa1[1, 3] = "";
                kassa1[2, 0] = "Kunlik umumiy hisob";
                kassa1[2, 1] = DateTime.Now.ToString("d");
                kassa1[2, 2] = sum.ToString();
                kassa1[2, 3] = foyda.ToString();

                string[] row = new string[4];

                for (int j = 0; j < 3; j++)
                {
                    row = new string[] { kassa1[j, 0], kassa1[j, 1], kassa1[j, 2], kassa1[j, 3] };
                    dataGridView3.Rows.Add(row);
                }
            }
        }

    private void guna2Button1_Click(object sender, EventArgs e)
        {
            bolean = true;
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                dataGridView1.Rows.Clear();
            }
            string[,] mass = new string[mah_soni(), 7]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM maxsulotlar", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass[i, 0] = reader.GetInt32(0).ToString();
                        mass[i, 1] = reader.GetString(1);
                        mass[i, 2] = reader.GetString(2);
                        mass[i, 3] = reader.GetString(3);
                        mass[i, 4] = reader.GetInt32(4).ToString();
                        mass[i, 5] = reader.GetString(5);
                        mass[i, 6] = reader.GetString(6);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                satr = dataGridView1.Rows.Count;
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass[j, 0], mass[j, 1], mass[j, 2], mass[j, 3], mass[j, 4], mass[j, 5], mass[j, 6] };
                    dataGridView1.Rows.Add(row);

                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            guna2Button1.Enabled = false;
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int satr = dataGridView1.Rows.Count;
            for (int i = 0; i < satr - 1; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    mass1[i, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            using (var conn = new NpgsqlConnection(connString))
            {

                Console.Out.WriteLine("Opening connection");
                conn.Open();

                using (var command = new NpgsqlCommand("delete from maxsulotlar", conn))
                {
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }


            using (var conn = new NpgsqlConnection(connString))
            {
                Console.Out.WriteLine("Opening connection");
                conn.Open();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    try
                    {
                        using (NpgsqlCommand cmd = new NpgsqlCommand("insert into maxsulotlar(id, nomi, asl_narxi, sotuv_narxi, miqdori, saqlash_muddati, keltirilgan_sana) values(@id, @nomi, @asl_narxi, @sotuv_narxi, @miqdori, @saqlash_muddati, @keltirilgan_sana)", conn))
                        {
                            if (r.Cells[0] != null && r.Cells[0].Value != null)
                                cmd.Parameters.AddWithValue("@id", int.Parse(r.Cells[0].Value.ToString()));
                            if (r.Cells[1] != null && r.Cells[1].Value != null)
                                cmd.Parameters.AddWithValue("@nomi", r.Cells[1].Value.ToString());
                            if (r.Cells[2] != null && r.Cells[2].Value != null)
                                cmd.Parameters.AddWithValue("@asl_narxi", r.Cells[2].Value.ToString());
                            if (r.Cells[3] != null && r.Cells[3].Value != null)
                                cmd.Parameters.AddWithValue("@sotuv_narxi", r.Cells[3].Value.ToString());
                            if (r.Cells[4] != null && r.Cells[4].Value != null)
                                cmd.Parameters.AddWithValue("@miqdori", int.Parse(r.Cells[4].Value.ToString()));
                            if (r.Cells[5] != null && r.Cells[5].Value != null)
                                cmd.Parameters.AddWithValue("@saqlash_muddati", r.Cells[5].Value.ToString());
                            if (r.Cells[6] != null && r.Cells[6].Value != null)
                                cmd.Parameters.AddWithValue("@keltirilgan_sana", r.Cells[6].Value.ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            guna2Button1.Enabled = true;
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            int k = 0;
            if (textBox1.Text != "") k++; else label5.Visible = true;
            if (textBox2.Text != "") k++; else label6.Visible = true;
            if (textBox3.Text != "") k++; else label7.Visible = true;
            if (textBox4.Text != "") k++; else label8.Visible = true;
            if (textBox6.Text != "") k++; else label12.Visible = true;
            if (k > 4)
            {
                int id = dataGridView1.Rows.Count;
                string nomi = textBox1.Text;
                string soni = textBox2.Text;
                string asl_narxi = textBox6.Text;
                string sotuv_narxi = textBox3.Text;
                string saqlash_m = textBox4.Text;
                string vaqti = DateTime.Now.ToString("d");
                string[] row = new string[6];
                row = new string[] { id.ToString(), nomi, asl_narxi, sotuv_narxi, soni, saqlash_m, vaqti };
                dataGridView1.Rows.Add(row);
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Visible = false;
        }
       
      

        private void button7_Click(object sender, EventArgs e)
        {
            string[,] mass1 = new string[mah_soni1(), 7]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("DELETE FROM muddati_utgan_maxsulotlar", conn))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new NpgsqlCommand("SELECT * FROM muddati_utgan_maxsulotlar", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mass1[i, 0] = reader.GetInt32(0).ToString();
                        mass1[i, 1] = reader.GetString(1);
                        mass1[i, 2] = reader.GetString(2);
                        mass1[i, 3] = reader.GetString(3);
                        mass1[i, 4] = reader.GetInt32(4).ToString();
                        mass1[i, 5] = reader.GetString(5);
                        mass1[i, 6] = reader.GetString(6);
                        i++;
                    }
                    reader.Close();
                }
                string[] row = new string[7];
                satr = dataGridView2.Rows.Count;
                mud_ut_soni = i;
                for (int j = 0; j < i; j++)
                {
                    row = new string[] { mass1[j, 0], mass1[j, 1], mass1[j, 2], mass1[j, 3], mass1[j, 4], mass1[j, 5], mass1[j, 6]};
                    dataGridView2.Rows.Add(row);
                }
            }
        }
    }      
}

