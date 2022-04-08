using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ratata
{
    /// <summary>
    /// Logika interakcji dla klasy Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void TwoPlayers(object sender, RoutedEventArgs e)
        {
            MainWindow objDwochGraczy = new MainWindow();
            this.Visibility = Visibility.Hidden;
            objDwochGraczy.Show();
        }
    }
}
