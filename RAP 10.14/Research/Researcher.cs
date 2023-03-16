using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Controller;

namespace RAP.Research
{
    public class Researcher
    {
        public int Id { get; set; } //researcher table id
        public Type JobType { get; set; } //researcher table type
        public string GivenName { get; set; } //researcher table given_name
        public string FamilyName { get; set; } //researcher table family_name
        public string Title { get; set; } //researcher table title
        public string Unit { get; set; } //researcher table unit
        public string Campus { get; set; } //researcher table campus
        public string Email { get; set; } //researcher table email
        public string Photo { get; set; } //researcher table photo

        //public Uri Photo { get; set; } tried to use uri type to make the photo display, failed
        public DateTime CurrentStart { get; set; } //researcher current_start
        public DateTime UtasStart { get; set; } //researcher utas_start
        public level CurrentLevel { get; set; } //researcher level
        public List<ResearcherPublication> Publications { get; set; }
        public List<Position> Position { get; set; } //researcher position
        public List<Publication> Publication { get; set; } //researchers' publication
        public Student Student { get; set; } //students in researchers
        public Staff Staff { get; set; } //staffs in researchers
        public string CurrentTitle //get the current title of the researcher
        {
            get
            {
                if (CurrentLevel == level.A)
                    {
                        return "Postdoc";
                    }
                    else if (CurrentLevel == level.B)
                    {
                        return "Lecturer";
                    }
                    else if (CurrentLevel == level.C)
                    {
                        return "Senior Lecturer";
                    }
                    else if (CurrentLevel == level.D)
                    {
                        return "Associate Professor";
                    }
                    else if (CurrentLevel == level.Student)
                    {
                        return "Student";
                    }
                else
                    {
                        return "Professor";
                    }
                
            }
        }

        //Combine last name and first name into one 
        public string Name
        {
            get
            {
                return this.GivenName + " " + FamilyName;
            }
        }

        public float Tenure
        {
            get { return (DateTime.Today - UtasStart).Days / 365; }
        }
        public string CurrentJob
        {
            get
            {
                if ((int)JobType == 0)
                {
                    return "Student";
                }
                else
                {
                    return CurrentLevel.ToString();
                }
            }
        }
        public string EarliestJob { get; set; } //from position table, get the smallest start of a certain id, and return with the level
        public List<PublicationCount> PublicationsCount { get; set; } //from researcher_publication table, get count number of a certain id




        public double ThreeYearAverage //Calculate the three year average of researchers
        {

            get
            {

                double count = 0.0;
                foreach (Publication a in Publication)
                {

                    if ((a.Age < 4) && (a.Age >= 1))
                    {
                        count = count + 1.0;
                    }
                }
                return Math.Round((double)(count / 3), 1);
            }
        }

        public string Performance //Calculate the performance of researchers
        {

            get
            {

                string performance = "";
                switch (CurrentLevel.ToString())
                {

                    case "Student":
                        performance = "None";
                        break;
                    case "A":
                        performance = ((ThreeYearAverage) / 0.5 * 100).ToString("f1") + "%";
                        break;
                    case "B":
                        performance = ((ThreeYearAverage) / 1.0 * 100).ToString("f1") + "%";
                        break;
                    case "C":
                        performance = ((ThreeYearAverage) / 2.0 * 100).ToString("f1") + "%";
                        break;
                    case "D":
                        performance = ((ThreeYearAverage) / 3.2 * 100).ToString("f1") + "%";
                        break;
                    case "E":
                        performance = ((ThreeYearAverage) / 4.0 * 100).ToString("f1") + "%";
                        break;
                }
                return performance;
            }

        }

        public int PublicationCount  //Calculate the number of publications
        {
            get { return Publication == null ? 0 : Publication.Count(); }
        }


        public override string ToString() //for testing, can be changed
        {
            return GivenName + " " + FamilyName + " (" + Title + ") ";
        }


    }
}
