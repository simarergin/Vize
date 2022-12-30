using GörselProgramlama;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GörselProgramlama
{
    public partial class OgrenciDers : Form
    {
        public OgrenciDers()
        {
            InitializeComponent();
        }

        IdareDBEntities db = new IdareDBEntities();

        void listele()
        {
            dataGridView1.DataSource = (from x in db.OgrenciDersTabloes
                                        select new
                                        {
                                            x.Id,
                                            x.DersTablo.Ad,
                                            x.OgrenciTablo.AdSoyad,

                                        }).ToList();

            var derslist = db.DersTabloes.ToList();
        }
        private void OgrenciDers_Load(object sender, EventArgs e)
        {
            listele();
            var ogrenciler = (from x in db.OgrenciTabloes
                              select new
                              {
                                  x.Id,
                                  x.AdSoyad

                              }).ToList();

            cmbogrenci.ValueMember = "Id";
            cmbogrenci.DisplayMember = "AdSoyad";
            cmbogrenci.DataSource = ogrenciler; listele();


            var dersler = (from x in db.DersTabloes
                           select new
                           {
                               x.Id,
                               x.Ad

                           }).ToList();

            cmbders.ValueMember = "Id";
            cmbders.DisplayMember = "Ad";
            cmbders.DataSource = dersler; listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            cmbders.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            cmbogrenci.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();

        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult sor = new DialogResult();
            sor = MessageBox.Show("Seçilen Kayıt Silinecektir. Onaylıyor musunuz?", "Sistem Mesajı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sor == DialogResult.Yes)
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                int ogrencidersID = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value);


                var ogrencidersbul = db.OgrenciDersTabloes.Find(ogrencidersID);
                db.OgrenciDersTabloes.Remove(ogrencidersbul);
                db.SaveChanges();
                MessageBox.Show("Ders Ve Öğrenci İlişkisi Silinecektir. Onaylıyor musunuz?", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            OgrenciDersTablo ekle = new OgrenciDersTablo();
            ekle.DersId = Convert.ToInt16(cmbders.SelectedValue);
            ekle.OgrenciId = Convert.ToInt16(cmbogrenci.SelectedValue);
            db.OgrenciDersTabloes.Add(ekle);
            db.SaveChanges();
            MessageBox.Show("Öğrenciye Ders Ataması Yapıldı.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int DersID = Convert.ToInt32(txtid.Text);

            var guncelle = db.OgrenciDersTabloes.Find(DersID);
            guncelle.Id = Convert.ToInt32(txtid.Text);
            guncelle.DersId = Convert.ToInt32(cmbders.Text);
            guncelle.OgrenciId = Convert.ToInt16(cmbogrenci.SelectedValue);
            db.SaveChanges();
            MessageBox.Show("Ders Kayıdı Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtid.Text);

            var ogrencidersbul = db.OgrenciDersTabloes.Find(Id);
            db.OgrenciDersTabloes.Remove(ogrencidersbul);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kayıdı Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox1.Text;
            var degerler = from item in db.OgrenciDersTabloes
                           where item.DersId.ToString().Contains(aranan)
                           where item.OgrenciId.ToString().Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
