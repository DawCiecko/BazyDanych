using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Pracownicy")]
public class Pracownik
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

    [Column("Wypłata")]
    public double Wyplata { get; set; }

    [MaxLength(100)]
    public string? Stanowisko { get; set; }
    public int SzpitalId { get; set; }
    public int DaneLogowaniaId { get; set; }


    public Szpital? Szpital { get; set; }
    public DaneLogowania? DaneLogowania { get; set; }

    public Lekarz? Lekarz { get; set; }

    public ICollection<Grafik> Grafiki { get; set; } = new List<Grafik>();


    [NotMapped]
    public string PelneNazwisko => $"{Imie} {Nazwisko}";
}
