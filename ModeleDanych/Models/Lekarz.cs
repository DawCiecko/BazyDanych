using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Lekarz")]
public class Lekarz
{
    [Key]
    public int ID { get; set; }

    public int IdPracownika { get; set; }

    [MaxLength(200)]
    public string? Specjalizacja { get; set; }


    public Pracownik? Pracownik { get; set; }

    public ICollection<Wizyta> Wizyty { get; set; } = new List<Wizyta>();
    public ICollection<Recepta> Recepty { get; set; } = new List<Recepta>();

    public ICollection<Skierowanie> Skierowania { get; set; } = new List<Skierowanie>();

    public ICollection<Procedura> WykonywaneProcedury { get; set; } = new List<Procedura>();
}
