using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.Infrastructure;

namespace DataRepository
{
    public static class Service
    {
        public static string GetTableNameFromSqlQuery(string sqlString)
        {
            return Tools.GetTableNameFromSqlQuery(sqlString);
        }
    }
}
