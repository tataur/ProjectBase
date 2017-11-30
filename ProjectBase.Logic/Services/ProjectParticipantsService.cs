using ProjectBase.DAL.DBContext;
using ProjectBase.DAL.Entities.Project;
using ProjectBase.Logic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBase.Logic.Services
{
    public class ProjectParticipantsService : IService<ParticipantDTO>
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();
        private readonly EmployeeService EService = new EmployeeService();
        private readonly ProjectService PService = new ProjectService();

        public List<ParticipantDTO> GetAll()
        {
            var entities = Context.Participants.ToList();
            var participantsDTO = new List<ParticipantDTO>();

            foreach (var item in entities)
            {
                var employee = EService.CreateEmployeeDTO(item.Employee);
                var project = PService.CreateProjectDTO(item.Project);

                var participant = new ParticipantDTO
                {
                    Id = item.Id,
                    Employee = employee,
                    Project = project
                };
                participantsDTO.Add(participant);
            }
            return participantsDTO;
        }

        public ParticipantDTO Find(Guid Id)
        {
            var entity = Context.Participants.FirstOrDefault(e => e.Id == Id);
            var employee = EService.CreateEmployeeDTO(entity.Employee);
            var project = PService.CreateProjectDTO(entity.Project);

            var participant = new ParticipantDTO
            {
                Id = entity.Id,
                Employee = employee,
                Project = project
            };
            return participant;
        }

        public void Create(ParticipantDTO item)
        {
            var employeeEntity = Context.Employees.FirstOrDefault(e => e.Id == item.Employee.Id);
            var projectEntity = Context.Projects.FirstOrDefault(p => p.Id == item.Project.Id);

            var participant = new ProjectParticipantEntity
            {
                Employee = employeeEntity,
                Project = projectEntity
            };
            participant.FillFieldsOnCreate();
            Context.Participants.Add(participant);
            Context.SaveChanges();
        }

        public void Edit(ParticipantDTO item)
        {
            var entity = Context.Participants.FirstOrDefault(e => e.Id == item.Id);

            //entity.Employee = item.Employee;
            //entity.Project = item.Project;
            Context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var participant = Find(Id);
            var entity = Context.Participants.FirstOrDefault(e => e.Id == participant.Id);

            Context.Participants.Remove(entity);
            Context.SaveChanges();
        }
    }
}
