using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.DAL.Entities.Project
{
    public class ParticipantsJson
    {
        public ParticipantsJson()
        {
            Participants = new List<ParticipantJson>();
        }

        public List<ParticipantJson> Participants { get; set; }
    }

    public class ParticipantJson
    {
        public string EmployeeId { get; set; }
    }
}
