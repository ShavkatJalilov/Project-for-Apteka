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
{    public partial class Form1 : Form
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
        public Form1()
        {
            InitializeComponent();
        }
        char[] belgi = { ' ', '/', '*', '+', '-', '\'', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '`', '~','.'};

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
        public int admin_soni()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("select * from admin_login_parol ", conn))
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
        public void kas_nomeri(string kassa) {
            MessageBox.Show("ishladi");
        using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("update kassa_login_parols set holat=true where login='"+kassa+"'", conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }          
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {

                guna2Button1.Enabled = true;
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {

                guna2Button1.Enabled = true;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Visible = false;
            label5.Visible = false;

            guna2Button1.Enabled = false;

            int nowyear = int.Parse(DateTime.Now.ToString("yyyy"));
            int nowmounth = int.Parse(DateTime.Now.ToString("MM"));
            int nowday = int.Parse(DateTime.Now.ToString("dd"));
           

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                int k = 0, l=0;

                string[,] idisi = new string[mah_soni(),6];


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
                        TimeSpan difference = start-end;                       
                        if (difference.Days<1)
                        {
                            idisi[l,0] = reader.GetInt32(0).ToString();
                            idisi[l, 1] = reader.GetString(1);
                            idisi[l, 2] = reader.GetString(2);
                            idisi[l, 3] = reader.GetInt32(3).ToString();
                            idisi[l, 4] = reader.GetString(4);
                            idisi[l, 5] = reader.GetString(5);
                            l++;
                        }
                    }
                    reader.Close();
                }
                int tr = mah_soni1();
                for (int i = 0; i < l; i++)
                {
                    tr++;
                    using (NpgsqlCommand command1 = new NpgsqlCommand("insert into muddati_utgan_maxsulotlar(id, nomi, narxi, miqdori, saqlash_muddati, keltirilgan_sana) values(@id, @nomi, @narxi, @miqdori, @saqlash_muddati, @keltirilgan_sana)", conn))
                    {
                        MessageBox.Show("ishladi2");
                        command1.Parameters.AddWithValue("@id", tr);
                        command1.Parameters.AddWithValue("@nomi", idisi[i,1]);
                        command1.Parameters.AddWithValue("@narxi", idisi[i,2]);
                        command1.Parameters.AddWithValue("@miqdori", int.Parse(idisi[i,3]));
                        command1.Parameters.AddWithValue("@saqlash_muddati", idisi[i,4]);
                        command1.Parameters.AddWithValue("@keltirilgan_sana", idisi[i,5]);
                        command1.ExecuteNonQuery();
                    using (var command2 = new NpgsqlCommand("delete from maxsulotlar where id=" + idisi[i,0], conn))
                        {
                            MessageBox.Show("o`chirdi");
                            command2.ExecuteNonQuery();
                        }
                    }
                }
                conn.Close();
            }
        }
    /*    private void button2_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string parol = textBox2.Text;

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string login1 = "", parol1 = "";int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM kassa_login_parols", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if(login.Equals(reader.GetString(2)) && parol.Equals(reader.GetString(3)))
                        {
                            count++;
                        }
                    }
                    reader.Close();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        login1 = reader.GetString(1);
                        parol1 = reader.GetString(2);
                    }
                    reader.Close();
                }

                if (login.Equals(login1) && parol.Equals(parol1))
                {
                    Form3 form3 = new Form3();
                    form3.Show();
                    this.Visible = false;
                }
               else if (count!=0)
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label4.Visible = true;
                    label5.Visible = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }

        }*/

       

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            label1.Left = this.Width / 2 - label1.Width / 2;
                 
            guna2Button1.Left = this.Width / 2 - guna2Button1.Width/2;
            guna2Button2.Left = this.Width - guna2Button2.Width *3;
            label1.Top = this.Height / 5+50;

            guna2Button1.Top = this.Height / 2 + 200;
            guna2Button2.Top = this.Height / 2 + 200;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string login = textBox1.Text;
            string parol = textBox2.Text;
            string[,] loginparol = new string[ admin_soni(),3];int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        loginparol[i, 1] = reader.GetString(1);
                        loginparol[i, 2] = reader.GetString(2);
                        i++;
                       
                    }
                    reader.Close();
                }
                int count = 0;
                for(int j = 0; j < admin_soni(); j++)
                {
                    if (login.Equals(loginparol[j,1]) && parol.Equals(loginparol[j,2]))
                    {
                        LoginParols loginParols = new LoginParols();
                        loginParols.Show();
                        this.Visible = false;
                        count++;
                    }
                }
                if (count == 0)
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label4.Visible = true;
                    label5.Visible = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
              
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
           
            string login = textBox1.Text;
            string parol = textBox2.Text;
            string[,] loginparol = new string[admin_soni(), 3]; int i = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string login1 = "", parol1 = ""; int count = 0;
                using (var command = new NpgsqlCommand("SELECT * FROM kassa_login_parols", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (login.Equals(reader.GetString(2)) && parol.Equals(reader.GetString(3)))
                        {
                            kas_nomeri(reader.GetString(2));
                            count++;
                        }
                    }
                    reader.Close();
                }
                using (var command = new NpgsqlCommand("SELECT * FROM admin_login_parol", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        loginparol[i, 1] = reader.GetString(1);
                        loginparol[i, 2] = reader.GetString(2);
                        i++;
                    }
                    reader.Close();
                }

                int admini = 0;
                for(int j = 0; j < admin_soni(); j++){
                    if (login.Equals(loginparol[j, 1]) && parol.Equals(loginparol[j, 2]))
                    {
                        Form3 form3 = new Form3();
                        form3.Show();
                        this.Visible = false;
                        admini++;
                    }
                }
                if (admini == 0)
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label4.Visible = true;
                    label5.Visible = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
              
                if (count != 0 )
                {
                    Form2 form2 = new Form2();
                    form2.Show();
                    this.Visible = false;
                }
                else if(count==0)
                {
                    MessageBox.Show("Parol yoki login xato !!!\n Iltimos qaytadan kiritng");
                    label4.Visible = true;
                    label5.Visible = true;
                    textBox1.Text = "";
                    textBox2.Text = "";
                }
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
