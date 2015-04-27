using System;
using IntellISenseSecurity.Base;

namespace IntellISenseSecurity
{
    public interface ICommandTermTrigger
    {
        Type[][] CommandTermTypes { get; }
        Action<CommandTermStack> Trigger { get; }
    }
}