using System.Runtime.InteropServices;

namespace TaminIranSocialInsurance
{
    [Guid("72310559-6DD2-47C7-837B-F020A0848BD9")]
    [ClassInterface(ClassInterfaceType.None)]
    [Table("DBF","DSKWOR00")]
    public class DskWor00
    {
        public string dsw_id {get;set;}//Workshop Id
        public decimal dsw_yy {get;set;}//Year of list
        public decimal dsw_mm {get;set;}//month of list
        public string dsw_listno {get;set;}//number of list
        public string dsw_id1 {get;set;}//insurance number 
        public string dsw_fname {get;set;}//first name
        public string dsw_lname {get;set;}//last name
        public string dsw_dname {get;set;}//father name
        public string dsw_idno {get;set;}//id number
        public string dsw_idplc {get;set;}//issu place
        public string dsw_idate {get;set;}//issu date
        public string dsw_bdate {get;set;}//birth date
        public string dsw_sex {get;set;}//gender
        public string dsw_nat {get;set;}//nationality
        public string dsw_ocp {get;set;}//ocupation
        public string dsw_sdate {get;set;}//start date
        public string dsw_edate {get;set;}//end date
        public decimal dsw_dd {get;set;}//number of dayes work
        public decimal dsw_rooz {get;set;}//dalily base amount
        public decimal dsw_mah {get;set;}//monthly base amount
        public decimal dsw_maz {get;set;}//othe monthly wages
        public decimal dsw_mash {get;set;}//included insureance amount
        public decimal dsw_totl {get;set;}//total salary amount
        public decimal dsw_bime {get;set;}//insurance amount
        public decimal dsw_prate {get;set;}//rate
        public string dsw_job {get;set;}//job code
        [PrimaryKey]
        public string per_natcod {get;set;}//national code
    }
}
