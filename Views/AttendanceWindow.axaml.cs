using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class AttendanceWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Attendance> _attendances;
    private MySqlConnection _connection;

    public AttendanceWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
                "select Attendance_ID, C.Surname as Client, " +
                "Number_Of_Classes_Per_Month, Number_Of_Classes_Client," +
                " Number_Of_Pass," +
                " Ratings " +
                "from pro1_11.Attendance "
                + "join pro1_11.Client C on Attendance.Client = C.Client_ID"
            ;
        _attendances = new List<Attendance>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curAttendance = new Attendance()
            {
                Attendance_ID = reader.GetInt32("Attendance_ID"),
                Client = reader.GetString("Client"),
                Number_Of_Classes_Per_Month = reader.GetInt32("Number_Of_Classes_Per_Month"),
                Number_Of_Classes_Client = reader.GetInt32("Number_Of_Classes_Client"),
                Number_Of_Pass = reader.GetInt32("Number_Of_Pass"),
                Ratings = reader.GetString("Ratings"),
            };
            _attendances.Add(curAttendance);
        }

        _connection.Close();
        AttendanceDataGrid.ItemsSource = _attendances;
    }

    private void Schledule_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        ScheduleWindow schleduleWindow = new ScheduleWindow();
        schleduleWindow.Show();
        this.Close();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
    private void Search_attendance_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<Attendance> attendanceSearch =
            _attendances.Where(x => x.Attendance_ID.ToString() == search_attendance.Text).ToList();
        AttendanceDataGrid.ItemsSource = attendanceSearch;
        if (search_attendance.Text == "")
        {
            ShowTable();
        }
    }
    

    private void AddAttendance_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        AddAttendance_Window addAttendance = new AddAttendance_Window();
        addAttendance.ShowDialog(this);
    }

    private void EditAttendance_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        var MyValue = AttendanceDataGrid.SelectedItem as Attendance;
        if (MyValue is null) return;

        EditAttendance_Window edit = new EditAttendance_Window(MyValue);
        edit.ShowDialog(this);
        edit.Closed += (o, args) =>
        {
            if (edit.Result)
            {
                ShowTable();
            }
        };
    }

    private void DeleteAttendance_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        var myValue = AttendanceDataGrid.SelectedItem as Attendance;
        if (myValue is null)
        {
            return;
        }
        DeleteAttendance_Window deleteAttendance = new DeleteAttendance_Window(myValue);
        deleteAttendance.ShowDialog(this);
        deleteAttendance.Closed += (o, args) =>
        {
            if (deleteAttendance.Result)
            {
                ShowTable();
            }
        };
    }
}