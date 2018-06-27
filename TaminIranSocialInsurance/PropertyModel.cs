using System.Reflection;

namespace TaminIranSocialInsurance
{
    public class PropertyModel
    {
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsComputed { get; set; }
        public bool IsNullable { get; set; }

        public PropertyInfo PropertyInfo { get; set; }
    }

}
