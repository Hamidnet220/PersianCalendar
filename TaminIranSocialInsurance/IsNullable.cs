using System;

namespace TaminIranSocialInsurance
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = false)]
    public class IsNullable:Attribute
    {
    }
}
