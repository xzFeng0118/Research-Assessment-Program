using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{
    public class ResearcherPublication
    {
        public int PublicationId { get; set; } //researcher_publication table researcher_id
        public string PublicationDoi { get; set; } //researcher_publication table doi
    }
}
