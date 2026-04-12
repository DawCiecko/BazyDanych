using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Produkty")]
public class Produkt
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nazwa { get; set; } = string.Empty;

    public ICollection<Recepta> Recepty { get; set; } = new List<Recepta>();

    public ICollection<Zamowienie> Zamowienia { get; set; } = new List<Zamowienie>();

    public ICollection<StanMagazynowy> StanyMagazynowe { get; set; } = new List<StanMagazynowy>();
}
