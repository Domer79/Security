using SystemTools.Interfaces;

namespace WebSecurity
{
    internal class AnonymousUser : IUser
    {
        private static AnonymousUser _anonymousUser;

        private AnonymousUser()
        {
        }

        public int IdMember
        {
            get { return 0; }
            set { }
        }

        public string Name
        {
            get { return Security.AnonymousUser; }
            set { }
        }

        public int IdUser
        {
            get { return IdMember; }
            set { IdMember = value; }
        }

        public string Login
        {
            get { return Name; }
            set { Name = value; }
        }

        public string DisplayName
        {
            get { return "Не авторизованный пользователь"; }
            set { }
        }
        public string Email { get; set; }
        public string Usersid { get; set; }

        #region static

        public static AnonymousUser Instance
        {
            get { return _anonymousUser ?? (_anonymousUser = new AnonymousUser()); }
        }

        #endregion
    }
}