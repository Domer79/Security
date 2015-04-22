using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;

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
                    methodParams = new[] {stack[3],}.Union(stack.GetAdditionalParams()).Select(ct => ct.CommandTerm).ToArray();
                    return string.Format("add{0}", stack[2]);
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

            var method = methodInfos.First(mi => String.Equals(mi.Name, methodName, StringComparison.CurrentCultureIgnoreCase) && mi.GetParameters().Select(p => p.ParameterType).SequenceEqual(@params.Select(p => p.GetType())));
            method.Invoke(this, @params ?? new object[] {});
        }

        private void AddUser(string userName)
        {
            AddUser(userName, null);
        }

        private void AddUser(string userName, string password)
        {
            Security.Instance.AddUser(userName, password);
        }
    }
}
