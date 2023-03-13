using System;
using System.Collections.Generic;
using System.Configuration;
using MyAgenda.Library.Data;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Day;
using MyAgenda.Library.Model.Schedule.Entry;
using MyAgenda.Library.Model.Schedule.Week;
using MySql.Data.MySqlClient;

namespace MyAgenda.Library
{
    /// <summary>
    /// Менеджер.
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// Запустить инициализацию базы данных.
        /// </summary>
        public static void Initialize()
        {
            Migrate();
        }

        /// <summary>
        /// Запустить миграции и создать таблицы для сущностей.
        /// </summary>
        private static void Migrate()
        {
            var link = new MySqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString);
            var query = string.Empty;
            var schemaList = new List<Schema>
            {
                Faculty.Schema,
                Course.Schema,
                Group.Schema,
                Teacher.Schema,
                Subject.Schema,
                WeekType.Schema,
                GroupDaySchedule.Schema,
                GroupWeekSchedule.Schema
            };

            link.Open();

            foreach (var schema in schemaList)
            {
                query += schema.ToCreateQuery();
            }

            var command = new MySqlCommand(query, link);

            command.ExecuteNonQuery();
            link.Close();
        }

        /// <summary>
        /// Получить список групп.
        /// </summary>
        /// <returns>Список групп.</returns>
        public static List<Group> GetGroupList()
        {
            Group group; // SELECT * FROM group;
            Course course; // SELECT * FROM course;
            Faculty faculty; // SELECT * FROM faculty;

            var groupList = new List<Group>();

            for (int i = 0; i < 10; i++)
            {
                faculty = new Faculty(i, Faculty.NameColumn);
                course = new Course(i, faculty, Course.NameColumn);
                group = new Group(i, course, Group.CodeColumn);

                groupList.Add(group);
            }

            return groupList;
        }

        /// <summary>
        /// Получить всех преподавателей.
        /// </summary>
        /// <returns>Список преподавателей.</returns>
        public static List<Teacher> GetTeacherList()
        {
            Teacher teacher; // SELECT * FROM teacher;

            var teacherList = new List<Teacher>();

            for (int i = 0; i < 10; i++)
            {
                teacher = new Teacher(i, Teacher.NameColumn, Teacher.SurnameColumn, Teacher.PatronymicColumn);

                teacherList.Add(teacher);
            }

            return teacherList;
        }

        /// <summary>
        /// Получить все типы недель.
        /// </summary>
        /// <returns>Список типов недель.</returns>
        public static List<WeekType> GetWeekTypeList()
        {
            // SELECT * FROM week_type;

            var weekTypeList = new List<WeekType>();

            weekTypeList.Add(new WeekType(0, AvailableWeekType.Red));
            weekTypeList.Add(new WeekType(1, AvailableWeekType.Blue));

            return weekTypeList;
        }

        /// <summary>
        /// Получить текущую учебную неделю для группы.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для группы или null.</returns>
        public static GroupWeekSchedule GetGroupWeekSchedule(Group group, WeekType weekType)
        {
            GroupWeekSchedule week; // SELECT * FROM week_schedule WHERE group_id = :groupId AND week_type_id = :weekTypeId ORDER BY id LIMIT 1;
            GroupDaySchedule day; // SELECT * FROM day_schedule WHERE id IN (:dayIds...);
            Subject subject; // SELECT * FROM subject WHERE id IN (:subjectIds...);
            Teacher teacher; // SELECT * FROM teacher WHERE id IN (:teacherIds...);

            var subjectList = new List<SubjectEntry>();
            var dayList = new List<DayScheduleEntry>();

            for (int i = 0; i < EntityEntry.PositionTypeCount * EntityEntry.PositionTypeCount; i++)
            {
                teacher = new Teacher(i, Teacher.NameColumn, Teacher.SurnameColumn, Teacher.PatronymicColumn);
                subject = new Subject(i, teacher, Subject.NameColumn, Subject.ClassroomColumn);

                int index = i;

                while (index >= EntityEntry.PositionTypeCount)
                {
                    index -= EntityEntry.PositionTypeCount;
                }

                subjectList.Add(new SubjectEntry(EntityEntry.GetPositionType(index), subject));
            }

            for (int i = 0; i < EntityEntry.PositionTypeCount; i++)
            {
                day = new GroupDaySchedule(i, subjectList.GetRange(i, EntityEntry.PositionTypeCount));

                dayList.Add(new DayScheduleEntry(EntityEntry.GetPositionType(i), day));
            }

            week = new GroupWeekSchedule(0, group, weekType, dayList);

            return week;
        }

        /// <summary>
        /// Получить текущую учебную неделю для преподавателя.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для преподавателя или null.</returns>
        public static TeacherWeekSchedule GetTeacherWeekSchedule(Teacher teacher, WeekType weekType)
        {
            /*
             * SELECT * FROM subject WHERE teacher_id = :teacherId ORDER BY id LIMIT 49;
             * 
             * SELECT * FROM day_schedule WHERE
             * first_subject_id IN (:subjectIds...) OR
             * second_subject_id IN (:subjectIds...) OR
             * third_subject_id IN (:subjectIds...) OR
             * fourth_subject_id IN (:subjectIds...) OR
             * fifth_subject_id IN (:subjectIds...) OR
             * sixth_subject_id IN (:subjectIds...) OR
             * seventh_subject_id IN (:subjectIds...);
             * 
             * SELECT * FROM week_schedule WHERE
             * week_type_id = :weekTypeId AND (
             * first_day_id IN (:dayIds...) OR
             * second_day_id IN (:dayIds...) OR
             * third_day_id IN (:dayIds...) OR
             * fourth_day_id IN (:dayIds...) OR
             * fifth_day_id IN (:dayIds...) OR
             * sixth_day_id IN (:dayIds...) OR
             * seventh_day_id IN (:dayIds...));
             */
            TeacherWeekSchedule week;
            DaySchedule day;
            Subject subject;

            var subjectList = new List<SubjectEntry>();
            var dayList = new List<DayScheduleEntry>();

            for (int i = 0; i < EntityEntry.PositionTypeCount * EntityEntry.PositionTypeCount; i++)
            {
                subject = new Subject(i, teacher, Subject.NameColumn, Subject.ClassroomColumn);

                int index = i;

                while (index >= EntityEntry.PositionTypeCount)
                {
                    index -= EntityEntry.PositionTypeCount;
                }

                subjectList.Add(new SubjectEntry(EntityEntry.GetPositionType(index), subject));
            }

            for (int i = 0; i < EntityEntry.PositionTypeCount; i++)
            {
                day = new DaySchedule(subjectList.GetRange(i, EntityEntry.PositionTypeCount));

                dayList.Add(new DayScheduleEntry(EntityEntry.GetPositionType(i), day));
            }

            week = new TeacherWeekSchedule(teacher, weekType, dayList);

            return week;
        }
    }
}
