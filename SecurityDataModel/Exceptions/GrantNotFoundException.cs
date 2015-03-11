namespace SecurityDataModel.Exceptions
{
    public class GrantNotFoundException : BaseException
    {
        public GrantNotFoundException(int idSecObject, int idRole, int idAccessType)
            : base("Такое разрешение не найдено. Аргументы: {0}", idSecObject, idRole, idAccessType)

        {

        }
    }
}