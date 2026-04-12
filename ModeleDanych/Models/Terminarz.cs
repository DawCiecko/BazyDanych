using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

[Table("Terminarz")]
public class Terminarz
{
    [Key]
    public int ID { get; set; }

    public int ProceduraId { get; set; }

    public DateTime Data { get; set; }


    public int SalaId { get; set; }


    public Procedura? Procedura { get; set; }
    public Gabinet? Sala { get; set; }
}
