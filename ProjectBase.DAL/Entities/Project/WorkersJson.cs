using System.Collections.Generic;

namespace ProjectBase.DAL.Entities.Project
{
    public class WorkersJson
    {
        public WorkersJson()
        {
            Workers = new List<WorkerJson>();
        }

        public List<WorkerJson> Workers { get; set; }
    }

    public class WorkerJson
    {
        public string EmployeeId { get; set; }
    }
}
