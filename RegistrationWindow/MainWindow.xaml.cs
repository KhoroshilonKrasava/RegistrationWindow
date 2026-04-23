using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RegistrationWindow.Data;
using RegistrationWindow.Data.Models;

namespace RegistrationWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            Test();
        }

        public void Test()
        {
           
            UsersContext userContext = new UsersContext();
            //User userBob = new User() { Login = "1233", PasswordHash = "123wqad" };
            //userContext.Users.Add(userBob);
            //userContext.SaveChanges();
            foreach (var user in userContext.Users)
            {
                var tb = new TextBlock();
                tb.Text = user.Login;
                tb.Margin = new Thickness(5);
                TestBox.Children.Add(tb);
                Console.WriteLine(user.Login + "  " + user.Id + "  " + user.PasswordHash);
            }
        }
    }
}