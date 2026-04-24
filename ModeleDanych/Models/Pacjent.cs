using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Pacjent")]
public class Pacjent
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Imie { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Nazwisko { get; set; } = string.Empty;

    [Required]
    [StringLength(11, MinimumLength = 11)]
    public string PESEL { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? NrTel { get; set; }

    [MaxLength(255)]
    public string? Adres { get; set; }

    public int DaneLogowaniaId { get; set; }
    public DaneLogowania? DaneLogowania { get; set; }

    public ICollection<Wizyta> Wizyty { get; set; } = new List<Wizyta>();
    public ICollection<Recepta> Recepty { get; set; } = new List<Recepta>();
    public ICollection<Skierowanie> Skierowania { get; set; } = new List<Skierowanie>();
    public ICollection<Procedura> Procedury { get; set; } = new List<Procedura>();

    public override string ToString()
    {
        return $"[{ID}] {Imie} {Nazwisko} - PESEL: {PESEL}";
    }
}
