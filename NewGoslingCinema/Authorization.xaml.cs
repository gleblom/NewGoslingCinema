using System.Threading;
using System.Windows;
using System.Windows.Input;


namespace NewGoslingCinema
{

    public partial class Authorization : Window
    {
        string login;

        string password;


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
                        Hide();

                        MainWindow.authorization = this;
                        MainWindow.name = login;

                        Login.Text = "";
                        Password.Text = "";

                        Thread newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
                        newWindowThread.SetApartmentState(ApartmentState.STA);
                        newWindowThread.IsBackground = true;
                        newWindowThread.Start();

                        MainWindow mainWindow = new MainWindow();
                        Application.Current.MainWindow = mainWindow;
                        Application.Current.MainWindow.Show();

                        Close();

                        break;
                }
            }
        }
        private void ThreadStartingPoint()
        {
            LoadWindow loadWindow = new LoadWindow();
            MainWindow.loadWindow = loadWindow;
            loadWindow.Show();
            System.Windows.Threading.Dispatcher.Run();
        }

        private void reg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.MainWindow = new Registration();
            Application.Current.MainWindow.Show();
            Close();
        }
    }
}
