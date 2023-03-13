using MyAgenda.Library.Model.Base;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.Library.Data.Provider
{
    /// <summary>
    /// Провайдер для работы с данными групп.
    /// </summary>
    internal static class GroupProvider
    {
        /// <summary>
        /// Строка в формате SQL для получения данных всех групп.
        /// </summary>
        public const string SelectGroupList = "SELECT * FROM `" + Group.Table + "`;";

        /// <summary>
        /// Строка в формате SQL для получения данных всех групп.
        /// </summary>
        public const string SelectCourseList = "SELECT * FROM `" + Course.Table + "`;";

        /// <summary>
        /// Строка в формате SQL для получения данных всех групп.
        /// </summary>
        public const string SelectFacultyList = "SELECT * FROM `" + Faculty.Table + "`;";

        /// <summary>
        /// Получить список групп.
        /// </summary>
        /// <returns>Список групп.</returns>
        public static List<Group> GetGroupList()
        {
            var query = SelectGroupList + SelectCourseList + SelectFacultyList;
            var reader = ConnectionManager.ExecuteReader(query);
            var schemaList = new List<Schema>();

            schemaList.AddRange(EntityDataReader.ReadGroupData(reader));
            schemaList.AddRange(EntityDataReader.ReadCourseData(reader));
            schemaList.AddRange(EntityDataReader.ReadFacultyData(reader));

            reader.Close();
            ConnectionManager.CloseConnection();

            // TODO: ASAP.

            var groupList = new List<Group>();

            return groupList;
        }
    }
}
