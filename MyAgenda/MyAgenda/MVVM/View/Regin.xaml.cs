using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyAgenda.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для Regin.xaml
    /// </summary>
    public partial class Regin : Page
    {
        public MainWindow mainWindow;
        public Regin(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.login);
        }

        private void regin_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length > 0) // проверяем логин
            {
                if (password.Password.Length > 0) // проверяем пароль
                {
                    if (password_Copy.Password.Length > 0) // проверяем второй пароль
                    {
                        string[] dataLogin = login.Text.Split('@'); // делим строку на две части
                        if (dataLogin.Length == 2) // проверяем если у нас две части
                        {
                            string[] data2Login = dataLogin[1].Split('.'); // делим вторую часть ещё на две части
                            if (data2Login.Length == 2)
                            {
                                if (password.Password.Length >= 6)
                                {
                                    bool en = true; // английская раскладка
                                    bool symbol = false; // символ
                                    bool number = false; // цифра
                                    for (int i = 0; i < password.Password.Length; i++) // перебираем символы
                                    {
                                        if (password.Password[i] >= 'А' && password.Password[i] <= 'Я') en = false; // если русская раскладка
                                        if (password.Password[i] >= '0' && password.Password[i] <= '9') number = true; // если цифры
                                        if (password.Password[i] == '_' || password.Password[i] == '-' || password.Password[i] == '!') symbol = true; // если символ
                                    }
                                    if (!en)
                                        MessageBox.Show("Доступна только английская раскладка"); // выводим сообщение
                                    else if (!symbol)
                                        MessageBox.Show("Добавьте один из следующих символов: _ - !"); // выводим сообщение
                                    else if (!number)
                                        MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                                    if (en && symbol && number) // проверяем соответствие
                                    {
                                        if (password.Password == password_Copy.Password) // проверка на совпадение паролей
                                        {
                                            MessageBox.Show("Пользователь зарегистрирован");
                                        }
                                        else MessageBox.Show("Пароли не совподают");
                                    }
                                }
                                else MessageBox.Show("пароль слишком короткий, минимум 6 символов");
                            }
                            else MessageBox.Show("Укажите логин в форме х@x.x");
                        }
                        else MessageBox.Show("Укажите логин в форме х@x.x");
                    }
                    else MessageBox.Show("Повторите пароль");
                }
                else MessageBox.Show("Укажите пароль");
            }
            else MessageBox.Show("Укажите логин");
        }
    }
}