using ModeleDanych.Data;
using ModeleDanych.Models;
using ModeleDanych.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //base.OnStartup(e);

            //using (var context = new AppDbContext())
            //{
            //    // 1. Upewniamy się, że baza danych i tabele istnieją
            //    context.Database.EnsureCreated();

            //    // 2. Sprawdzamy, czy w bazie są już jakieś dane logowania. 
            //    // Jeśli nie ma, tworzymy konta testowe.
            //    if (!context.DaneLogowania.Any())
            //    {
            //        var authService = new AuthService(context);

            //        // Pracownik wymaga przypisania do szpitala, więc tworzymy testowy szpital
            //        var testowySzpital = new Szpital { Adres = "ul. Testowa 1, 00-000 Warszawa" };
            //        context.Szpitale.Add(testowySzpital);
            //        context.SaveChanges();

            //        // --- KONTO PACJENTA ---
            //        // Login: pacjent | Hasło: test
            //        authService.ZarejestrujPacjenta(
            //            imie: "Jan",
            //            nazwisko: "Kowalski",
            //            pesel: "12345678901",
            //            nrTel: "111222333",
            //            adres: "ul. Pacjenta 5",
            //            login: "pacjent",
            //            haslo: "test");

            //        // --- KONTO PRACOWNIKA ---
            //        // Login: pracownik | Hasło: test
            //        authService.ZarejestrujPracownika(
            //            imie: "Anna",
            //            nazwisko: "Nowak",
            //            pesel: "09876543210",
            //            nrTel: "333222111",
            //            adres: "ul. Pracownicza 10",
            //            wyplata: 5000,
            //            stanowisko: "Recepcja",
            //            szpitalId: testowySzpital.ID,
            //            login: "pracownik",
            //            haslo: "test");

            //        // --- KONTO LEKARZA ---
            //        // Login: lekarz | Hasło: test
            //        authService.ZarejestrujPracownika(
            //            imie: "Tomasz",
            //            nazwisko: "Lekarski",
            //            pesel: "55555555555",
            //            nrTel: "555666777",
            //            adres: "ul. Kliniczna 2",
            //            wyplata: 15000,
            //            stanowisko: "Lekarz",
            //            szpitalId: testowySzpital.ID,
            //            login: "lekarz",
            //            haslo: "test");

            //        var lekarzPracownik = context.Pracownicy.FirstOrDefault(p => p.DaneLogowania.Login == "lekarz" || p.PESEL == "55555555555");
            //        if (lekarzPracownik != null)
            //        {
            //            context.Lekarze.Add(new Lekarz
            //            {
            //                IdPracownika = lekarzPracownik.ID,
            //                Specjalizacja = "Kardiolog"
            //            });
            //            context.SaveChanges();
            //        }
            //    }
            //}
        }
    }
}
