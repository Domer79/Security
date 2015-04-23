using SecurityDataModel.Events.EventArgs;
using SecurityDataModel.Repositories;

namespace SecurityDataModel.Events.Delegates
{
    public delegate void UserAddedEventHandler(object sender, UserAddedEventArgs args);
}