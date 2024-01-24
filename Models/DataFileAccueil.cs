namespace CWeb.Models
{
    public class DataFileAccueil
    {
        public IEnumerable<Patient> Attente { get; set; }
        public IEnumerable<Patient> Accueil1 { get; set; }
        public IEnumerable<Patient> Accueil2 { get; set; }
        public IEnumerable<Patient> Accueil3 { get; set; }
    }
}
