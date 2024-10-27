using System.Windows;

namespace NewGoslingCinema
{

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
                string dt = SessionList.SelectedItem.ToString();
                string[] timedate = SessionParser.TimeDateParser(SessionList.SelectedItem.ToString());
                mainWindow.Cage.Items.Add("Фильм:" + filmname.Text + "," + " Дата: " + 
                    timedate[0] + " Время: " + timedate[1]);
                mainWindow.times.Add(timedate[1]);
                mainWindow.dates.Add(timedate[0]);
                mainWindow.pageFilms.Add(filmname.Text);
            }
        }
    }
}
