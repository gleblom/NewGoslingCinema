using System;
using System.Text.RegularExpressions;

namespace NewGoslingCinema
{
    class TimeAndDateParse
    {
        //static Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");
        //static Regex r = new Regex(@"[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}");
        public static string TimeParse(string time)
        {
            DateTime newTime = Convert.ToDateTime(time);
            time = newTime.ToShortTimeString();
            return time;
        }
        public static string[] TimeDateParse(string dateTime)
        {
            Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");
            Regex r = new Regex(@"[0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2}");
            string date = regex.Match(dateTime).ToString();
            string time = r.Match(dateTime).ToString();
            time = TimeParse(time);
            string[] TimeAndDate = { date, time };
            return TimeAndDate;
        }
        public static string[] TimeDateParser(string dateTime)
        {
            Regex r = new Regex(@"[0-9]{1,2}:[0-9]{1,2}");
            Regex regex = new Regex(@"\d{2}.\d{2}.\d{4}");
            string date = regex.Match(dateTime).ToString();
            string time = r.Match(dateTime).ToString();
            time = TimeParse(time);
            string[] TimeAndDate = { date, time };
            return TimeAndDate;
        }
    }
}
