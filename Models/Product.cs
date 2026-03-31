using System.ComponentModel.DataAnnotations;

namespace SimpleERP.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Produktname")]
        public string Name { get; set; } = string.Empty;

        [Range(0.01, 10000.00)]
        [Display(Name = "Preis")]
        public decimal Price { get; set; }

        [Display(Name = "Lagerbestand")]
        public int Stock { get; set; }

        [Display(Name = "Kategorie")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        [Display(Name = "Lieferant")]
        public int? SupplierId { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
