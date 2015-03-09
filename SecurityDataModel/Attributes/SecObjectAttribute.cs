using System;

namespace SecurityDataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class SecObjectAttribute : Attribute
    {
        public abstract Attribute[] GetColumnAttributes();
    }
}