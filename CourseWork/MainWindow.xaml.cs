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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CourseWork
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dataTable = new DataTable(); // создаём таблицу в приложении
        SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString); // строка подключения

        public MainWindow()
        {
            InitializeComponent();
        }
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text.Length > 0) // проверяем введён ли логин
            {
                if (password.Password.Length > 0) // проверяем введён ли пароль
                {
                    // ищем в базе данных пользователя с такими данными
                    DataTable loginuser = this.Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + login.Text +
                        "' AND [password] = '" + password.Password +
                        "' AND [role] = 'admin'");

                    if (loginuser.Rows.Count > 0) // если такая запись существует
                    {
                        MessageBox.Show("Администратор авторизовался");
                        Admin win2 = new Admin();
                        win2.Show();
                        this.Close();

                    }
                    else
                    {
                        loginuser = this.Select("SELECT * FROM [dbo].[users] WHERE [login] = '" + login.Text +
                            "' AND [password] = '" + password.Password +
                            "' AND [role] = 'user'");

                        if (loginuser.Rows.Count > 0)
                        {
                            MessageBox.Show("Студент авторизовался");
                            Student win3 = new Student();
                            win3.Show();
                            this.Close();
                        }
                        else
                            MessageBox.Show("Пользователь не найден");
                    }
                }
                else
                    MessageBox.Show("Введите пароль");
            }
            else
                MessageBox.Show("Введите логин");
        }
        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработки запросов
        {
            sqlConnection.Open(); // открываем БД
            SqlCommand sqlCommand = sqlConnection.CreateCommand(); // создаём команду
            sqlCommand.CommandText = selectSQL; // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable); // возвращает таблицу с результатом
            sqlConnection.Close();
            return dataTable;
        }



    }
}

