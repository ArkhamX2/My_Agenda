using MyAgenda.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAgenda.Library;

namespace MyAgenda.Test.Integration
{
    /// <summary>
    /// Консольная программа интеграционного тестирования.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Входная функция.
        /// </summary>
        private static void Main()
        {
            // TODO: Начать разработку тестов интеграции.

            Manager.GetGroup(0);
            Manager.GetSubject(0);
            Manager.GetCourse(0);
            Manager.GetDaySchedule(0);
            Manager.GetFaculty(0);
            Manager.GetTeacher(0);
            Manager.GetWeekType(0);
            Manager.GetGroupWeekSchedule(0, Manager.GetGroup(0), Manager.GetWeekType(0));
            Manager.GetTeacherWeekSchedule(Manager.GetTeacher(0), Manager.GetWeekType(0));
        }
    }
}
