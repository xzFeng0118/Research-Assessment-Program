using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Database;
using RAP.Research;
using System.Collections.ObjectModel;

namespace RAP.Controller
{
    //The Cumulative Details screen displays detailed publication information.
    public class PublicationCount //Declare the data to be called. Record year and quantity.
    {
        public int Year { get; set; }
        public int Count { get; set; }

        private database dataadapter = null;

        //Number of publications acquired.
        public override string ToString() //Convert Year and Cont to string format and display them in the following form.
        {

            return "Year: " + Year + "  Quantity:   " + Count;

        }

        //Filter information on publications by a user-selected range of years.
        public ObservableCollection<Publication> GetPublicationsByRange(int rid, int start, int end) 
        {

            ObservableCollection<Publication> allPublications = new ObservableCollection<Publication>();

            allPublications = dataadapter.GetPubByID(rid);

            ObservableCollection<Publication> filteredPublications = new ObservableCollection<Publication>();

            //Output c when the posting time of c is greater than the start time and less than the end time.
            var result = from Publication c in allPublications
                         where (c.Year >= start) && (c.Year <= end)  
                         select c;

            ObservableCollection<Publication> filterPublications = new ObservableCollection<Publication>(result.ToList());
            return filteredPublications;
        }



    }
}
