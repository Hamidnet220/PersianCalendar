using System;

namespace TaminIranSocialInsurance
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false,Inherited = false)]
   public class TableAttribute:Attribute
    {
        public TableAttribute(string schema, string table)
        {
            Schema = schema;
            Table = table;
        }
        public string Schema { get; set; }
        public string Table { get; set; }

    }
}
