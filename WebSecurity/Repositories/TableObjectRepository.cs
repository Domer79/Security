﻿using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Models;
using SecurityDataModel.Repositories;
using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class TableObjectRepository : SecObjectRepository<TableObject>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        public TableObjectRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}