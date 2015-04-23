using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using SystemTools.WebTools.Helpers;
using DataRepository;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class ActionResultRepository : SecObjectRepository<IActionResultObject>
    {
        public static ISecObject GetActionResult(string controller, string action)
        {
            return new ActionResultRepository().GetActionResultObject(controller, action);
        }

        private ISecObject GetActionResultObject(string controller, string action)
        {
            return GetSecObject(ControllerHelper.GetActionPath(controller, action));
        }
    }
}
