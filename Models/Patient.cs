using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CWeb.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Ticket { get; set; }
        public string? Receptionne { get; set; }
        public string? Accueil { get; set; }
        public string? ResultatConsultation { get; set; }
        public int? Cout { get; set; }
        public string? Finition { get; set; }
        public string? Service { get; set; }
        public string? ReceptionneService { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public string? Sexe { get; set; }
        public int? Age { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }
    }
}
