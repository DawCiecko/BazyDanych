using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Dostawy")]
public class Dostawa
{
    [Key]
    public int ID { get; set; }

    public int ZamowienieId { get; set; }

    public DateTime DataDostawy { get; set; }

    public int MagazynId { get; set; }


    public Zamowienie? Zamowienie { get; set; }
    public Magazyn? Magazyn { get; set; }
}
