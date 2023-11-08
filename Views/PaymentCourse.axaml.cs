using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices.JavaScript;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace study_practice;

public partial class PaymentCourse : Window
{
    private BuyCourse_Window BuyCourseWindow = new BuyCourse_Window();
    public Client Client { get; init; }
    public Course Course { get; init; }
    private List<Payments_Method> _methods;
    private List<Payments> _payments;
    private MySqlConnection _connection;
    private string _con = "server=10.10.1.24;database=pro1_11;user=user_01;password=user01pro";
    public PaymentCourse()
    {
        InitializeComponent();
        FillMethod();
  //     InsertClient();
        
    }

  /*  public void InsertClient()
    {
        _methods = new List<Payments_Method>();
        string sql =
                "insert into pro1_11.Client (Name, Surname, Patronymic, Phone_Number, Birthday, Studied_Languages, Languages_Level, What_Language_Study)" +
                "  values (@Name, @Surname, @Patronymic, @Phone_Number, @Birthday, @Studied_Languages, @Languages_Level, @What_Language_Study );"
            ;
        _connection = new MySqlConnection(_con);
        _connection.Open();
        
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Name", MySqlDbType.String);
        command.Parameters["@Name"].Value = BuyCourseWindow.ClientName.Text;
        command.Parameters.Add("@Surname", MySqlDbType.String);
        command.Parameters["@Surname"].Value = BuyCourseWindow.ClientSurname.Text;
        command.Parameters.Add("@Patronymic", MySqlDbType.String);
        command.Parameters["@Patronymic"].Value = BuyCourseWindow.ClientPatronymic.Text;
        command.Parameters.Add("@Phone_Number", MySqlDbType.String);
        command.Parameters["@Phone_Number"].Value = BuyCourseWindow.ClientPhoneNumber.Text;
        command.Parameters.Add("@Birthday", MySqlDbType.DateTime);
        command.Parameters["@Birthday"].Value = BuyCourseWindow.ClientBirthday.SelectedDate;
        command.Parameters.Add("@Studied_Languages", MySqlDbType.String);
        command.Parameters["@Studied_Languages"].Value = BuyCourseWindow.ClientStudiedLanguages.Text;
        command.Parameters.Add("@Languages_Level", MySqlDbType.String);
        command.Parameters["@Languages_Level"].Value = BuyCourseWindow.ClientLanguagesLevel.Text;
        command.Parameters.Add("@What_Language_Study", MySqlDbType.String);
        command.Parameters["@What_Language_Study"].Value = BuyCourseWindow.ClientWhatLanguageStudy.Text;
        command.ExecuteNonQuery();
        _connection.Close();

    }
    */
    public void FillMethod()
    {
        string sql = "select Method_Name from Payment_Method";
        _methods = new List<Payments_Method>();
        _connection = new MySqlConnection(_con);
        _connection.Open();

        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var curMethod = new Payments_Method()
            {
                Method_Name = reader.GetString("Method_Name")
            
            };
            _methods.Add(curMethod);
        }

        _connection.Close();
        var methodComboBox = this.Find<ComboBox>("MethodComboBox");
        methodComboBox.ItemsSource = _methods;


    }

    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        BuyCourse_Window buyCourseWindow = new BuyCourse_Window();
        buyCourseWindow.Show();
        this.Close();
    }


    private void ConfirmPaymentBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        string sql =
            " select * from Payments;" +
            " select  PM.Method_Name as Payment_Method from Payments " +
            " join Payments_Method PM on Payments.Payment_Method = PM.Method_ID " +
            "   insert into Payments(Payment_Method) value (1); ";
        _connection = new MySqlConnection(_con);
        _connection.Open();
        
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.Add("@Payment_Method", MySqlDbType.Int32);
        command.Parameters["@Payment_Method"].Value = MethodComboBox.SelectedValue;
        command.ExecuteNonQuery(); 
        _connection.Close();

        Success_BuyWindow successBuyWindow = new Success_BuyWindow();
        successBuyWindow.Show();
        this.Close();
    }
}