using System;

namespace TaminIranSocialInsurance
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited=false)]
    public class ComputedColumnAttribute:Attribute
    {
    }
}
