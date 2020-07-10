using System;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels
{
    public class EmployeeEditOutputModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        public int CompanyId { get; set; }

        public string LevelName { get; set; }

        public string OfficeName { get; set; }
    }
}
