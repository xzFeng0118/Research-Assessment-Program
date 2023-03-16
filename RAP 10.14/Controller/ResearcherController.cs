using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RAP.Research;
using RAP.Database;

namespace RAP.Controller
{
    class ResearcherController
    {
        private List<Researcher> ResearcherList; //Declare the data to be called
        public List<Researcher> Workers { get { return ResearcherList; } set { } }

        private ObservableCollection<Researcher> DisplayResearcher;
        public ObservableCollection<Researcher> VisibleResearcher { get { return DisplayResearcher; } set { } }

        public ResearcherController()
        {
            //Retrieve the details of the researcher from the database.
            ResearcherList = database.LoadAll(); //Retrieve the list of researchers from the database

            //Set the visibility of the condition and display the appropriate content when the condition is met
            DisplayResearcher = new ObservableCollection<Researcher>(ResearcherList); 

            //Loop through the search list, taking a different section each time and displaying it separately.
            foreach (Researcher a in ResearcherList)
            {

                a.Position = database.LoadPosition(a.Id);            
                a.PublicationsCount = database.CntPublications(a.Id);
                a.Publication = database.LoadPublication(a.Id);

                //Loading the student section
                if (a.CurrentTitle == "Student")
                {

                    a.Student = database.LoadStudent(a.Id);

                }
                //Loading the staff section
                else
                {

                    a.Staff = database.LoadStaff(a.Id);

                }
            }
        }




        //Combine two filters
        public void Filter(string name, string level)
        {

            //If the researcher level has not yet been selected, filter the results by name.
            SearchByName(name);
            if (level != "All")
            {
                SearchByLevel(level);
            }
        }


        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleResearcher;
        }


        //Filter the list of researchers by their level.
          public void SearchByLevel(string level)
          {
              List<Researcher> view = DisplayResearcher.ToList();
              foreach (Researcher a in view)
              {
                  if (level == a.CurrentLevel.ToString())
                  {
                  }
                  else
                  {
                      DisplayResearcher.Remove(a);
                  }
              }
          }



        //Filter the list of researchers by their name.
        public void SearchByName(string name)
        {
            var SearchName = from Researcher a in ResearcherList
                             where (a.Name.ToLower()).Contains(name.ToLower())
                             select a;
            DisplayResearcher.Clear(); 
            SearchName.ToList().ForEach(DisplayResearcher.Add);
        }


    }
}
