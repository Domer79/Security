using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management.Instrumentation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;
using SystemTools.WebTools.Attributes;
using SystemTools.WebTools.Infrastructure;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;
using WebSecurity.Data;
using WebSecurity.Exceptions;
using WebSecurity.Infrastructure;
using WebSecurity.IntellISense;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Delete;
using WebSecurity.IntellISense.Grant.AccessTypes;
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
                    var accessTypeParams = stack.OfType<CommandTermAccessTypeBase>().Count() != 0 ? (IEnumerable<CommandTermBase>) stack.OfType<CommandTermAccessTypeBase>() : stack.OfType<CommandTermExec>();

                    methodParams = new CommandTermBase[]
                    {
                        stack.GetCommandTerm<CommandTermRoleName>(), 
                        stack.GetCommandTerm<CommandTermSecObjectName>()
                    }
                    .Concat(accessTypeParams)
                    .Select(ct => ct.CommandTerm)
                    .ToArray();

                    return stack[1].CommandTerm;
                }
                case "delete":
                {
                    if (stack.GetCommandTerm<CommandTermFrom>() != null)
                    {
                        methodParams = new[] {stack[3], stack[5]}.Select(ct => ct.CommandTerm).ToArray();
                        return string.Format("delete{0}from", stack[2]);
                    }

                    methodParams = new []{stack[3]}.Select(ct => ct.CommandTerm).ToArray();
                    return string.Format("delete{0}", stack[2]);
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
            var existObjects = new List<string>();
            foreach (var alias in Tools.GetSecurityObjects())
            {
                var secObject = alias is ActionAliasAttribute
                    ? (SecObject)new ActionResultObject() { ActionAlias = alias.Alias, Description = alias.Description }
                    : new TableObject() { EntityName = alias.Alias, Description = alias.Description };

                try
                {
                    SecObjectRepository.Add(secObject);
                }
                catch (Exception e)
                {
                    SecObjectRepository.DeleteFromContext(secObject);

                    if (e.GetErrorMessage().IndexOf("UQ_SecObject_ObjectName", StringComparison.Ordinal) != -1)
                        existObjects.Add(alias.Alias);
                    else
                    {
                        throw;
                    }
                }
            }

            if (existObjects.Count != 0)
                throw new InfoException(string.Format("Объекты {0} уже присутствуют в базе данных", existObjects.Aggregate((current, next) => string.Format("{0}, {1}", current, next))));
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

        #region grant

        private void Grant(string roleName, string objectName, string accessType1, string accessType2,
            string accessType3, string accessType4)
        {
            var at1 = (SecurityAccessType)Enum.Parse(typeof (SecurityAccessType), accessType1, true);
            SecurityAccessType at2;
            SecurityAccessType at3;
            SecurityAccessType at4;

            if (Enum.TryParse(accessType2, true, out at2))
                at1 |= at2;

            if (Enum.TryParse(accessType3, true, out at3))
                at1 |= at3;

            if (Enum.TryParse(accessType4, true, out at4))
                at1 |= at4;

            Security.Instance.Grant(roleName, objectName, at1);
        }

        #endregion

        #region Delete

        private void DeleteMemberFrom(string memberName, string roleName)
        {
            Security.Instance.DeleteMemberFromRole(memberName, roleName);
        }

        private void DeleteUserFrom(string username, string groupName)
        {
            Security.Instance.DeleteUserFromGroup(username, groupName);
        }

        private void DeleteGroup(string groupName)
        {
            Security.Instance.DeleteGroup(groupName);
        }

        private void DeleteController(string controllerName)
        {
            Security.Instance.DeleteController(controllerName);
        }

        private void DeleteTable(string tableName)
        {
            Security.Instance.DeleteTable(tableName);
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
