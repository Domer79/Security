using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.WebTools.Attributes;

namespace DataRepository.Infrastructure
{
    public class EntityMetadata
    {
        private readonly Type _entityType;

        internal EntityMetadata(Type entityType)
        {
            _entityType = entityType;
        }

        public string EntityName
        {
            get { return _entityType.Name; }
        }

        public string EntityAlias
        {
            get { return EntityAliasAttribute != null ? EntityAliasAttribute.Alias : EntityName; }
        }

        public string EntityDescription
        {
            get { return EntityAliasAttribute != null ? EntityAliasAttribute.Description : null; }
        }

        public AliasAttributeBase EntityAliasAttribute
        {
            get
            {
                var aliasAttribute = (EntityAliasAttribute)Attribute.GetCustomAttribute(_entityType, typeof(EntityAliasAttribute));
                return aliasAttribute ?? new EntityAliasAttribute(EntityName);
            }
        }


    }
}
