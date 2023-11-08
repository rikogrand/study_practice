using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class AttendanceStatsWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<AttendanceStats> _attendanceStats;
    private MySqlConnection _connection;

    public AttendanceStatsWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
            "select  Sum(Client) as Client, " +
            "Sum(Number_Of_Classes_Per_Month) as Number_Of_Classes_Per_Month, " +
            "SUM(Number_Of_Classes_Client) as Number_Of_Classes_Client," +
            "SUM(Number_Of_Pass) as Number_Of_Pass," +
            "AVG(Ratings) as Ratings " +
            "from Attendance;";
            
        _attendanceStats = new List<AttendanceStats>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curAttendanceStats = new AttendanceStats()
            {
                Client = reader.GetInt32("Client"),
                Number_Of_Classes_Per_Month = reader.GetInt32("Number_Of_Classes_Per_Month"),
                Number_Of_Classes_Client = reader.GetInt32("Number_Of_Classes_Client"),
                Number_Of_Pass = reader.GetInt32("Number_Of_Pass"),
                Ratings = reader.GetInt32("Ratings"),
            };
        
            _attendanceStats.Add(curAttendanceStats);
            
        }

        _connection.Close();
        AttendanceStatsDataGrid.ItemsSource = _attendanceStats;
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