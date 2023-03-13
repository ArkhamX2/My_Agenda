using MyAgenda.Library.Model;
using MyAgenda.Library.Model.Base;
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

            Manager.Initialize();

            var groupList = Manager.GetGroupList();
            var teacherList = Manager.GetTeacherList();
            var weekTypeList = Manager.GetWeekTypeList();

            var groupWeek = Manager.GetGroupWeekSchedule(groupList[0], weekTypeList[0]);
            var teacherWeek = Manager.GetTeacherWeekSchedule(teacherList[0], weekTypeList[0]);
        }
    }
}
