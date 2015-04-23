using SystemTools.Interfaces;

namespace SecurityDataModel.Events.EventArgs
{
    public class UserAddedEventArgs
    {
        /// <summary>
        /// �������������� ����� ��������� ������ <see cref="T:System.Object"/>.
        /// </summary>
        public UserAddedEventArgs(IUser user)
        {
            User = user;
        }

        public IUser User { get; private set; }
    }
}