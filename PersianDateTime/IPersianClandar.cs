using System;
using System.Runtime.InteropServices;

namespace PersainDateTime
{
    [Guid("3824474E-5459-40F3-A141-AE6C96CEFF5C")]
    [ComVisible(true)]
    public interface IPersianClandar
    {
        string PAddDay(double dayNumber);
        string PAddDay(string date,double dayNumber);
        DateTime JulianDate(string date);
        string PDateTime();
        string PDateTime(DateTime date,int mode);
        string PDateTime(string date, int mode);
        string PShortDate();
        string PTomorrowLongDate();
        string PCurrentMonthName();
        string PNextMonthName();
        int PNextMountNumber();
        string PMonthName(string date);
        string PMonthName(int monthNumber);
        string YearMonthName(string YearMonthNumber);
        int PCurrentMonthNumber();
        int PCurrentMonthNumber(string date);
        string DayOfWeek(string date);
        string CurrentDayOfWeek();
        string PYear();
        string PDayEvents(string date);
        string PDayInCurrentMonth();
        int PDayInMonth(string date);
        bool IsLeapYear(string persianYear);
        bool IsLeapYearByDate(string date);
        bool IsHolyday(string date);
        int RamazandDaysNumber { get; set; } 
        int CountDaysOfYear(string persianYear);
        int CountDaysOfYearByDate(string YearMonth);
        int CountDaysOfMonth(string date);
        int CountDaysOfMonth(int monthNumber);
        int PDayInYear(string date);
        string PSessionName(string date);
        bool IsFriday(string date);
        string PShortDate(DateTime date);
        string DateTimeDiff(string date1, string date2);
        string CreatePersianDate(string YearMonth);

    }
}