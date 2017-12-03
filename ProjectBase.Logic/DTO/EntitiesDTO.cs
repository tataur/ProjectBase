using ProjectBase.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectBase.Logic.DTO
{
    public class EntitiesDTO
    {
        private static readonly EFProjectBaseContext Context = new EFProjectBaseContext();

        public EmployeeDTO Employee { get; set; }
        public List<EmployeeDTO> Employees { get; set; }

        public CompanyDTO Company { get; set; }
        public List<CompanyDTO> Companies { get; set; }

        public ProjectDTO Project { get; set; }
        public List<ProjectDTO> Projects { get; set; }

        public WorkerDTO Worker { get; set; }
        public List<WorkerDTO> Workers { get; set; }
    }

    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите email")]
        public string Email { get; set; }
        public bool IsChief { get; set; }
    }

    public class CompanyDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class ProjectDTO
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Выберите заказчика")]
        public CompanyDTO CompanyCustomer { get; set; }

        [Required(ErrorMessage = "Выберите исполнителя")]
        public CompanyDTO CompanyPerformer { get; set; }

        [Required(ErrorMessage = "Выберите руководителя")]
        public EmployeeDTO ProjectChief { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }

        [Required(ErrorMessage = "Задайте приоритет")]
        public int Priority { get; set; }
        public string Comment { get; set; }
    }

    public class WorkerDTO
    {
        public Guid Id { get; set; }
        public EmployeeDTO Employee { get; set; }
        public ProjectDTO Project { get; set; }
    }
}
