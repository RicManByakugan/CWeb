namespace CWeb.Models
{
    public class DataFileSerivce
    {
        public IEnumerable<Patient> GENERALE { get; set; }
        public IEnumerable<Patient> MATERNITE { get; set; }
        public IEnumerable<Patient> VACCINATION { get; set; }
        public IEnumerable<Patient> PEDIATRIE { get; set; }
        public IEnumerable<Patient> BLOCOPERATOIR { get; set; }
        public IEnumerable<Patient> DENTAIRE { get; set; }
    }
}
