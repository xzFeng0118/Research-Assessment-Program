using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RAP.Research
{

    public class Publication
    {
        public string Doi { get; set; } //publication table doi
        public string PublicationTitle { get; set; } //publication table title
        public string Authors { get; set; } //publication table authors
        public Int32 Year { get; set; } //publication table year. The years are all represented by int
        public OutputType PublicationType { get; set; } //publication table type
        public string CiteAs { get; set; } //publication table cite_as
        public DateTime Available { get; set; } //publication table available
        public int Age //publication table year, the years are all represented by int
        {
            get { return Convert.ToInt32(DateTime.Today.ToString("yyyy")) - Year; }
        }

        public int PublicationAge //Calculate number of days that publiction has published
        {
            get
            {
                return (DateTime.Today - Available).Days;
            }
        }
        public override string ToString() //for testing, can be changed
        {
            return Doi + "\t" + PublicationTitle + "\t" + Year + "\t" + Authors + "\t" + Available;
        }



    }
}
