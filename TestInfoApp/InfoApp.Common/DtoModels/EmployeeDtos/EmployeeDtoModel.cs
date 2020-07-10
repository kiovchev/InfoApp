using System;

namespace InfoApp.Common.DtoModels.EmployeeDtos
{
    public class EmployeeDtoModel
    {
        public int EmploeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public string ExperienceLevel { get; set; }
    }
}
