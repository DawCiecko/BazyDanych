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
        public async Task<List<Grafik>> GetGrafikLekarzy()
        {
            var data = await _context.Grafiki
                .Include(g => g.Pracownik)
                .ThenInclude(p => p.Lekarz)
                .Where(g => g.Pracownik!.Lekarz != null && g.Pracownik.Lekarz.Specjalizacja == "Internista")
                .ToListAsync();
            return data
                .OrderBy(g => g.DataStart.Date)
                .ThenBy(g => g.DataStart.TimeOfDay)
                .ToList();

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
    }
}
