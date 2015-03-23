using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SystemTools.Interfaces;
using SystemTools.WebTools.Infrastructure;
using DataRepository;
using SecurityDataModel.Models;

namespace SecurityDataModel.Repositories
{
    public class SecuritySettings : ISecuritySettings
    {
        private readonly Repository<Settings> _repo;
        private IdentificationMode _idenficationMode = IdentificationMode.None;

        public SecuritySettings(SecurityContext context)
        {
            _repo = new Repository<Settings>(context);
        }

        public IdentificationMode IdentificationMode
        {
            get
            {
                if (_idenficationMode != IdentificationMode.None)
                    return _idenficationMode;

                var dbRawSqlQuery = _repo.SqlQuery<string>("select sec.GetIdentificationMode()");
                _idenficationMode = (IdentificationMode) Enum.Parse(typeof (IdentificationMode), dbRawSqlQuery.First());
                return _idenficationMode;
            }
            set
            {
                _repo.ExecuteNonQuery("exec sec.SetIdentificationMode @mode = @p0", value.ToString());
                _idenficationMode = IdentificationMode.None;
            }
        }
    }
}
