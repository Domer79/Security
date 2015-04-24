using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using SystemTools.Extensions;
using SystemTools.WebTools.Attributes;
using SystemTools.WebTools.Helpers;

namespace WebSecurity.Infrastructure
{
    public class ControllerCollection : IEnumerable<ControllerInfo>
    {
        private readonly Dictionary<MethodInfo, ControllerInfo> _controllerInfoList = new Dictionary<MethodInfo, ControllerInfo>();

        static ControllerCollection()
        {
            Assemblies = new List<Assembly>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ControllerCollection()
        {
            Init();
        }

        public static List<Assembly> Assemblies { get; private set; }

        public static ControllerCollection GetControllerCollection()
        {
            return new ControllerCollection();
        }

        private Dictionary<MethodInfo, ControllerInfo> ControllerInfoList
        {
            get { return _controllerInfoList; }
        }

        private void Init()
        {
            var methods = Assemblies.SelectMany(a => a.GetTypes()).Where(t => t.GetInterface("IController") != null).SelectMany(type => type.GetMethods().Where(mi => mi.ReturnType.Is<ActionResult>()));
            foreach (var methodInfo in methods)
            {
                Add(methodInfo);
            }
        }

        private void Add(MethodInfo methodInfo)
        {
            var controllerInfo = new ControllerInfo(methodInfo);

            if (ControllerInfoList.Values.Any(ci => string.Equals(ci.Alias, controllerInfo.Alias, StringComparison.InvariantCultureIgnoreCase)))
                throw new InvalidOperationException("Нельзя добавить два одинаковых метода");

            ControllerInfoList.Add(methodInfo, controllerInfo);
        }

        public ControllerInfo this[MethodInfo methodInfo]
        {
            get { return ControllerInfoList[methodInfo]; }
        }

        public ControllerInfo GetControllerInfo(Type controllerType, MethodInfo methodInfo)
        {
            return GetControllerInfo(controllerType, methodInfo.Name);
        }

        public ControllerInfo GetControllerInfo(Type controllerType, string methodName)
        {
            var methodInfo =  ControllerInfoList.Keys.First(k => k.DeclaringType == controllerType && k.Name == methodName);
            return this[methodInfo];
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<ControllerInfo> GetEnumerator()
        {
            return ControllerInfoList.Values.GetEnumerator();
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
        private readonly MethodInfo _methodInfo;

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ControllerInfo()
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ControllerInfo(MethodInfo methodInfo)
        {
            if (methodInfo == null) 
                throw new ArgumentNullException("methodInfo");

            if (methodInfo.DeclaringType == null)
                throw new InvalidOperationException("У метода отсутствует объявляющий его тип");

            _methodInfo = methodInfo;
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

        internal string ControllerFullName
        {
// ReSharper disable once PossibleNullReferenceException
            get { return MethodInfo.DeclaringType.Name; }
        }

        public string Action
        {
            get { return MethodInfo.Name; }
        }

        public string Alias
        {
            get
            {
                return ActionAlias != null ? ActionAlias.Alias : string.Format("{0}{1}", Controller, Action);
            }
        }

        private ActionAliasAttribute ActionAlias
        {
            get { return ((ActionAliasAttribute) Attribute.GetCustomAttribute(MethodInfo, typeof (ActionAliasAttribute))); }
        }

        public string Description
        {
            get { return ActionAlias != null ? ActionAlias.Description : null; }
        }

        public MethodInfo MethodInfo
        {
            get { return _methodInfo; }
        }

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
