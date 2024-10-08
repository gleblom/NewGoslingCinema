﻿using System;
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
using System.Threading;

namespace NewGoslingCinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Film> films = Film.GetFilms();
        public List<string> links = Parser.GetLinks();
        public List<FilmPage> pages = new List<FilmPage>();
        public Authorization authorization;


        public MainWindow(Authorization authorization)
        {
            InitializeComponent();
            Parser.GetName(films);
            Parser.GetFilmDetails(links, films);
            Parser.Image(films);
            Film.ToNormalText(films);
            CreateImages(films);
            this.authorization = authorization;
            //something.Content = Parser.ImgUrl[0];
            //Parser.Image(films);
            //Film.SetHqimage(films);
            //Parser.GetFilmDetails(links, films);
            //CreateImages(films);


        }
        private void CreateImages(List<Film> films)
        {
            var sp = new StackPanel();
            for (int i = 0; i <films.Count; i++)
            {
                Image image = new Image();
                image.Source = films[i].image;
                image.MouseDown += Image_MouseDown;
                sp.Children.Add(image);
            }
            Scroll.Content = sp;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(pages.Count != 0)
            {
                pages[0].Close();
                pages.Clear();
                Image image = sender as Image;
                CreateFilmPage(image);
            }
            else
            {
                Image image = sender as Image;
                CreateFilmPage(image);
            }
        }
        private void CreateFilmPage(Image image)
        {
            FilmPage page = new FilmPage();
            pages.Add(page);
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
            page.year.Content = info.Item3;
            page.info.Text = info.Item4;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            authorization.Close();
        }



    }
}
