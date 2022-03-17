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
using System.IO;

namespace depo_uygulaması
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }
        public OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=datam.accdb");
        public DataTable tablo = new DataTable();
        public OleDbDataAdapter adtr = new OleDbDataAdapter();
        public OleDbCommand kmt = new OleDbCommand();
        string DosyaAdi = "";
        int id;
        int hareket;
        int Mouse_X;
        int Mouse_Y;

        private void button3_Click(object sender, EventArgs e)
        {
            Gecmis gcms = new Gecmis();
            gcms.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Arama arma = new Arama();
            arma.Show();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            
            try
            {
                baglanti.Open();
                kmt = new OleDbCommand("select * from stokbil where dosyaAdi='" + dataGridView1.CurrentRow.Cells[9].Value.ToString() + "'", baglanti);
                OleDbDataReader oku = kmt.ExecuteReader();
                oku.Read();
                if (oku.HasRows)
                {
                    pictureBox1.ImageLocation = oku[9].ToString();
                    id = Convert.ToInt32(oku[0].ToString());
                }
                baglanti.Close();
            }
            catch
            {
                baglanti.Close();
            }
        }

        private void btnResimSil_Click(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "";
            DosyaAdi = "";
        }

        private void btnResimEkle_Click(object sender, EventArgs e)
        {
            DosyaAc.Filter = "Resim dosyaları |*.jpg;*.jpeg;*.gif;*.bmp;" +
                "*.png;*ico|JPEG Files ( *.jpg;*.jpeg )|*.jpg;*.jpeg|GIF Files ( *.gif )|*.gif|BMP Files ( *.bmp )" +
                "|*.bmp|PNG Files ( *.png )|*.png|Icon Files ( *.ico )|*.ico";
            DosyaAc.Title = "Resim Seçiniz";
            DosyaAc.InitialDirectory = Application.StartupPath + @"\\DataPicture\";
            if (DosyaAc.ShowDialog() == DialogResult.OK)
            {
                DosyaAdi = DosyaAc.FileName.ToString();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.ImageLocation = DosyaAdi;
                File.WriteAllBytes(DosyaAdi, File.ReadAllBytes(DosyaAc.FileName));
                MessageBox.Show("Resim Eklendi !");

            }
            else
            {
                MessageBox.Show("Dosya Girmediniz!");
            }
            DosyaAc.Dispose();
            try
            {
                baglanti.Open();
                kmt.Connection = baglanti;
                kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici) VALUES ('" + "Resim Ekleme İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox8.Text + "') ";
                kmt.ExecuteNonQuery();

                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Geçmiş Kaydetmede Hata!");
            }
        }

        private void btnStokGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap;
                cevap = MessageBox.Show("Güncellemek istediğinizden eminmisiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes && dataGridView1.CurrentRow.Cells[3].Value.ToString().Trim() != "")
                {

                    baglanti.Open();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "update stokbil set stokAdi=@pstokAdi, stokModeli=@pstokModeli, stokSeriNo=@pstokSeriNo, stokSehir=@pstokSehir, stokBayi=@pstokBayi, stokAdedi=@pstokAdedi, stokTarih=@pstokTarih, kayitYapan=@pkayitYapan, dosyaAdi=@pdosyaAdi WHERE stokBarkod='" + dataGridView1.CurrentRow.Cells[3].Value.ToString() + "' ";
                    kmt.Parameters.AddWithValue("@pstokAdi", textBox1.Text);
                    kmt.Parameters.AddWithValue("@pstokModeli", textBox2.Text);
                    kmt.Parameters.AddWithValue("@pstokSeriNo", textBox3.Text);
                    kmt.Parameters.AddWithValue("@pstokSehir", comboBox1.Text);
                    kmt.Parameters.AddWithValue("@pstokBayi", textBox6.Text);
                    kmt.Parameters.AddWithValue("@pstokAdedi", textBox7.Text);
                    kmt.Parameters.AddWithValue("@pstokTarih", dateTimePicker1.Text);
                    kmt.Parameters.AddWithValue("@pkayitYapan", textBox8.Text);
                    kmt.Parameters.AddWithValue("@pdosyaAdi", DosyaAdi);
                    
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    baglanti.Close();
                    listele();


                }
            }
            catch
            {
                MessageBox.Show("Güncelleme İşlemi Yapılamadı !", "HATA!");
                baglanti.Close();
            }
            try
            {
                baglanti.Open();
                kmt.Connection = baglanti;
                kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici) VALUES ('" + "Güncelleme İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox8.Text + "') ";
                kmt.ExecuteNonQuery();

                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Geçmiş Kaydetmede Hata!","HATA!");
            }

        }

        private void btnStokSil_Click(object sender, EventArgs e)
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
                MessageBox.Show("Kayıt Silinemedi !","HATA!");
                baglanti.Close();
            }
            try
            {
                baglanti.Open();
                kmt.Connection = baglanti;
                kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici) VALUES ('" + "Silme İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox8.Text + "') ";
                kmt.ExecuteNonQuery();

                baglanti.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("Geçmiş Kaydetmede Hata!","HATA!");
                baglanti.Close();
            }
            
        }

        private void btnStokEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Trim() == "") errorProvider1.SetError(textBox1, "Boş geçilmez");
                else errorProvider1.SetError(textBox1, "");
                if (textBox2.Text.Trim() == "") errorProvider1.SetError(textBox2, "Boş geçilmez");
                else errorProvider1.SetError(textBox2, "");
                if (textBox3.Text.Trim() == "") errorProvider1.SetError(textBox3, "Boş geçilmez");
                else errorProvider1.SetError(textBox3, "");
                if (textBox7.Text.Trim() == "") errorProvider1.SetError(textBox7, "Boş geçilmez");
                else errorProvider1.SetError(textBox7, "");
                if (textBox8.Text.Trim() == "") errorProvider1.SetError(textBox8, "Boş geçilmez");
                else errorProvider1.SetError(textBox8, "");
                if (textBox4.Text.Trim() == "" && textBox4.Text.Length < 11) errorProvider1.SetError(textBox4, "Boş geçilmez min 11 karakter!");
                else errorProvider1.SetError(textBox4, "");
                if (comboBox1.Text.Trim() == "") errorProvider1.SetError(comboBox1, "Boş geçilmez");
                else errorProvider1.SetError(comboBox1, "");
                if (textBox6.Text.Trim() == "") errorProvider1.SetError(textBox6, "Boş geçilmez");
                else errorProvider1.SetError(textBox6, "");
                



                if (textBox1.Text.Trim() != "" && textBox2.Text.Trim() != "" && textBox3.Text.Trim() != "" && textBox4.Text.Trim() != "" && comboBox1.Text.Trim() != "" && textBox6.Text.Trim() != "" && textBox7.Text.Trim() != "" && textBox8.Text.Trim() != "")
                {
                    baglanti.Open();
                    kmt.Connection = baglanti;
                    kmt.CommandText = "INSERT INTO stokbil(stokAdi,stokModeli,stokSeriNo,stokBarkod,stokSehir,stokBayi,stokAdedi,stokTarih,kayitYapan,dosyaAdi) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + comboBox1.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + dateTimePicker1.Text + "','" + textBox8.Text + "','" + DosyaAdi + "') ";
                    kmt.ExecuteNonQuery();
                    kmt.Dispose();
                    baglanti.Close();
                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (this.Controls[i] is TextBox) this.Controls[i].Text = "";
                    }
                    listele();
                    if (DosyaAdi != "") File.WriteAllBytes(DosyaAdi, File.ReadAllBytes(DosyaAc.FileName));
                    MessageBox.Show("Kayıt İşlemi Tamamlandı ! ", "İşlem Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch
            {
                MessageBox.Show("Boş Bırakılamaz!","HATA!");
                baglanti.Close();
            }
            baglanti.Open();
            kmt.Connection = baglanti;
            kmt.CommandText = "INSERT INTO hareket(hareket,tarih,kullanici)  VALUES ('" + "Ekleme İşlemi Yapılmıştır..." + "','" + DateTime.Now.ToLongDateString() + "','" + textBox8.Text + "') ";

            kmt.ExecuteNonQuery();

            baglanti.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }



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
                dataGridView1.Columns[2].Width = 40;
                dataGridView1.Columns[3].Width = 120;
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 80;
                dataGridView1.Columns[6].Width = 40;
                dataGridView1.Columns[7].Width = 80;
                dataGridView1.Columns[8].Width = 80;
                dataGridView1.Columns[9].Width = 40;
            }
            catch
            {
                baglanti.Close();
            }
        }

    }
}
