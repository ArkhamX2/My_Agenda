using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAgenda.Library.Data.Provider
{
    /// <summary>
    /// Менеджер подключения к базе данных.
    /// </summary>
    internal static class ConnectionManager
    {
        /// <summary>
        /// Подключение.
        /// </summary>
        private static MySqlConnection _connection = null;

        /// <summary>
        /// Установить подключение.
        /// TODO: Держать открытым подключение в Manager.OpenConnection()?
        /// </summary>
        /// <returns>Подключение.</returns>
        public static MySqlConnection OpenConnection()
        {
            if (_connection is null)
            {
                _connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["Main"].ConnectionString);
            }

            // Пингуем подключение на предмет обрыва связи.
            // Создает небольшую задержку.
            if (!_connection.Ping())
            {
                _connection.Close();
                _connection.Open();
            }

            return _connection;
        }

        /// <summary>
        /// Закрыть подключение.
        /// </summary>
        /// <returns>Подключение.</returns>
        public static void CloseConnection()
        {
            if (_connection is null)
            {
                return;
            }

            _connection.Close();
        }

        /// <summary>
        /// Произвести запрос к базе данных и получить поток для чтения
        /// результирующих данных.
        /// </summary>
        /// <param name="query">Строка - запрос в SQL формате.</param>
        /// <returns>Поток для чтения результирующих данных.</returns>
        public static MySqlDataReader ExecuteReader(string query)
        {
            return new MySqlCommand(query, OpenConnection()).ExecuteReader();
        }

        /// <summary>
        /// Произвести запрос к базе данных и получить количество затронутых записей.
        /// </summary>
        /// <param name="query">Строка - запрос в SQL формате.</param>
        /// <returns>Количество затронутых записей.</returns>
        public static int ExecuteNonQuery(string query)
        {
            var command = new MySqlCommand(query, OpenConnection());
            var affectedRows = command.ExecuteNonQuery();

            CloseConnection();

            return affectedRows;
        }
    }
}
