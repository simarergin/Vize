using InternetProgramlama.data;

namespace InternetProgramlama.Models
{
    public class OgrenciDersModel
    {
        public List<OgrenciDersModel>? OgrenciDersList { get; set; }
    }
    public class OgrenciDers
    {
        public int Id { get; set; }
        public int DersId { get; set; }
        public int OgrenciId { get; set; }


    }
}
