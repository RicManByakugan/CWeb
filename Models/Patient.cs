using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CWeb.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Ticket { get; set; }
        public string? Receptionne { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Sexe { get; set; }
        public int Age { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }
    }
}
