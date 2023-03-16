using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{

    public class Position
    {
        public int PositionId { get; set; } //position table id
        public level Level { get; set; } //position table level
        public DateTime Start { get; set; } //position table start
        public DateTime End { get; set; } //position table end
        public string PositionName { get; set; }

        public string PositionTitle
        {
            get
            {
                if ((int)Level == 1)
                {
                    return "Postdoc";
                }
                else if ((int)Level == 2)
                {
                    return "Lecturer";
                }
                else if ((int)Level == 3)
                {
                    return "Senior Lecturer";
                }
                else if ((int)Level == 4)
                {
                    return "Associate Professor";
                }
                else if ((int)Level == 5)
                {
                    return "Professor";
                }
                else
                {
                    return "Student";
                }
            }
        }

        
        public override string ToString() //for testing, can be changed
        {
            return Start.ToShortDateString() + "\t" + End.ToShortDateString() + "\t" + PositionName;
        }
    }
}
