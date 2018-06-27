using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SalaryTools
{
    [Guid("0CED15CB-D63E-4C34-9867-E0E89E9A30BA")]
    [ComVisible(true)]
    public interface ICalculation
    {
        double CalAnnualBonus(string year, int WorkDays, double dailyBaseRate);
        double CalSevrencePay(string year, int WorkDays, double dailyBaseRate);
        double CalVacationAmount(string year, int WorkDays, int sumVacationDays, double dailyBaseRate);

    }
}
