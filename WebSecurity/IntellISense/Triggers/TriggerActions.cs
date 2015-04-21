using System;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using IntellISenseSecurity.Base;
using SecurityDataModel.Models;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Grant;
using WebSecurity.Repositories;

namespace WebSecurity.IntellISense.Triggers
{
    public class TriggerActions
    {
        #region GRANT trigger actions
        /// <summary>
        /// Триггер на терм exec для <see cref="CommandTermOnGrant"/>
        /// </summary>
        /// <param name="commandTerm">Тип входного параметра должен быть <see cref="CommandTermOnGrant"/></param>
        public static void GrantExecTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermOnGrant == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermOnGrant;
            ct.SecObjectCollection = new ActionResultRepository().GetQueryableCollection();
        }

        /// <summary>
        /// Триггер на термы select insert update delete для <see cref="CommandTermOnGrant"/>
        /// </summary>
        /// <param name="commandTerm">Тип входного параметра должен быть <see cref="CommandTermOnGrant"/></param>
        public static void DoTableTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermOnGrant == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermOnGrant;
            ct.SecObjectCollection = new TableObjectRepository().GetQueryableCollection();
        }

        /// <summary>
        /// Триггер на терм to для <see cref="CommandTermTo"/>
        /// </summary>
        /// <param name="commandTerm">Тип входного параметра должен быть <see cref="CommandTermTo"/></param>
        public static void GrantTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermTo == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermTo;
            ct.NextCommandTermList = RoleRepository.GetRoleCollection().ToList().Select(r => new CommandTermRoleName{RoleName = r.RoleName});
        }

        public static void GrantToRoleNameTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermRoleName == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermRoleName;
            ct.NextCommandTermList = new List<CommandTermBase> { new CommandTermOnGrant() };
        }

        #endregion

        #region SET GROUP trigger actions

        /// <summary>
        /// Триггер на терм To для <see cref="CommandTermTo"/>
        /// </summary>
        /// <param name="commandTerm">Тип входного параметра должен быть <see cref="CommandTermTo"/></param>
        public static void SetGroupToTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermTo == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermTo;
            var userQuery = UserRepository.GetUserCollection();
            ct.NextCommandTermList = userQuery.ToList().Select(u => new CommandTermUserName{UserName = u.Login});
        }

        /// <summary>
        /// Триггер на set group для <see cref="CommandTermCommonGroup"/>
        /// </summary>
        /// <param name="commandTerm">Тип входного параметра должен быть <see cref="CommandTermCommonGroup"/></param>
        public static void SetGroupTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermCommonGroup == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermCommonGroup;
            var groupQuery = GroupRepository.GetGroupCollection();
            ct.NextCommandTermList = groupQuery.ToList().Select(gr => new CommandTermGroupName {GroupName = gr.GroupName});
        }

        public static void SetGroupGroupNameTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermGroupName == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermGroupName;
            ct.NextCommandTermList = new List<CommandTermBase> {new CommandTermTo()};
        }

        #endregion

        #region SET ROLE triggers

        public static void SetRoleTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermCommonRole == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermCommonRole;
            ct.NextCommandTermList = RoleRepository.GetRoleCollection().ToList().Select(gr => new CommandTermRoleName { RoleName = gr.RoleName });
        }

        public static void SetRoleRoleNameTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermRoleName == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermRoleName;
            ct.NextCommandTermList = new List<CommandTermBase> { new CommandTermTo() };
        }

        public static void SetRoleToTrigger(CommandTermBase commandTerm)
        {
            if (commandTerm as CommandTermTo == null)
                throw new ArgumentNullException("commandTerm");

            var ct = commandTerm as CommandTermTo;
            var query = UserRepository.GetUserCollection().Select(u => u.Login).Union(GroupRepository.GetGroupCollection().Select(g => g.GroupName));
            ct.NextCommandTermList = query.ToList().Select(u => new CommandTermMemberName { MemberName = u });
        }

        #endregion
    }
}