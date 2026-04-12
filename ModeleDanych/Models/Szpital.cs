using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Szpital")]
public class Szpital
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(255)]
    public string Adres { get; set; } = string.Empty;


    public ICollection<Gabinet> Gabinety { get; set; } = new List<Gabinet>();

    public ICollection<Pracownik> Pracownicy { get; set; } = new List<Pracownik>();

    public ICollection<Poradnia> Poradnie { get; set; } = new List<Poradnia>();

    public Magazyn? Magazyn { get; set; }
}
