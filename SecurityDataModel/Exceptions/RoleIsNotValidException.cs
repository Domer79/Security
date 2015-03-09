using System;

namespace SecurityDataModel.Exceptions
{
    public class RoleIsNotValidException : Exception
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.Exception"/>.
        /// </summary>
        public RoleIsNotValidException()
        {
        }

        /// <summary>
        /// ¬ыполн€ет инициализацию нового экземпл€ра класса <see cref="T:System.Exception"/>, использу€ указанное сообщение об ошибке.
        /// </summary>
        /// <param name="message">—ообщение, описывающее ошибку.</param>
        public RoleIsNotValidException(string message) 
            : base(message)
        {
        }
    }
}