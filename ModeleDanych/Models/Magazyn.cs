using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Magazyn")]
public class Magazyn
{
    [Key]
    public int ID { get; set; }
    public int SzpitalId { get; set; }

    public Szpital? Szpital { get; set; }

    public ICollection<StanMagazynowy> StanyMagazynowe { get; set; } = new List<StanMagazynowy>();

    public ICollection<Dostawa> Dostawy { get; set; } = new List<Dostawa>();
}
