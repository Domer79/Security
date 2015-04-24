using System.ComponentModel.DataAnnotations.Schema;
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