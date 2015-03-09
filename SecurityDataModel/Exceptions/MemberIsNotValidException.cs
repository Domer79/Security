using System;

namespace SecurityDataModel.Exceptions
{
    public class MemberIsNotValidException : Exception
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.Exception"/>.
        /// </summary>
        public MemberIsNotValidException()
        {
        }

        /// <summary>
        /// ¬ыполн€ет инициализацию нового экземпл€ра класса <see cref="T:System.Exception"/>, использу€ указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">—ообщение, описывающее ошибку.</param>
        public MemberIsNotValidException(string message) 
            : base(message)
        {
        }
    }
}