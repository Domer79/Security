using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure;
using SystemTools;
using SecurityDataModel.Attributes;
using SecurityDataModel.Models;

namespace WebSecurity.Data
{
    internal class TableObject : SecObject
    {
        private string _dbName;
        private string _schema;

        [NotMapped]
        public string TableName
        {
            get { return ObjectName; }
            set { ObjectName = value; }
        }

        [Column1]
        public string Schema
        {
            get { return _schema; }
            set { _schema = value ?? "dbo"; }
        }

        [Column2]
        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value ?? ApplicationSettings.DatabaseName; }
        }

        /// <summary>
        /// ���������� ������, ������� ������������ ������� ������.
        /// </summary>
        /// <returns>
        /// ������, �������������� ������� ������.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", DbName, Schema, TableName);
        }
    }

    public class TableObjectComparator : IEqualityComparer<object>
    {
        /// <summary>
        /// ����������, ����� �� ��� ��������� �������.
        /// </summary>
        /// <returns>
        /// true, ���� ��������� ������� �����; � ��������� ������ � false.
        /// </returns>
        /// <param name="x">������ ������������ ������ ���� <paramref name="T"/>.</param><param name="y">������ ������������ ������ ���� <paramref name="T"/>.</param>
        public new bool Equals(object x, object y)
        {
            var tableObject = x as TableObject ?? y as TableObject;
            var tableName = x as string ?? y as string;

            if (tableObject == null || tableName == null)
                return false;

            const string pattern = @"\[*(?<dbname>[\w]+)*\]*\.*\[*(?<schema>[\w]+)\]*\.*\[*(?<table>[\w]+)\]*";
        }

        /// <summary>
        /// ���������� ���-��� ���������� �������.
        /// </summary>
        /// <returns>
        /// ���-��� ���������� �������.
        /// </returns>
        /// <param name="obj">������ <see cref="T:System.Object"/>, ��� �������� ���������� ���������� ���-���.</param><exception cref="T:System.ArgumentNullException">��� ��������� <paramref name="obj"/> �������� ��������� ����� � �������� ��������� <paramref name="obj"/> � null.</exception>
        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }
}