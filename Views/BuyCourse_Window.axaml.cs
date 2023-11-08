using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace study_practice;

public partial class BuyCourse_Window : Window
{
    private MySqlConnection _connection;
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";

    public BuyCourse_Window()
    {
        InitializeComponent();
    }


    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow courses = new MainWindow();
        courses.Show();
        this.Close();

    }

    private void BuyCourseBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql =
            "insert into Client (Name, Surname, Patronymic, Phone_Number, Birthday, Studied_Languages, Languages_Level, What_Language_Study)" +
            "  values (@Name, @Surname, @Patronymic, @Phone_Number, @Birthday, @Studied_Languages, @Languages_Level, @What_Language_Study );";
            
         _connection = new MySqlConnection(_con);
        _connection.Open();

         MySqlCommand command = new MySqlCommand(sql, _connection);
         command.Parameters.Add("@Name", MySqlDbType.String);
         command.Parameters["@Name"].Value = ClientName.Text;
         command.Parameters.Add("@Surname", MySqlDbType.String);
         command.Parameters["@Surname"].Value = ClientSurname.Text;
         command.Parameters.Add("@Patronymic", MySqlDbType.String);
         command.Parameters["@Patronymic"].Value = ClientPatronymic.Text;
         command.Parameters.Add("@Phone_Number", MySqlDbType.String);
         command.Parameters["@Phone_Number"].Value = ClientPhoneNumber.Text;
         command.Parameters.Add("@Birthday", MySqlDbType.DateTime);
         command.Parameters["@Birthday"].Value = ClientBirthday.SelectedDate;
         command.Parameters.Add("@Studied_Languages", MySqlDbType.String);
         command.Parameters["@Studied_Languages"].Value = ClientStudiedLanguages.Text;
         command.Parameters.Add("@Languages_Level", MySqlDbType.String);
         command.Parameters["@Languages_Level"].Value = ClientLanguagesLevel.Text;
         command.Parameters.Add("@What_Language_Study", MySqlDbType.String);
         command.Parameters["@What_Language_Study"].Value = ClientWhatLanguageStudy.Text;
         command.ExecuteNonQuery();
         _connection.Close();
         PaymentCourse paymentCourse = new PaymentCourse();
         paymentCourse.Show();
         this.Close();
         


    }
}