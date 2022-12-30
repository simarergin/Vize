using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GörselProgramlama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Ogrenci ogr = new Ogrenci();
            ogr.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OkulYonetim oklyntm = new OkulYonetim();
            oklyntm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ders ders = new Ders();
            ders.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OgrenciDers ogrders = new OgrenciDers();
            ogrders.Show();
        }
    }
}
