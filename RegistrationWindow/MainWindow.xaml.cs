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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public MainWindow( MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }

    }

    
}