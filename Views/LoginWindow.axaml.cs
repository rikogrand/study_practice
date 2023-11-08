using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace study_practice;

public partial class LoginWindow : Window
{
    private MySqlConnection _connection;
    private string _con = "server=10.10.1.24;database=pro1_1;user=user_01;password=user01pro";

    public LoginWindow()
    {
        InitializeComponent();
    }

    private void BackButton_OnClick(object? sender, RoutedEventArgs e)
    {
        SelectionWindow selectionWindow = new SelectionWindow();
        selectionWindow.Show();
        this.Close();
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {

        MySqlConnection _connection;
        string sql = "SELECT Login, Password from pro1_11.AdminTools WHERE Login = @login and Password = @password";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginTextBox.Text;
        var login = command.Parameters["@login"].Value;
        command.Parameters.Add("@password", MySqlDbType.VarChar).Value = PasswordTextBox.Text;
        var password = command.Parameters["@password"].Value;
        var reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            if(LoginTextBox.Text.Equals(login) && PasswordTextBox.Text.Equals(password))
            {
                ScheduleWindow scheduleWindow = new ScheduleWindow();
                scheduleWindow.Show();
                this.Close();
            }
        }
    }

    private void HidePasswordCheckBox_OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if(PasswordTextBox.PasswordChar == (char)0)
        {
            PasswordTextBox.PasswordChar = '•';
        }
        else
        {
            PasswordTextBox.PasswordChar = (char)0;
        }

   
    }
}