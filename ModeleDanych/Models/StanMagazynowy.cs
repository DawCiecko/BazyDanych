using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ModeleDanych.Models;

[Table("StanMagazynowy")]
[PrimaryKey(nameof(MagazynId), nameof(ProduktId))]
public class StanMagazynowy
{

    public int MagazynId { get; set; }

    public int ProduktId { get; set; }

    [Range(0, int.MaxValue)]
    public int Ilosc { get; set; }

    public Magazyn? Magazyn { get; set; }
    public Produkt? Produkt { get; set; }
}
