using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class Der
    {
        public Der()
        {
            OgrenciDers = new HashSet<OgrenciDer>();
        }

        public int Id { get; set; }
        public string Ad { get; set; } = null!;
        public int Kredisi { get; set; }
        public int OkulYonetimId { get; set; }

        public virtual OkulYonetim OkulYonetim { get; set; } = null!;
        public virtual ICollection<OgrenciDer> OgrenciDers { get; set; }
    }
}
