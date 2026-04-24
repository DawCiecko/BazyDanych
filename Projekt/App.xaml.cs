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
            base.OnStartup(e);

            using (var context = new AppDbContext())
            {
                // 1. Upewniamy się, że baza danych i tabele istnieją
                context.Database.EnsureCreated();

                // 2. Sprawdzamy, czy w bazie są już jakieś dane logowania. 
                // Jeśli nie ma, tworzymy konta testowe.
                if (!context.DaneLogowania.Any())
                {
                    var authService = new AuthService(context);

                    // Pracownik wymaga przypisania do szpitala, więc tworzymy testowy szpital
                    var testowySzpital = new Szpital { Adres = "ul. Testowa 1, 00-000 Warszawa" };
                    context.Szpitale.Add(testowySzpital);
                    context.SaveChanges();

                    // --- GABINETY ---
                    var gabinet1 = new Gabinet { Numer = 101, SzpitalId = testowySzpital.ID };
                    var gabinet2 = new Gabinet { Numer = 102, SzpitalId = testowySzpital.ID };
                    var gabinet3 = new Gabinet { Numer = 201, SzpitalId = testowySzpital.ID };
                    var gabinet4 = new Gabinet { Numer = 202, SzpitalId = testowySzpital.ID };
                    var gabinet5 = new Gabinet { Numer = 301, SzpitalId = testowySzpital.ID };
                    context.Gabinety.AddRange(gabinet1, gabinet2, gabinet3, gabinet4, gabinet5);
                    context.SaveChanges();

                    // --- BADANIA ---
                    var badania = new List<Badanie>
                    {
                        new Badanie { Nazwa = "Morfologia krwi" },
                        new Badanie { Nazwa = "RTG klatki piersiowej" },
                        new Badanie { Nazwa = "EKG" },
                        new Badanie { Nazwa = "USG jamy brzusznej" },
                        new Badanie { Nazwa = "Badanie moczu" },
                        new Badanie { Nazwa = "Poziom glukozy" },
                        new Badanie { Nazwa = "TSH (tarczyca)" },
                        new Badanie { Nazwa = "Rezonans magnetyczny" },
                        new Badanie { Nazwa = "Tomografia komputerowa" },
                        new Badanie { Nazwa = "Gastroskopia" }
                    };
                    context.Badania.AddRange(badania);
                    context.SaveChanges();

                    // --- PRODUKTY (leki) ---
                    var produkty = new List<Produkt>
                    {
                        new Produkt { Nazwa = "Paracetamol 500mg" },
                        new Produkt { Nazwa = "Ibuprofen 400mg" },
                        new Produkt { Nazwa = "Amoksycylina 1g" },
                        new Produkt { Nazwa = "Metformina 850mg" },
                        new Produkt { Nazwa = "Amlodypina 5mg" },
                        new Produkt { Nazwa = "Omeprazol 20mg" },
                        new Produkt { Nazwa = "Atorwastatyna 20mg" },
                        new Produkt { Nazwa = "Aspiryna 100mg" }
                    };
                    context.Produkty.AddRange(produkty);
                    context.SaveChanges();

                    // --- MAGAZYN + STANY MAGAZYNOWE ---
                    var magazyn = new Magazyn { SzpitalId = testowySzpital.ID };
                    context.Magazyny.Add(magazyn);
                    context.SaveChanges();

                    foreach (var prod in produkty)
                    {
                        context.StanyMagazynowe.Add(new StanMagazynowy
                        {
                            MagazynId = magazyn.ID,
                            ProduktId = prod.ID,
                            Ilosc = new Random().Next(10, 200)
                        });
                    }
                    context.SaveChanges();

                    // --- KONTO PACJENTA ---
                    // Login: pacjent | Hasło: test
                    authService.ZarejestrujPacjenta(
                        imie: "Jan",
                        nazwisko: "Kowalski",
                        pesel: "12345678901",
                        nrTel: "111222333",
                        adres: "ul. Pacjenta 5",
                        login: "pacjent",
                        haslo: "test");

                    // --- KONTO PRACOWNIKA ---
                    // Login: pracownik | Hasło: test
                    authService.ZarejestrujPracownika(
                        imie: "Anna",
                        nazwisko: "Nowak",
                        pesel: "09876543210",
                        nrTel: "333222111",
                        adres: "ul. Pracownicza 10",
                        wyplata: 5000,
                        stanowisko: "Recepcja",
                        szpitalId: testowySzpital.ID,
                        login: "pracownik",
                        haslo: "test");

                    // --- KONTO LEKARZA ---
                    // Login: lekarz | Hasło: test
                    authService.ZarejestrujPracownika(
                        imie: "Tomasz",
                        nazwisko: "Lekarski",
                        pesel: "55555555555",
                        nrTel: "555666777",
                        adres: "ul. Kliniczna 2",
                        wyplata: 15000,
                        stanowisko: "Lekarz",
                        szpitalId: testowySzpital.ID,
                        login: "lekarz",
                        haslo: "test");

                    var lekarzPracownik = context.Pracownicy.FirstOrDefault(p => p.DaneLogowania.Login == "lekarz" || p.PESEL == "55555555555");
                    if (lekarzPracownik != null)
                    {
                        var lekarz = new Lekarz
                        {
                            IdPracownika = lekarzPracownik.ID,
                            Specjalizacja = "Kardiolog"
                        };
                        context.Lekarze.Add(lekarz);
                        context.SaveChanges();

                        // --- PRZYKŁADOWE PROCEDURY ---
                        var pacjent = context.Pacjenci.FirstOrDefault();
                        if (pacjent != null)
                        {
                            var proc1 = new Procedura
                            {
                                TypBadaniaId = badania[0].ID, // Morfologia
                                LekarzWykonujacyId = lekarz.ID,
                                PacjentId = pacjent.ID,
                                DataWykonania = DateTime.Now.AddDays(-30),
                                InfoDodatkowe = "Badanie kontrolne"
                            };
                            var proc2 = new Procedura
                            {
                                TypBadaniaId = badania[2].ID, // EKG
                                LekarzWykonujacyId = lekarz.ID,
                                PacjentId = pacjent.ID,
                                DataWykonania = DateTime.Now.AddDays(-14),
                                InfoDodatkowe = "Badanie kardiologiczne"
                            };
                            var proc3 = new Procedura
                            {
                                TypBadaniaId = badania[3].ID, // USG
                                LekarzWykonujacyId = lekarz.ID,
                                PacjentId = pacjent.ID,
                                DataWykonania = DateTime.Now.AddDays(-7),
                                InfoDodatkowe = "USG kontrolne"
                            };
                            context.Procedury.AddRange(proc1, proc2, proc3);
                            context.SaveChanges();

                            // --- WYNIKI BADAŃ ---
                            context.Wyniki.Add(new Wynik
                            {
                                ProceduraId = proc1.ID,
                                OpisBadania = "WBC: 6.2, RBC: 4.8, HGB: 14.2 - wyniki w normie"
                            });
                            context.Wyniki.Add(new Wynik
                            {
                                ProceduraId = proc2.ID,
                                OpisBadania = "Rytm zatokowy miarowy, HR 72/min, bez zmian ST"
                            });
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}
