using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SystemTools.Interfaces;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Grant
{
    internal class CommandTermExec : CommandTermBase
    {
        private readonly CommandTermTo _commandTermTo = new CommandTermTo();

        protected override string GetCommandTerm()
        {
            return "exec";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return _commandTermTo;
        }
    }

    internal class CommandTermTo : CommandTermBase
    {
        protected override string GetCommandTerm()
        {
            return "to";
        }

        protected override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            return new RoleNameCommandTermList<CommandTermRoleNameForGrant>();
        }
    }

    internal class RoleNameCommandTermList<TCommandTermRoleName> : IEnumerable<CommandTermBase> where TCommandTermRoleName : CommandTermRoleNameBase, new()
    {
        private readonly IQueryable<IRole> _query = new RoleRepository().GetQueryableCollection();

        /// <summary>
        /// ���������� �������������, ����������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ��������� <see cref="T:System.Collections.Generic.IEnumerator`1"/>, ������� ����� �������������� ��� �������� ��������� ���������.
        /// </returns>
        public IEnumerator<CommandTermBase> GetEnumerator()
        {
            return _query.Select(r => new TCommandTermRoleName{RoleName = r.RoleName}).GetEnumerator();
        }

        /// <summary>
        /// ���������� �������������, �������������� �������� � ���������.
        /// </summary>
        /// <returns>
        /// ������ <see cref="T:System.Collections.IEnumerator"/>, ������� ����� �������������� ��� �������� ���������.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal abstract class CommandTermRoleNameBase : CommandTermBase
    {
        public string RoleName { get; set; }

        protected sealed override string GetCommandTerm()
        {
            return RoleName;
        }

        protected override abstract IEnumerable<CommandTermBase> GetNextCommandTerms();
    }

    internal class CommandTermRoleNameForGrant : CommandTermRoleNameBase
    {
        protected override IEnumerable<CommandTermBase> GetNextCommandTerms()
        {
            yield return new CommandTermOnGrant
        }
    }
}