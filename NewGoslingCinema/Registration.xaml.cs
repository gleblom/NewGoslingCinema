using System.Windows;
using System.Windows.Input;

namespace NewGoslingCinema
{

    public partial class Registration : Window
    {
        string login;
        string password;
        public Registration()
        {
            InitializeComponent();
        }

        private async void SignUp_Click(object sender, RoutedEventArgs e)
        {
            login = Login.Text;
            password = Password.Text;
            if(Login.Text != "" && Password.Text != "")
            {
                int i = await SqlClass.Registration(login, password);
                if(i == 0) 
                {
                    MessageBox.Show("Регистрация прошла успешно!");
                    Login.Text = "";
                    Password.Text = "";
                }
                else
                {
                    MessageBox.Show("Такой пользователь уже существует!");
                }
            }
        }

        private void auth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow = new Authorization();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
