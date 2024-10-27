using System;
using System.Collections.Generic;
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
        public string year { get; set; }

        public static List<Film> GetFilms()
        {
            List<Film> films = new List<Film>(20);
            for (int i = 0; i < 20; i++)
            {
                Film film = new Film();
                films.Add(film);
            }
            return films;
        }
        public static Tuple<string, string, string, string> Find(List<Film> films, BitmapImage image)
        {
            foreach(var f in films)
            {
                if (f.image.ToString() == image.ToString())
                {
                    return Tuple.Create(f.name, f.genre, f.year, f.otherInfo);
                }
            }
            return Tuple.Create("", "", "", "");
        }

        public static void ToNormalText(List<Film> films)
        {
            var charsToRemove = new string[] 
            { "n", "b", "s", "p", "&", "h", "l", "e", "i", "d", "a", "l", "q", "u", "o", ";", "m", "e", "r"};
            foreach (var d in charsToRemove)
            {
                foreach(var film in films)
                {
                    film.otherInfo = film.otherInfo.Replace(d, string.Empty);
                }
            }
        }
    }
}
