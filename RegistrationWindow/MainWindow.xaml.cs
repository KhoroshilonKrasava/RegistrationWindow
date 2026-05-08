using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using RegistrationWindow.Commands;
using RegistrationWindow.Data;
using RegistrationWindow.Data.Models;
using RegistrationWindow.VM;

namespace RegistrationWindow
{

    public partial class MainWindow : Window
    {
        //private int count = 0;

        //private TextBox dynamicTextBox = null;
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    if (count is not >= 1)
        //    {
        //        dynamicTextBox = new TextBox();
        //        count++;
        //        dynamicTextBox.Style = (Style)this.Resources["TextBox"];
        //        int index = MainPanel.Children.IndexOf(targetElement);
        //        MainPanel.Children.Insert(index + 1, dynamicTextBox);
        //    }
        //    return;
        //}

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

    }


}