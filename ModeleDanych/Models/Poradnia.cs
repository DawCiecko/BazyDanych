using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Poradnia")]
public class Poradnia
{
    [Key]
    public int ID { get; set; }

    public int SzpitalId { get; set; }

    [MaxLength(255)]
    public string? Adres { get; set; }

    public Szpital? Szpital { get; set; }
}
