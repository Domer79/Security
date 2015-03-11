using SystemTools.Extensions;

namespace SecurityDataModel.Exceptions
{
    internal class AccessTypeNotFoundException : BaseException
    {
        public AccessTypeNotFoundException(params object[] args) 
            : base("AccessType не найден, дополнительные аргументы: {0}", args)
        {
        }
    }
}