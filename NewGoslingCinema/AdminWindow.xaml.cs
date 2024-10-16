using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        string film;
        string time;
        string date;

        public List<Film> films = Film.GetFilms();
        public AdminWindow()
        {
            InitializeComponent();
            Parser.GetName(films);
            FillFilms();
            SqlClass.Sessione(Sessions);
        }
        private void FillFilms()
        {
            foreach(var film in films)
            {
                FilmList.Items.Add(film.name);
            }
        }


        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            string dates = Convert.ToString(DateTime.Value);
            if (FilmList.SelectedItem != null && dates != "")
            {
                film = FilmList.SelectedItem.ToString();

                Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");
                Regex r = new Regex(@"[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}");
                date = regex.Match(dates).ToString();
                time = r.Match(dates).ToString();
                SqlClass.AddSession(film, time, date);
                SqlClass.Sessione(Sessions);
            }
            else
            {
                MessageBox.Show("Невозможно добавить сеанс!");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView item = (DataRowView)Sessions.SelectedItem;
                if (item != null) 
                {
                    SqlClass.Delete(item["SessionID"]);
                    SqlClass.Sessione(Sessions);
                }

            }
            catch
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }
    }
}
