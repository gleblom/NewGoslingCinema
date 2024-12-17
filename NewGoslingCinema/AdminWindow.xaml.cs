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
            SqlClass.SelectAdmins(AdminGrid);
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
                    SqlClass.Sessione(Sessions);
                }

            }
            catch
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
        }

        private async void AddAdmin_Click(object sender, RoutedEventArgs e)
        {
            var itemsource = AdminGrid.ItemsSource;
            foreach (DataRowView item in itemsource)
            {
                if (item["UserID"].ToString() == "")
                {
                    var login = item["UserLogin"].ToString();
                    var password = item["UserPassword"].ToString();
                    if(login != "" && password != "")
                    {
                        if (password.Length > 4)
                        {
                            int i = await SqlClass.AdminRegistration(login, password);
                            if (i == 0)
                            {
                                MessageBox.Show("Регистрация прошла успешно!");
                                SqlClass.SelectAdmins(AdminGrid);
                                SqlClass.SelectAdmins(AdminGrid);
                            }
                            else
                            {
                                MessageBox.Show("Такой пользователь уже существует!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пароль должен состоять минимум из 5 символов!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполните пустые поля!");
                    }
                }
            }
        }

        private void DeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AdminGrid.Items.Count > 1)
                {
                    DataRowView item = (DataRowView)AdminGrid.SelectedItem;
                    if (item != null)
                    {
                        SqlClass.DeleteUser(item["UserID"]);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Выберите поле для удаления!");
            }
            SqlClass.SelectAdmins(AdminGrid);
            SqlClass.SelectAdmins(AdminGrid);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow = new Authorization();
            Application.Current.MainWindow.Show();
            Close();
        }

        private void AdminGrid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            AdminGrid.Columns[0].IsReadOnly = true;
        }
    }
}
