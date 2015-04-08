using SystemTools.Interfaces;
using SystemTools.WebTools.Helpers;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class ActionResultObject : SecObject, IActionResultObject
    {
        private string _path;

        [ObjectName]
        string IActionResultObject.Path
        {
            get { return _path; }
            set { _path = string.IsNullOrEmpty(value) ? ControllerHelper.GetActionPath(Controller, Action) : value; }
        }

        [Column1]
        public string Action { get; set; }

        [Column2]
        public string Controller { get; set; }

        [Column3]
        public string AppName { get; set; }
    }

    public interface IActionResultObject : ISecObject
    {
        string Path { get; set; }
        string Action { get; set; }
        string Controller { get; set; }
    }
}