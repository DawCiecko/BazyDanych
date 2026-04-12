using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Gabinet")]
public class Gabinet
{
    [Key]
    public int ID { get; set; }

    public int Numer { get; set; }

    public int SzpitalId { get; set; }

    public Szpital? Szpital { get; set; }

    public ICollection<Wizyta> Wizyty { get; set; } = new List<Wizyta>();
    public ICollection<Grafik> Grafiki { get; set; } = new List<Grafik>();
    public ICollection<Terminarz> Terminarze { get; set; } = new List<Terminarz>();
}
