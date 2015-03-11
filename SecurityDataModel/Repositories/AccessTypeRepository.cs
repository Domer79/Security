using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Extensions;
using DataRepository;
using Interfaces;
using SecurityDataModel.Exceptions;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class AccessTypeRepository : IAccessTypeRepository
    {
        private readonly Repository<AccessType> _repo;

        public AccessTypeRepository(SecurityContext context)
        {
            _repo = new Repository<AccessType>(context);
        }

        public void SetNewAccessType<T>()
        {
            SetNewAccessTypes(typeof(T));
        }

        public void SetNewAccessTypes(Type type)
        {
            if (!type.Is<Enum>())
                throw new AccessTypeValidException("Тип должен быть типом перечисления. Аргументы: {0}", type);

            SetNewAccessTypes(Enum.GetNames(type));
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

        public IQueryable<IAccessType> GetQueryableCollection()
        {
            return _repo;
        }
    }
}
