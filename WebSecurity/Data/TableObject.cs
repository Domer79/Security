using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class TableObject : SecObject
    {
        [ObjectName]
        public string TableName { get; set; }

        [Column1]
        public string Schema { get; set; }

        [Column2]
        public string DbName { get; set; }
    }
}