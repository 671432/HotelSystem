using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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
using HotelLibrary.Interfaces;
using HotelLibrary.Models;
using HotelLibrary.Services;

namespace WPFReceptionist
{
    /// <summary>
    /// Interaction logic for CreateUser.xaml
    /// </summary>
    /// 


    public partial class CreateUser : Window
    {

        private readonly IUserService _userService;
        private ObservableCollection<User> Users;
        public CreateUser(IUserService userService)
        {
            InitializeComponent();
            this._userService = userService;
            this.Users = new ObservableCollection<User>();

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(Firstname.Text) && 
                !string.IsNullOrWhiteSpace(Lastname.Text) && 
                !string.IsNullOrWhiteSpace(Phonenumber.Text))
            {

                await _userService.CreateUser(Firstname.Text, Lastname.Text, Phonenumber.Text);
                MessageBox.Show("New customer created!");
                
            }else{

                MessageBox.Show("Please fill in all the fields.");

            }
            
        }
    }
}
