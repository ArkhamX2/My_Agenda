using System.Collections.Generic;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule;
using MyAgenda.Library.Model.Schedule.Entry;
using MyAgenda.Library.Model.Schedule.Week;

namespace MyAgenda.Library
{
    /// <summary>
    /// Менеджер.
    /// </summary>
    public static class Manager
    {
        /// <summary>
        /// Получить факультет.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Факультет.</returns>
        public static Faculty GetFaculty(int id)
        {
            return new Faculty(id, Faculty.NameColumn);
        }

        /// <summary>
        /// Получить курс.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Курс.</returns>
        public static Course GetCourse(int id)
        {
            return new Course(id, GetFaculty(id), Course.NameColumn);
        }

        /// <summary>
        /// Получить группу.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Группа.</returns>
        public static Group GetGroup(int id)
        {
            return new Group(id, GetCourse(id), Group.CodeColumn);
        }

        /// <summary>
        /// Получить занятие.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Занятие.</returns>
        public static Subject GetSubject(int id)
        {
            return new Subject(id, GetTeacher(id), Subject.NameColumn, Subject.ClassroomColumn);
        }

        /// <summary>
        /// Получить преподавателя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Преподаватель.</returns>
        public static Teacher GetTeacher(int id)
        {
            return new Teacher(id, Teacher.NameColumn, Teacher.SurnameColumn, Teacher.PatronymicColumn);
        }

        /// <summary>
        /// Получить тип недели.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Тип недели.</returns>
        public static WeekType GetWeekType(int id)
        {
            return new WeekType(id, AvailableWeekType.Red);
        }

        /// <summary>
        /// Получить учебный день.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Учебный день.</returns>
        public static DaySchedule GetDaySchedule(int id)
        {
            return new DaySchedule(id, new List<SubjectEntry>
            {
                new SubjectEntry(EntryPosition.First, GetSubject(id)),
                new SubjectEntry(EntryPosition.Second, GetSubject(id)),
                new SubjectEntry(EntryPosition.Third, GetSubject(id))
            });
        }

        /// <summary>
        /// Получить учебную неделю для группы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для группы.</returns>
        public static GroupWeekSchedule GetGroupWeekSchedule(int id, Group group, WeekType weekType)
        {
            return new GroupWeekSchedule(id, group, weekType, new List<DayScheduleEntry>
            {
                new DayScheduleEntry(EntryPosition.First, GetDaySchedule(id)),
                new DayScheduleEntry(EntryPosition.Second, GetDaySchedule(id)),
                new DayScheduleEntry(EntryPosition.Third, GetDaySchedule(id))
            });
        }

        /// <summary>
        /// Получить учебную неделю для группы.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для группы.</returns>
        public static TeacherWeekSchedule GetTeacherWeekSchedule(Teacher teacher, WeekType weekType)
        {
            return new TeacherWeekSchedule(teacher, weekType, new List<DayScheduleEntry>
            {
                new DayScheduleEntry(EntryPosition.First, GetDaySchedule(0)),
                new DayScheduleEntry(EntryPosition.Second, GetDaySchedule(0)),
                new DayScheduleEntry(EntryPosition.Third, GetDaySchedule(0))
            });
        }
    }
}
