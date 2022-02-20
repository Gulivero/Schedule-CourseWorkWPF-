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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        DataTable dataTable = new DataTable(); // создаём таблицу в приложении
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString); // строка подключения


        public AdminPanel()
        {
            InitializeComponent();
            Filling();
        }
        void Filling()
        {
            cn.Open();
            string query = "select * from [dbo].[Группы] order by Код_группы asc";
            SqlCommand command = new SqlCommand(query, cn);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                string group = dr.GetString(1);
                Group.Items.Add(group);
            }
            cn.Close();
            cn.Open();
            string query2 = "select * from [dbo].[Дни_недели] order by Код_дня_недели asc";
            SqlCommand command2 = new SqlCommand(query2, cn);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                string day = dr2.GetString(1);
                Day.Items.Add(day);
            }
            cn.Close();
            cn.Open();
            string query3 = "select * from [dbo].[Пары] order by Номер_пары asc";
            SqlCommand command3 = new SqlCommand(query3, cn);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read())
            {
                string para = dr3.GetString(0);
                Para.Items.Add(para);
            }
            cn.Close();
            cn.Open();
            string query4 = "select * from [dbo].[Дисциплины] order by Код_дисциплины asc";
            SqlCommand command4 = new SqlCommand(query4, cn);
            SqlDataReader dr4 = command4.ExecuteReader();
            while (dr4.Read())
            {
                string subject = dr4.GetString(1);
                Subject.Items.Add(subject);
            }
            cn.Close();
            cn.Open();
            string query5 = "select * from [dbo].[Виды_занятий] order by Код_вида asc";
            SqlCommand command5 = new SqlCommand(query5, cn);
            SqlDataReader dr5 = command5.ExecuteReader();
            while (dr5.Read())
            {
                string classf = dr5.GetString(1);
                Class.Items.Add(classf);
            }
            cn.Close();
            cn.Open();
            string query6 = "select * from [dbo].[Чётность] order by [Неделя] asc";
            SqlCommand command6 = new SqlCommand(query6, cn);
            SqlDataReader dr6 = command6.ExecuteReader();
            while (dr6.Read())
            {
                string classf = dr6.GetString(0);
                Week.Items.Add(classf);
            }
            cn.Close();
            cn.Open();
            string query7 = "select * from [dbo].[Пары] order by [Номер_пары] asc";
            SqlCommand command7 = new SqlCommand(query7, cn);
            SqlDataReader dr7 = command7.ExecuteReader();
            while (dr7.Read())
            {
                string classf = dr7.GetString(1);
                string classa = dr7.GetString(2);
                Start.Items.Add(classf);
                End.Items.Add(classa);
            }
            cn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Kod.Text != "")
            {
                cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter($"DELETE FROM Расписание WHERE [Код расписания] = {Convert.ToInt32(Kod.Text)}", cn);
                adapter.Fill(dataTable);
                cn.Close();
                MessageBox.Show("Занятие успешно удалено");
            }
            else MessageBox.Show("Введите код занятия");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Kod.Text != "")
            {
                if (Group.SelectedItem != null)
                {
                    if (Day.SelectedItem != null)
                    {
                        if (Week.SelectedItem != null)
                        {
                            if (Para.SelectedItem != null)
                            {
                                if (Subject.SelectedItem != null)
                                {
                                    if (Class.SelectedItem != null)
                                    {
                                        if (Audit.Text != "")
                                        {
                                            if (Start.SelectedItem != null)
                                            {
                                                if (End.SelectedItem != null)
                                                {
                                                    cn.Open();
                                                    DataTable clas = new DataTable();
                                                    SqlDataAdapter adap = new SqlDataAdapter($"SELECT * FROM [dbo].[Расписание] WHERE [Код расписания] = '{Convert.ToInt32(Kod.Text)}'", cn);
                                                    adap.Fill(clas);
                                                    cn.Close();

                                                    if (clas.Rows.Count > 0) // если такая запись существует
                                                    {
                                                        MessageBox.Show("Занятие с таким кодом уже существует");
                                                    }
                                                    else
                                                    {
                                                        cn.Open();
                                                        SqlDataAdapter adapter = new SqlDataAdapter($"INSERT INTO [dbo].[Расписание] values  " +
                                                            $"({Convert.ToInt32(Kod.Text)}, '{Group.SelectedItem}', '{Day.SelectedItem}', '{Week.SelectedItem}', '{Para.SelectedItem}', '{Subject.SelectedItem}', '{Class.SelectedItem}', '{Audit.Text}', '{Start.SelectedItem}', '{End.SelectedItem}');", cn);
                                                        adapter.Fill(dataTable);
                                                        cn.Close();
                                                        MessageBox.Show("Занятие успешно добавлено");
                                                    }
                                                }
                                                else MessageBox.Show("Выберите время конца пары");
                                            }
                                            else MessageBox.Show("Выберите время начала пары");
                                        }
                                        else MessageBox.Show("Введите аудиторию");
                                    }
                                    else MessageBox.Show("Выберите вид занятия");
                                }
                                else MessageBox.Show("Выберите дисциплину");
                            }
                            else MessageBox.Show("Выберите пару");
                        }
                        else MessageBox.Show("Выберите чётность недели");
                    }
                    else MessageBox.Show("Выберите день недели");
                }
                else MessageBox.Show("Выберите группу");
            }
            else MessageBox.Show("Введите код расписания");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Kod.Text != "")
            {
                if (Group.SelectedItem != null)
                {
                    if (Day.SelectedItem != null)
                    {
                        if (Week.SelectedItem != null)
                        {
                            if (Para.SelectedItem != null)
                            {
                                if (Subject.SelectedItem != null)
                                {
                                    if (Class.SelectedItem != null)
                                    {
                                        if (Audit.Text != "")
                                        {
                                            if (Start.SelectedItem != null)
                                            {
                                                if (End.SelectedItem != null)
                                                {
                                                        cn.Open();
                                                        SqlDataAdapter adapter = new SqlDataAdapter($"UPDATE [dbo].[Расписание] SET " +
                                                            $"Группа = '{Group.SelectedItem}', [День недели] = '{Day.SelectedItem}', Неделя = '{Week.SelectedItem}', " +
                                                            $"Пара = '{Para.SelectedItem}', Дисциплина = '{Subject.SelectedItem}', [Вид занятия] = '{Class.SelectedItem}', " +
                                                            $"Аудитория = '{Audit.Text}', Начало = '{Start.SelectedItem}', Конец = '{End.SelectedItem}' " +
                                                            $"WHERE [Код расписания] = {Convert.ToInt32(Kod.Text)};", cn);
                                                        adapter.Fill(dataTable);
                                                        cn.Close();
                                                        MessageBox.Show("Занятие успешно изменено");
                                                    
                                                }
                                                else MessageBox.Show("Выберите время конца пары");
                                            }
                                            else MessageBox.Show("Выберите время начала пары");
                                        }
                                        else MessageBox.Show("Введите аудиторию");
                                    }
                                    else MessageBox.Show("Выберите вид занятия");
                                }
                                else MessageBox.Show("Выберите дисциплину");
                            }
                            else MessageBox.Show("Выберите пару");
                        }
                        else MessageBox.Show("Выберите чётность недели");
                    }
                    else MessageBox.Show("Выберите день недели");
                }
                else MessageBox.Show("Выберите группу");
            }
            else MessageBox.Show("Введите код расписания");
        }

        private void Kod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val))
            {
                e.Handled = true;
            }
        }

        private void Kod_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}