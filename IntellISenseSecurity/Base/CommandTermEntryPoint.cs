using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IntellISenseSecurity.Base
{
    public abstract class CommandTermEntryPoint : CommandTermBase
    {
        protected sealed override string GetCommandTerm()
        {
            return null;
        }

        protected override abstract IEnumerable<CommandTermBase> GetNextCommandTerms(params object[] @params);

        public virtual ICommandTermTrigger[] Triggers
        {
            get
            {
                return GetCommandTermTriggers();
            }
        }

        private ICommandTermTrigger[] GetCommandTermTriggers()
        {
            var assembly = Assembly.GetAssembly(GetType());
            var commandTermTriggers = assembly
                .GetTypes()
                .Where(t => t.GetInterface("ICommandTermTrigger") != null && t.GetConstructors().Any(ci => ci.GetParameters().Length == 0))
                .Select(t => (ICommandTermTrigger)t.GetConstructor(new Type[]{}).Invoke(new object[]{}))
                .ToArray();

            return commandTermTriggers;
        }
    }
}