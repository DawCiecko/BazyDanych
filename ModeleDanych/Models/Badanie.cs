using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;


[Table("Badania")]
public class Badanie
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(255)]
    public string Nazwa { get; set; } = string.Empty;

    public ICollection<Procedura> Procedury { get; set; } = new List<Procedura>();

    public ICollection<Skierowanie> Skierowania { get; set; } = new List<Skierowanie>();
}
