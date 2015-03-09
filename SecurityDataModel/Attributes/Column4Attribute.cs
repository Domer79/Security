using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityDataModel.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class Column4Attribute : SecObjectAttribute
    {
        public override Attribute[] GetColumnAttributes()
        {
            return new Attribute[] { new ColumnAttribute("type4"), new StringLengthAttribute(100), };
        }
    }
}