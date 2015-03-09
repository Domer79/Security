using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class ActionResultObject : SecObject
    {
        [ObjectName]
        public string ActionName { get; set; }

        [Column1]
        public string Controller { get; set; }

        [Column2]
        public string AppName { get; set; }
    }
}