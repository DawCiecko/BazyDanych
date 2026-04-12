using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModeleDanych.Models;

public enum TypKonta
{
    Pacjent,
    Lekarz,
    Administracja
}

[Table("DaneLogowania")]
public class DaneLogowania
{
    [Key]
    public int ID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = string.Empty;

    [Required]
    [MaxLength(512)]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    [MaxLength(256)]
    public string Salt { get; set; } = string.Empty;

    public TypKonta? TypKonta { get; set; }
}
