﻿using System.Data.Entity;
using DataRepository;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public abstract class SecurityRepositoryBase<TEntity> : RepositoryBase<TEntity> 
        where TEntity : ModelBase
    {
        protected sealed override DbContext GetContext()
        {
            return Tools.Context;
        }
    }
}
