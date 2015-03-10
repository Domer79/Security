using System;
using SystemTools.Extensions;
using Interfaces;

namespace SecurityDataModel.Exceptions
{
    public class MemberNotFoundException : BaseException
    {
        /// <summary>
        /// ¬ыполн€ет инициализацию нового экземпл€ра класса <see cref="T:System.Exception"/>, использу€ указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">—ообщение, описывающее ошибку.</param>
        public MemberNotFoundException(string message) 
            : base(message)
        {
        }

        public MemberNotFoundException(IMember member)
            : this(string.Format("”частник безопасности IdMember = {0}, Name = {1} не найден", member.IdMember, member.Name))
        {
            
        }

        public MemberNotFoundException(params object[] args)
            : base("”частник безопасности не найден: {0}", args.SplitReverse())
        {
        }
    }
}