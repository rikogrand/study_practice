using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class Schedule_ClientWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Course_Schedule> _schedules;
    private MySqlConnection _connection;

    public Schedule_ClientWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
                "select Course_Schedule_ID, Day_Of_Week, Class_Time from pro1_11.Course_Schedule"
            ;
        _schedules = new List<Course_Schedule>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curSchedule = new Course_Schedule()
            {
                Course_Schedule_ID = reader.GetInt32("Course_Schedule_ID"),
                Day_Of_Week = reader.GetString("Day_Of_Week"),
                Class_Time = reader.GetDateTime("Class_Time")

            };
            _schedules.Add(curSchedule);
        }

        _connection.Close();
        ScheduleDataGrid.ItemsSource = _schedules;

    }

    private void Teacher_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        TeacherWindow teacherWindow = new TeacherWindow();
        teacherWindow.Show();
        this.Close();
    }

    private void Course_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow courseWindow = new MainWindow();
        courseWindow.Show();
        this.Close();
    }
}