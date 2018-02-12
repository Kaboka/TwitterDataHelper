using MongoDB.Driver;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TwitterDataHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var settings = System.IO.File.ReadAllText("ConnectionSettings.txt").Split(' ');
            var database = new DatabaseHandler(settings[0],settings[1]);
            numberOfUsers.Content = database.CountUsers();
            linkThemost.ItemsSource = database.TwitterUsersThatlinkTheMost();
           // mostMentionedUsers = database.MostMentionedUsers();
            activeUsers.ItemsSource = database.MostAtciveUsers();
            happiness.ItemsSource = database.UserHappyness(4);
            negative.ItemsSource = database.UserHappyness(0);
        }
    }
}
