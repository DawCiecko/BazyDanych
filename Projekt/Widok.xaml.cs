using ModeleDanych.Models;
using ModeleDanych.Services;
using ModeleDanych.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ModeleDanych.Data;

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy Widok.xaml
    /// </summary>
    public partial class Widok : Window
    {
        LoginResult _lr;
        private List<Button> CategoryButtons = new();
        private string currentAction = "";

        public Widok(LoginResult lr)
        {
            InitializeComponent();
            _lr = lr;
            if (lr.CzyPacjent)
            {
                this.Title = "Panel pacjenta";
                lbl_hello.Content = $"Witaj, {lr.ZalogowanyPacjent!.Imie}!";
                CreateCatButton("Umów wizytę u specjalisty", On_Click_UmówWizytęUSpecjalisty);
                CreateCatButton("Kalendarz wizyt", On_Click_PrzeglądajKalendarzWizyt);
                CreateCatButton("Recepty", On_Click_PrzeglądajRecepty);
                CreateCatButton("Historia leczenia", On_Click_PrzeglądajHistorięLeczenia);
                CreateCatButton("Informacje o koncie", On_Click_InformacjeKonta);

            }
            else
            {
                this.Title = "Panel obsługi";
                lbl_hello.Content = $"Witaj, {lr.ZalogowanyPracownik!.Imie}!";
                if (lr.ZalogowanyPracownik!.Lekarz != null)
                {
                    this.Title += " - lekarz";
                    CreateCatButton("Kartoteka pacjenta", On_Click_KartotekaPacjenta);
                    CreateCatButton("Wystaw receptę", On_Click_WystawReceptę);
                    CreateCatButton("Wystaw zlecenie", On_Click_WystawZlecenie);
                    CreateCatButton("Wpisz wyniki badań", On_Click_WpiszWyniki);
                    CreateCatButton("Magazyn", On_Click_DostępDoMagazynu);
                    CreateCatButton("Umów procedurę", On_Click_UmówProcedurę);
                    CreateCatButton("Dodaj Grafik", On_Click_DodajGrafik);

                }
                else
                {
                    this.Title += " - pracownik";
                    CreateCatButton("Magazyn", On_Click_DostępDoMagazynu);
                }
            }
        }

        void CreateCatButton(string content, RoutedEventHandler clickHandler)
        {
            Button btn = new Button();
            TextBlock tb = new();
            tb.Text = content;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.TextAlignment = TextAlignment.Center;

            btn.Content = tb;
            btn.Visibility = Visibility.Visible;
            btn.Click += ResetToBase;
            btn.Click += clickHandler;
            

            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Top;
            btn.Height = 45;
            btn.Width = 90;
            


            Grid.SetColumn(btn, 1);

            int topMargin = CategoryButtons.Count * 50;
            btn.Margin = new Thickness(0, topMargin, 0, 0);

            CategoryButtons.Add(btn);
            grid_btn_cat.Children.Add(btn);
        }
        async void On_Click_UmówWizytęUSpecjalisty(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz specjalizację:";
            cmbx_kol2.Visibility = Visibility.Visible;
            cmbx_kol2.SelectionChanged += On_Specjalizacja_SelectionChanged;
            lsbx_2kol.SelectionChanged += On_lsbx_UmówWizytę_SelectionChanged;
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var specjalizacje = await DBM.GetSpecjalizacje();
                cmbx_kol2.ItemsSource = specjalizacje;
            }
        }
        async void On_Click_PrzeglądajKalendarzWizyt(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Twoje wizyty:";
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var wizyty = await DBM.GetWizytyPacjenta(_lr.ZalogowanyPacjent!.ID);
                lsbx_2kol.ItemsSource = wizyty.Select(w => $"{w.Data:dd.MM.yyyy HH:mm} - Dr {w.Lekarz?.Pracownik?.Nazwisko} (Gabinet: {w.Gabinet?.Numer})").ToList();
            }
        }
        async void On_Click_PrzeglądajRecepty(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Twoje recepty:";
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var recepty = await DBM.GetReceptyPacjenta(_lr.ZalogowanyPacjent!.ID);
                lsbx_2kol.ItemsSource = recepty.Select(r => $"Kod: {r.Kod} - Dr {r.LekarzWystawiajacy?.Pracownik?.Nazwisko} - Prod.ID: {r.ProduktId}").ToList();
            }
        }
        async void On_Click_PrzeglądajHistorięLeczenia(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Historia procedur i badań (kliknij by zobaczyć wynik):";
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var procedury = await DBM.GetHistoriaProcedurPacjenta(_lr.ZalogowanyPacjent!.ID);
                lsbx_2kol.ItemsSource = procedury;
            }
            lsbx_2kol.SelectionChanged += On_Historia_SelectionChanged;
        }
        async void On_Click_InformacjeKonta(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Informacje o Tobie:";
            var pacjent = _lr.ZalogowanyPacjent!;
            List<string> info = new()
            {
                $"Imię: {pacjent.Imie}",
                $"Nazwisko: {pacjent.Nazwisko}",
                $"PESEL: {pacjent.PESEL}",
                $"Telefon: {pacjent.NrTel}",
                $"Adres: {pacjent.Adres}"
            };
            lsbx_2kol.ItemsSource = info;
        }
        async void On_Click_KartotekaPacjenta(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Lista Pacjentów:";
            currentAction = "KartotekaPacjenta";
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var pacjenci = await DBM.GetAllPacjenci();
                lsbx_2kol.ItemsSource = pacjenci;
            }
            lsbx_2kol.SelectionChanged += On_Kartoteka_SelectionChanged;
        }
        async void On_Click_WystawReceptę(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            window_action_panel.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz pacjenta:";
            currentAction = "WystawRecepte";
            lbl_action_title.Content = "Wystaw Receptę (zaznacz pacjenta po lewej)";
            
            lbl_input1.Content = "Wybierz produkt:";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.Visibility = Visibility.Visible;
            cmbx_action_input1.DisplayMemberPath = "Nazwa";
            
            btn_action.Visibility = Visibility.Visible;

            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                lsbx_2kol.ItemsSource = await DBM.GetAllPacjenci();
                cmbx_action_input1.ItemsSource = await DBM.GetAllProdukty();
            }
        }
        async void On_Click_WystawZlecenie(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            window_action_panel.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz pacjenta:";
            currentAction = "WystawZlecenie";
            lbl_action_title.Content = "Wystaw Zlecenie";
            
            lbl_input1.Content = "Wybierz badanie:";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.Visibility = Visibility.Visible;
            cmbx_action_input1.DisplayMemberPath = "Nazwa";
            
            btn_action.Visibility = Visibility.Visible;

            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                lsbx_2kol.ItemsSource = await DBM.GetAllPacjenci();
                cmbx_action_input1.ItemsSource = await DBM.GetAllBadania();
            }
        }
        async void On_Click_WpiszWyniki(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            window_action_panel.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz procedurę:"; // Uproszczenie dla prototypu
            currentAction = "WpiszWykini";
            lbl_action_title.Content = "Wpisz Wyniki";
            
            lbl_input1.Content = "Opis wyniku:";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Visible;
            
            btn_action.Visibility = Visibility.Visible;
            
            using (var context = new AppDbContext())
            {
                lsbx_2kol.ItemsSource = context.Procedury
                    .Include(p => p.TypBadania)
                    .Include(p => p.Pacjent)
                    .Include(p => p.Wynik)
                    .Where(p => p.Wynik == null)
                    .ToList();
            }
        }
        async void On_Click_UmówWizytęUSpecjalistyLek(object sender, RoutedEventArgs e)
        {
            // Można na razie wywołać uproszczoną wersję pacjenta
            On_Click_UmówWizytęUSpecjalisty(sender, e);
        }
        async void On_Click_DostępDoMagazynu(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Stan Magazynu:";

            // Konfiguracja panelu akcji dla magazynu
            window_action_panel.Visibility = Visibility.Visible;
            currentAction = "ZarzadzajMagazynem";
            lbl_action_title.Content = "Aktualizuj zapasy";

            lbl_input1.Content = "Wybierz produkt:";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.Visibility = Visibility.Visible;
            cmbx_action_input1.DisplayMemberPath = "Nazwa";

            lbl_input2.Content = "Ilość (+ by dodać, - by odjąć):";
            lbl_input2.Visibility = Visibility.Visible;
            tb_input2.Visibility = Visibility.Visible;

            btn_action.Visibility = Visibility.Visible;

            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var stan = await DBM.GetStanMagazynowy();
                lsbx_2kol.ItemsSource = stan.Select(s => $"{s.Produkt?.Nazwa} - Ilość: {s.Ilosc}").ToList();
                cmbx_action_input1.ItemsSource = await DBM.GetAllProdukty();
            }
        }
        async void On_Click_UmówProcedurę(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            window_action_panel.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz pacjenta:";
            currentAction = "UmowProcedura";
            lbl_action_title.Content = "Umów Procedurę";
            
            lbl_input1.Content = "Wybierz badanie:";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.Visibility = Visibility.Visible;
            cmbx_action_input1.DisplayMemberPath = "Nazwa";
            
            lbl_input2.Content = "Data (RRRR-MM-DD):";
            lbl_input2.Visibility = Visibility.Visible;
            tb_input2.Visibility = Visibility.Visible;

            btn_action.Visibility = Visibility.Visible;

            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                lsbx_2kol.ItemsSource = await DBM.GetAllPacjenci();
                cmbx_action_input1.ItemsSource = await DBM.GetAllBadania();
            }
        }
        async void On_Specjalizacja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_kol2.SelectedItem is string selectedSpecjalizacja)
            {
                using (var context = new AppDbContext())
                {
                    var DBM = new DBManagerService(context);
                    var dostepneWizyty = await DBM.GetDostepneWizyty(selectedSpecjalizacja);
                    lsbx_2kol.ItemsSource = dostepneWizyty;
                }
            }
        }
        async void On_lsbx_UmówWizytę_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbx_2kol.SelectedItem is Wizyta selectedWizyta)
            {
                MessageBoxResult res = MessageBox.Show($"Czy na pewno chcesz umówić wizytę:\n{selectedWizyta}", "Potwierdzenie Wizyty", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    using (var context = new AppDbContext())
                    {
                        var DBM = new DBManagerService(context);
                        int pacjentId = _lr.ZalogowanyPacjent != null ? _lr.ZalogowanyPacjent.ID : 1; 
                        bool success = await DBM.BookWizyta(pacjentId, selectedWizyta.ID);
                        if (success)
                        {
                            MessageBox.Show("Umówiono wizytę.");
                            lsbx_2kol.SelectedItem = null;
                            // Odśwież listę
                            var dostepneWizyty = await DBM.GetDostepneWizyty((string)cmbx_kol2.SelectedItem);
                            lsbx_2kol.ItemsSource = dostepneWizyty;
                        }
                    }
                } 
            }
        }

        async void On_Kartoteka_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentAction == "KartotekaPacjenta" && lsbx_2kol.SelectedItem is Pacjent selectedPacjent)
            {
                int pacjentId = selectedPacjent.ID;
                if (pacjentId > 0)
                {
                    window_action_panel.Visibility = Visibility.Visible;
                    lbl_action_title.Content = "Wyniki badań pacjenta";
                    lsbx_action.Visibility = Visibility.Visible;
                    
                    // Ukryj zbędne formularze jeśli były widoczne
                    lbl_input1.Visibility = Visibility.Collapsed;
                    tb_input1.Visibility = Visibility.Collapsed;
                    btn_action.Visibility = Visibility.Collapsed;

                    using (var context = new AppDbContext())
                    {
                        var DBM = new DBManagerService(context);
                        var procedury = await DBM.GetHistoriaProcedurPacjenta(pacjentId);
                        lsbx_action.ItemsSource = procedury.Select(p => 
                            $"{p.TypBadania?.Nazwa} ({p.DataWykonania:dd.MM.yyyy})\nOpis/Wynik: {(p.Wynik != null ? p.Wynik.OpisBadania : "Brak wyniku")}\n"
                        ).ToList();
                    }
                }
            }
        }

        private void On_Historia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbx_2kol.SelectedItem is Procedura proc)
            {
                if (proc.Wynik != null)
                {
                    MessageBox.Show($"Wynik badania {proc.TypBadania?.Nazwa} z dnia {proc.DataWykonania:dd.MM.yyyy}:\n\n{proc.Wynik.OpisBadania}", "Wynik Badania");
                }
                else
                {
                    MessageBox.Show("Dla tego badania nie wpisano jeszcze wyniku.", "Brak Wyniku");
                }
            }
        }

        void ResetToBase(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Collapsed;
            lsbx_2kol.ItemsSource = null;
            cmbx_kol2.ItemsSource = null;
            cmbx_kol2.Visibility = Visibility.Collapsed;
            cmbx_kol2.SelectionChanged -= On_Specjalizacja_SelectionChanged;
            lsbx_2kol.SelectionChanged -= On_lsbx_UmówWizytę_SelectionChanged;
            lsbx_2kol.SelectionChanged -= On_Kartoteka_SelectionChanged;
            lsbx_2kol.SelectionChanged -= On_Historia_SelectionChanged;

            window_action_panel.Visibility = Visibility.Collapsed;
            lbl_input1.Visibility = Visibility.Collapsed;
            lbl_input2.Visibility = Visibility.Collapsed;
            lbl_input3.Visibility = Visibility.Collapsed;
            tb_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.Visibility = Visibility.Collapsed;
            cmbx_action_input1.ItemsSource = null;
            cmbx_action_input3.Visibility = Visibility.Collapsed;
            cmbx_action_input3.ItemsSource = null;
            tb_input2.Visibility = Visibility.Collapsed;
            tb_input3.Visibility = Visibility.Collapsed;
            btn_action.Visibility = Visibility.Collapsed;
            lsbx_action.Visibility = Visibility.Collapsed;
            tb_input1.Text = "";
            tb_input2.Text = "";
            tb_input3.Text = "";
            currentAction = "";
        }
        async void On_Click_DodajGrafik(object sender, RoutedEventArgs e)
        {
            window_action_panel.Visibility = Visibility.Visible;
            currentAction = "DodajGrafik";
            lbl_action_title.Content = "Generuj grafik wizyt";
            
            lbl_input1.Content = "Data Start (RRRR-MM-DD GG:MM):";
            lbl_input1.Visibility = Visibility.Visible;
            tb_input1.Visibility = Visibility.Visible;

            lbl_input2.Content = "Data Koniec (RRRR-MM-DD GG:MM):";
            lbl_input2.Visibility = Visibility.Visible;
            tb_input2.Visibility = Visibility.Visible;

            lbl_input3.Content = "Gabinet:";
            lbl_input3.Visibility = Visibility.Visible;
            cmbx_action_input3.Visibility = Visibility.Visible;
            cmbx_action_input3.DisplayMemberPath = "Numer";

            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                cmbx_action_input3.ItemsSource = await DBM.GetAllGabinety();
            }
            
            btn_action.Visibility = Visibility.Visible;
        }
        async void On_btn_action_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                int lekarzId = _lr.ZalogowanyPracownik?.Lekarz?.ID ?? 0;

                int pacjentId = (lsbx_2kol.SelectedItem as Pacjent)?.ID ?? 0;

                if (currentAction == "WystawRecepte")
                {
                    if (pacjentId > 0 && cmbx_action_input1.SelectedItem is Produkt prod)
                    {
                        await DBM.AddRecepta(new Recepta { PacjentId = pacjentId, ProduktId = prod.ID, LekarzWystawiajacyId = lekarzId, Kod = new Random().Next(1000, 9999) });
                        MessageBox.Show("Dodano receptę.");
                    }
                }
                else if (currentAction == "WystawZlecenie")
                {
                    if (pacjentId > 0 && cmbx_action_input1.SelectedItem is Badanie bad)
                    {
                        await DBM.AddSkierowanie(new Skierowanie { PacjentId = pacjentId, BadanieId = bad.ID, LekarzWystawiajacyId = lekarzId, Kod = new Random().Next(1000, 9999), Data = DateTime.Now });
                        MessageBox.Show("Wystawiono skierowanie.");
                    }
                }
                else if (currentAction == "WpiszWykini")
                {
                    if (lsbx_2kol.SelectedItem is Procedura proc)
                    {
                        await DBM.AddWynik(new Wynik { ProceduraId = proc.ID, OpisBadania = tb_input1.Text });
                        MessageBox.Show("Dodano wynik.");
                    }
                }
                else if (currentAction == "UmowProcedura")
                {
                    if (pacjentId > 0 && cmbx_action_input1.SelectedItem is Badanie bad && DateTime.TryParse(tb_input2.Text, out DateTime data))
                    {
                        await DBM.BookProcedura(pacjentId, bad.ID, lekarzId, data);
                        MessageBox.Show("Umówiono procedurę.");
                    }
                }
                else if (currentAction == "ZarzadzajMagazynem")
                {
                    if (cmbx_action_input1.SelectedItem is Produkt prod && int.TryParse(tb_input2.Text, out int zmiana))
                    {
                        bool success = await DBM.UpdateStanMagazynowy(prod.ID, zmiana);
                        if (success)
                        {
                            MessageBox.Show("Zaktualizowano stan magazynowy.");
                            // Odśwież listę
                            var stan = await DBM.GetStanMagazynowy();
                            lsbx_2kol.ItemsSource = stan.Select(s => $"{s.Produkt?.Nazwa} - Ilość: {s.Ilosc}").ToList();
                        }
                        else
                        {
                            MessageBox.Show("Błąd aktualizacji (np. próba odjęcia więcej niż jest na stanie).");
                        }
                    }
                }
                else if (currentAction == "DodajGrafik")
                {
                    if (DateTime.TryParse(tb_input1.Text, out DateTime start) && 
                        DateTime.TryParse(tb_input2.Text, out DateTime end) && 
                        cmbx_action_input3.SelectedItem is Gabinet selectedGabinet)
                    {
                        await DBM.GenerateWizyty(start, end, lekarzId, selectedGabinet.ID);
                        MessageBox.Show("Wygenerowano puste wizyty w grafiku.");
                    }
                    else
                    {
                        MessageBox.Show("Błędne dane. Sprawdź format daty i wybierz gabinet.");
                    }
                }
            }
        }
    }
}
