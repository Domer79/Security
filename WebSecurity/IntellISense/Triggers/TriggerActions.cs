using System;
using System.Collections.Generic;
using System.Linq;
using IntellISenseSecurity;
using IntellISenseSecurity.Base;
using SecurityDataModel.Infrastructure;
using SecurityDataModel.Models;
using WebSecurity.IntellISense.Common;
using WebSecurity.IntellISense.Delete;
using WebSecurity.Repositories;
using GroupRepository = WebSecurity.Repositories.GroupRepository;
using RoleOfMemberRepository = WebSecurity.Repositories.RoleOfMemberRepository;
using RoleRepository = WebSecurity.Repositories.RoleRepository;
using UserGroupsDetailRepository = WebSecurity.Repositories.UserGroupsDetailRepository;
using UserRepository = WebSecurity.Repositories.UserRepository;
using WebSecurityTools = WebSecurity.Infrastructure.Tools;

namespace WebSecurity.IntellISense.Triggers
{
    public class TriggerActions
    {
        #region GRANT trigger actions

        /// <summary>
        /// Триггер на терм exec для <see cref="CommandTermOn"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void GrantExecTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermOn == null)
                throw new InvalidOperationException("commandTerm as CommandTermOn == null");

            commandTerm.NextCommandTerms = new ActionResultRepository().GetQueryableCollection().ToList().Select(so => new CommandTermSecObjectName(so.ObjectName));;
        }

        /// <summary>
        /// Триггер на термы select insert update delete для <see cref="CommandTermOn"/>
        /// </summary>
        /// <param name="stack">Стек команды</param>
        public static void DoTableTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermOn == null)
                throw new InvalidOperationException("commandTerm as CommandTermOn == null");

            commandTerm.NextCommandTerms = new TableObjectRepository().GetQueryableCollection().ToList().Select(so => new CommandTermSecObjectName(so.ObjectName)); ;
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

            commandTerm.NextCommandTerms = RoleRepository.GetRoleCollection().ToList().Select(r => new CommandTermRoleName(r.RoleName));
        }

        public static void GrantToRoleNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermRoleName == null)
                throw new InvalidOperationException("commandTerm as CommandTermRoleName == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> { new CommandTermOn() };
        }

        #endregion

        #region set triggers

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

            var userQuery = UserRepository.GetUserCollection();
            commandTerm.NextCommandTerms = userQuery.ToList().Select(u => new CommandTermUserName(u.Login));
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

            var groupQuery = GroupRepository.GetGroupCollection();
            commandTerm.NextCommandTerms = groupQuery.ToList().Select(gr => new CommandTermGroupName(gr.GroupName));
        }

        public static void SetGroupGroupNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermGroupName == null)
                throw new InvalidOperationException("commandTerm as CommandTermGroupName == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> {new CommandTermTo()};
        }

        #endregion

        #region SET ROLE triggers

        public static void SetRoleTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonRole == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonRole == null");

            commandTerm.NextCommandTerms = RoleRepository.GetRoleCollection().ToList().Select(gr => new CommandTermRoleName(gr.RoleName));
        }

        public static void SetRoleRoleNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermRoleName == null)
                throw new InvalidOperationException("commandTerm as CommandTermRoleName == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> { new CommandTermTo() };
        }

        public static void SetRoleToTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermTo == null)
                throw new InvalidOperationException("commandTerm as CommandTermTo == null");

            var query = UserRepository.GetUserCollection().Select(u => u.Login).Union(GroupRepository.GetGroupCollection().Select(g => g.GroupName));
            commandTerm.NextCommandTerms = query.ToList().Select(u => new CommandTermMemberName(u));
        }

        #endregion

        #region Set password triggers

        public static void SetPasswordAdditionalParamTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermAdditionalParam == null)
                throw new InvalidOperationException("commandTerm as CommandTermAdditionalParam == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> {new CommandTermFor()};
        }

        public static void SetPasswordForTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermFor == null)
                throw new InvalidOperationException("commandTerm as CommandTermFor == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> {new CommandTermCommonUser()};
        }

        public static void SetPasswordForUserTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonUser == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonUser == null");

            commandTerm.NextCommandTerms = UserRepository.GetUserCollection()
                .Select(u => u.Login)
                .ToList()
                .Where(login => !WebSecurityTools.IsWindowsUser(login))
                .Select(login => new CommandTermUserName(login));
        }

        #endregion

        #endregion

        #region delete triggers

        #region DELETE MEMBER triggers

        public static void DeleteMemberMemberName(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermMemberName == null)
                throw new InvalidOperationException("commandTerm as CommandTermMemberName == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> {new CommandTermFrom()};
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
//            var query = RoleRepository.GetRoleCollection();
            var query = new RoleOfMemberRepository().GetQueryableCollection().Cast<RoleOfMember>().Where(rm => rm.MemberName == memberName).Select(rm => rm.RoleName);
            commandTerm.NextCommandTerms = query.ToList().Select(roleName => new CommandTermRoleName(roleName));
        }

        #endregion

        #region delete user triggers

        public static void DeleteUserTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonUser == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonUser == null");

            var query = UserRepository.GetUserCollection();
            commandTerm.NextCommandTerms = query.ToList().Select(u => new CommandTermUserName(u.Login));
        }

        public static void DeleteUserUserNameTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermUserName == null)
                throw new InvalidOperationException("commandTerm as CommandTermUserName == null");

            commandTerm.NextCommandTerms = new List<CommandTermBase> { new CommandTermFrom() };
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
//            var query = GroupRepository.GetGroupCollection();
            var query = new UserGroupsDetailRepository().GetQueryableCollection().Where(ug => ug.Login == userName);
            commandTerm.NextCommandTerms = query.ToList().Select(g => new CommandTermGroupName(g.GroupName));
        }

        #endregion

        public static void DeleteGroupTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonGroup == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonGroup == null");

            var query = GroupRepository.GetGroupCollection();
            commandTerm.NextCommandTerms = query.ToList().Select(g => new CommandTermGroupName(g.GroupName));
        }

        public static void DeleteControllerTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonController == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonController == null");

            var query = new ActionResultRepository().GetQueryableCollection();
            commandTerm.NextCommandTerms = query.ToList().Select(so => new CommandTermSecObjectName(so.ObjectName));
        }

        public static void DeleteTableTrigger(CommandTermStack stack)
        {
            var commandTerm = stack.LastCommandTerm;
            if (commandTerm as CommandTermCommonTable == null)
                throw new InvalidOperationException("commandTerm as CommandTermCommonTable == null");

            var query = new TableObjectRepository().GetQueryableCollection();
            commandTerm.NextCommandTerms = query.ToList().Select(so => new CommandTermSecObjectName(so.ObjectName));
        }

        #endregion
    }
}