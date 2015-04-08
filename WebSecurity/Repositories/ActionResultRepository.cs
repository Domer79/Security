using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class ActionResultRepository : SecObjectRepository<ActionResultObject>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public ActionResultRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
