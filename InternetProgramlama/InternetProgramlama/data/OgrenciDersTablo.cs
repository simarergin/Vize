using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class OgrenciDersTablo
    {
        public int Id { get; set; }
        public int? DersId { get; set; }
        public int? OgrenciId { get; set; }

        public virtual DersTablo Ders { get; set; } = null!;
        public virtual OgrenciTablo? Ogrenci { get; set; } = null!;
    }
}
