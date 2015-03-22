using System.ComponentModel;

namespace SecurityDataModel.Infrastructure
{
    public enum MemberType
    {
        [Description("Пользователь")]
        User,
        [Description("Группа")]
        Group
    }
}