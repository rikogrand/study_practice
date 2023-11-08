using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class PaymentStatsWindow : Window

{
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    private List<PaymentStats> _paymentStats;
    private MySqlConnection _connection;

    public PaymentStatsWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    public void ShowTable()
    {
        string sql =
                "select PM.Method_Name as Method_Name, " +
                "COUNT(Payment_Method) as Payment_Method " +
                " from Payments " +
                "join pro1_11.Payments_Method PM on Payments.Payment_Method = PM.Method_ID " +
                "Group by Payment_Method"
            ;
        _paymentStats = new List<PaymentStats>();
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {

            var curPaymentStats = new PaymentStats()
            {
               Method_Stats_Name = reader.GetString("Method_Name"),
               Payment_Stats_Method = reader.GetInt32("Payment_Method")
            };
        
            _paymentStats.Add(curPaymentStats);
            
        }

        _connection.Close();
        PaymentStatsDataGrid.ItemsSource = _paymentStats;
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