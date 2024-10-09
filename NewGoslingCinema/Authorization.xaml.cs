using System;
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
            password = Password.Password;
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
                        MessageBox.Show("Вход в аккаунт успешно выполнен!");
                        Thread newWindowThread = new Thread(new ThreadStart(CreateWindow));
                        newWindowThread.SetApartmentState(ApartmentState.STA);
                        newWindowThread.Start();
                        break;
                }
            }
        }
        void CreateWindow()
        {
            var window = new MainWindow();
            window.Show();
            System.Windows.Threading.Dispatcher.Run();

        }


    }
}
