using System.Windows;
using System.Windows.Controls;
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
                if (Password.Text.Length > 4) 
                {
                    int i = await SqlClass.Registration(login, password);
                    if (i == 0)
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

        private void auth_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow = new Authorization();
            Application.Current.MainWindow.Show();
            Close();
        }

        private void Password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length < 5)
            {
                MessageBox.Show("Пароль должен состоять минимум из 5 символов!");
            }
        }
    }
}
