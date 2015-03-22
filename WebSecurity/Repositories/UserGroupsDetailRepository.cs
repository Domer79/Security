﻿using WebSecurity.Data;

namespace WebSecurity.Repositories
{
    internal class UserGroupsDetailRepository : SecurityDataModel.Repositories.UserGroupsDetailRepository
    {
        public UserGroupsDetailRepository() 
            : base(new WebMvcSecurityContext())
        {
        }
    }
}
