using System;
using System.Runtime.InteropServices;

namespace SalaryTools
{
    [Guid("3349A91F-12A3-4A7B-AB54-F34EADCA8B81")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Calculation:ICalculation
    {
        PersainDateTime.PersianCalendar PCal = new PersainDateTime.PersianCalendar();
        
      
        public double CalAnnualBonus(string year,int WorkDays,double dailyBaseRate)
        {
            double yearDays = PCal.CountDaysOfYear(year);
            double fullBonus = dailyBaseRate * 60;
            double ratio = (WorkDays / yearDays);

            return ratio * fullBonus;
        }

        public double CalSevrencePay(string year, int WorkDays, double dailyBaseRate)
        {
            double yearDays = PCal.CountDaysOfYear(year);
            double fullSevrence = dailyBaseRate * 30;
            double ratio = (WorkDays / yearDays);

            return ratio * fullSevrence;
        }

        public double CalVacationAmount(string year, int WorkDays,int sumVacationDays, double dailyBaseRate)
        {
            double yearDays = PCal.CountDaysOfYear(year);
            double sumAllowVacationDays = (WorkDays / yearDays) * 30;
            double sumUsesVacationsDays = sumVacationDays;
            double sumRemainedVacationsDays = sumAllowVacationDays - sumUsesVacationsDays;

            return dailyBaseRate * sumRemainedVacationsDays;
        }
    }
}
