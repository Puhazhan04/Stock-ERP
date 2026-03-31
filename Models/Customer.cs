using System.ComponentModel.DataAnnotations;

namespace StockERP.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Vorname")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Nachname")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Voller Name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
