using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace depo_uygulaması
{
    public partial class Arama : Form
    {
        public Arama()
        {
            InitializeComponent();
        }
        public OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Ace.Oledb.12.0;Data Source=datam.accdb");
        public DataTable tablo = new DataTable();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        public void listele()
        {
            tablo.Clear();
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select stokAdi,stokModeli,stokSeriNo,stokBarkod,stokSehir,stokBayi,stokAdedi,stokTarih,kayitYapan,dosyaAdi From stokbil", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            adtr.Dispose();
            baglanti.Close();
            try
            {
                
                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridView1.Columns[0].HeaderText = "STOK ADI";
                dataGridView1.Columns[1].HeaderText = "STOK MODELİ";
                dataGridView1.Columns[2].HeaderText = "STOK SERİNO";
                dataGridView1.Columns[3].HeaderText = "BARKOD NO";
                dataGridView1.Columns[4].HeaderText = "STOK ŞEHİR";
                dataGridView1.Columns[5].HeaderText = "STOK BAYİ";
                dataGridView1.Columns[6].HeaderText = "STOK ADEDİ";
                dataGridView1.Columns[7].HeaderText = "STOK TARİH";
                dataGridView1.Columns[8].HeaderText = "KAYIT YAPAN";
                dataGridView1.Columns[9].HeaderText = "Resim Yolu";
                
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].Width = 70;
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 70;
                dataGridView1.Columns[7].Width = 80;
                dataGridView1.Columns[8].Width = 80;
                dataGridView1.Columns[9].Width = 60;
            }
            catch
            {
                baglanti.Close();
            }
        }
        private void btnStokModelAra_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter adtr = new OleDbDataAdapter("select * From stokbil", baglanti);
            if (radioButton1.Checked == true)
            {
                if (textBox6.Text.Trim() == "")
                {
                    tablo.Clear();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "Select * from stokbil";
                    adtr.SelectCommand = kmt;
                    adtr.Fill(tablo);
                }
                if (Convert.ToBoolean(baglanti.State) == false)
                {
                    baglanti.Open();
                }
                if (textBox6.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil where(stokAdi='" + textBox6.Text + "' )";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    baglanti.Close();
                }


            }
            else if (radioButton2.Checked == true)
            {
                if (textBox6.Text.Trim() == "" && textBox1.Text.Trim() != "")
                {
                    tablo.Clear();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "Select * from stokbil";
                    adtr.SelectCommand = kmt;
                    adtr.Fill(tablo);
                }
                if (Convert.ToBoolean(baglanti.State) == false)
                {
                    baglanti.Open();
                }
                if (textBox6.Text.Trim() != "" && textBox1.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil where(stokModeli='" + textBox6.Text + "' )";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    baglanti.Close();
                }

            }
            else if (radioButton3.Checked == true)
            {
                if (textBox6.Text.Trim() == "" )
                {
                    tablo.Clear();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "Select * from stokbil";
                    adtr.SelectCommand = kmt;
                    adtr.Fill(tablo);
                }
                if (Convert.ToBoolean(baglanti.State) == false)
                {
                    baglanti.Open();
                }
                if (textBox6.Text.Trim() != "" && textBox1.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil where(stokSehir='" + textBox6.Text + "' )";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    baglanti.Close();
                }
            }
            else if(radioButton4.Checked == true)
            {
                if (textBox6.Text.Trim() == "")
                {
                    tablo.Clear();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "Select * from stokbil";
                    adtr.SelectCommand = kmt;
                    adtr.Fill(tablo);
                }
                if (Convert.ToBoolean(baglanti.State) == false)
                {
                    baglanti.Open();
                }
                if (textBox6.Text.Trim() != "" && textBox1.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil where(stokBarkod='" + textBox6.Text + "' )";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    baglanti.Close();
                }
            }
            else if(radioButton5.Checked == true)
            {
                if(textBox6.Text.Trim() == "")
                {
                    tablo.Clear();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "Select * from stokbil";
                    adtr.SelectCommand = kmt;
                    adtr.Fill(tablo);
                }
                if(Convert.ToBoolean(baglanti.State) == false)
                {
                    baglanti.Open();
                }
                if(textBox6.Text.Trim() != "" && textBox1.Text.Trim() != "")
                {
                    adtr.SelectCommand.CommandText = " Select * From stokbil where(stokBayi='" + textBox6.Text + "')";
                    tablo.Clear();
                    adtr.Fill(tablo);
                    baglanti.Close();
                }
                baglanti.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı Adı Boş Bırakılamaz.");
                baglanti.Close();
            }
            baglanti.Open();
            kmt.Connection = baglanti;
            kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici)  VALUES ('" + "Arama İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox1.Text + "') ";

            kmt.ExecuteNonQuery();

            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnaSayfa giris = new AnaSayfa();
            giris.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void Arama_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Kaydı silmek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() != "")
                {
                    baglanti.Open();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "DELETE from stokbil WHERE stokBarkod='" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "' ";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    baglanti.Close();
                    listele();
                }

            }
            catch
            {
                MessageBox.Show("Kayıt Silme İşlemi Gerçekleştirilemedi !");
                baglanti.Close();
            }
            try
            {
                baglanti.Open();
                kmt.Connection = baglanti;
                kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici) VALUES ('" + "Silme İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox1.Text + "') ";
                kmt.ExecuteNonQuery();

                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Hareket Kayıt Edilemedi !");
                baglanti.Close();
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            pictureBox2.ImageLocation = dataGridView1.CurrentRow.Cells[9].Value.ToString();
        }
    }
}
