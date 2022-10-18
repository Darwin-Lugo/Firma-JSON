namespace Firma.Core.Entitys
{
    public partial class Persona : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public DateTime DateCrea { get; set; }
        public string Firma { get; set; } = null!;
        public string StatusData { get; set; } = null!;
    }

}
