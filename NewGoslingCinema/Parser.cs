using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace NewGoslingCinema
{
    class Parser
    {
        public static string url = "https://www.kinoafisha.info/person/8004002/";
        public static List<string> ImgUrl = new List<string>();
        public static HtmlDocument GetDocument(string url)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            return doc;
        }

        public static void GetName(List<Film> films)
        {
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//a[contains(@class, 'personFilmsList_itemTitle ellipsis-2')]");
            int i = 0;
            foreach (var name in names)
            {
                if (i != 21)
                {
                    films[i].name = name.InnerText;
                    i++;
                }

            }
        }
        public static void Image(List<Film> films)
        {
            int i = 0;
            foreach (var url in ImgUrl)
            {
                if (i != 21)
                {
                    HtmlDocument doc = GetDocument(url);
                    var xpath = "//a[contains(@class, 'postersList_item grid_cell3')]";
                    var img = doc.DocumentNode.SelectSingleNode(xpath).Attributes["href"].Value;
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(img, UriKind.Absolute);
                    bitmap.EndInit();
                    films[i].image = bitmap;
                    films[i].url = img;
                    i++;
                }

            }
        }

        public static List<string> GetLinks()
        {
            var filmLinks = new List<string>();
            HtmlDocument doc = GetDocument(url);
            HtmlNodeCollection linkNodes = doc.DocumentNode.SelectNodes("//a[contains(@class, 'personFilmsList_itemTitle ellipsis-2')]");
            var baseUri = new Uri(url);
            int i = 0;
            foreach (var link in linkNodes)
            {
                if (i != 21)
                {
                    string href = link.Attributes["href"].Value;
                    filmLinks.Add(new Uri(baseUri, href).AbsoluteUri);
                }
            }
            return filmLinks;
        }
        public static void GetFilmDetails(List<string> urls, List<Film> films)
        {
            int i = 0;
            foreach (var url in urls)
            {
                if (i != 21)
                {
                    HtmlDocument document = GetDocument(url);
                    var imgs = "//a[contains(@data-subcontent-btn, 'posters')]";
                    var imgUrl = document.DocumentNode.SelectSingleNode(imgs).Attributes["href"].Value;
                    var baseUri = new Uri(url);
                    ImgUrl.Add(new Uri(baseUri, imgUrl).AbsoluteUri);
                    var gen = "//span[contains(@class, 'filmInfo_genreItem button-main')]";
                    films[i].genre = document.DocumentNode.SelectSingleNode(gen).InnerText;
                    var year = "//span[contains(@class, 'filmInfo_infoData')]";
                    var nodes = document.DocumentNode.SelectNodes(year);
                    foreach (var node in nodes)
                    {
                        if (node.InnerText.Length == 4)
                        {
                            films[i].year = node.InnerText;
                        }
                    }
                    var info = "//div[contains(@data-tabs-content-item, '1')]/div/div/div/div/p";
                    if (document.DocumentNode.SelectSingleNode(info)?.InnerText == null)
                    {
                        info = "//div[contains(@data-tabs-content-item, '1')]/div/div/p";
                        if (document.DocumentNode.SelectSingleNode(info)?.InnerText == "Добро и зло под масками друг друга.")
                        {
                            var sn = document.DocumentNode.SelectNodes(info);
                            foreach (var node in sn)
                            {
                                if (node.InnerText != "Добро и зло под масками друг друга.")
                                {
                                    films[i].otherInfo = node.InnerText;
                                }
                            }
                        }
                        else if (document.DocumentNode.SelectSingleNode(info)?.InnerText == "Когда речь идет о деньгах, совесть молчит. " +
                            "А уж если речь об огромных деньгах!..")
                        {
                            var sn = document.DocumentNode.SelectNodes(info);
                            foreach (var node in sn)
                            {
                                if (node.InnerText != "Когда речь идет о деньгах, совесть молчит. " +
                                    "А уж если речь об огромных деньгах!..")
                                {
                                    films[i].otherInfo = node.InnerText;
                                }
                            }
                        }
                        else
                        {
                            if (i == 0)
                            {

                            }
                            else
                            {
                                films[i].otherInfo = document.DocumentNode.SelectSingleNode(info).InnerText;
                            }
                        }


                    }
                    else
                    {
                        films[i].otherInfo = document.DocumentNode.SelectSingleNode(info).InnerText;
                    }
                    i++;
                }
            }
        }

    }
}
