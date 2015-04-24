using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemTools.WebTools.Attributes;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using SecurityDataModel.Repositories;
using WebSecurity.Data;
using WebSecurity.Infrastructure;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Grant.AccessTypes.Base;
using WebSecurity.Repositories;

namespace WebSecurity.CmdRun
{
    public class CommandRunDispatcher
    {
        private readonly ISecurityCommandRun _commandRun;

        public CommandRunDispatcher(ISecurityCommandRun commandRun)
        {
            if (commandRun == null) 
                throw new ArgumentNullException("commandRun");

            _commandRun = commandRun;
        }

        public void Run(CommandTermStack stack)
        {
            if (stack == null) 
                throw new ArgumentNullException("stack");

            string[] methodParams;
            _commandRun.Run(_commandRun.GetMethodName(stack, out methodParams), methodParams);
        }
    }

    public interface ISecurityCommandRun
    {
        string GetMethodName(CommandTermStack stack, out string[] methodParams);
        void Run(string methodName, object[] @params);
    }

    public class SecurityCommandRun : ISecurityCommandRun
    {
        public string GetMethodName(CommandTermStack stack, out string[] methodParams)
        {
            switch (stack[1].CommandTerm)
            {
                case "add":
                {
                    methodParams = stack.GetAdditionalParams().Select(ct => ct.CommandTerm).ToArray();
                    return string.Format("add{0}", stack[2]);
                }
                case "set":
                {
                    methodParams = new[] {stack[3], stack[5]}.Select(ct => ct.CommandTerm).ToArray();
                    return string.Format("set{0}", stack[2]);
                }
                case "grant":
                {
                    var paramList = stack.OfType<CommandTermAccessTypeBase>()
                }
                default:
                {
                    throw new NotImplementedException(string.Format("Метод {0} не реализован", stack[0]));
                }
            }
        }

        public void Run(string methodName, object[] @params)
        {
            var methodInfos = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            var method = methodInfos.First(mi => String.Equals(mi.Name, methodName, StringComparison.CurrentCultureIgnoreCase));
            method.Invoke(this, new ParameterCollection(method.GetParameters().Count(), @params).ToArray());
        }

        #region Run Methods

        #region Add

        private void AddUser(string userName, string password, string email, string displayName, string sid)
        {
            Security.Instance.AddUser(userName, password, email, displayName.Trim(new[] {'"'}), sid);
        }

        private void AddGroup(string groupName, string description)
        {
            Security.Instance.AddGroup(groupName, description.Trim(new[] { '"' }));
        }

        private void AddRole(string roleName, string description)
        {
            Security.Instance.AddRole(roleName, description.Trim(new[] { '"' }));
        }

        private void AddController(string controllerName)
        {
            Security.Instance.AddController(controllerName);
        }

        private void AddTable(string tableName)
        {
            Security.Instance.AddTable(tableName);
        }

        private void AddAllSecurityObjects()
        {
            var actionResultRepository = new ActionResultRepository();
            var tableObjectRepository = new TableObjectRepository();
            foreach (var alias in Tools.GetSecurityObjects())
            {
                if (alias is ActionAliasAttribute)
                    actionResultRepository.Add(new ActionResultObject(){ActionAlias = alias.Alias, Description = alias.Description});
                
                if (alias is EntityAliasAttribute)
                    tableObjectRepository.Add(new TableObject(){EntityName = alias.Alias, Description = alias.Description});
            }
        }

        #endregion

        #region Set

        private void SetRole(string roleName, string memberName)
        {
            Security.Instance.SetRole(roleName, memberName);
        }

        private void SetGroup(string groupName, string login)
        {
            Security.Instance.SetGroup(groupName, login);
        }

        #endregion

        #endregion
    }

    internal class ParameterCollection : IEnumerable<object>
    {
        private readonly int _paramCount;
        private readonly List<object> _values = new List<object>();
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ParameterCollection(int paramCount, object[] values)
        {
            _paramCount = paramCount;
            AddValues(values);
        }

        private void AddValues(object[] values)
        {
            var i = 0;
            while (i < _paramCount)
            {
                if (i >= values.Length)
                    _values.Add(null);
                else
                    _values.Add(values[i]);

                i++;
            }
        }

        /// <summary>
        /// Возвращает перечислитель, выполняющий итерацию в коллекции.
        /// </summary>
        /// <returns>
        /// Интерфейс <see cref="T:System.Collections.Generic.IEnumerator`1"/>, который может использоваться для перебора элементов коллекции.
        /// </returns>
        public IEnumerator<object> GetEnumerator()
        {
            return _values.GetEnumerator();
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
}
