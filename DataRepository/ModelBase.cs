using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace TaxorgRepository.Models
{
    public abstract class ModelBase
    {
        private object _theKey;

        public override int GetHashCode()
        {
            return TheKey.GetHashCode();
        }

        public object TheKey
        {
            get { return _theKey ?? (_theKey = GetKey()); }
        }

        private object GetKey()
        {
            var pi = GetType().GetProperties().FirstOrDefault(p => Attribute.IsDefined((MemberInfo) p, typeof (KeyAttribute)));
            if (pi == null)
                throw new KeyNotFoundException();

            return pi.GetValue(this);
        }
    }
}