/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Research.Researcher> test = GenerateResearcherTestData();
            DisplayEmployees(test);

            List<Research.Researcher> GenerateResearcherTestData()
            {
                List<Research.Researcher> data = new List<Research.Researcher>();
                data.Add(new Research.Researcher { GivenName = "Jane", FamilyName = "a", Id = 1, JobType = Type.Staff, CurrentLevel = level.A });
                data.Add(new Research.Researcher { GivenName = "John", FamilyName = "a", Id = 3, JobType = Type.Student });
                data.Add(new Research.Researcher { GivenName = "Mary", FamilyName = "a", Id = 7, JobType = Type.Staff, CurrentLevel = level.C });
                data.Add(new Research.Researcher { GivenName = "Lindsay", FamilyName = "a", Id = 5, JobType = Type.Student });
                data.Add(new Research.Researcher { GivenName = "Meilin", FamilyName = "a", Id = 2, JobType = Type.Staff, CurrentLevel = level.D });

                return data;
            }
            void DisplayEmployees(List<Research.Researcher> staff)
            {
                foreach (Research.Researcher e in staff)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}*/
