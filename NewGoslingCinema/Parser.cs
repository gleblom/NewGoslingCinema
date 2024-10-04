using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;

namespace NewGoslingCinema
{
    class Parser
    {
        public static string url = "https://www.ivi.ru/person/rajan_gosling";
        public static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        public static void GetName(List<Film> films)
        {
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//span[contains(@class, 'nbl-slimPosterBlock__titleText')]");
            int i = 0;
            foreach (var name in names)
            {
                if (i != 6)
                {
                    films[i].name = name.InnerText;
                    i++;
                }

            }
        }
        //public static void GetInfo(string url, List<Film> films)
        //{
        //    var bookLinks = new List<string>();
        //    HtmlDocument doc = GetDocument(url);
        //    HtmlNodeCollection information = doc.DocumentNode.SelectNodes("//div[contains(@class, 'personFilmsList_itemOther')]/span");
        //    int i = 0;
        //    int j = 0;
        //    foreach (var info in information)
        //    {
        //        if (info.Attributes["class"].Value == "personFilmsList_itemGenres ellipsis-1")
        //        {
        //            films[i].genre = info.InnerText;
        //            i++;
        //        }
        //        if (info.Attributes["class"].Value == "personFilmsList_itemGen ellipsis-1")
        //        {
        //            films[j].otherInfo = info.InnerText;
        //            j++;
        //        }
        //    }
        //}
        public static void Image(List<Film> films)
        {
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection Images = doc.DocumentNode.SelectNodes("//div[contains(@class, 'nbl-poster__imageWrapper')]" +
                "/picture/img");
            int i = 0;
            foreach (var imag in Images)
            {
             
                string img = imag.Attributes["src"].Value;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(img, UriKind.Absolute);
                bitmap.EndInit();
                films[i].image = bitmap;
                i++;
            }
        }

        public static List<string> GetLinks()
        {
            var filmLinks = new List<string>();
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'ivi-carousel-item ivi-carousel-item-type_poster')]/a");
            var baseUri = new Uri(url);
            foreach (var link in linkNodes)
            {
                string href = link.Attributes["href"].Value;
                filmLinks.Add(new Uri(baseUri, href).AbsoluteUri);
            }
            return filmLinks;
        }
        public static void GetFilmDetails(List<string> urls, List<Film> films)
        {
            int i = 0;
            foreach (var url in urls)
            {
                if (i != 6)
                {
                    HtmlDocument document = GetDocument(url);
                    //var gen = "//span[contains(@test-id, 'meta_genre')]";
                    var year = "//div[contains(@class, 'watchParams__item')]/a[contains(@class, 'nbl-link nbl-link_style_wovou')]";
                    //films[i].genre = document.DocumentNode.SelectSingleNode(gen).InnerText;
                    films[i].otherInfo = document.DocumentNode.SelectSingleNode(year).InnerText;
                    i++;
                }
            }
        }
    }
}
