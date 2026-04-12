using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Wyniki")]
public class Wynik
{
    [Key]
    public int ID { get; set; }

    public int ProceduraId { get; set; }

    [MaxLength(2000)]
    public string? OpisBadania { get; set; }


    public Procedura? Procedura { get; set; }
}
