using SystemTools.Interfaces;
using SecurityDataModel.Repositories;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class TableObjectRepository : SecObjectRepository<TableObject>
    {
        public static ISecObject GetTableObject(string entity)
        {
            return new TableObjectRepository().GetSecObject(entity);
        }
    }
}