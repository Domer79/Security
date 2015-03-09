using System;

namespace SecurityDataModel.Exceptions
{
    internal class InvalidSecObjectPropertyType : Exception
    {
        /// <summary>
        /// »нициализирует новый экземпл€р класса <see cref="T:System.Exception"/>.
        /// </summary>
        public InvalidSecObjectPropertyType(string propertyName)
            : base(string.Format("“ип свойства {0}, должен быть string", propertyName))
        {
        }
    }
}