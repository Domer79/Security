using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using SystemTools;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class TableObject : SecObject
    {
        [NotMapped]
        public string EntityName
        {
            get { return ObjectName; }
            set { ObjectName = value; }
        }
    }
}