using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Wizyty")]
public class Wizyta
{
    [Key]
    public int ID { get; set; }

    public int? PacjentId { get; set; }

    public int LekarzId { get; set; }

    public int GabinetId { get; set; }

    public DateTime Data { get; set; }

    [MaxLength(1000)]
    public string? InformacjeDodatkowe { get; set; }

    public Pacjent? Pacjent { get; set; }
    public Lekarz? Lekarz { get; set; }
    public Gabinet? Gabinet { get; set; }

    public override string ToString()
    {
        return $"{Data:dd.MM.yyyy HH:mm} - Dr {Lekarz?.Pracownik?.Nazwisko} (Gabinet: {Gabinet?.Numer}) {(PacjentId == null ? "[Wolne]" : "[Zarezerwowane]")}";
    }
}
