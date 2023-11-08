using System;
using System.Collections;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class ScheduleWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Course_Schedule> _schedules;
    private MySqlConnection _connection;

    public ScheduleWindow()
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
    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Attendance_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        AttendanceWindow attendanceWindow = new AttendanceWindow();
        attendanceWindow.Show();
        this.Close();
    }

    private void Payment_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        PaymentWindow paymentWindow = new PaymentWindow();
        paymentWindow.Show();
        this.Close();
    }

    private void Stats_Choose_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        StatsSelectWindow statsSelectWindow = new StatsSelectWindow();
        statsSelectWindow.Show();
        this.Close();
    }
}