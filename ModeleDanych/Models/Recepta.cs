using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Recepta")]
public class Recepta
{
    [Key]
    public int ID { get; set; }

    public int Kod { get; set; }

    public int LekarzWystawiajacyId { get; set; }

    public int PacjentId { get; set; }

    public int ProduktId { get; set; }


    public Lekarz? LekarzWystawiajacy { get; set; }
    public Pacjent? Pacjent { get; set; }
    public Produkt? Produkt { get; set; }
}
