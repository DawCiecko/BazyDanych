using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Skierowania")]
public class Skierowanie
{
    [Key]
    public int ID { get; set; }

    public int Kod { get; set; }

    public int LekarzWystawiajacyId { get; set; }

    public int PacjentId { get; set; }

    public int BadanieId { get; set; }

    public DateTime Data { get; set; }

    public Lekarz? LekarzWystawiajacy { get; set; }
    public Pacjent? Pacjent { get; set; }
    public Badanie? Badanie { get; set; }
    public Procedura? Procedura { get; set; }
}
