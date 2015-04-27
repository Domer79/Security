using System;
using System.Collections.Generic;
using System.Linq;
using SystemTools.Interfaces;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using SecurityDataModel.Models;
using WebSecurity.IntellISense.CommandTermCommon;
using WebSecurity.IntellISense.Delete;
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
        /// <param name="stack">Стек команды</param>
        public static void GrantExecTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermOnGrant == null)
                throw new InvalidOperationException("commandTerm as CommandTermOnGrant == null");

            var ct = commandTerm as CommandTermOnGrant;
            ct.SecObjectCollection = new ActionResultRepository().GetQueryableCollection();
        }

        /// <summary>
        /// Триггер на термы select insert update delete для <see cref="CommandTermOnGrant"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void DoTableTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermOnGrant == null)
                throw new InvalidOperationException("commandTerm as CommandTermOnGrant == null");

            var ct = commandTerm as CommandTermOnGrant;
            ct.SecObjectCollection = new TableObjectRepository().GetQueryableCollection();
        }

        /// <summary>
        /// Триггер на терм to для <see cref="CommandTermTo"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void GrantTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermTo == null)
                throw new InvalidOperationException("commandTerm as CommandTermTo == null");

            var ct = commandTerm as CommandTermTo;
            ct.NextCommandTermList = RoleRepository.GetRoleCollection().ToList().Select(r => new CommandTermRoleName{RoleName = r.RoleName});
        }

        public static void GrantToRoleNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermRoleName == null)
                throw new InvalidOperationException("commandTerm as CommandTermRoleName == null");

            var ct = commandTerm as CommandTermRoleName;
            ct.NextCommandTermList = new List<CommandTermBase> { new CommandTermOnGrant() };
        }

        #endregion

        #region SET GROUP trigger actions

        /// <summary>
        /// Триггер на терм To для <see cref="CommandTermTo"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void SetGroupToTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermTo == null)
                throw new InvalidOperationException("commandTerm as CommandTermTo == null");

            var ct = commandTerm as CommandTermTo;
            var userQuery = UserRepository.GetUserCollection();
            ct.NextCommandTermList = userQuery.ToList().Select(u => new CommandTermUserName{UserName = u.Login});
        }

        /// <summary>
        /// Триггер на set group для <see cref="CommandTermCommonGroup"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void SetGroupTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonGroup == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonGroup == null");

            var ct = commandTerm as CommandTermCommonGroup;
            var groupQuery = GroupRepository.GetGroupCollection();
            ct.NextCommandTermList = groupQuery.ToList().Select(gr => new CommandTermGroupName {GroupName = gr.GroupName});
        }

        public static void SetGroupGroupNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermGroupName == null)
                throw new InvalidOperationException("commandTerm as CommandTermGroupName == null");

            var ct = commandTerm as CommandTermGroupName;
            ct.NextCommandTermList = new List<CommandTermBase> {new CommandTermTo()};
        }

        #endregion

        #region SET ROLE triggers

        public static void SetRoleTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonRole == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonRole == null");

            var ct = commandTerm as CommandTermCommonRole;
            ct.NextCommandTermList = RoleRepository.GetRoleCollection().ToList().Select(gr => new CommandTermRoleName { RoleName = gr.RoleName });
        }

        public static void SetRoleRoleNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermRoleName == null)
                throw new InvalidOperationException("commandTerm as CommandTermRoleName == null");

            var ct = commandTerm as CommandTermRoleName;
            ct.NextCommandTermList = new List<CommandTermBase> { new CommandTermTo() };
        }

        public static void SetRoleToTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermTo == null)
                throw new InvalidOperationException("commandTerm as CommandTermTo == null");

            var ct = commandTerm as CommandTermTo;
            var query = UserRepository.GetUserCollection().Select(u => u.Login).Union(GroupRepository.GetGroupCollection().Select(g => g.GroupName));
            ct.NextCommandTermList = query.ToList().Select(u => new CommandTermMemberName { MemberName = u });
        }

        #endregion

        #region delete triggers

        #region DELETE MEMBER triggers

        public static void DeleteMemberMemberName(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermMemberName == null)
                throw new InvalidOperationException("commandTerm as CommandTermMemberName == null");

            var ct = commandTerm as CommandTermMemberName;
            ct.NextCommandTermList = new List<CommandTermBase> {new CommandTermFrom()};
        }

        public static void DeleteMemberFromTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            var commandTermMemberName = stack.GetCommandTerm<CommandTermMemberName>();

            if (commandTerm as CommandTermFrom == null)
                throw new InvalidOperationException("commandTerm as CommandTermFrom == null");

            if (commandTermMemberName == null)
                throw new InvalidOperationException("Отсутствует имя участника безопасности");

            var memberName = commandTermMemberName.CommandTerm;
            var ct = commandTerm as CommandTermFrom;
//            var query = RoleRepository.GetRoleCollection();
            var query = new RoleOfMemberRepository().GetQueryableCollection().Cast<RoleOfMember>().Where(rm => rm.MemberName == memberName);
            ct.NextCommandTermList = query.ToList().Select(r => new CommandTermRoleName { RoleName = r.RoleName });
        }

        #endregion

        #region delete user triggers

        public static void DeleteUserTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonUser == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonUser == null");

            var ct = commandTerm as CommandTermCommonUser;
            var query = UserRepository.GetUserCollection();
            ct.NextCommandTermList = query.ToList().Select(u => new CommandTermUserName { UserName = u.Login });
        }

        public static void DeleteUserUserNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermUserName == null)
                throw new InvalidOperationException("commandTerm as CommandTermUserName == null");

            var ct = commandTerm as CommandTermUserName;
            ct.NextCommandTermList = new List<CommandTermBase> { new CommandTermFrom() };
        }

        public static void DeleteUserFromTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            var commandTermUserName = stack.GetCommandTerm<CommandTermUserName>();

            if (commandTerm as CommandTermFrom == null)
                throw new InvalidOperationException("commandTerm as CommandTermFrom == null");

            if (commandTermUserName == null)
                throw new InvalidOperationException("Отсутствует имя пользователя");

            var userName = commandTermUserName.CommandTerm;
            var ct = commandTerm as CommandTermFrom;
//            var query = GroupRepository.GetGroupCollection();
            var query = new UserGroupsDetailRepository().GetQueryableCollection().Where(ug => ug.Login == userName);
            ct.NextCommandTermList = query.ToList().Select(g => new CommandTermGroupName { GroupName = g.GroupName });
        }

        #endregion

        public static void DeleteGroupTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonGroup == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonGroup == null");

            var ct = commandTerm as CommandTermCommonGroup;
            var query = GroupRepository.GetGroupCollection();
            ct.NextCommandTermList = query.ToList().Select(g => new CommandTermGroupName { GroupName = g.GroupName });
        }

        public static void DeleteControllerTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonController == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonController == null");

            var ct = commandTerm as CommandTermCommonController;
            var query = new ActionResultRepository().GetQueryableCollection();
            ct.NextCommandTermList = query.ToList().Select(so => new CommandTermSecObjectName(so.ObjectName));
        }

        public static void DeleteTableTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonTable == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonTable == null");

            var ct = commandTerm as CommandTermCommonTable;
            var query = new TableObjectRepository().GetQueryableCollection();
            ct.NextCommandTermList = query.ToList().Select(so => new CommandTermSecObjectName(so.ObjectName));
        }

        #endregion
    }
}