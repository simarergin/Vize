using System;
using System.Collections.Generic;

namespace InternetProgramlama.data
{
    public partial class OgrenciDer
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        public int OgrenciId { get; set; }

        public virtual Der Ders { get; set; } = null!;
        public virtual Ogrenci Ogrenci { get; set; } = null!;
    }
}
