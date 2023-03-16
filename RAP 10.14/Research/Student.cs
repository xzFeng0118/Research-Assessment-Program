using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Database;

namespace RAP.Research
{
    public class Student : Researcher
    {
        public string Degree { get; set; } //researcher table degree

        public int SupervisorId { get; set; } //researcher table supervisor_id

        public string SupervisorName { get { return database.GetName(SupervisorId); } } //supervisor's name of the student

    }
}
