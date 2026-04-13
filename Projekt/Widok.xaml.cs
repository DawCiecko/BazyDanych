using ModeleDanych.Models;
using ModeleDanych.Services;
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

        public Widok(LoginResult lr)
        {
            InitializeComponent();
            _lr = lr;
            if (lr.CzyPacjent)
            {
                this.Title = "Panel pacjenta";
                lbl_hello.Content = $"Witaj, {lr.ZalogowanyPacjent!.Imie}!";
                CreateCatButton("Umów wizytę", On_Click_UmówWizytę);
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
                    CreateCatButton("Umów wizytę u specjalisty", On_Click_UmówWizytęUSpecjalistyLek);
                    CreateCatButton("Magazyn", On_Click_DostępDoMagazynu);
                    

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

        async void On_Click_UmówWizytę(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Godziny przyjęć:";
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var grafiki = await DBM.GetGrafikLekarzy();
                lsbx_2kol.ItemsSource = grafiki;
            }
        }
        async void On_Click_UmówWizytęUSpecjalisty(object sender, RoutedEventArgs e)
        {
            window_sub_listbox_col2.Visibility = Visibility.Visible;
            lbl_2kol.Content = "Wybierz specjalizację:";
            cmbx_kol2.Visibility = Visibility.Visible;
            cmbx_kol2.SelectionChanged += On_Specjalizacja_SelectionChanged;
            using (var context = new AppDbContext())
            {
                var DBM = new DBManagerService(context);
                var specjalizacje = await DBM.GetSpecjalizacje();
                cmbx_kol2.ItemsSource = specjalizacje;
            }
        }
        async void On_Click_PrzeglądajKalendarzWizyt(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_PrzeglądajRecepty(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_PrzeglądajHistorięLeczenia(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_InformacjeKonta(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_KartotekaPacjenta(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_WystawReceptę(object sender, RoutedEventArgs e)
        {
        }
        async void On_Click_WystawZlecenie(object sender, RoutedEventArgs e)
        {

        }
        async void On_Click_WpiszWyniki(object sender, RoutedEventArgs e)
        {

        }
        async void On_Click_UmówWizytęUSpecjalistyLek(object sender, RoutedEventArgs e)
        {

        }
        async void On_Click_DostępDoMagazynu(object sender, RoutedEventArgs e)
        {

        }
        async void On_Click_UmówProcedurę(object sender, RoutedEventArgs e)
        {

        }
        async void On_Specjalizacja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbx_kol2.SelectedItem is string selectedSpecjalizacja)
            {
                using (var context = new AppDbContext())
                {
                    var DBM = new DBManagerService(context);
                    var grafiki = await DBM.GetGrafikLekarzy(selectedSpecjalizacja);
                    lsbx_2kol.ItemsSource = grafiki;
                }
            }
        }
        async void On_lsbx_UmówWizytę_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsbx_2kol.SelectedItem is Grafik selectedGrafik)
            {
                MessageBoxResult res = MessageBox.Show($"Czy na pewnoe chcesz umówić wizytę: {selectedGrafik}", "Potwierdzenie Wizyty", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    using (var context = new AppDbContext())
                    {
                        var DBM = new DBManagerService(context);
                        // Wprowadzić endpointa w DBManagerService do umawiania
                    }
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
        }
    }
}
