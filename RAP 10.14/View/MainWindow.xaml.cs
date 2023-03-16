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
using System.Windows.Shapes;
using RAP.Controller;
using RAP.Research;
using RAP.Database;

namespace RAP.View
{
    public partial class MainWindow : Window
    {
        private const string Key = "ResearcherList";
        private ResearcherController researcherController;

        public MainWindow()
        {

            //RAPAdapter.LoadAll()//
            InitializeComponent();
            //Define the researcher controller
            researcherController = (ResearcherController)(Application.Current.FindResource(Key) as ObjectDataProvider).ObjectInstance;

            // Guaranteed to be initially empty
            CumulitiveCount.Visibility = System.Windows.Visibility.Hidden;




        }
        //Button 1 Show
        private void Button_Click_1(object sender, RoutedEventArgs a)
        {
            SupervisionName Button1 = new SupervisionName();
            Button1.DataContext = Show.DataContext;
            Button1.ShowDialog();
        }

        //Button 2 CumulitiveCount  After the button is displayed in the listbox below

        private void Button_Click_2(object sender, RoutedEventArgs a)
        {

            if (CumulitiveCount.Visibility == System.Windows.Visibility.Hidden) 
            // Make sure to click once to display the corresponding content, and to hide it twice
            {
                CumulitiveCount.Visibility = System.Windows.Visibility.Visible;
            }
            else
                CumulitiveCount.Visibility = System.Windows.Visibility.Hidden; // Ensure that the initial page has no content.
        }

        //Button 3 Search trigger button for Year range
        private void Button_Click_3(object sender, RoutedEventArgs a)
        {

            //Name the researcher file R, and take out the corresponding information in this.

            Researcher R = listbox_Researcher.SelectedItem as Researcher;

            // Define the format of the year entered in the two textboxes (int)
            int From = Int32.Parse(T1.Text);
            int To = Int32.Parse(T2.Text);

            //Add the information in the publication to the publicationlist, then compare the year in P with the input, and finally output P.

            var Yearrange = from Publication P in R.Publication
                            where (P.Year >= From) && (P.Year <= To)
                            select P;

            // Enter the obtained Year range into the list below and display it.
            listbox_Publication.ItemsSource = Yearrange;
        }

        // Button 4 Year range clear button
        private void Button_Click_4(object sender, RoutedEventArgs a)
        {
            List<Publication> PList = ((Researcher)listbox_Researcher.SelectedItem).Publication;

            // Determine the content in the form, and then clear it.
            listbox_Publication.ItemsSource = null;
            listbox_Publication.ItemsSource = PList;
        }


        // Define the textbox_T1 of From in publication
        private void T1_TextChanged(object sender, TextChangedEventArgs a)
        {

        }

        // Define the textbox_T2 of To in the publication
        private void T2_TextChanged(object sender, TextChangedEventArgs a)
        {

        }




        //Define the Listbox in the researcher name list
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs RNL)
        {

            
            if (RNL.AddedItems.Count > 0)
            {
                DetailsPanel.DataContext = RNL.AddedItems[0];
                Photo.DataContext = RNL.AddedItems[0];
                CumulitiveCount.DataContext = RNL.AddedItems[0];
                listbox_Publication.DataContext = RNL.AddedItems[0];
            }

        }


        //The initial state of the list that defines the publication part is empty when the controller does not reply, and the corresponding information is displayed when replying.
        private void Listbox_Publication_SelectionChanged(object sender, SelectionChangedEventArgs a)
        {
            if (researcherController == null)
            {
            }
            else
            {
                PublicationPanel.DataContext = listbox_Publication.SelectedItem;
            }

        }


        //Custom function. Thereby loading the subpage to the main page.
        private void User_loaded(object sender, RoutedEventArgs e)
        {

        }


        //Custom FrameworkElement.loaded
        private void Window_loaded(object sender, RoutedEventArgs a)
        {

        }

        // Define listbox
        private void ListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
