using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class TableObject : SecObject
    {
        [NotMapped]
        public string TableName
        {
            get { return ObjectName; }
            set { ObjectName = value; }
        }

        [Column1]
        public string Schema { get; set; }

        [Column2]
        public string DbName { get; set; }
    }
}