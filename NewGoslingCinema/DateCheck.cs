using System;
using System.Collections.Generic;

namespace NewGoslingCinema
{
    class DateCheck
    {
        static List<DateTime> DateTimes = new List<DateTime>();
        public static async void DeletePastSessions()
        {
            DateTimes = await SqlClass.Sessione();
            foreach(var dt in DateTimes)
            {
                if(DateTime.Now > dt)
                {
                  string[] vs = SessionParser.TimeDateParser(dt.ToString());
                    SqlClass.DeleteDate(vs[0], vs[1]);
                }
            }
        }
    }
}
