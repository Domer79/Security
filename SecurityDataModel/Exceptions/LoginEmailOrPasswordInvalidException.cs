namespace SecurityDataModel.Exceptions
{
    internal class LoginEmailOrPasswordInvalidException : BaseException
    {
        public LoginEmailOrPasswordInvalidException() 
            : base("Логин, email или пароль неверны")
        {
        }
    }
}