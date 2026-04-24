using Microsoft.EntityFrameworkCore;
using ModeleDanych.Models;

namespace ModeleDanych.Data;

public class AppDbContext : DbContext
{
    public DbSet<Pacjent> Pacjenci { get; set; }
    public DbSet<Pracownik> Pracownicy { get; set; }
    public DbSet<Lekarz> Lekarze { get; set; }
    public DbSet<DaneLogowania> DaneLogowania { get; set; }

    public DbSet<Szpital> Szpitale { get; set; }
    public DbSet<Gabinet> Gabinety { get; set; }
    public DbSet<Poradnia> Poradnie { get; set; }
    public DbSet<Magazyn> Magazyny { get; set; }

    public DbSet<Produkt> Produkty { get; set; }
    public DbSet<StanMagazynowy> StanyMagazynowe { get; set; }
    public DbSet<Zamowienie> Zamowienia { get; set; }
    public DbSet<Dostawa> Dostawy { get; set; }

    public DbSet<Badanie> Badania { get; set; }
    public DbSet<Procedura> Procedury { get; set; }
    public DbSet<Wynik> Wyniki { get; set; }
    public DbSet<Terminarz> Terminarze { get; set; }

    public DbSet<Wizyta> Wizyty { get; set; }
    public DbSet<Skierowanie> Skierowania { get; set; }
    public DbSet<Recepta> Recepty { get; set; }
    public DbSet<Grafik> Grafiki { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var folder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                     ?? AppContext.BaseDirectory;
        var dbPath = Path.Combine(folder, "szpital.db");
        options.UseSqlite($"Data Source={dbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<StanMagazynowy>()
            .HasKey(s => new { s.MagazynId, s.ProduktId });

        modelBuilder.Entity<Lekarz>()
            .HasOne(l => l.Pracownik)
            .WithOne(p => p.Lekarz)
            .HasForeignKey<Lekarz>(l => l.IdPracownika);

        modelBuilder.Entity<Wizyta>()
            .HasOne(w => w.Pacjent)
            .WithMany(p => p.Wizyty)
            .HasForeignKey(w => w.PacjentId)
            .IsRequired(false);

        modelBuilder.Entity<Wizyta>()
            .HasOne(w => w.Lekarz)
            .WithMany(l => l.Wizyty)
            .HasForeignKey(w => w.LekarzId);

        modelBuilder.Entity<Wizyta>()
            .HasOne(w => w.Gabinet)
            .WithMany(g => g.Wizyty)
            .HasForeignKey(w => w.GabinetId);

        modelBuilder.Entity<Procedura>()
            .HasOne(p => p.Pacjent)
            .WithMany(pa => pa.Procedury)
            .HasForeignKey(p => p.PacjentId);

        modelBuilder.Entity<Procedura>()
            .HasOne(p => p.LekarzWykonujacy)
            .WithMany(l => l.WykonywaneProcedury)
            .HasForeignKey(p => p.LekarzWykonujacyId);

        modelBuilder.Entity<Procedura>()
            .HasOne(p => p.TypBadania)
            .WithMany(b => b.Procedury)
            .HasForeignKey(p => p.TypBadaniaId);

        modelBuilder.Entity<Procedura>()
            .HasOne(p => p.Skierowanie)
            .WithOne(s => s.Procedura)
            .HasForeignKey<Procedura>(p => p.SkierowanieId);

        modelBuilder.Entity<Wynik>()
            .HasOne(w => w.Procedura)
            .WithOne(p => p.Wynik)
            .HasForeignKey<Wynik>(w => w.ProceduraId);

        modelBuilder.Entity<Terminarz>()
            .HasOne(t => t.Procedura)
            .WithOne(p => p.Terminarz)
            .HasForeignKey<Terminarz>(t => t.ProceduraId);

        modelBuilder.Entity<Skierowanie>()
            .HasOne(s => s.Pacjent)
            .WithMany(p => p.Skierowania)
            .HasForeignKey(s => s.PacjentId);

        modelBuilder.Entity<Skierowanie>()
            .HasOne(s => s.LekarzWystawiajacy)
            .WithMany(l => l.Skierowania)
            .HasForeignKey(s => s.LekarzWystawiajacyId);

        modelBuilder.Entity<Skierowanie>()
            .HasOne(s => s.Badanie)
            .WithMany(b => b.Skierowania)
            .HasForeignKey(s => s.BadanieId);

        modelBuilder.Entity<Recepta>()
            .HasOne(r => r.Pacjent)
            .WithMany(p => p.Recepty)
            .HasForeignKey(r => r.PacjentId);

        modelBuilder.Entity<Recepta>()
            .HasOne(r => r.LekarzWystawiajacy)
            .WithMany(l => l.Recepty)
            .HasForeignKey(r => r.LekarzWystawiajacyId);

        modelBuilder.Entity<Recepta>()
            .HasOne(r => r.Produkt)
            .WithMany(p => p.Recepty)
            .HasForeignKey(r => r.ProduktId);

        modelBuilder.Entity<Gabinet>()
            .HasOne(g => g.Szpital)
            .WithMany(s => s.Gabinety)
            .HasForeignKey(g => g.SzpitalId);

        modelBuilder.Entity<Terminarz>()
            .HasOne(t => t.Sala)
            .WithMany(g => g.Terminarze)
            .HasForeignKey(t => t.SalaId);

        modelBuilder.Entity<Pracownik>()
            .HasOne(p => p.Szpital)
            .WithMany(s => s.Pracownicy)
            .HasForeignKey(p => p.SzpitalId);

        modelBuilder.Entity<Pracownik>()
            .HasOne(p => p.DaneLogowania)
            .WithOne()
            .HasForeignKey<Pracownik>(p => p.DaneLogowaniaId);

        modelBuilder.Entity<Pacjent>()
            .HasOne(p => p.DaneLogowania)
            .WithOne()
            .HasForeignKey<Pacjent>(p => p.DaneLogowaniaId);

        modelBuilder.Entity<Poradnia>()
            .HasOne(p => p.Szpital)
            .WithMany(s => s.Poradnie)
            .HasForeignKey(p => p.SzpitalId);

        modelBuilder.Entity<Magazyn>()
            .HasOne(m => m.Szpital)
            .WithOne(s => s.Magazyn)
            .HasForeignKey<Magazyn>(m => m.SzpitalId);

        modelBuilder.Entity<StanMagazynowy>()
            .HasOne(sm => sm.Magazyn)
            .WithMany(m => m.StanyMagazynowe)
            .HasForeignKey(sm => sm.MagazynId);

        modelBuilder.Entity<StanMagazynowy>()
            .HasOne(sm => sm.Produkt)
            .WithMany(p => p.StanyMagazynowe)
            .HasForeignKey(sm => sm.ProduktId);

        modelBuilder.Entity<Zamowienie>()
            .HasOne(z => z.Produkt)
            .WithMany(p => p.Zamowienia)
            .HasForeignKey(z => z.ProduktId);

        modelBuilder.Entity<Dostawa>()
            .HasOne(d => d.Zamowienie)
            .WithMany(z => z.Dostawy)
            .HasForeignKey(d => d.ZamowienieId);

        modelBuilder.Entity<Dostawa>()
            .HasOne(d => d.Magazyn)
            .WithMany(m => m.Dostawy)
            .HasForeignKey(d => d.MagazynId);

        modelBuilder.Entity<Grafik>()
            .HasOne(g => g.Pracownik)
            .WithMany(p => p.Grafiki)
            .HasForeignKey(g => g.PracownikId);

        modelBuilder.Entity<Grafik>()
            .HasOne(g => g.Gabinet)
            .WithMany(g => g.Grafiki)
            .HasForeignKey(g => g.GabinetId);

        modelBuilder.Entity<DaneLogowania>()
            .Property(d => d.TypKonta)
            .HasConversion<int>();
    }
}
