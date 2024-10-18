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

namespace NewGoslingCinema
{
    /// <summary>
    /// Логика взаимодействия для FilmPage.xaml
    /// </summary>
    public partial class FilmPage : Window
    {
        public MainWindow mainWindow;
        public FilmPage()
        {
            InitializeComponent();
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if(SessionList.SelectedItem != null)
            {
                mainWindow.Cage.Items.Add(SessionList.SelectedItem);
            }
        }
    }
}
