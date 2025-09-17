using HotelLibrary.Models;
using Microsoft.VisualBasic.ApplicationServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {

            InitializeComponent();
            bool loggedInStatus = Authenticate.isLoggedIn;
            // Loaded += MainWindow_Loaded;
        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Debug.WriteLine("MainWindow_Loaded method called.");
        //    UpdateDbConnectionStatus();
        //}

        //Login check

        //private bool CheckDbConnection()
        //{

        //}

        //private void UpdateDbConnectionStatus()
        //{
        //    Debug.WriteLine("UpdateDbConnectionStatus method called.");
        //    bool isLoggedIn = CheckDbConnection();
        //    if (isLoggedIn)
        //    {
        //        LoginStatus.Text = $"Logged in as {Authenticate.User}";
        //        LoginStatus.Foreground = Brushes.Green;
        //    }
        //    else
        //    {
        //        LoginStatus.Text = "Not logged in!";
        //        LoginStatus.Foreground = Brushes.Red;
        //    }
        //}




        private void Authenticate_Click(object sender, RoutedEventArgs e)
        {
            var a = App.GetServices<Authenticate>();
            a.Show();
        }



        private void CreateReservation_Click(object sender, RoutedEventArgs e)
        {
            var a = App.GetServices<CreateReservation>();
            a.Show();
        }

        private void ManageReservation_Click(object sender, RoutedEventArgs e)
        {
            var a = App.GetServices<ManageReservation>();
            a.Show();
        }

        private void ServiceRoom_Click(object sender, RoutedEventArgs e)
        {
            var a = App.GetServices<ServiceRoom>();
            a.Show();
        }

        private void FeedBack_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}