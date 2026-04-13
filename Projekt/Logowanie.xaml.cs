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
using ModeleDanych.Services;

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
            //informacja bedzie wychodzila po sprawdzeniu danych uzytkownika wiec te zmienne tutaj sa zeby sie nie czepialo o bledy
            bool czyistnieje = true;
            int typkonta = 1;
            Window okno = null;
            if (czyistnieje == true)
            {
                if (typkonta == 1)
                {
                    okno = new PacjentOkno();
                }
                if(typkonta == 2)
                {
                    okno = new LekarzOkno();
                }
                if (typkonta == 3)
                {
                    okno = new PracownikOkno();
                }
                if (okno != null)
                {
                    okno.Show();
                    this.Close();
                }
            }
        }
    }
}