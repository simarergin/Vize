using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class OkulYonetimTablo
    {
        public OkulYonetimTablo()
        {
            DersTablos = new HashSet<DersTablo>();
        }

        public int Id { get; set; }
        public string? AdSoyad { get; set; } = null!;
        public string? Gorevi { get; set; } = null!;
        public int? YonetimTip { get; set; }

        public virtual ICollection<DersTablo> DersTablos { get; set; }
    }
}
