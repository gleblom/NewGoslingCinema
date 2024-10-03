using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace NewGoslingCinema
{
    public class Film
    {
        public string name { get; set; }
        public string genre { get; set; }
        public BitmapImage image { get; set; }
        public string URL { get; set; }
        public string otherInfo { get; set; }

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
                if (f.image == image)
                {
                    return Tuple.Create(f.name, f.genre, f.otherInfo);
                }
            }
            return Tuple.Create("", "", "");
        }
    }
}
