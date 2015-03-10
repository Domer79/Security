using System;
using SystemTools.Extensions;
using Interfaces;

namespace SecurityDataModel.Exceptions
{
    public class MemberNotFoundException : BaseException
    {
        /// <summary>
        /// ��������� ������������� ������ ���������� ������ <see cref="T:System.Exception"/>, ��������� ��������� ��������� �� ������.
        /// </summary>
        /// <param name="message">���������, ����������� ������.</param>
        public MemberNotFoundException(string message) 
            : base(message)
        {
        }

        public MemberNotFoundException(IMember member)
            : this(string.Format("�������� ������������ IdMember = {0}, Name = {1} �� ������", member.IdMember, member.Name))
        {
            
        }

        public MemberNotFoundException(params object[] args)
            : base("�������� ������������ �� ������: {0}", args.SplitReverse())
        {
        }
    }
}