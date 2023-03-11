using System.Collections.Generic;
using MyAgenda.Library.Model.Base;
using MyAgenda.Library.Model.Schedule.Day;
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
        /// <returns>Факультет или null.</returns>
        public static Faculty GetFaculty(int id)
        {
            return new Faculty(id, Faculty.NameColumn);
        }

        /// <summary>
        /// Получить все факультеты.
        /// </summary>
        /// <returns>Список факультетов.</returns>
        public static List<Faculty> GetFacultyList()
        {
            return new List<Faculty>
            {
                GetFaculty(0),
                GetFaculty(1),
                GetFaculty(2)
            };
        }

        /// <summary>
        /// Сохранить факультет.
        /// </summary>
        /// <param name="faculty">Факультет.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetFaculty(Faculty faculty)
        {
            return true;
        }

        /// <summary>
        /// Получить курс.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Курс или null.</returns>
        public static Course GetCourse(int id)
        {
            return new Course(id, GetFaculty(id), Course.NameColumn);
        }

        /// <summary>
        /// Получить все курсы.
        /// </summary>
        /// <returns>Список курсов.</returns>
        public static List<Course> GetCourseList()
        {
            return new List<Course>
            {
                GetCourse(0),
                GetCourse(1),
                GetCourse(2)
            };
        }

        /// <summary>
        /// Получить все курсы указанного факультета.
        /// </summary>
        /// <param name="faculty">Факультет.</param>
        /// <returns>Список курсов.</returns>
        public static List<Course> GetCourseList(Faculty faculty)
        {
            return new List<Course>
            {
                GetCourse(0),
                GetCourse(1),
                GetCourse(2)
            };
        }

        /// <summary>
        /// Сохранить курс.
        /// </summary>
        /// <param name="course">Курс.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetCourse(Course course)
        {
            return true;
        }

        /// <summary>
        /// Получить группу.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Группа или null.</returns>
        public static Group GetGroup(int id)
        {
            return new Group(id, GetCourse(id), Group.CodeColumn);
        }

        /// <summary>
        /// Получить список групп.
        /// </summary>
        /// <returns>Список групп.</returns>
        public static List<Group> GetGroupList()
        {
            return new List<Group>
            {
                GetGroup(0),
                GetGroup(1),
                GetGroup(2)
            };
        }

        /// <summary>
        /// Получить список групп указанного курса.
        /// </summary>
        /// <param name="course">Курс.</param>
        /// <returns>Список групп.</returns>
        public static List<Group> GetGroupList(Course course)
        {
            return new List<Group>
            {
                GetGroup(0),
                GetGroup(1),
                GetGroup(2)
            };
        }

        /// <summary>
        /// Сохранить группу.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetGroup(Group group)
        {
            return true;
        }

        /// <summary>
        /// Получить преподавателя.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Преподаватель или null.</returns>
        public static Teacher GetTeacher(int id)
        {
            return new Teacher(id, Teacher.NameColumn, Teacher.SurnameColumn, Teacher.PatronymicColumn);
        }

        /// <summary>
        /// Получить всех преподавателей.
        /// </summary>
        /// <returns>Список преподавателей.</returns>
        public static List<Teacher> GetTeacherList()
        {
            return new List<Teacher>
            {
                GetTeacher(0),
                GetTeacher(1),
                GetTeacher(2)
            };
        }

        /// <summary>
        /// Сохранить преподавателя.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetTeacher(Teacher teacher)
        {
            return true;
        }

        /// <summary>
        /// Получить занятие.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Занятие или null.</returns>
        public static Subject GetSubject(int id)
        {
            return new Subject(id, GetTeacher(id), Subject.NameColumn, Subject.ClassroomColumn);
        }

        /// <summary>
        /// Получить список занятий.
        /// </summary>
        /// <returns>Список занятий.</returns>
        public static List<Subject> GetSubjectList()
        {
            return new List<Subject>
            {
                GetSubject(0),
                GetSubject(1),
                GetSubject(2)
            };
        }

        /// <summary>
        /// Получить список занятий указанного преподавателя.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <returns>Список занятий.</returns>
        public static List<Subject> GetSubjectList(Teacher teacher)
        {
            return new List<Subject>
            {
                GetSubject(0),
                GetSubject(1),
                GetSubject(2)
            };
        }

        /// <summary>
        /// Сохранить занятие.
        /// </summary>
        /// <param name="subject">Занятие.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetSubject(Subject subject)
        {
            return true;
        }

        /// <summary>
        /// Получить тип недели.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Тип недели или null.</returns>
        public static WeekType GetWeekType(int id)
        {
            return new WeekType(id, AvailableWeekType.Red);
        }

        /// <summary>
        /// Получить все типы недель.
        /// </summary>
        /// <returns>Список типов недель.</returns>
        public static List<WeekType> GetWeekTypeList()
        {
            return new List<WeekType>
            {
                GetWeekType(0),
                GetWeekType(1)
            };
        }

        /// <summary>
        /// Сохранить тип недели.
        /// </summary>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetWeekType(WeekType weekType)
        {
            return true;
        }

        /// <summary>
        /// Получить учебный день для группы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Учебный день для группы или null.</returns>
        public static GroupDaySchedule GetGroupDaySchedule(int id)
        {
            return new GroupDaySchedule(id, new List<SubjectEntry>
            {
                new SubjectEntry(EntryPosition.First, GetSubject(id)),
                new SubjectEntry(EntryPosition.Second, GetSubject(id)),
                new SubjectEntry(EntryPosition.Third, GetSubject(id))
            });
        }

        /// <summary>
        /// Получить все учебные дни для всех групп.
        /// </summary>
        /// <returns>Список учебных дней для групп.</returns>
        public static List<GroupDaySchedule> GetGroupDayScheduleList()
        {
            return new List<GroupDaySchedule>
            {
                GetGroupDaySchedule(0),
                GetGroupDaySchedule(1),
                GetGroupDaySchedule(2)
            };
        }

        /// <summary>
        /// Получить все учебные дни для указанной группы.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <returns>Список учебных дней для группы.</returns>
        public static List<GroupDaySchedule> GetGroupDayScheduleList(Group group)
        {
            return new List<GroupDaySchedule>
            {
                GetGroupDaySchedule(0),
                GetGroupDaySchedule(1),
                GetGroupDaySchedule(2)
            };
        }

        /// <summary>
        /// Сохранить учебный день для группы.
        /// </summary>
        /// <param name="day">Учебный день для группы.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetGroupDaySchedule(GroupDaySchedule day)
        {
            return true;
        }

        /// <summary>
        /// Получить учебную неделю для группы.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <returns>Учебная неделя для группы или null.</returns>
        public static GroupWeekSchedule GetGroupWeekSchedule(int id)
        {
            return new GroupWeekSchedule(id, GetGroup(id), GetWeekType(id), new List<DayScheduleEntry>
            {
                new DayScheduleEntry(EntryPosition.First, GetGroupDaySchedule(id)),
                new DayScheduleEntry(EntryPosition.Second, GetGroupDaySchedule(id)),
                new DayScheduleEntry(EntryPosition.Third, GetGroupDaySchedule(id))
            });
        }

        /// <summary>
        /// Получить текущую учебную неделю для группы.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для группы или null.</returns>
        public static GroupWeekSchedule GetGroupWeekSchedule(Group group, WeekType weekType)
        {
            return new GroupWeekSchedule(0, group, weekType, new List<DayScheduleEntry>
            {
                new DayScheduleEntry(EntryPosition.First, GetGroupDaySchedule(0)),
                new DayScheduleEntry(EntryPosition.Second, GetGroupDaySchedule(0)),
                new DayScheduleEntry(EntryPosition.Third, GetGroupDaySchedule(0))
            });
        }

        /// <summary>
        /// Получить все учебные недели для группы.
        /// </summary>
        /// <param name="group">Группа.</param>
        /// <returns>Список учебных недель для группы.</returns>
        public static List<GroupWeekSchedule> GetGroupWeekScheduleList(Group group)
        {
            return new List<GroupWeekSchedule>
            {
                GetGroupWeekSchedule(group, GetWeekType(0)),
                GetGroupWeekSchedule(group, GetWeekType(1))
            };
        }

        /// <summary>
        /// Сохранить учебную неделю для группы.
        /// </summary>
        /// <param name="week">Учебная неделя для группы.</param>
        /// <returns>Статус сохранения.</returns>
        public static bool SetGroupWeekSchedule(GroupWeekSchedule week)
        {
            return true;
        }

        /// <summary>
        /// Собрать все учебные дни для указанного преподавателя на основании данных в базе.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <returns>Список учебных дней для преподавателя.</returns>
        public static List<DaySchedule> BuildDayScheduleList(Teacher teacher)
        {
            return new List<DaySchedule>
            {
                new DaySchedule(new List<SubjectEntry>
                {
                    new SubjectEntry(EntryPosition.First, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Second, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Third, GetSubject(0))
                }),
                new DaySchedule(new List<SubjectEntry>
                {
                    new SubjectEntry(EntryPosition.First, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Second, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Third, GetSubject(0))
                }),
                new DaySchedule(new List<SubjectEntry>
                {
                    new SubjectEntry(EntryPosition.First, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Second, GetSubject(0)),
                    new SubjectEntry(EntryPosition.Third, GetSubject(0))
                })
            };
        }

        /// <summary>
        /// Собрать учебную неделю для преподавателя на основании данных в базе.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <param name="weekType">Тип недели.</param>
        /// <returns>Учебная неделя для преподавателя или null.</returns>
        public static TeacherWeekSchedule BuildTeacherWeekSchedule(Teacher teacher, WeekType weekType)
        {
            var dayList = BuildDayScheduleList(teacher);
            var entryList = new List<DayScheduleEntry>();

            for (int i = 0; i < dayList.Count; i++)
            {
                EntryPosition position = EntityEntry.GetPositionType(i);

                entryList.Add(new DayScheduleEntry(position, dayList[i]));
            }

            return new TeacherWeekSchedule(teacher, weekType, entryList);
        }

        /// <summary>
        /// Собрать все учебные недели для преподавателя на основании данных в базе.
        /// </summary>
        /// <param name="teacher">Преподаватель.</param>
        /// <returns>Список учебных недель для преподавателя.</returns>
        public static List<TeacherWeekSchedule> BuildTeacherWeekScheduleList(Teacher teacher)
        {
            return new List<TeacherWeekSchedule>
            {
                BuildTeacherWeekSchedule(teacher, GetWeekType(0)),
                BuildTeacherWeekSchedule(teacher, GetWeekType(1))
            };
        }
    }
}
