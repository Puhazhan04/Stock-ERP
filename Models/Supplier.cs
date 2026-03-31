using System.ComponentModel.DataAnnotations;

namespace StockERP.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Lieferantenname")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Kontaktperson")]
        public string? ContactPerson { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
