using GörselProgramlama;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProgramlama
{
    public partial class OkulYonetim : Form
    {
        public OkulYonetim()
        {
            InitializeComponent();
        }

        IdareDBEntities db = new IdareDBEntities();
        public string deger;

        void listele()
        {
            dataGridView1.DataSource = db.OkulYonetimTabloes.ToList();
            dataGridView1.Columns[4].Visible = false;

            var yonetimlist = db.OkulYonetimTabloes.ToList();


        }
        private void OkulYonetim_Load(object sender, EventArgs e)
        {
            listele();
            cmbyonetim.Items.Add("İdare");
            cmbyonetim.Items.Add("Öğretmen");
            cmbyonetim.Items.Add("Öğrenci İşleri");
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int sum1 = Convert.ToInt32(e.Value);
                if (sum1 == 11)
                {
                    e.Value = "İdare";
                }
                else if (sum1 == 12)
                {
                    e.Value = "Öğretmen";
                }
                else if (sum1 == 13)
                {
                    e.Value = "Öğrenci İşleri";
                }
            }
        }
        public enum YonetimTip
        {
            İdare,Öğretmen,Öğrenciİşleri
        }
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            OkulYonetimTablo ekle = new OkulYonetimTablo();
            if (cmbyonetim.Text == "İdare")
            {
                ekle.YonetimTip = 11;
            }
            else if (cmbyonetim.Text == "Öğretmen")
            {
                ekle.YonetimTip = 12;
            }
            else if (cmbyonetim.Text == "Öğrenci İşleri")
            {
                ekle.YonetimTip = 13;
            }

            db.OkulYonetimTabloes.Add(ekle);
            db.SaveChanges();
            MessageBox.Show("Yönetim Kaydı Oluşturuldu.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtadsoyad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtgorevi.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            int deger = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[3].Value);
            if (deger == 11)
            {
                label5.Text = "İdare";
            }
            else if (deger == 12)
            {
                label5.Text = "Öğretmen";
            }
            else if (deger == 13)
            {
                label5.Text = "Öğrenci İşleri";
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult sor = new DialogResult();
            sor = MessageBox.Show("Seçilen Kayıt Silinecektir. Onaylıyor musunuz?", "Sistem Mesajı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sor == DialogResult.Yes)
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                int YonetimID = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value);


                var yonetimbul = db.OkulYonetimTabloes.Find(YonetimID);
                db.OkulYonetimTabloes.Remove(yonetimbul);
                db.SaveChanges();
                MessageBox.Show("Yönetim Kayıdı Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            btnkaydet.Enabled = true;
            int YonetimID = Convert.ToInt32(txtid.Text);

            var guncelle = db.OkulYonetimTabloes.Find(YonetimID);
            guncelle.AdSoyad = txtadsoyad.Text;
            guncelle.Gorevi = txtgorevi.Text;
            if (label4.Text == "İdare")
            {
                guncelle.YonetimTip = 11;
            }
            else if (label4.Text == "Öğretmen")
            {
                guncelle.YonetimTip = 12;
            }
            else if (label4.Text == "Öğrenci İşleri")
            {
                guncelle.YonetimTip = 13;
            }
            db.SaveChanges();
            MessageBox.Show("Yönetim Kayıdı Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtid.Text);

            var yonetimbul = db.OgrenciTabloes.Find(Id);
            db.OgrenciTabloes.Remove(yonetimbul);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kayıdı Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void cmbyonetim_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = cmbyonetim.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox1.Text;
            var degerler = from item in db.OkulYonetimTabloes
                           where item.AdSoyad.Contains(aranan)
                          // where item.Gorevi.Contains(aranan)
                          // where item.YonetimTip.ToString().Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
