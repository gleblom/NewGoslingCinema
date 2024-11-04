using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace NewGoslingCinema
{
    public partial class AdminWindow : Window
    {
        string film;

        List<Film> films = Film.GetFilms();
        public AdminWindow()
        {
            InitializeComponent();
            Parser.GetName(films);
            FillFilms();
            SqlClass.Sessione(Sessions);
            DateCheck.DeletePastSessions();
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
            string dates = Convert.ToString(DateTimer.Value);
            if (FilmList.SelectedItem != null && dates != "")
            {
                if(DateTime.Now < DateTimer.Value)
                {
                    film = FilmList.SelectedItem.ToString();

                    string[] dt = SessionParser.TimeDateParse(dates);
                    SqlClass.AddSession(film, dt[1], dt[0]);
                    Thread.Sleep(100);
                    SqlClass.Sessione(Sessions);
                }
                else
                {
                    MessageBox.Show("Невозможно добавить сеанс!");
                }
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
