using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
namespace study_practice;
public partial class MainWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Course> _courses;
    private MySqlConnection _connection;

    public MainWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
            "select Course_ID," +
            " Course_Name, " +
            "Number_Of_Seats, " +
            "Number_Of_Busy, " +
            "Price, " +
            "T.Teacher_Surname as Teacher  " +
            "from pro1_11.Course" +
            " join pro1_11.Teacher T on Course.Teacher = T.Teacher_ID"
            ;
        _courses = new List<Course>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curCourse = new Course()
            {
                Course_ID = reader.GetInt32("Course_ID"),
                Course_Name = reader.GetString("Course_Name"),
                Number_Of_Seats = reader.GetInt32("Number_Of_Seats"),
                Number_Of_Busy = reader.GetInt32("Number_Of_Busy"),
                Price = reader.GetDecimal("Price"),
                Teacher = reader.GetString("Teacher"),
            };
            _courses.Add(curCourse);
        }
        _connection.Close();
        CourseDataGrid.ItemsSource = _courses;
    }
    

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void BuyCourse_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        BuyCourse_Window buyCourseWindow = new BuyCourse_Window();
        buyCourseWindow.Show();
        this.Close();
    }

    private void Schledule_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        Schedule_ClientWindow scheduleClientWindow = new Schedule_ClientWindow();
        scheduleClientWindow.Show();
        this.Close();
    }

    private void InfoCourses_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        InfoCoursesWindow infoCoursesWindow = new InfoCoursesWindow();
        infoCoursesWindow.Show();
    }
}