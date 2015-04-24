using System.ComponentModel.DataAnnotations.Schema;
using SystemTools.Interfaces;
using SystemTools.WebTools.Helpers;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class ActionResultObject : SecObject
    {
        [NotMapped]
        public string ActionAlias
        {
            get { return ObjectName; }
            set { ObjectName = value; }
        }
    }
}