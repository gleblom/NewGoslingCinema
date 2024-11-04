using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using iTextSharp.text;
using Document = iTextSharp.text.Document;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;
using System.Diagnostics;
using System.Reflection.Metadata;

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

        public static string name;

        string session;

        public static Authorization authorization;

        public static LoadWindow loadWindow;

        FilmPage filmPage;

        public List<string> dates = new List<string>();

        public List<string> times = new List<string>();

        public List<string> pageFilms = new List<string>();


        public MainWindow()
        {
            InitializeComponent();

            Parser.GetName(films);

            Parser.GetFilmDetails(links, films);

            Film.ToNormalText(films);

            Parser.Image(films);
           
            CreateImages(films);
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
            SqlClass.ShowSessions(info.Item1, page.SessionList);
            page.mainWindow = this;
            filmPage = page;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SqlClass.SelectTickets(name, Tickets);
            loadWindow.Dispatcher.Invoke(CloseWindow);

        }
        private void CloseWindow()
        {
            loadWindow.Close();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int i = Cage.SelectedIndex;
            dates.RemoveAt(i);
            times.RemoveAt(i);
            pageFilms.RemoveAt(i);
            Cage.Items.Remove(Cage.SelectedItem);
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (Cage.SelectedItem != null)
            {
                int index = Cage.SelectedIndex;
                session = Cage.SelectedItem.ToString();
                SqlClass.Tickets(name, dates[index], times[index], pageFilms[index]);
                Tickets.Items.Add(session);
                dates.RemoveAt(index);
                times.RemoveAt(index);
                pageFilms.RemoveAt(index);
                Cage.Items.Remove(Cage.SelectedItem);
            }
        }

        private void WasteAll_Click(object sender, RoutedEventArgs e)
        {
            Cage.Items.Clear();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if(filmPage != null)
            {
                filmPage.Close();
            }
            Application.Current.MainWindow = new Authorization();
            Close();
            Application.Current.MainWindow.Show();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (filmPage != null)
            {
                filmPage.Close();
            }
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            int i = 300;
            if (Tickets.Items.Count > 20)
            {
                i = 270;

            }
            if(Tickets.Items.Count > 35)
            {
                i = 240;
            }
            if(Tickets.Items.Count > 50)
            {
                i = 210;
            }
            MessageBox.Show("Обычная стоимость билета - 300. Для постоянных посетителей действуют постоянные скидки: " +
                $"\n Текущая стоимость билета - {i}" +
                "\n Куплено более 20 билетов - 10%" +
                "\n Куплено более 35 билетов - 20%" +
                "\n Куплено более 50 билетов - 30%", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void SavePDF(object sender, RoutedEventArgs e) {
            try
            {

                string ticket = Tickets.SelectedItem.ToString();
                string i = Convert.ToString(Tickets.SelectedIndex) + ".pdf";
                string path = Directory.GetCurrentDirectory() + $@"\Tickets";
                if(Directory.Exists(path)) 
                {
                    CreatePDF(path, ticket, i);
                }
                else
                {
                    Directory.CreateDirectory(path);
                    CreatePDF(path, ticket, i);
                }
            }
            catch
            {

            }
        }
        private void CreatePDF(string path, string info, string i)
        {
            path = Directory.GetCurrentDirectory() + $@"\Tickets\{i}";
            var document = new Document(PageSize.A7, 20, 20, 30, 20);
            string ttf = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "ARIALNBI.TTF");
            var baseFont = BaseFont.CreateFont(ttf, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            var font = new Font(baseFont, Font.DEFAULTSIZE, Font.NORMAL);
            using (var writer = PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create)))
            {

                document.Open();
                document.NewPage();
                document.Add(new Paragraph(info, font));
                document.Close();
                writer.Close();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            string path = Directory.GetCurrentDirectory() + @"\Tickets";
            if (Directory.Exists(path))
            {
                Process.Start("explorer.exe", path);
            }
            else 
            {
                Directory.CreateDirectory(path);
                Process.Start("explorer.exe", path);
            }
        }

        private void BuyAll_Click(object sender, RoutedEventArgs e)
        {
            if(Cage.Items.Count > 0) 
            {
                int i = 0;

                foreach (var item in Cage.Items)
                {
                    SqlClass.Tickets(name, dates[i], times[i], pageFilms[i]);
                    i++;
                    Tickets.Items.Add(item);
                }
                Cage.Items.Clear();
            }
            else
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Stop);
            }
        }
    }
}
