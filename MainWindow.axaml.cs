using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
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
            "select Course_ID, Course_Name, Number_Of_Seats, Number_Of_Busy, Price, Teacher  from pro1_11.Course" +
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
                Course_Schedule = reader.GetInt32("Course_Schedule"),
                Teacher = reader.GetInt32("Teacher"),
            };
            _courses.Add(curCourse);
        }
        _connection.Close();
        CourseDataGrid.ItemsSource = _courses;
    }
}