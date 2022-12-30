using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class OkulYonetim
    {
        public OkulYonetim()
        {
            Ders = new HashSet<Der>();
        }

        public int Id { get; set; }
        public string AdSoyad { get; set; } = null!;
        public string Gorevi { get; set; } = null!;
        public string YonetimTip { get; set; } = null!;

        public virtual ICollection<Der> Ders { get; set; }
    }
}
