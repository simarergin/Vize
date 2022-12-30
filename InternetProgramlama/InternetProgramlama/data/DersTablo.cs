using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class DersTablo
    {
        public DersTablo()
        {
            OgrenciDersTablos = new HashSet<OgrenciDersTablo>();
        }

        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public double? Kredisi { get; set; }
        public int? OkulYonetimId { get; set; }

        public virtual OkulYonetimTablo OkulYonetim { get; set; } = null!;
        public virtual ICollection<OgrenciDersTablo> OgrenciDersTablos { get; set; }
    }
}
