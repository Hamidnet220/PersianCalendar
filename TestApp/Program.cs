using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TaminIranSocialInsurance;

namespace TestApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
           var pc = new PersainDateTime.PersianCalendar();
            pc.AddArabicHolyday(78,"شهادت حضرت علی");

            Console.WriteLine(pc.IsHolyday("1397/03/16"));
            //var tmRepo = new DswRepository(@"Provider = VFPOLEDB.1; Data Source = D:\Dropbox\Work\Arvanda-Co\14-84-247\1395\9604");
            //var entity = tmRepo.GetAll();
            ////var tm = new TaminTools();
            ////tm.FoxproToNetClassGenerator("d:\\test","Provider = VFPOLEDB.1; Data Source = D:\\test\\DSKWOR00.dbf; Password =; Collating Sequence = MACHINE","DskWor00");
            ////"Provider = VFPOLEDB.1; Data Source = D:\\test\\DSKWOR00.dbf; Password =; Collating Sequence = MACHINE"


            //IDswRepository dskworkRepo = new DswRepository("Provider = VFPOLEDB.1; Data Source = D:\\test;");

            //var dskw = dskworkRepo.Find("1911201727");

            ////dskworkRepo.Remove(dskw);
            //dskworkRepo.Add(new DskWor00
            //{
            //    dsw_id = "3880011639",
            //    dsw_yy = 96M,
            //    dsw_mm = 3M,
            //    dsw_listno = "01",
            //    dsw_id1 = "55555555",
            //    dsw_idno = "1459",
            //    dsw_fname = "حمیدرضا",
            //    dsw_lname = "کیانی بخش",
            //    dsw_dname = "کرمعلی",
            //    dsw_idplc = "رامهرمز",
            //    dsw_idate = "",
            //    dsw_bdate = "19/05/62",
            //    dsw_sex = "مرد",
            //    dsw_nat = "ایرانی",
            //    dsw_ocp = "نگهبان",
            //    dsw_sdate = "01/01/96",
            //    dsw_edate = "26/05/96",
            //    dsw_dd = 31,
            //    dsw_rooz = 310000M,
            //    dsw_mah = 8000000M,
            //    dsw_maz = 8000000M,
            //    dsw_mash = 8000000M,
            //    dsw_totl = 20000000M,
            //    dsw_bime = 4000000M,
            //    dsw_prate = 0M,
            //    dsw_job = "028047",
            //    per_natcod = "1911201727"

            //});

            Application.EnableVisualStyles();
           Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
