using ModeleDanych.Data;
using ModeleDanych.Models;
using ModeleDanych.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    
        private void BTNZatwierdz_Clicked(object sender, RoutedEventArgs e)
        {
            string login = TBLogin.Text;
            string haslo = TBHaslo.Password;
            // login : hasło
            // lekarz : test
            // pacjent : test
            // pracownik : test
            using (var context = new AppDbContext())
            {
                context.Database.EnsureCreated();
                AuthService authService = new(context);
                LoginResult result = authService.Zaloguj(login, haslo);
                Window okno = null;
                if (result.Sukces)
                {
                    //if (result.CzyPacjent)
                    //{
                    //    okno = new PacjentOkno();
                    //}
                    //if (result.CzyPracownik)
                    //{
                    //    if (result.ZalogowanyPracownik!.Lekarz != null)
                    //    {
                    //        okno = new LekarzOkno();
                    //    }
                    //    else
                    //    {
                    //        okno = new PracownikOkno();
                    //    }
                    //}
                    //if (okno != null)
                    //{
                    //    okno.Show();
                    //    this.Close();
                    //}
                    Widok widok = new(result);
                    widok.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Wiadomosc, "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}