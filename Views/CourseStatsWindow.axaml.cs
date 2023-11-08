using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class CourseStatsWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<CourseStats> _courseStats;
    private MySqlConnection _connection;

    public CourseStatsWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
            "select Sum(Number_Of_Seats) as Number_Of_Seats, " +
            "Sum(Number_Of_Busy) as Number_Of_Busy, " +
            " Sum(Price*Number_Of_Busy) as Price, " +
            " Sum(Teacher) as Teacher " +
            "from Course ";
            
        _courseStats = new List<CourseStats>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curCourseStats = new CourseStats()
            {
                Number_Of_Seats = reader.GetInt32("Number_Of_Seats"),
                Number_Of_Busy = reader.GetInt32("Number_Of_Busy"),
                Price = reader.GetDecimal("Price"),
                Teacher = reader.GetInt32("Teacher")
            };
        
            _courseStats.Add(curCourseStats);
            
        }

        _connection.Close();
        CourseStatsDataGrid.ItemsSource = _courseStats;
    }

    private void StatsSelectWindow_OnClick(object? sender, RoutedEventArgs e)
    {
        StatsSelectWindow statsSelectWindow = new StatsSelectWindow();
        statsSelectWindow.Show();
        this.Close();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}