namespace InternetProgramlama.Models
{
    public class OgrenciModel
    {
        public List<Ogrenci> OgrenciList { get; set; }
    }

    public class Ogrenci
    {
        public int Id { get; set; }
        public string AdSoyad { get; set; }
        public int KayitTarih { get; set; }
        public int OgrenciNo { get; set; }
        public DateTime? DTarih { get; set; }
        public string Bolum { get; set; }
    }
}
