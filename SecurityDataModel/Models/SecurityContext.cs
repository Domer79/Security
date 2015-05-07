using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using SystemTools;
using SystemTools.Extensions;
using DataRepository;
using DataRepository.Infrastructure;
using SecurityDataModel.Attributes;
using SecurityDataModel.EntityConfigurations;
using SecurityDataModel.Exceptions;

namespace SecurityDataModel.Models
{
    public abstract class SecurityContext : RepositoryDataContext
    {
        protected SecurityContext()
            : base(ApplicationCustomizer.SecurityConnectionString)
        {
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<AccessType> AccessTypes { get; set; }
//        public virtual DbSet<SecObject> SecObjects { get; set; }

        public virtual DbSet<Grant> Grants { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<RoleOfMember> RoleOfMembers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserGroupsDetail> UserGroupsDetails { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        internal virtual DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new AccessTypeConfiguration());
            modelBuilder.Configurations.Add(new SecObjectConfiguration());
            modelBuilder.Configurations.Add(new GrantConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RoleOfMemberConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserGroupsDetailConfiguration());
            modelBuilder.Configurations.Add(new MemberConfiguration());
            SecObjectDbSetInit();
        }

        private void SecObjectDbSetInit()
        {
            var enumerable = ContextInfo.GetDbSetProperties(GetType()).Where(CheckSecObjectType).Select(pi => pi.PropertyType.GetGenericArguments().FirstOrDefault()).ToArray();

            var propertyDescriptors = enumerable
                .SelectMany(t => TypeDescriptor.GetProperties(t).Cast<PropertyDescriptor>())
                .ToArray();

            foreach (var propertyDescriptor in propertyDescriptors)
            {
                SetAttributes(propertyDescriptor);
            }
        }

        protected override bool ShouldValidate()
        {
            return !ApplicationCustomizer.EnableSecurity;
        }

        static void SetAttributes(PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor == null) 
                throw new ArgumentNullException("propertyDescriptor");

            var secObjectAttribute =
                propertyDescriptor.Attributes.Cast<Attribute>()
                    .Where(a => a.Is<SecObjectAttribute>())
                    .Select(a => a as SecObjectAttribute)
                    .FirstOrDefault();

            if (secObjectAttribute == null)
                return;

            if (!propertyDescriptor.PropertyType.Is<string>())
                throw new InvalidSecObjectPropertyType(propertyDescriptor.Name);

            foreach (var attribute in secObjectAttribute.GetColumnAttributes())
            {
                propertyDescriptor.Attributes.Add(attribute);
            }
        }

        private static bool CheckSecObjectType(PropertyInfo pi)
        {
            var genericArguments = pi.PropertyType.GetGenericArguments();
            var checkSecObjectType = genericArguments.FirstOrDefault().Is(typeof (SecObject));
            return checkSecObjectType;
        }
    }
}
