using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Zamowienia")]
public class Zamowienie
{
    [Key]
    public int ID { get; set; }

    public int ProduktId { get; set; }

    [Range(1, int.MaxValue)]
    public int Ilosc { get; set; }


    public Produkt? Produkt { get; set; }

    public ICollection<Dostawa> Dostawy { get; set; } = new List<Dostawa>();
}
