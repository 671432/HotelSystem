using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for Authenticate.xaml
    /// </summary>
    public partial class Authenticate : Window
    {
        internal static bool isLoggedIn { get; set; }
        private readonly IUserService _userService;
        private ObservableCollection<User> Users;

        public Authenticate(IUserService userService)
        {
            this.Users = new ObservableCollection<User>();
            this._userService = userService;
            InitializeComponent();
            DataContext = this; //need this for error message to show up
            Loaded += AuthWindow_Loaded;
        }

        private void AuthWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //Debug.WriteLine("AuthWindow_Loaded method called.");
            UpdateLoginStatus();
        }

        private bool CheckLoginStatus()
        {
            try
            {
                
                {
                    //not implemented, so alwais return false
                }
                return false; // If no exception occurred, login succeeded
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Console.WriteLine($"Failed to Login: {ex.Message}");
                return false; // login failed
            }
        }

        private void UpdateLoginStatus()
        {
            bool isLoggedIn = CheckLoginStatus();
            if (isLoggedIn)
            {
                loginFeedback.Text = $"Succeeded Mother Fucker";
                loginFeedback.Foreground = Brushes.Green;
            }
            else
            {
                loginFeedback.Text = "Error 404: Fuck you! (this check is not implemented yet)";
                loginFeedback.Foreground = Brushes.Red;
            }
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void loginFeedback_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void Loggin_Click(object sender, RoutedEventArgs e)
        {
            if (firstName != null && Phone != null)
            {

                var receptionists = await _userService.GetUsersByRoleAsync("Receptionist");
                if (receptionists != null)
                {
                    bool isLoggedIn = receptionists.Any(s => s.Name == firstName.Text && s.Phone == Phone.Text);
                }

            }
            else
            {

            }
        }
    }
}
