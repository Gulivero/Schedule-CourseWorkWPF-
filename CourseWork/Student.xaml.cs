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
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : Window
    {

        DataTable dataTable = new DataTable(); // создаём таблицу в приложении
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["connect"].ConnectionString); // строка подключения


        public Student()
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
                GroupFilter.Items.Add(group);
            }
            cn.Close();
            cn.Open();
            string query2 = "select * from [dbo].[Дни_недели] order by Код_дня_недели asc";
            SqlCommand command2 = new SqlCommand(query2, cn);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                string day = dr2.GetString(1);
                DayFilter.Items.Add(day);
            }
            cn.Close();
            cn.Open();
            string query3 = "select * from [dbo].[Пары] order by Номер_пары asc";
            SqlCommand command3 = new SqlCommand(query3, cn);
            SqlDataReader dr3 = command3.ExecuteReader();
            while (dr3.Read())
            {
                string para = dr3.GetString(0);
                ParaFilter.Items.Add(para);
            }
            cn.Close();
            cn.Open();
            string query4 = "select * from [dbo].[Дисциплины] order by Код_дисциплины asc";
            SqlCommand command4 = new SqlCommand(query4, cn);
            SqlDataReader dr4 = command4.ExecuteReader();
            while (dr4.Read())
            {
                string subject = dr4.GetString(1);
                SubjectFilter.Items.Add(subject);
            }
            cn.Close();
            cn.Open();
            string query5 = "select * from [dbo].[Виды_занятий] order by Код_вида asc";
            SqlCommand command5 = new SqlCommand(query5, cn);
            SqlDataReader dr5 = command5.ExecuteReader();
            while (dr5.Read())
            {
                string classf = dr5.GetString(1);
                ClassFilter.Items.Add(classf);
            }
            cn.Close();
            cn.Open();
            string query6 = "select * from [dbo].[Чётность] order by [Неделя] asc";
            SqlCommand command6 = new SqlCommand(query6, cn);
            SqlDataReader dr6 = command6.ExecuteReader();
            while (dr6.Read())
            {
                string classf = dr6.GetString(0);
                WeekFilter.Items.Add(classf);
            }
            cn.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dataTable.Clear();
            cn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание]" +
                                                        "ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                        "SELECT * FROM #TemporaryTable " +
                                                        "DROP TABLE #TemporaryTable ", cn);
            adapter.Fill(dataTable);
            Grid.DataContext = dataTable.DefaultView;
            cn.Close();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            int firstDayOfYear = (int)new DateTime(date.Year, 1, 1).DayOfWeek;
            int weekNumber = (((date.DayOfYear + firstDayOfYear) / 7 + 1) % 2) + 1;
            dataTable.Clear();
            cn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание] where [День недели] = '" + date.ToString("dddd") +
                                                        "' and ([Неделя] like '%-' or [Неделя] like '%" + weekNumber +
                                                    "%') ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                        "SELECT * FROM #TemporaryTable " +
                                                        "DROP TABLE #TemporaryTable ", cn);
            adapter.Fill(dataTable);
            Grid.DataContext = dataTable.DefaultView;
            cn.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Group.SelectedItem != null)
            {
                DateTime date = DateTime.Now;
                int firstDayOfYear = (int)new DateTime(date.Year, 1, 1).DayOfWeek;
                int weekNumber = (((date.DayOfYear + firstDayOfYear) / 7 + 1) % 2) + 1;
                dataTable.Clear();
                cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание] where [День недели] = '" + date.ToString("dddd") +
                                                            "' and ([Неделя] like '%-' or [Неделя] like '%" + weekNumber +
                                                        "%') and Группа = '" + Group.SelectedItem + "' ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                            "SELECT * FROM #TemporaryTable " +
                                                            "DROP TABLE #TemporaryTable ", cn);
                adapter.Fill(dataTable);
                Grid.DataContext = dataTable.DefaultView;
                cn.Close();
            }
            else MessageBox.Show("Сначала выберите группу");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (Group.SelectedItem != null)
            {
                DateTime date = DateTime.Now;
                int firstDayOfYear = (int)new DateTime(date.Year, 1, 1).DayOfWeek;

                int weekNumber = (((date.DayOfYear + firstDayOfYear) / 7 + 1) % 2) + 1;
                dataTable.Clear();
                cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание] where [День недели] = '" + date.ToString("dddd") +
                                                            "' and ([Неделя] like '%-' or [Неделя] like '%" + weekNumber +
                                                        "%') and Группа = '" + Group.SelectedItem + "' and Дисциплина != '-'" +
                                                        "ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                            "SELECT * FROM #TemporaryTable " +
                                                            "DROP TABLE #TemporaryTable ", cn);
                adapter.Fill(dataTable);
                Grid.DataContext = dataTable.DefaultView;
                cn.Close();
                int count_row = Grid.Items.Count - 1;
                MessageBox.Show("Всего пар сегодня: " + count_row.ToString() + " пары\n" +
                    "Время учёбы на сегодня: " + (count_row * 95 / 60).ToString() + " часов " +
                    (count_row * 95 % 60).ToString() + " минут");
            }
            else MessageBox.Show("Сначала выберите группу");
        }
        //1
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            GroupFilter.IsEnabled = true;
            
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            GroupFilter.IsEnabled = false;
            GroupFilter.SelectedValue = null;
        }
        //2
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
                DayFilter.IsEnabled = true;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            DayFilter.IsEnabled = false;
            DayFilter.SelectedValue = null;
        }
        //3
        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
                ParaFilter.IsEnabled = true;
        }

        private void CheckBox_Unchecked_2(object sender, RoutedEventArgs e)
        {
            ParaFilter.IsEnabled = false;
            ParaFilter.SelectedValue = null;
        }
        //4
        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
                SubjectFilter.IsEnabled = true;
        }

        private void CheckBox_Unchecked_3(object sender, RoutedEventArgs e)
        {
            SubjectFilter.IsEnabled = false;
            SubjectFilter.SelectedValue = null;
        }
        //5
        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
                ClassFilter.IsEnabled = true;
        }

        private void CheckBox_Unchecked_4(object sender, RoutedEventArgs e)
        {
            ClassFilter.IsEnabled = false;
            ClassFilter.SelectedValue = null;
        }
        //6
        private void CheckBox_Checked_5(object sender, RoutedEventArgs e)
        {
            WeekFilter.IsEnabled = true;
        }

        private void CheckBox_Unchecked_5(object sender, RoutedEventArgs e)
        {
            WeekFilter.IsEnabled = false;
            WeekFilter.SelectedValue = null;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            dataTable.Clear();
            cn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание] where [Группа] like '%" + GroupFilter.SelectedItem +
                                                        "%' and [День недели] like '%" + DayFilter.SelectedItem +
                                                        "%' and [Пара] like '%" + ParaFilter.SelectedItem +
                                                        "%' and [Дисциплина] like '%" + SubjectFilter.SelectedItem +
                                                        "%' and [Вид занятия] like '%" + ClassFilter.SelectedItem +
                                                        "%' and ([Неделя] like '%-' or [Неделя] like '%" + WeekFilter.SelectedItem +
                                                        "%') ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                        "SELECT * FROM #TemporaryTable " +
                                                        "DROP TABLE #TemporaryTable ", cn);
            adapter.Fill(dataTable);
            Grid.DataContext = dataTable.DefaultView;
            cn.Close();
        }

        private void calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = calendar1.SelectedDate;
            int firstDayOfYear = (int)new DateTime(selectedDate.Value.Date.Year, 1, 1).DayOfWeek;
            int weekNumber = (((selectedDate.Value.Date.DayOfYear + firstDayOfYear) / 7 + 1) % 2) + 1;
                dataTable.Clear();
                cn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * INTO #TemporaryTable FROM [dbo].[Расписание] where [День недели] = '" + selectedDate.Value.Date.ToString("dddd") +
                                                            "' and ([Неделя] like '%-' or [Неделя] like '%" + weekNumber +
                                                        "%') ALTER TABLE #TemporaryTable DROP COLUMN [Код расписания]" +
                                                            "SELECT * FROM #TemporaryTable " +
                                                            "DROP TABLE #TemporaryTable ", cn);
                adapter.Fill(dataTable);
                Grid.DataContext = dataTable.DefaultView;
                cn.Close();
            
        }

    }
}
