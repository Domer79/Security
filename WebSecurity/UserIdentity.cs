using System;
using System.Security.Principal;
using SystemTools.Interfaces;
using SecurityDataModel.Models;

namespace WebSecurity
{
    public class UserIdentity : IIdentity
    {
        private readonly IUser _user;

        public UserIdentity(IUser user)
        {
            _user = user;
        }

        /// <summary>
        /// �������� ��� �������� ������������.
        /// </summary>
        /// <returns>
        /// ��� ������������, �� ���� �������� ����������� ��� ���������.
        /// </returns>
        public string Name
        {
            get { return User.Login; }
        }

        /// <summary>
        /// �������� ��� ������������ �������� �����������.
        /// </summary>
        /// <returns>
        /// ��� �������� �����������, ����������� ��� ������������� ������������.
        /// </returns>
        public string AuthenticationType
        {
            get { return typeof(User).ToString(); }
        }

        /// <summary>
        /// �������� ��������, ������������, ������ �� ������������ �������� �����������.
        /// </summary>
        /// <returns>
        /// true, ���� ������������ ������ �������� �����������; � ��������� ������ � false.
        /// </returns>
        public bool IsAuthenticated
        {
            get { return User != AnonymousUser.Instance; }
        }

        public IUser User
        {
            get { return _user ?? AnonymousUser.Instance; }
        }

        /// <summary>
        /// ���������� ������, ������� ������������ ������� ������.
        /// </summary>
        /// <returns>
        /// ������, �������������� ������� ������.
        /// </returns>
        public override string ToString()
        {
            return Name;
        }
    }
}