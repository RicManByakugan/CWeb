namespace CWeb.Models
{
    public class Activite
    {
        public int Id { get; set; }
        public string? Admin { get; set; }
        public string? AdminPrenom { get; set; }
        public string? AdminPoste { get; set; }
        public string? Descirption { get; set; }
        public string? Cible { get; set; }
        public string? CiblePrenom { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
