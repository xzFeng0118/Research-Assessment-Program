using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{
    public class Staff : Researcher
    {
        public level CurLevel { get; set; } //researcher table level
        public float ThreeYearAverage { get; set; }//from publication & researcher_publication table, get all doi of a certain id, use doi to check if the year is after 2018, count all after 2018 and divide it by 3
        public List<Position> Positions { get; set; }
        public float? Performance
        {
            get
            {
                if ((int)CurLevel == 1)
                {
                    return ThreeYearAverage / (float)0.5;
                }
                else if ((int)CurLevel == 2)
                {
                    return ThreeYearAverage / 1;
                }
                else if ((int)CurLevel == 3)
                {
                    return ThreeYearAverage / 2;
                }
                else if ((int)CurLevel == 4)
                {
                    return ThreeYearAverage / (float)3.2;
                }
                else if ((int)CurLevel == 5)
                {
                    return ThreeYearAverage / 4;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Researcher> Student { get; set; }

        public int SupervisingStudentNumber //Count the number of students that one supervisor has
        {
            get
            {
                int Number = 0;
                Number = Student == null ? 0 : Student.Count();
                return Number;
            }
        }
    }
}
