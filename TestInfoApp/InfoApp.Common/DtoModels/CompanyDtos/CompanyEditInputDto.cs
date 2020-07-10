using System;

namespace InfoApp.Common.DtoModels.CompanyDtos
{
    public class CompanyEditInputDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public DateTime Creationdate { get; set; }
    }
}
