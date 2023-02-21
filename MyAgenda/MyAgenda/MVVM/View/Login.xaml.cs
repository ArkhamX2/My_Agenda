using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWindow;
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();

            mainWindow = _mainWindow;
        }

        private void regin_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.regin);
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content.Visibility = Visibility.Visible;
            mainWindow.TopMenu.Visibility = Visibility.Visible;
            mainWindow.frame.Visibility = Visibility.Collapsed;
            //if (login.Text.Length > 0) // проверяем введён ли логин     
            //{
            //    if (password.Password.Length > 0) // проверяем введён ли пароль         
            //    {             // ищем в базе данных пользователя с такими данными         
            //        DataTable dt_user = mainWindow.Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + login.Text + "' AND [password] = '" + password.Password + "'");
            //        if (dt_user.Rows.Count > 0) // если такая запись существует       
            //        {
            //            MessageBox.Show("Пользователь авторизовался"); // говорим, что авторизовался         
            //        }
            //        else MessageBox.Show("Пользователя не найден"); // выводим ошибку  
            //    }
            //    else MessageBox.Show("Введите пароль"); // выводим ошибку    
            //}
            //else MessageBox.Show("Введите логин"); // выводим ошибку 
        }
    }
}
