namespace SecurityDataModel.Exceptions
{
    public class UserGroupNotFoundException : BaseException
    {
        public UserGroupNotFoundException(string message, params object[] args) 
            : base(message, args)
        {
        }
    }
}