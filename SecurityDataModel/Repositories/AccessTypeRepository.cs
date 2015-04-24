using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;
using SystemTools.Interfaces;
using DataRepository;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class AccessTypeRepository : IAccessTypeRepository
    {
        private readonly SecurityRepository<AccessType> _repo;

        public AccessTypeRepository()
        {
            _repo = new SecurityRepository<AccessType>();
        }

        public void SetNewAccessTypes<T>()
        {
            SetNewAccessTypes(new []{typeof(T)});
        }

        public void SetNewAccessTypes<T1, T2>()
        {
            SetNewAccessTypes(new[]
            {
                typeof (T1), 
                typeof (T2)
            });
        }

        public void SetNewAccessTypes<T1, T2, T3>()
        {
            SetNewAccessTypes(new[]
            {
                typeof(T1), 
                typeof(T2), 
                typeof(T3)
            });
        }

        public void SetNewAccessTypes<T1, T2, T3, T4>()
        {
            SetNewAccessTypes(new[]
            {
                typeof (T1), 
                typeof (T2), 
                typeof (T3), 
                typeof (T4)
            });
        }

        public void SetNewAccessTypes(Type[] types)
        {
            if (!types.All(t => t.Is<Enum>()))
                throw new AccessTypeValidException("Тип должен быть типом перечисления. Аргументы: {0}", types);

            SetNewAccessTypes(types.SelectMany(Enum.GetNames).ToArray());
        }

        public void SetNewAccessTypes(string[] accessNames)
        {
            foreach (var accessName in accessNames)
            {
                AddAccessType(accessName);
            }

            var exceptAccessNames = _repo.Select(at => at.AccessName).Except(accessNames).ToList();
            foreach (var accessName in exceptAccessNames)
            {
                TryDeleteAccessName(accessName);
            }
        }

        private void TryDeleteAccessName(string accessName)
        {
            try
            {
                var accessType = _repo.FirstOrDefault(at => at.AccessName == accessName);
                if (accessType == null)
                    throw new AccessTypeNotFoundException(accessName);

                _repo.Delete(accessType);
                _repo.SaveChanges();
            }
            catch (Exception e)
            {
                throw new AccessTypeDeleteException(e.Message, accessName);
            }
        }

        private void AddAccessType(string accessName)
        {
            if (string.IsNullOrEmpty(accessName))
                throw new AccessTypeValidException(accessName);

            if (_repo.Any(at => at.AccessName == accessName))
                return;

            _repo.InsertOrUpdate(new AccessType{AccessName = accessName});
            _repo.SaveChanges();
        }

        public IAccessType GetSingleAccessType(Enum enumValue)
        {
            return GetAccessTypes(enumValue)[0];
        }

        public IAccessType[] GetAccessTypes(Enum enumValue)
        {
            var accessTypes = enumValue.GetFlags().Select(f => Enum.GetName(enumValue.GetType(), f)).Select(f => (IAccessType)_repo.FirstOrDefault(at => at.AccessName == f)).ToArray();

            if (accessTypes.Any(at => at == null))
                throw new AccessTypeNotFoundException(accessTypes.First(at => at == null));

            return accessTypes;
        }

        public IAccessType GetAccessType(string accessType)
        {
            if (accessType == null) 
                throw new ArgumentNullException("accessType");

            return _repo.First(at => at.AccessName == accessType);
        }

        public IQueryable<IAccessType> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
