using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R.Services
{
    public static class Helper
    {
        public static string Miladi2Shamsi(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return "تاریخ نامعتبر";
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            string persianDate = $"{year:0000}/{month:00}/{day:00}";
            return persianDate;
        }
        public static string Miladi2ShamsiWithTime(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return "تاریخ نامعتبر";
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);

            int hour = persianCalendar.GetHour(dateTime);
            int minute = persianCalendar.GetMinute(dateTime);

            string persianDate = $"{year:0000}/{month:00}/{day:00} {hour:00}:{minute:00}";
            return persianDate;
        }
        public static int Miladi2ShamsiYear(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetYear(dateTime);
            return year;
        }
        public static int Miladi2ShamsiMonth(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetMonth(dateTime);
            return year;
        }
        public static int Miladi2ShamsiDay(DateTime dateTime)
        {
            if (dateTime == null || dateTime == DateTime.MinValue)
                return 0;
            PersianCalendar persianCalendar = new PersianCalendar();

            int year = persianCalendar.GetDayOfMonth(dateTime);
            return year;
        }
    }
}
