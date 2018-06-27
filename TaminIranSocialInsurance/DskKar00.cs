using System;
using System.Runtime.InteropServices;

namespace TaminIranSocialInsurance
{
    [Guid("C1986F26-BD90-4DFE-9EE6-542F1DC8D051")]
    [ClassInterface(ClassInterfaceType.None)]
    [Table("DskKar00", "DBF")]
    public class DskKar00
    {
        public string dsk_id {get;set;}
        public string dsk_name {get;set;}
        public string dsk_farm {get;set;}
        public string dsk_adrs {get;set;}
        public decimal dsk_kind {get;set;}
        public decimal dsk_yy {get;set;}
        public decimal dsk_mm {get;set;}
        public string dsk_listno {get;set;}
        public string dsk_disc {get;set;}
        public decimal dsk_num {get;set;}
        public decimal dsk_tdd {get;set;}
        public decimal dsk_trooz {get;set;}
        public decimal dsk_tmah {get;set;}
        public decimal dsk_tmaz {get;set;}
        public decimal dsk_tmash {get;set;}
        public decimal dsk_ttotl {get;set;}
        public decimal dsk_tbime {get;set;}
        public decimal dsk_tkoso {get;set;}
        public decimal dsk_bic {get;set;}
        public decimal dsk_rate {get;set;}
        public decimal dsk_prate {get;set;}
        public decimal dsk_bimh {get;set;}
        public string mon_pym {get;set;}
    }
}
