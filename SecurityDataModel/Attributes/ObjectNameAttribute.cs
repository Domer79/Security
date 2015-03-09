using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityDataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ObjectNameAttribute : SecObjectAttribute
    {
        public override Attribute[] GetColumnAttributes()
        {
            return new Attribute[] {new ColumnAttribute("objectName"), new RequiredAttribute(), new StringLengthAttribute(200)};
        }
    }
}