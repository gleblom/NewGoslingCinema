using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;
using System.Diagnostics;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Windows.Documents;
using Paragraph = iText.Layout.Element.Paragraph;
using iText.Kernel.Font;
using iText.IO.Image;
using QRCoder;
using System.Drawing;

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

        public int price;

        public int fullPrice = 0;


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
            //sp.ItemHeight = 300;
            //sp.ItemWidth = 250;
            WrapPanel wp = new WrapPanel();
            for (int i = 0; i <films.Count; i++)
            {
                Image image = new Image();
                image.Width = 250;
                image.Height = 300;
                image.Source = films[i].image;
                image.MouseDown += Image_MouseDown;
                wp.Children.Add(image);
            }
            Scroll.Content = wp;
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
            SetPrice();
            loadWindow.Dispatcher.Invoke(CloseWindow);

        }
        private void CloseWindow()
        {
            loadWindow.Close();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            fullPrice = fullPrice - price;
            cost.Content = fullPrice;
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
                fullPrice = fullPrice - price;
                cost.Content = fullPrice;
                int index = Cage.SelectedIndex;
                session = Cage.SelectedItem.ToString();
                SqlClass.Tickets(name, dates[index], times[index], pageFilms[index]);
                Tickets.Items.Add(session);
                dates.RemoveAt(index);
                times.RemoveAt(index);
                pageFilms.RemoveAt(index);
                Cage.Items.Remove(Cage.SelectedItem);
                SetPrice();
            }
        }

        private void WasteAll_Click(object sender, RoutedEventArgs e)
        {
            Cage.Items.Clear();
            cost.Content = 0;
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

            DateTime today = DateTime.Today;
            int m = today.Month;
            int d = today.Day;
            if (m == 11 && d == 12)
            {
                GoslingBirthDay();
            }
            else
            {
                ShowInfo();
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CreatePDF(string path, string info, string i)
        {
            path = Directory.GetCurrentDirectory() + $@"\Tickets\{i}";

            using (PdfWriter writer = new PdfWriter(path))
            {
                using (PdfDocument pdfDocument = new PdfDocument(writer))
                {
                    Document document = new Document(pdfDocument, PageSize.A6);
                    var font = PdfFontFactory.CreateFont("C:\\Windows\\Fonts\\arial.ttf", "Identity-H");
                    document.SetFont(font);
                    iText.Layout.Element.Image Image = new(ImageDataFactory.Create(CreateQR()));
                    document.Add(new Paragraph(info));
                    document.Add(Image);
                    document.Close();
                }
            }
        }
        private byte[] CreateQR()
        {
            QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
            QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode("Самый уникальный QR для самого уникального кинотеатра!", QRCodeGenerator.ECCLevel.M);
            Bitmap bitmap = new QRCode(qRCodeData).GetGraphic(5);
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));
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
                cost.Content = 0;
            }
            else
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Stop);
            }
            SetPrice();
        }

        private void SetPrice()
        {
            price = 300;
            DateTime today = DateTime.Today;
            int m = today.Month;
            int d = today.Day;
            if (m == 11 && d == 12)
            {
                price = 150;
                if (Tickets.Items.Count > 20)
                {
                    price = 135;

                }
                if (Tickets.Items.Count > 35)
                {
                    price = 120;
                }
                if (Tickets.Items.Count > 50)
                {
                    price = 105;
                }
            }
            else
            {
                if (Tickets.Items.Count > 20)
                {
                    price = 270;
                }
                if (Tickets.Items.Count > 35)
                {
                    price = 240;
                }
                if (Tickets.Items.Count > 50)
                {
                    price = 210;
                }
            }
        }
        private void ShowInfo()
        {
            MessageBox.Show("Обычная стоимость билета - 300. Для постоянных посетителей действуют постоянные скидки: " +
                $"\n Текущая стоимость билета - {price}" +
                "\n Куплено более 20 билетов - 10%" +
                "\n Куплено более 35 билетов - 20%" +
                "\n Куплено более 50 билетов - 30%", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void GoslingBirthDay()
        {
            MessageBox.Show("Специально предложение! В честь дня рождения Райана Гослинга действует скидка в 50%!" +
                $"\n Текущая стоимость билета - {price}", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
