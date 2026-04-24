using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Procedury")]
public class Procedura
{
    [Key]
    public int ID { get; set; }

    public int TypBadaniaId { get; set; }

    public DateTime DataWykonania { get; set; }

    public int? SkierowanieId { get; set; }

    public int LekarzWykonujacyId { get; set; }
    public int PacjentId { get; set; }

    [MaxLength(1000)]
    public string? InfoDodatkowe { get; set; }
    public Badanie? TypBadania { get; set; }
    public Skierowanie? Skierowanie { get; set; }
    public Lekarz? LekarzWykonujacy { get; set; }
    public Pacjent? Pacjent { get; set; }

    public Wynik? Wynik { get; set; }
    public Terminarz? Terminarz { get; set; }

    public override string ToString()
    {
        return $"{DataWykonania:dd.MM.yyyy} - {TypBadania?.Nazwa ?? $"Badanie {TypBadaniaId}"} (Pacjent: {Pacjent?.Nazwisko ?? PacjentId.ToString()})";
    }
}
