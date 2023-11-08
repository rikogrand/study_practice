using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class PaymentWindow : Window
{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<Payments> _payments;
    private MySqlConnection _connection;

    public PaymentWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
                "select Payment_ID," +
                " PM.Method_Name as Payment_Method " +
                "from pro1_11.Payments "
                + "join pro1_11.Payments_Method PM on Payments.Payment_Method = PM.Method_ID"
            ;
        _payments = new List<Payments>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curAttendance = new Payments()
            {
                Payment_ID = reader.GetInt32("Payment_ID"),
                Payment_Method = reader.GetString("Payment_Method"),
            };
            _payments.Add(curAttendance);
        }

        _connection.Close();
        PaymentDataGrid.ItemsSource = _payments;
    }

    private void Schledule_Window_OnClick(object? sender, RoutedEventArgs e)
    {
        ScheduleWindow scheduleWindow = new ScheduleWindow();
        scheduleWindow.Show();
        this.Close();
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}