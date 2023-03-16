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
using RAP.Controller;
using RAP.Research;
using RAP.Database;

namespace RAP.View
{
    /// <summary>
    /// The interaction logic of User.xaml
    /// </summary>
    public partial class User : UserControl
    {

        private const string Key = "ResearcherList";
        private ResearcherController researcherController;

        public User()
        {
            InitializeComponent();
            //Define the researcher controller
            researcherController = (ResearcherController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;

        }

        //Define searchbox
        private void Search_TextChanged(object sender, TextChangedEventArgs a)
        //Searching by key word achieved here through LINQ
        {

            if (researcherController == null)
            {
                //null in the box
            }
            else
            {
                string name = SearchKeyWord.Text;
                string level = Combo1.SelectedItem.ToString();
                researcherController.Filter(name, level);
            }
        }


        //Define drop-down list
        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs a)
        {
            if (researcherController == null)
            {
                // null in the box
            }
            else
            {
                string name = SearchKeyWord.Text;
                string level = null;
                if (null == Combo1.SelectedItem)
                {
                    level = "All";
                }
                else
                {
                    level = Combo1.SelectedItem.ToString();
                }
                researcherController.Filter(name, level);
            }
        }

        //Custom function User_loaded
        private void User_Loaded(object sender, RoutedEventArgs a)
        {
            // Using enum to define employmentlevel (在researcher中定义的level)
            var EmployeeLevelList = Enum.GetValues(typeof(level)).Cast<level>();
            Combo1.ItemsSource = EmployeeLevelList;
            Combo1.SelectedIndex = 0;

        }

    }
}
