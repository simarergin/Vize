using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class Ogrenci
    {
        public Ogrenci()
        {
            OgrenciDers = new HashSet<OgrenciDer>();
        }

        public int Id { get; set; }
        public string AdSoyad { get; set; } = null!;
        public int KayitTarih { get; set; }
        public int OgrenciNo { get; set; }
        public DateTime DTarih { get; set; }
        public string Bolum { get; set; } = null!;

        public virtual ICollection<OgrenciDer> OgrenciDers { get; set; }
    }
}
