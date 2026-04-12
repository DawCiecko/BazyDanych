using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Grafik")]
public class Grafik
{
    [Key]
    public int ID { get; set; }

    public int PracownikId { get; set; }

    public DateTime DataStart { get; set; }
    public DateTime DataKoniec { get; set; }
    public int GabinetId { get; set; }

    public Pracownik? Pracownik { get; set; }
    public Gabinet? Gabinet { get; set; }


    [NotMapped]
    public TimeSpan CzasPracy => DataKoniec - DataStart;
}
