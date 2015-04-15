using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using SystemTools.Extensions;
using SystemTools.WebTools.Helpers;

namespace WebSecurity.Infrastructure
{
    public class ControllerCollection : IEnumerable<ControllerInfo>
    {
        private List<ControllerInfo> _controllerInfoList;

        static ControllerCollection()
        {
            Assemblies = new List<Assembly>();
        }

        public static List<Assembly> Assemblies { get; private set; }

        public static ControllerCollection GetControllerCollection()
        {
            return new ControllerCollection();
        }

        private List<ControllerInfo> ControllerInfoList
        {
            get
            {
                if (_controllerInfoList == null)
                    Init();

                return _controllerInfoList;
            }
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<ControllerInfo> GetEnumerator()
        {
            return ControllerInfoList.GetEnumerator();
        }

        private void Init()
        {
            _controllerInfoList = new List<ControllerInfo>();
            foreach (var type in Assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetInterface("IController") != null))
            {
                foreach (var methodInfo in type.GetMethods().Where(mi => mi.ReturnType.Is<ActionResult>()))
                {
                    Add(type, methodInfo);
                }
            }
        }

        public void Add(Type type, MethodInfo methodInfo)
        {
            if (type == null) 
                throw new ArgumentNullException("type");

            if (methodInfo == null) 
                throw new ArgumentNullException("methodInfo");

            Add(type.Name, methodInfo.Name);
        }

        public void Add(string controllerName, string actionName)
        {
            if (controllerName == null) 
                throw new ArgumentNullException("controllerName");

            if (actionName == null) 
                throw new ArgumentNullException("actionName");

            if (Contains(controllerName, actionName))
                return;

            ControllerInfoList.Add(new ControllerInfo(controllerName, actionName));
        }

        private bool Contains(string controller, string action)
        {
            return ControllerInfoList.Any(ci => ci.ControllerFullName == controller && ci.Action == action);
        }

        /// <summary>
        /// Возвращает перечислитель, осуществляющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Collections.IEnumerator"/>, который может использоваться для перебора коллекции.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class ControllerInfo
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ControllerInfo()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ControllerInfo(string controllerFullName, string action)
        {
            ControllerFullName = controllerFullName;
            Action = action;
        }

        public string Controller
        {
            get
            {
                const string pattern = @"(?i)(?<name>\w+)(controller)";

                if (!ControllerFullName.RxIsMatch(pattern))
                    throw new InvalidOperationException("Имя контроллера не соответствует соглашению");

                var rx = new Regex(pattern);
                return rx.Match(ControllerFullName).Groups["name"].Value;

            }
        }

        internal string ControllerFullName { get; set; }

        public string Action { get; set; }

        /// <summary>
        /// Возвращает строку, которая представляет текущий объект.
        /// </summary>
        /// <returns>
        /// Строка, представляющая текущий объект.
        /// </returns>
        public override string ToString()
        {
            return ControllerHelper.GetActionPath(Controller, Action);
        }
    }
}
