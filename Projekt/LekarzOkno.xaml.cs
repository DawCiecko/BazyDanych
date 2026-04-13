using System;
using System.Collections.Generic;
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

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy LekarzOkno.xaml
    /// </summary>
    public partial class LekarzOkno : Window
    {
        public LekarzOkno()
        {
            InitializeComponent();
        }

        private void BTNMagazyn_Clicked(object sender, RoutedEventArgs e)
        {
            Window okno = new MagazynOkno();
            okno.Show();
        }
    }
}
