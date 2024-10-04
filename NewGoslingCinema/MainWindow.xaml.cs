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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewGoslingCinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Film> films = Film.GetFilms();
        public List<string> links = Parser.GetLinks();


        public MainWindow()
        {
            InitializeComponent();
            Parser.GetName(films);
            Parser.Image(films);
            Film.SetHqimage(films);
            Parser.GetFilmDetails(links, films);
            CreateImages(films);
            

        }
        private void CreateImages(List<Film> films)
        {
            var sp = new StackPanel();
            for (int i = 0; i <films.Count; i++)
            {
                Image image = new Image();
                image.Source = films[i].hightQualityImage;
                image.MouseDown += Image_MouseDown;
                sp.Children.Add(image);
            }
            Scroll.Content = sp; 
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            FilmPage page = new FilmPage();
            BitmapImage bitmap = new BitmapImage();
            var s = Convert.ToString(image.Source);
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(s, UriKind.Absolute);
            bitmap.EndInit();
            var info = Film.Find(films, bitmap);
            page.Show();

            page.Poster.Source = image.Source;
            page.filmname.Text = info.Item1;
            page.genre.Content = info.Item2;
            page.year.Text = info.Item3;

            
        }
    }
}
