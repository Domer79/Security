namespace SecurityDataModel.Exceptions
{
    internal class SecObjectNotFoundException : BaseException
    {
        public SecObjectNotFoundException(int idSecObject)
            : base("Объект безопасности не найден. Идентификатор: {0}", idSecObject)
        {

        }
    }
}