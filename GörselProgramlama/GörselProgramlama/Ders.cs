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
    public partial class Ders : Form
    {
        public Ders()
        {
            InitializeComponent();
        }

        IdareDBEntities db = new IdareDBEntities();

        void listele()
        {
            dataGridView1.DataSource = (from x in db.DersTabloes
                                        select new
                                        {
                                            x.Id,
                                            x.Ad,
                                            x.Kredisi,
                                            x.OkulYonetimId

                                        }).ToList();

            var derslist = db.DersTabloes.ToList();

        }
        private void Ders_Load(object sender, EventArgs e)
        {
            listele();
        }
        private void btnkaydet_Click(object sender, EventArgs e)
        {

            DersTablo ekle = new DersTablo();
            ekle.Ad = txtad.Text;
            ekle.Kredisi = Convert.ToDouble(txtkredi.Text);
            ekle.OkulYonetimId = Convert.ToInt32(txtokulyonetim.Text);
            db.DersTabloes.Add(ekle);
            db.SaveChanges();
            MessageBox.Show("Ders Kaydedildi.", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

            txtid.Clear();
            txtad.Clear();
            txtkredi.Clear();
            txtokulyonetim.Clear();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtkredi.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txtokulyonetim.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult sor = new DialogResult();
            sor = MessageBox.Show("Seçilen Kayıt Silinecektir. Onaylıyor musunuz?", "Sistem Mesajı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (sor == DialogResult.Yes)
            {
                int secilen = dataGridView1.SelectedCells[0].RowIndex;
                int DersID = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value);


                var dersbul = db.DersTabloes.Find(DersID);
                db.DersTabloes.Remove(dersbul);
                db.SaveChanges();
                MessageBox.Show("Ders Kayıdı Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            int DersID = Convert.ToInt32(txtid.Text);

            var guncelle = db.DersTabloes.Find(DersID);
            guncelle.Ad = txtad.Text;
            guncelle.Kredisi = Convert.ToInt32(txtkredi.Text);
            guncelle.OkulYonetimId = Convert.ToInt32(txtokulyonetim.Text);
            db.SaveChanges();
            MessageBox.Show("Ders Kayıdı Güncellendi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            int Id = Convert.ToInt32(txtid.Text);

            var dersbul = db.DersTabloes.Find(Id);
            db.DersTabloes.Remove(dersbul);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kayıdı Silindi", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }
        //private void button2_Click(object sender, EventArgs e)

        // listele();


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string aranan = textBox1.Text;
            var degerler = from item in db.DersTabloes
                           where item.Ad.Contains(aranan)
                          // where item.Kredisi.ToString().Contains(aranan)
                          //  where item.OkulYonetimId.ToString().Contains(aranan)
                           select item;
            dataGridView1.DataSource = degerler.ToList();
        }
    }
}
