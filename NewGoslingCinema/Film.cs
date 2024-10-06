using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;



namespace NewGoslingCinema
{
    public class Film
    {
        public string name { get; set; }
        public string genre { get; set; }
        public BitmapImage image { get; set; }
        public string url { get; set; }
        public string otherInfo { get; set; }
        public BitmapImage hightQualityImage { get; set; }
        public string year { get; set; }

        static DirectoryInfo dir = new DirectoryInfo
            ("C:/Users/glebl/source/repos/NewGoslingCinema/NewGoslingCinema/HighQualityPosters/");
        static FileInfo[] Links = dir.GetFiles();


        public static List<Film> GetFilms()
        {
            List<Film> films = new List<Film>(6);
            for (int i = 0; i < 6; i++)
            {
                Film film = new Film();
                films.Add(film);
            }
            return films;
        }
        public static Tuple<string, string, string> Find(List<Film> films, BitmapImage image)
        {
            foreach(var f in films)
            {
                if (f.hightQualityImage.ToString() == image.ToString())
                {
                    return Tuple.Create(f.name, f.genre, f.otherInfo);
                }
            }
            return Tuple.Create("", "", "");
        }
        public static void SetHqimage(List<Film> films)
        {
            for (int i = 0; i<6; i++)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(Links[i].FullName, UriKind.Absolute);
                bitmap.EndInit();
                films[i].hightQualityImage = bitmap;
            }
   
        }
    }
}
