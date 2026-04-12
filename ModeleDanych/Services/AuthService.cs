using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ModeleDanych.Data;
using ModeleDanych.Models;

namespace ModeleDanych.Services;

public class LoginResult
{
    public bool Sukces { get; set; }
    public string Wiadomosc { get; set; } = string.Empty;
    
    public Pacjent? ZalogowanyPacjent { get; set; }
    public Pracownik? ZalogowanyPracownik { get; set; }

    public bool CzyPacjent => ZalogowanyPacjent != null;
    
    public bool CzyPracownik => ZalogowanyPracownik != null;
}

public class AuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public LoginResult Zaloguj(string login, string haslo)
    {
        var daneLogowania = _context.DaneLogowania.FirstOrDefault(d => d.Login == login);
        
        if (daneLogowania == null)
        {
            return new LoginResult { Sukces = false, Wiadomosc = "Niepoprawny login lub hasło." };
        }

        string wyliczonyHash = HashPassword(haslo, daneLogowania.Salt);
        if (daneLogowania.PasswordHash != wyliczonyHash)
        {
            return new LoginResult { Sukces = false, Wiadomosc = "Niepoprawny login lub hasło." };
        }
        var pacjent = _context.Pacjenci.FirstOrDefault(p => p.DaneLogowaniaId == daneLogowania.ID);
        if (pacjent != null)
        {
            return new LoginResult 
            { 
                Sukces = true, 
                Wiadomosc = "Pomyślnie zalogowano w trybie Pacjenta.",
                ZalogowanyPacjent = pacjent 
            };
        }

        var pracownik = _context.Pracownicy
            .Include(p => p.Lekarz) 
            .FirstOrDefault(p => p.DaneLogowaniaId == daneLogowania.ID);
            
        if (pracownik != null)
        {
            return new LoginResult 
            { 
                Sukces = true, 
                Wiadomosc = "Pomyślnie zalogowano w trybie Pracownika.",
                ZalogowanyPracownik = pracownik 
            };
        }

        return new LoginResult 
        { 
            Sukces = false, 
            Wiadomosc = "Błąd bazy danych: konto nie przypisane ani do Pacjenta ani do Pracownika." 
        };
    }
    public static string GenerateSalt()
    {
        return Guid.NewGuid().ToString("N");
    }

    public static string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = password + salt;
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        return Convert.ToBase64String(bytes);
    }

    public void ZarejestrujPacjenta(string imie, string nazwisko, string pesel, string nrTel, string adres, string login, string haslo)
    {
        if (_context.DaneLogowania.Any(d => d.Login == login))
        {
            throw new Exception("Login jest już zajęty.");
        }
        var salt = GenerateSalt();
        var passwordHash = HashPassword(haslo, salt);
        var daneLogowania = new DaneLogowania
        {
            Login = login,
            PasswordHash = passwordHash,
            Salt = salt,
            TypKonta = TypKonta.Pacjent
        };
        _context.DaneLogowania.Add(daneLogowania);
        _context.SaveChanges();
        var pacjent = new Pacjent
        {
            Imie = imie,
            Nazwisko = nazwisko,
            PESEL = pesel,
            NrTel = nrTel,
            Adres = adres,
            DaneLogowaniaId = daneLogowania.ID
        };
        _context.Pacjenci.Add(pacjent);
        _context.SaveChanges();
    }

    public void ZarejestrujPracownika(string imie, string nazwisko, string pesel, string nrTel, string adres, double wyplata, string stanowisko, int szpitalId, string login, string haslo)
    {
        if (_context.DaneLogowania.Any(d => d.Login == login))
        {
            throw new Exception("Login jest już zajęty.");
        }
        var salt = GenerateSalt();
        var passwordHash = HashPassword(haslo, salt);
        var daneLogowania = new DaneLogowania
        {
            Login = login,
            PasswordHash = passwordHash,
            Salt = salt,
            TypKonta = TypKonta.Lekarz
        };
        _context.DaneLogowania.Add(daneLogowania);
        _context.SaveChanges();
        var pracownik = new Pracownik
        {
            Imie = imie,
            Nazwisko = nazwisko,
            PESEL = pesel,
            NrTel = nrTel,
            Adres = adres,
            Wyplata = wyplata,
            Stanowisko = stanowisko,
            SzpitalId = szpitalId,
            DaneLogowaniaId = daneLogowania.ID
        };
        _context.Pracownicy.Add(pracownik);
        _context.SaveChanges();
    }
}
