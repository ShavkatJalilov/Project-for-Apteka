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
using System.Data;

namespace kurs_ishi
{
    public partial class Form2 : Form
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

        public Form2()
        {
            InitializeComponent();
        }
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
        public int foyda1()
        {
            int k = 0;
            using (var conn = new NpgsqlConnection(connString))
            {

                conn.Open();

                using (var command = new NpgsqlCommand("SELECT * FROM kunlik_foyda where kassalar='" + kassanomeri + "'", conn))
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
        public string kassa()
        {
            string nomi = "";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand("select nomi from kassa_login_parols where holat=true", conn))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        nomi = reader.GetString(0);
                    }
                    reader.Close();
                }
                using (var command = new NpgsqlCommand("update kassa_login_parols set holat=false where nomi='" + nomi + "'", conn))
                {
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
            return nomi;
        }
        double summ = 0;
        double itogo;
        /*  private void button1_Click(object sender, EventArgs e)
          {
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
              }

              string[] row = new string[7];
              string maxsulot = textBox1.Text;
              string soni = textBox2.Text;
              int k = 0;
              for(int j = 0; j < i; j++)
              {
                  if (maxsulot.Equals(mass[j, 1]))
                  {
                      int son = int.Parse(soni);
                      int soni1 = int.Parse(mass[j, 4]);
                      if (son <soni1)
                      {
                          summ = Convert.ToDouble(soni) * Convert.ToDouble(mass[j, 3]);
                          itogo += summ;
                          row = new string[] { mass[j, 1], soni, mass[j, 3], summ.ToString() };
                          dataGridView1.Rows.Add(row);
                          k = 1;
                      }
                      else
                      {

                          MessageBox.Show("Bazada bunday miqdorda ushbu maxsulot mavjud emas !!!"); break;
                      }

                  }
              }
              if (k != 1)
              {              
                 label2.Visible = true;

              }
              else
              {
                  label2.Visible = false;
              }
              textBox3.Text = itogo + " UZS";
          }*/

        /*  private void button4_Click(object sender, EventArgs e)
          {
              Form1 form1 = new Form1();
              form1.Show();
              this.Visible = false;
          }*/
        string kassanomeri;
        private void Form2_Load(object sender, EventArgs e)
        {
            label2.Visible = false;
            textBox3.Text = "0.00 UZS";
            kassanomeri = kassa();

        }
        char[] belgi = { ' ', '/', '*', '+', '-', '\'', '!', '@', '#', '$', '%', '^', '&', '(', ')', '_', '=', '`', '~', '.' };

        Form1 form1 = new Form1();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(belgi))
            {
                guna2Button2.Enabled = false;
                guna2Button1.Enabled = false;
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    int jamisumm = 0;
        //    string[,] mass1 = new string[mah_soni(), 7];
        //    int satr = dataGridView1.Rows.Count;
        //    for (int i = 0; i < satr - 1; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            mass1[i, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();                   
        //        }
        //        jamisumm+=int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
        //    }
        //    string[] row = new string[4];

        //    for(int j=0; j < satr - 1; j++)
        //    {
        //        row = new string[] { mass1[j, 0], mass1[j, 1], mass1[j, 2], mass1[j, 3] };
        //        dataGridView2.Rows.Add(row);
        //    }
        //    richTextBox1.Text = "\t\t'Ziyo Nur Med Farm' MChJ ga\n\t\t\tqarashli Dorixona\n\n\tKassa № 1\t\t"+" "+DateTime.Now.ToString("G")+"\n\nT/R Nomi: \t\t\tSoni: \tNarxi: \tSumma:\n------------------------------------------------------------------------\n";
        //    if (satr != 1)
        //    {
        //        for (int i = 0; i < satr - 1; i++)
        //        {
        //            richTextBox1.Text +=(i + 1) + " " + mass1[i, 0] + "\t" + mass1[i, 1] + "\t" + mass1[i, 2] + "\t" + mass1[i, 3] + "\n";
        //        }
        //    }
        //    if(satr==1) richTextBox1.Text +=" 0\t\t\t\t0\t0\t0";     
        //    richTextBox1.Text += "\n------------------------------------------------------------------------\n\tJami summa: \t\t\t" + jamisumm + "\n\n\tXarid qilingan dorilar qaytarib olinmaydi\n\t\tva almashtirib berilmaydi !!!\n\n\t\tXaridingiz uchun raxmat !!!";

        //    for (int j = 0; j < dataGridView1.RowCount; j++)
        //    {
        //        dataGridView1.Rows.Clear();
        //    }
        //}

        /*  private void button3_Click(object sender, EventArgs e)
          {
              string[,] mass1 = new string[mah_soni(), 7];
              int satr = dataGridView2.Rows.Count;
              for (int i = 0; i < satr - 1; i++)
              {
                  for (int j = 0; j < 4; j++)
                  {
                      mass1[i, j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                  }
              }
              string[] row = new string[4];
              for (int j = 0; j < satr - 1; j++)
              {
                  row = new string[] { mass1[j, 0], mass1[j, 1], mass1[j, 2], mass1[j, 3] };
                  dataGridView1.Rows.Add(row);
              }
              for (int j = 0; j < dataGridView1.RowCount; j++)
              {
                  dataGridView2.Rows.Clear();
              }
              richTextBox1.Text = "";
          }
        */
        /*       private void button5_Click(object sender, EventArgs e)
               {
                   double aslsumm = 0;
                   double jamisumm = 0;
                   double foyda=0;
                   double soni1 = 0;
                   string[,] mass1 = new string[mah_soni(), 7];
                   int satr = dataGridView2.Rows.Count;
                   for (int i = 0; i < satr - 1; i++)
                   {
                       for (int k = 0; k < 4; k++)
                       {
                           mass1[i, k] = dataGridView2.Rows[i].Cells[k].Value.ToString();
                       }
                       jamisumm += int.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString());
                   }
                   string[,] mah = new string[mah_soni(), 7]; int j = 0;
                   using (var conn = new NpgsqlConnection(connString))
                   {
                       conn.Open();
                       using (var command = new NpgsqlCommand("SELECT * FROM maxsulotlar", conn))
                       {
                           var reader = command.ExecuteReader();
                           while (reader.Read())
                           {
                               mah[j, 0] = reader.GetInt32(0).ToString();
                               mah[j, 1] = reader.GetString(1);
                               mah[j, 2] = reader.GetString(2);
                               mah[j, 3] = reader.GetString(3);
                               mah[j, 4] = reader.GetInt32(4).ToString();
                               mah[j, 5] = reader.GetString(5);
                               mah[j, 6] = reader.GetString(6);
                               j++;
                           }
                           reader.Close();
                       }                
                       for (int h = 0; h < j; h++)
                       {
                           if (mah[h, 1].Equals(mass1[h, 0]))
                           {                        
                               mah[h, 4] = (Convert.ToDouble(mah[h, 4]) - Convert.ToDouble(mass1[h, 1])).ToString();
                               double a = Convert.ToDouble(mah[h, 2]);
                               double soni = Convert.ToDouble(mass1[h, 1]);                       
                               aslsumm = a;
                               soni1 = soni;                        
                           }
                       }             
                       double aslnarxi =soni1*aslsumm;
                       foyda = jamisumm - aslnarxi;
                       using (var command = new NpgsqlCommand("DELETE FROM maxsulotlar", conn))
                       {
                           command.ExecuteNonQuery();
                       }

                       for (int i = 0; i < j; i++)
                       {
                           using (NpgsqlCommand cmd = new NpgsqlCommand("insert into maxsulotlar(id, nomi, asl_narxi, sotuv_narxi, miqdori, saqlash_muddati, keltirilgan_sana) values(@id, @nomi, @asl_narxi, @sotuv_narxi, @miqdori, @saqlash_muddati, @keltirilgan_sana)", conn))
                           {
                               cmd.Parameters.AddWithValue("@id", int.Parse(mah[i,0]));
                               cmd.Parameters.AddWithValue("@nomi", mah[i, 1]);
                               cmd.Parameters.AddWithValue("@asl_narxi", mah[i,2]);
                               cmd.Parameters.AddWithValue("@sotuv_narxi", mah[i, 3]);
                               cmd.Parameters.AddWithValue("@miqdori", Convert.ToDouble(mah[i, 4]));
                               cmd.Parameters.AddWithValue("@saqlash_muddati", mah[i, 5]);
                               cmd.Parameters.AddWithValue("@keltirilgan_sana", mah[i, 6]);
                               cmd.ExecuteNonQuery();                      
                           }
                       }
                       int f1 = 0;
                       string[,] mass2 = new string[2000, 5];                
                       using(var command2=new NpgsqlCommand("SELECT * FROM kunlik_foyda", conn))
                       {
                           int f=0;
                           var reader = command2.ExecuteReader();
                           while (reader.Read())
                           {
                               mass2[f, 0] = reader.GetInt32(0).ToString();
                               mass2[f, 1] = reader.GetString(1);
                               mass2[f, 2] = reader.GetString(2);
                               mass2[f, 3] = reader.GetString(3);
                               mass2[f, 4] = reader.GetString(4);                       
                               f++;                       
                           }
                           f1 = f;
                           reader.Close();
                       }
                       int nowyear = int.Parse(DateTime.Now.ToString("yyyy"));
                       int nowmounth = int.Parse(DateTime.Now.ToString("MM"));
                       int nowday = int.Parse(DateTime.Now.ToString("dd"));
                       for(int k = 0; k < f1; k++)
                       {
                           string[] sanasi = mass2[k, 2].Split(belgi);
                           int yili = int.Parse(sanasi[2]);
                           int oy = int.Parse(sanasi[1]);
                           int kun = int.Parse(sanasi[0]);
                           DateTime start = new DateTime(yili, oy, kun);
                           DateTime end = new DateTime(nowyear, nowmounth, nowday);
                           TimeSpan difference = start - end;
                           if (difference.Days == 0)
                           {                       
                               mass2[k, 3] = (Convert.ToDouble(mass2[k, 3]) + jamisumm).ToString();
                               mass2[k, 4] =(Convert.ToDouble(mass2[k,4])+ foyda).ToString();
                               MessageBox.Show("ishladi2");
                           }
                           if(difference.Days>=1)
                           {
                               mass2[k, 0] = (f1 + 1).ToString();
                               mass2[k, 1] = "Kassa № 1";
                               mass2[k, 2] = DateTime.Now.ToString("d");
                               mass2[k, 3] = jamisumm.ToString();
                               mass2[k, 4] = foyda.ToString();
                           }
                       }                
                      using (var command = new NpgsqlCommand("DELETE FROM kunlik_foyda", conn))
                       {
                           command.ExecuteNonQuery();
                       }
                       try
                       {
                           for (int i = 0; i < mass2.Length; i++)
                           {
                               using (NpgsqlCommand cmd = new NpgsqlCommand("insert into kunlik_foyda(kassalar, sana, kassaga_kirim, foyda) values(@kassalar, @sana, @kassaga_kirim, @foyda)", conn))
                               { 
                                   cmd.Parameters.AddWithValue("@kassalar", mass2[i, 1]);
                                   cmd.Parameters.AddWithValue("@sana", mass2[i, 2]);
                                   cmd.Parameters.AddWithValue("@kassaga_kirim", mass2[i, 3]);
                                   cmd.Parameters.AddWithValue("@foyda", mass2[i, 4]);
                                   cmd.ExecuteNonQuery();
                               }
                           }
                           conn.Close();
                       }
                          catch(Exception ex) { }
                       richTextBox1.Text = "\t\t'Ziyo Nur Med Farm'MChJ ga\n\t\t\tqarashli Dorixona\n\n\tKassa № 1\t\t";
                       textBox1.Text = "";
                       textBox2.Text = "";
                       textBox3.Text = "0 UZS";
                       for (int s = 0; s < dataGridView2.RowCount; s++)
                       {
                           dataGridView2.Rows.Clear();
                       }
                   } 
               }
        */
        private void Form2_SizeChanged(object sender, EventArgs e)
        {

            flowLayoutPanel1.Width = this.Width * 50 / 100;
            guna2DataGridView1.Width = this.Width / 2;
            richTextBox1.Width = this.Width / 2;
            splitContainer1.Width = this.Width;
            splitContainer1.Height = this.Height * 70 / 100;
            /*guna2DataGridView1.Top = this.Height / 5;*/
            richTextBox1.Top = this.Height / 5;
            guna2Button2.Left = this.Width / 2 + guna2Button2.Width - 50;
            guna2Button4.Left = this.Width - guna2Button4.Width * 2 + 38;
            guna2Button5.Left = this.Width - guna2Button5.Width * 3;
            guna2Button2.Top = this.Height / 27;
            guna2Button4.Top = this.Height / 27;
            guna2Button5.Top = this.Height / 27;
            /*label5.Left = this.Width- label5.Width*2+140;
            label5.Top = this.Height / 10;*/

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label5.Text = DateTime.Now.ToString("G");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
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
            }

            string[] row = new string[7];
            string maxsulot = textBox1.Text;
            string soni = textBox2.Text;
            int k = 0;
            for (int j = 0; j < i; j++)
            {
                if (maxsulot.Equals(mass[j, 1]))
                {
                    int son = int.Parse(soni);
                    int soni1 = int.Parse(mass[j, 4]);
                    if (son < soni1)
                    {
                        summ = Convert.ToDouble(soni) * Convert.ToDouble(mass[j, 3]);
                        itogo += summ;
                        row = new string[] { mass[j, 1], soni, mass[j, 3], summ.ToString() };
                        guna2DataGridView1.Rows.Add(row);
                        k = 1;
                    }
                    else
                    {
                        MessageBox.Show("Bazada bunday miqdorda ushbu maxsulot mavjud emas !!!"); break;
                    }

                }
            }
            if (k != 1)
            {
                label2.Visible = true;

            }
            else
            {
                label2.Visible = false;
            }
            textBox3.Text = itogo + " UZS";
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            int jamisumm = 0;
            string[,] mass1 = new string[mah_soni(), 7];
            int satr = guna2DataGridView1.Rows.Count;
            for (int i = 0; i < satr - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mass1[i, j] = guna2DataGridView1.Rows[i].Cells[j].Value.ToString();
                }
                jamisumm += int.Parse(guna2DataGridView1.Rows[i].Cells[3].Value.ToString());
            }
            string[] row = new string[4];

            for (int j = 0; j < satr - 1; j++)
            {
                row = new string[] { mass1[j, 0], mass1[j, 1], mass1[j, 2], mass1[j, 3] };
                dataGridView2.Rows.Add(row);
            }

            richTextBox1.Text = "\t\t'Ziyo Nur Med Farm' MChJ ga\n\t\t\tqarashli Dorixona\n\n\t" + kassanomeri + "\t\t" + " " + DateTime.Now.ToString("G") + "\n\nT/R Nomi: \t\t\tSoni: \tNarxi: \tSumma:\n------------------------------------------------------------------------\n";
            if (satr != 1)
            {
                for (int i = 0; i < satr - 1; i++)
                {
                    richTextBox1.Text += (i + 1) + " " + mass1[i, 0] + "\t\t\t" + mass1[i, 1] + "\t" + mass1[i, 2] + "\t" + mass1[i, 3] + "\n";
                }
            }
            if (satr == 1) richTextBox1.Text += " 0\t\t\t\t0\t0\t0";
            richTextBox1.Text += "\n------------------------------------------------------------------------\n\tJami summa: \t\t\t" + jamisumm + "\n\n\tXarid qilingan dorilar qaytarib olinmaydi\n\t\tva almashtirib berilmaydi !!!\n\n\t\tXaridingiz uchun raxmat !!!";

            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                guna2DataGridView1.Rows.Clear();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string[,] mass1 = new string[mah_soni(), 7];
            int satr = dataGridView2.Rows.Count;
            for (int i = 0; i < satr - 1; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    mass1[i, j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }
            string[] row = new string[4];
            for (int j = 0; j < satr - 1; j++)
            {
                row = new string[] { mass1[j, 0], mass1[j, 1], mass1[j, 2], mass1[j, 3] };
                guna2DataGridView1.Rows.Add(row);
            }
            for (int j = 0; j < guna2DataGridView1.RowCount; j++)
            {
                dataGridView2.Rows.Clear();
            }
            richTextBox1.Text = "";
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();
            this.Visible = false;
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            double aslsumm = 0;
            double jamisumm = 0;
            double foyda = 0;
            double soni1 = 0;
            string[,] mass1 = new string[mah_soni(), 7];
            int satr = dataGridView2.Rows.Count;
            for (int i = 0; i < satr - 1; i++)
            {
                for (int k = 0; k < 4; k++)
                {
                    mass1[i, k] = dataGridView2.Rows[i].Cells[k].Value.ToString();
                }
                jamisumm += Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString());
            }
            string[,] mah = new string[mah_soni(), 7]; int j = 0;
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                for (int h1 = 0; h1 < dataGridView2.Rows.Count; h1++)
                {
                  

                    using (var command = new NpgsqlCommand("SELECT * FROM maxsulotlar where nomi='" + mass1[h1, 0] + "'", conn))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            mah[j, 0] = reader.GetInt32(0).ToString();
                            mah[j, 1] = reader.GetString(1);
                            mah[j, 2] = reader.GetString(2);
                            mah[j, 3] = reader.GetString(3);
                            mah[j, 4] = reader.GetInt32(4).ToString();
                            mah[j, 5] = reader.GetString(5);
                            mah[j, 6] = reader.GetString(6);
                            j++;
                        }
                        reader.Close();
                    }
                }
                for (int k1 = 0; k1 < dataGridView2.Rows.Count-1; k1++)
                {
                    string soni = Convert.ToString(Convert.ToInt32(mah[k1, 4]) - Convert.ToInt32(mass1[k1, 1]));
                    using (var command = new NpgsqlCommand("update maxsulotlar set miqdori='" + soni + "' where nomi='" + mass1[k1, 0] + "'", conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                for (int g = 0; g < dataGridView2.Rows.Count; g++)
                {
                    using (var command2 = new NpgsqlCommand("SELECT asl_narxi FROM maxsulotlar where nomi='" + mass1[g, 0] + "'", conn))
                    {
                        var reader1 = command2.ExecuteReader();
                        while (reader1.Read())
                        {
                            aslsumm += Convert.ToDouble(reader1.GetString(0)) * Convert.ToDouble(mass1[g, 1]);
                        }
                        reader1.Close();
                    }
                }
                foyda = jamisumm - aslsumm;
                aslsumm = 0;
                int f1 = 0;
                string[,] mass2 = new string[foyda1(), 5];
                using (var command2 = new NpgsqlCommand("SELECT * FROM kunlik_foyda where kassalar='" + kassanomeri + "' and sana='" + DateTime.Now.ToString("d") + "'", conn))
                {
                    int f = 0;
                    var reader1 = command2.ExecuteReader();
                    while (reader1.Read())
                    {

                        mass2[f, 3] = reader1.GetString(3);
                        mass2[f, 4] = reader1.GetString(4);
                        f++;
                    }
                    f1 = f;
                    reader1.Close();
                }
                int nowyear = int.Parse(DateTime.Now.ToString("yyyy"));
                int nowmounth = int.Parse(DateTime.Now.ToString("MM"));
                int nowday = int.Parse(DateTime.Now.ToString("dd"));
                if (f1 == 0)
                {
                    using (NpgsqlCommand cmd = new NpgsqlCommand("insert into kunlik_foyda(kassalar, sana, kassaga_kirim, foyda) values(@kassalar, @sana, @kassaga_kirim, @foyda)", conn))
                    {
                        cmd.Parameters.AddWithValue("@kassalar", kassanomeri);
                        cmd.Parameters.AddWithValue("@sana", DateTime.Now.ToString("d"));
                        cmd.Parameters.AddWithValue("@kassaga_kirim", jamisumm);
                        cmd.Parameters.AddWithValue("@foyda", foyda);
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    for (int k = 0; k < foyda1(); k++)
                    {
                        string summasi = (Convert.ToDouble(mass2[k, 3]) + jamisumm).ToString();
                        string foydasi = (Convert.ToDouble(mass2[k, 4]) + foyda).ToString();
                        using (var command = new NpgsqlCommand("update kunlik_foyda set foyda='" + foydasi + "', kassaga_kirim='" + summasi + "' where kassalar='" + kassanomeri + "' and sana='" + DateTime.Now.ToString("d") + "'", conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                }
                richTextBox1.Text = "\t\t'Ziyo Nur Med Farm'MChJ ga\n\t\t\tqarashli Dorixona\n\n\t" + kassanomeri + "\t\t" +label5.Text;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "0 UZS";
                for (int s = 0; s < dataGridView2.RowCount; s++)
                {
                    dataGridView2.Rows.Clear();
                }
            }
        }
    }
}
