namespace SecurityDataModel.Exceptions
{
    internal class LoginEmailOrPasswordInvalidException : BaseException
    {
        public LoginEmailOrPasswordInvalidException() 
            : base("�����, email ��� ������ �������")
        {
        }
    }
}