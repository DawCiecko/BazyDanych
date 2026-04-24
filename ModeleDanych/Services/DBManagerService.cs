using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModeleDanych.Data;
using ModeleDanych.Models;

namespace ModeleDanych.Services
{
    public class DBManagerService
    {
        private readonly AppDbContext _context;
        public DBManagerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Grafik>> GetGrafikLekarzy(string specjalizacja)
        {
            var data = await _context.Grafiki
                .Include(g => g.Pracownik)
                .ThenInclude(p => p.Lekarz)
                .Where(g => g.Pracownik!.Lekarz != null && g.Pracownik.Lekarz.Specjalizacja == specjalizacja)
                .ToListAsync();
            return data
                .OrderBy(g => g.DataStart.Date)
                .ThenBy(g => g.DataStart.TimeOfDay)
                .ToList();
        }
        public async Task<List<string?>> GetSpecjalizacje()
        {
            return await _context.Lekarze
                .Where(l => !string.IsNullOrEmpty(l.Specjalizacja))
                .Select(l => l.Specjalizacja)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Wizyta>> GetDostepneWizyty(string specjalizacja)
        {
            return await _context.Wizyty
                .Include(w => w.Lekarz)
                .ThenInclude(l => l!.Pracownik)
                .Include(w => w.Gabinet)
                .Where(w => w.PacjentId == null && w.Lekarz!.Specjalizacja == specjalizacja)
                .OrderBy(w => w.Data)
                .ToListAsync();
        }

        public async Task<bool> BookWizyta(int pacjentId, int wizytaId)
        {
            var wizyta = await _context.Wizyty.FirstOrDefaultAsync(w => w.ID == wizytaId);
            if (wizyta == null || wizyta.PacjentId != null) return false;

            wizyta.PacjentId = pacjentId;
            wizyta.InformacjeDodatkowe = "Umówiono przez pacjenta";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GenerateWizyty(DateTime start, DateTime end, int lekarzId, int gabinetId)
        {
            if (start >= end) return false;

            for (DateTime t = start; t < end; t = t.AddMinutes(15))
            {
                var wizyta = new Wizyta
                {
                    PacjentId = null,
                    LekarzId = lekarzId,
                    GabinetId = gabinetId,
                    Data = t,
                    InformacjeDodatkowe = "Wygenerowano z grafiku"
                };
                _context.Wizyty.Add(wizyta);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Wizyta>> GetWizytyPacjenta(int pacjentId)
        {
            return await _context.Wizyty
                .Include(w => w.Lekarz)
                .ThenInclude(l => l!.Pracownik)
                .Include(w => w.Gabinet)
                .Where(w => w.PacjentId == pacjentId)
                .OrderByDescending(w => w.Data)
                .ToListAsync();
        }

        public async Task<List<Recepta>> GetReceptyPacjenta(int pacjentId)
        {
            return await _context.Recepty
                .Include(r => r.LekarzWystawiajacy)
                .ThenInclude(l => l!.Pracownik)
                .Where(r => r.PacjentId == pacjentId)
                .OrderByDescending(r => r.ID)
                .ToListAsync();
        }

        public async Task<List<Skierowanie>> GetSkierowaniaPacjenta(int pacjentId)
        {
            return await _context.Skierowania
                .Include(s => s.LekarzWystawiajacy)
                .ThenInclude(l => l!.Pracownik)
                .Where(s => s.PacjentId == pacjentId)
                .OrderByDescending(s => s.Data)
                .ToListAsync();
        }

        public async Task<List<Procedura>> GetHistoriaProcedurPacjenta(int pacjentId)
        {
            return await _context.Procedury
                .Include(p => p.TypBadania)
                .Include(p => p.LekarzWykonujacy)
                .ThenInclude(l => l!.Pracownik)
                .Include(p => p.Wynik)
                .Where(p => p.PacjentId == pacjentId)
                .OrderByDescending(p => p.DataWykonania)
                .ToListAsync();
        }

        public async Task<List<Pacjent>> GetAllPacjenci()
        {
            return await _context.Pacjenci
                .OrderBy(p => p.Nazwisko)
                .ThenBy(p => p.Imie)
                .ToListAsync();
        }

        public async Task<List<Produkt>> GetAllProdukty()
        {
            return await _context.Produkty.OrderBy(p => p.Nazwa).ToListAsync();
        }

        public async Task<List<Badanie>> GetAllBadania()
        {
            return await _context.Badania.OrderBy(b => b.Nazwa).ToListAsync();
        }

        public async Task<bool> AddRecepta(Recepta recepta)
        {
            _context.Recepty.Add(recepta);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AddSkierowanie(Skierowanie skierowanie)
        {
            _context.Skierowania.Add(skierowanie);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> AddWynik(Wynik wynik)
        {
            _context.Wyniki.Add(wynik);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<StanMagazynowy>> GetStanMagazynowy()
        {
            return await _context.StanyMagazynowe
                .Include(s => s.Produkt)
                .Include(s => s.Magazyn)
                .ToListAsync();
        }

        public async Task<bool> UpdateStanMagazynowy(int produktId, int zmianaIlosci)
        {
            var stan = await _context.StanyMagazynowe.FirstOrDefaultAsync(s => s.ProduktId == produktId);
            if (stan == null)
            {
                if (zmianaIlosci <= 0) return false;
                var magazyn = await _context.Magazyny.FirstOrDefaultAsync();
                if (magazyn == null) return false;
                
                stan = new StanMagazynowy
                {
                    MagazynId = magazyn.ID,
                    ProduktId = produktId,
                    Ilosc = zmianaIlosci
                };
                _context.StanyMagazynowe.Add(stan);
            }
            else
            {
                if (stan.Ilosc + zmianaIlosci < 0) return false;
                stan.Ilosc += zmianaIlosci;
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> BookProcedura(int pacjentId, int badanieId, int lekarzId, DateTime data)
        {
            var procedura = new Procedura
            {
                PacjentId = pacjentId,
                TypBadaniaId = badanieId,
                LekarzWykonujacyId = lekarzId,
                DataWykonania = data,
                InfoDodatkowe = "Umówiono przez system"
            };

            _context.Procedury.Add(procedura);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Gabinet>> GetAllGabinety()
        {
            return await _context.Gabinety
                .Include(g => g.Szpital)
                .OrderBy(g => g.Numer)
                .ToListAsync();
        }
    }
}
