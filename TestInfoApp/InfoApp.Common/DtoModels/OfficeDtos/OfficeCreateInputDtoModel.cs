using System;
using System.Collections.Generic;
using System.Text;

namespace InfoApp.Common.DtoModels.OfficeDtos
{
    public class OfficeCreateInputDtoModel
    {
        public string OfficeName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string CityName { get; set; }

        public string StreetName { get; set; }

        public int StreetNumber { get; set; }

        public string Headquaters { get; set; }
    }
}
