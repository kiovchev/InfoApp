using System;
using System.Collections.Generic;
using System.Text;

namespace InfoApp.Common.DtoModels.OfficeDtos
{
    public class OfficeDtoModel
    {
        public int OfficeId { get; set; }

        public string OfficeName { get; set; }

        public string CompanyName { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public string Street { get; set; }

        public int StreetNumber { get; set; }

        public string Headquarters { get; set; }
    }
}
