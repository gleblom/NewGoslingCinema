﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

    public partial class Authorization : Window
    {
        public string login;
        public string password;
        public Authorization()
        {
            InitializeComponent();
        }

        private async void SignIn_Click(object sender, RoutedEventArgs e)
        {
            login = Login.Text;
            password = Password.Text;
            if (login != "" && password != "")
            {
                int i = await SqlClass.Auth(login, password);
                switch (i)
                {
                    case -1:
                        MessageBox.Show("Такого аккаунта не существует");
                        break;
                    case 0:
                        MessageBox.Show("Неправильный пароль!");
                        break;
                    case 1:
                        MainWindow.authorization = this;
                        MainWindow.name = login;
                        Login.Text = "";
                        Password.Text = "";
                        Loading.Visibility = Visibility.Visible;
                        Loading.IsIndeterminate = true;
                        load.Visibility = Visibility.Visible;
                        IsEnabled = false;
                        Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));

                        newWindowThread.SetApartmentState(ApartmentState.STA);
                        newWindowThread.IsBackground = true;
                        newWindowThread.Start();
                        //MainWindow.name = login;
                        //MainWindow mainWindow = new MainWindow();
                        //mainWindow.Show();
                        //Hide();
                        break;
                }
            }
        }
        private void ThreadStartingPoint()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }


    }
}
