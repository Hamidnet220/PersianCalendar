using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;

[assembly: CLSCompliant(true)]

namespace PersainDateTime
{
    [Guid("F9076CE4-4905-4C65-A108-E8489CE538FF")]
    [ClassInterface(ClassInterfaceType.None)]
    public class PersianCalendar:IPersianClandar
    {
        private Dictionary<int,string> PersianHolydayNumber = new Dictionary<int, string>();
        private Dictionary<int,string> ArabicHolydayNumber=new Dictionary<int, string>();
        HijriCalendar HejriGhamariCalendar = new HijriCalendar();

        private Dictionary<int,string> DayEventsTitles=new Dictionary<int, string>();

        public int RamazandDaysNumber { get; set; } = 29;

        public PersianCalendar()
        {
            Cultures.InitializePersianCulture();
            
        }

        [ComVisible(true)]
        public string PAddDay(double dayNumber)
        {
            var date = DateTime.Today.AddDays(dayNumber);
            return PDateTime(date,5);
        }

        public string PAddDay(string date, double dayNumber)
        {

            var date1 = (DateTime.Parse(date).AddDays(dayNumber));
            return PDateTime(date1, 5);
        }

        [ComVisible(true)]
        public DateTime JulianDate(string date)
        {
            return DateTime.Parse(date);
        }

        [ComVisible(true)]
        public string PDateTime()
        {
            return DateTime.Today.ToString("D");
        }

        [ComVisible(true)]
        public string PDateTime(string date, int mode)
        {
            var dt = DateTime.Parse(date);
            return PDateTime(dt, mode);
        }

        [ComVisible(true)]
        public string PDateTime(DateTime date, int mode)
        {
            switch (mode)
            {
                case 1:
                    return date.ToString("hh:mm:ss-yyyy/MM/dd");
                case 2:
                    return date.ToString("D");
                case 3:
                    return date.ToString("F");
                case 4:
                    return date.ToString("f");
                case 5:
                    return date.ToString("g");
                case 6:
                    return date.ToString("yyyy MMMM dd");
                case 7:
                    return date.ToString("yyyy-M-d dddd");
                case 8:
                    return date.ToString("Y");
                case 9:
                    return date.ToString("yy") + date.ToString("MM");
                case 10:
                    return date.ToString("MM");
                case 11:
                    return date.ToString("yyyy");
                case 12:
                    return date.ToString("yy/MM/dd");
                case 13:
                    return date.ToString("yyyy-MM-dd-hh-mm-ss");
                
                default:
                    return date.ToString("yyyy MMM dd hh:mm:ss");
            }
            
        }

        [ComVisible(true)]
        public string PShortDate()
        {
            return DateTime.Today.ToString("d");
        }

        [ComVisible(true)]
        public string PTomorrowLongDate()
        {
            return DateTime.Today.AddDays(1).ToString("d");
        }

        [ComVisible(true)]
        public string PCurrentMonthName()
        {
            return DateTime.Today.ToString("MMM");
        }

        [ComVisible(true)]
        public string PNextMonthName()
        {
            return PMonthName(PNextMountNumber());
        }

        [ComVisible(true)]
        public int PNextMountNumber()
        {
            return PCurrentMonthNumber() + 1;
        }

        [ComVisible(true)]
        public string PMonthName(string date)
        {
            var sDate = DateTime.Parse(date);
            return sDate.ToString("MMM");
        }

        [ComVisible(true)]
        public string PMonthName(int monthNumber)
        {
            switch (monthNumber)
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return "ورودی نامعبر";
            }
            
        }

        [ComVisible(true)]
        public int PCurrentMonthNumber(string date)
        {
            return int.Parse(DateTime.Parse(date).ToString("MM"));
        }

        [ComVisible(true)]
        public string DayOfWeek(string date)
        {
            var sDate = DateTime.Parse(date);
            return sDate.ToString("dddd");
        }

        [ComVisible(true)]
        public string CurrentDayOfWeek()
        {
            return DateTime.Today.ToString("dddd");
        }

        [ComVisible(true)]
        public string PYear()
        {
            return DateTime.Today.ToString("yyyy");
        }

        [ComVisible(true)]
        public string PDayEvents(string date)
        {
            var dayNumber = PDayInYear(date);
            string dayEvents="";
            if (IsHolyday(date))
            {
                if (PersianHolydayNumber.ContainsKey(dayNumber))
                    dayEvents += PersianHolydayNumber[dayNumber];
                if (ArabicHolydayNumber.ContainsKey(JalaliToHejriDayInyer(date)))
                    dayEvents += ArabicHolydayNumber[JalaliToHejriDayInyer(date)];
            }
            if (DayEventsTitles.ContainsKey(dayNumber))
            {
                dayEvents+=DayEventsTitles[dayNumber];
            }
            return dayEvents;

        }

        [ComVisible(true)]
        public string PDayInCurrentMonth()
        {
            return DateTime.Today.ToString("dd"); 
        }

        [ComVisible(true)]
        public int PDayInMonth(string date)
        {
            return int.Parse(DateTime.Parse(date).ToString("dd"));
        }

        [ComVisible(true)]
        public int PCurrentMonthNumber()
        {
            return int.Parse(DateTime.Today.ToString("MM"));
        }

        [ComVisible(true)]
        public bool IsLeapYear(string persianYear)
        {
            var date =DateTime.Parse(persianYear + "/01/01");
            return DateTime.IsLeapYear(date.Year);
        }

        [ComVisible(true)]
        public bool IsLeapYearByDate(string date)
        {
            if (date.Length == 4)
                date = CreatePersianDate(date);
            var convertedDate = DateTime.Parse(date);
            return DateTime.IsLeapYear(convertedDate.Year);
        }

        public bool IsHolyday(string date)
        {
            HolydaysInPersianCalendar();
            DayEventsInPersianCalendar();

            if (IsHolydayInPersianCalendar(date) || IsHolydayInArabicCalendar(date))
                return true;
            return false;

        }

        private bool IsHolydayInPersianCalendar(string date)
        {
            var dayNumber = PDayInYear(date);
            if (PersianHolydayNumber.ContainsKey(dayNumber))
            {
                return true;
            }

            
            return false;

        }

        private bool IsHolydayInArabicCalendar(string date)
        {
            var dayNumber = PDayInYear(date);
            if (ArabicHolydayNumber.ContainsKey(dayNumber))
            {
                return true;
            }


            return false;

        }

        private int JalaliToHejriDayInyer(string date)
        {
            DateTime herjriGhamriDate = JulianDate(date);
            var hejriMonthNumber = HejriGhamariCalendar.GetMonth(herjriGhamriDate);
            var hejriDayInyer = HejriGhamariCalendar.GetDayOfYear(herjriGhamriDate);
            if (hejriMonthNumber > 9)
            {
                if (RamazandDaysNumber == 30)
                    hejriDayInyer--;
            }
            return hejriDayInyer;
        }

        [ComVisible(true)]
        public int CountDaysOfYear(string persianYear)
        {
            if (IsLeapYear(persianYear))
                return 366;
            return 365;
        }

        [ComVisible(true)]
        public int CountDaysOfYearByDate(string YearMonth)
        {

            if (IsLeapYearByDate(YearMonth))
                return 366;
            return 365;
        }

        [ComVisible(true)]
        public int CountDaysOfMonth(string date)
        {
            if (date.Length == 4)
                date=CreatePersianDate(date);
            var sDate = DateTime.Parse(date);
            var monthNumber = int.Parse(sDate.ToString("MM"));
            if (monthNumber>=1 && monthNumber<=6)
                return 31;
             if (monthNumber > 6 && monthNumber <= 12 && IsLeapYear(sDate.ToString("yyyy")))
                return 30;
             if (monthNumber > 6 && monthNumber <= 12 && IsLeapYear(sDate.ToString("yyyy")) == false)
            {
                if (monthNumber > 6 && monthNumber <= 11)
                    return 30;
                if (monthNumber == 12)
                    return 29;
            }
            return default(int);
        }

        [ComVisible(true)]
        public int CountDaysOfMonth(int monthNumberCurrentYear)
        {
            
            if (monthNumberCurrentYear >= 1 && monthNumberCurrentYear <= 6)
                return 31;
            if (monthNumberCurrentYear > 6 && monthNumberCurrentYear <= 12 && IsLeapYear(DateTime.Today.ToString("yyyy")))
                return 30;
            if (monthNumberCurrentYear > 6 && monthNumberCurrentYear <= 12 && IsLeapYear(DateTime.Today.ToString("yyyy")) == false)
            {
                if (monthNumberCurrentYear > 6 && monthNumberCurrentYear <= 11)
                    return 30;
                if (monthNumberCurrentYear == 12)
                    return 29;
            }
            return default(int);
        }

        [ComVisible(true)]
        public int PDayInYear(string date)
        {

            var monthCounter = PCurrentMonthNumber(date);
            var sumDays = 0;
            for (int i = 1; i < monthCounter; i++)
            {
                sumDays += CountDaysOfMonth(i);
            }
            sumDays += PDayInMonth(date);
           return sumDays;
        }

        [ComVisible(true)]
        public string PSessionName(string date)
        {
            var sDate = DateTime.Parse(date);
            var monthNumber =int.Parse(sDate.ToString("MM"));
            if (monthNumber <= 3)
                return "بهار";
            if (monthNumber > 3 && monthNumber <= 6)
                return "تابستان";
            if (monthNumber > 6 && monthNumber <= 9)
                return "پاییز";
            if (monthNumber > 9)
                return "زمستان";
            else
            {
                return "بدون نام";
            }
        }

        [ComVisible(true)]
        public bool IsFriday(string date)
        {
          var dayName=DateTime.Parse(date).ToString("dddd");
            if (dayName == "جمعه")
                return true;
            return false;
        }

        [ComVisible(true)]
        public string PShortDate(DateTime date)
        {
            return date.ToString("d");
        }

        [ComVisible(true)]
        public string DateTimeDiff(string date1, string date2)
        {
            var d1 = DateTime.Parse(date1);
            var d2 = DateTime.Parse(date2);
            var resul = d2 -d1;
            return resul.ToString();
        }

        private void HolydaysInPersianCalendar()
        {
            PersianHolydayNumber.Clear();
            PersianHolydayNumber.Add(1, "عید نوروز-تعطیل");
            PersianHolydayNumber.Add(2, "عید نوروز-تعطیل");
            PersianHolydayNumber.Add(3, "عید نوروز-تعطیل");
            PersianHolydayNumber.Add(4, "عید نوروز-تعطیل");
            PersianHolydayNumber.Add(12, "روز جمهوری اسلامی-تعطیل");
            PersianHolydayNumber.Add(13, "روز طبیعت -تعطیل");
            PersianHolydayNumber.Add(76, "رحلت امام خمینی-تعطیل");
            PersianHolydayNumber.Add(77, "قیام 15 خرداد-تعطیل");
            PersianHolydayNumber.Add(298, "روز انقلاب-تعطیل");
            PersianHolydayNumber.Add(365, "روز ملی شدن صنعت نفت-تعطیل");
            
        }

        public void AddArabicHolyday(int dayInYear,string title)
        {
            ArabicHolydayNumber.Add(dayInYear, title);
        }

        private void DayEventsInPersianCalendar()
        {
            DayEventsTitles.Clear();
            DayEventsTitles.Add(42, "روز جهانی کار و کارگر");
            DayEventsTitles.Add(107, "روز قلم");
            DayEventsTitles.Add(111, "روز ادبیات کودک و نوجوانان");
            DayEventsTitles.Add(114, "روز فرهنگ پهلوانی و ورزش زورخانه ای");
            DayEventsTitles.Add(223, "زادروز کورش کبیر پادشاه ایران زمین");


        }

        public string YearMonthName(string YearMonth)
        {
           
            return PDateTime(CreatePersianDate(YearMonth), 8);
        }

        [ComVisible(true)]
        public string CreatePersianDate(string YearMonth)
        {
            return YearMonth.Substring(0, 2) + "/" + YearMonth.Substring(2) + "/01";
        }
    }
}
