using MyAgenda.Library.Model;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Day;
using MyAgenda.Library.Model.Schedule.Week;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.Library.Data.Provider
{
    /// <summary>
    /// Класс для считывания данных сущностей из базы данных MySQL.
    /// </summary>
    internal static class EntityDataReader
    {
        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы факультетов
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadFacultyData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = Faculty.Schema;
                schema = ReadDataIntoSchema(reader, schema, Faculty.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, Faculty.NameColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы курсов
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadCourseData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = Course.Schema;
                schema = ReadDataIntoSchema(reader, schema, Course.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, Course.FacultyIdColumn);
                schema = ReadDataIntoSchema(reader, schema, Course.NameColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы групп
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadGroupData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = Group.Schema;
                schema = ReadDataIntoSchema(reader, schema, Group.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, Group.CourseIdColumn);
                schema = ReadDataIntoSchema(reader, schema, Group.CodeColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы преподавателей
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadTeacherData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = Teacher.Schema;
                schema = ReadDataIntoSchema(reader, schema, Teacher.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, Teacher.NameColumn);
                schema = ReadDataIntoSchema(reader, schema, Teacher.SurnameColumn);
                schema = ReadDataIntoSchema(reader, schema, Teacher.PatronymicColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы занятий
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadSubjectData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = Subject.Schema;
                schema = ReadDataIntoSchema(reader, schema, Subject.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, Subject.TeacherIdColumn);
                schema = ReadDataIntoSchema(reader, schema, Subject.NameColumn);
                schema = ReadDataIntoSchema(reader, schema, Subject.ClassroomColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы типов недель
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadWeekTypeData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = WeekType.Schema;
                schema = ReadDataIntoSchema(reader, schema, WeekType.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, WeekType.TypeColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы учебных дней для групп
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadGroupDayScheduleData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = GroupDaySchedule.Schema;
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.FirstSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.SecondSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.ThirdSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.FourthSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.FifthSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.SixthSubjectIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupDaySchedule.SeventhSubjectIdColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в список схем таблицы учебных недель для групп
        /// и перейти к следующему множеству.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <returns>Список схем таблицы, заполненных данными.</returns>
        public static List<Schema> ReadGroupWeekScheduleData(MySqlDataReader reader)
        {
            Schema schema;
            List<Schema> schemaList = new List<Schema>();

            while (reader.Read())
            {
                schema = GroupWeekSchedule.Schema;
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.IdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.FirstDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.SecondDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.ThirdDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.FourthDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.FifthDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.SixthDayIdColumn);
                schema = ReadDataIntoSchema(reader, schema, GroupWeekSchedule.SeventhDayIdColumn);

                schemaList.Add(schema);
            }

            reader.NextResult();

            return schemaList;
        }

        /// <summary>
        /// Считать данные из указанного потока в схему таблицы.
        /// </summary>
        /// <param name="reader">Поток данных из базы данных MySQL.</param>
        /// <param name="schema">Схема таблицы.</param>
        /// <param name="columnName">Название столбца.</param>
        /// <returns>Схема таблицы со считанными в столбец данными.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static Schema ReadDataIntoSchema(MySqlDataReader reader, Schema schema, string columnName)
        {
            int ordinal = reader.GetOrdinal(columnName);

            if (!reader.IsDBNull(ordinal))
            {
                schema.SetColumnData(columnName, reader.GetValue(ordinal));

                return schema;
            }

            if (!schema.GetColumn(columnName).IsNullable)
            {
                throw new ArgumentNullException("Невозможно считать данные в схему.");
            }

            return schema;
        }
    }
}
