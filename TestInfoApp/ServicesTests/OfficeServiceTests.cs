using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class OfficeServiceTests
    {
        [Fact]
        public async Task GetAllOfficesShouldReturnTwo()
        {
            var countryData = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    CountryName = "Bulgaria"
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Germany"
                },
                new Country
                {
                    CountryId = 3,
                    CountryName = "UK"
                }
            } as IEnumerable<Country>;

            var country = countryData.ToList()[0];

            var cityData = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   Country = country
                }
            } as IEnumerable<City>;

            var city = cityData.ToList()[0];

            var companyData = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1"
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2"
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3"
                }
            } as IEnumerable<Company>;

            var company = companyData.ToList()[0];

            var officeData = new List<Office>
            {
                new Office
                {
                    OfficeId = 1,
                    OfficeName = "Office 1",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 1",
                    StreetNumber = 1,
                    Headquarters = true
                },
                 new Office
                {
                    OfficeId = 2,
                    OfficeName = "Office 2",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 2",
                    StreetNumber = 1,
                    Headquarters = true
                }
            } as IEnumerable<Office>;

            var cityRepo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            var companyRepo = new Mock<IRepository<Company>>();
            var repo = new Mock<IRepository<Office>>();

            repo.Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult(officeData));
            countryRepo.Setup(x => x.GetByIdAsync(country.CountryId))
                .Returns(Task.FromResult(countryData.FirstOrDefault(x => x.CountryId == country.CountryId)));
            cityRepo.Setup(x => x.GetByIdAsync(city.CityId))
                .Returns(Task.FromResult(cityData.FirstOrDefault(x => x.CityId == city.CityId)));
            companyRepo.Setup(x => x.GetByIdAsync(company.CompanyId))
                .Returns(Task.FromResult(companyData.FirstOrDefault(x => x.CompanyId == company.CompanyId)));

            var service = new OfficeService(repo.Object, 
                                            countryRepo.Object, 
                                            cityRepo.Object, 
                                            companyRepo.Object);


            var offices = await service.GetAllOffices(company.CompanyId);
            var expected = offices.Count;
            var actual = 2;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task GetOfficeByIdShouldReturnOfficeEditOutputDto()
        {
            var countryData = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    CountryName = "Bulgaria"
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Germany"
                },
                new Country
                {
                    CountryId = 3,
                    CountryName = "UK"
                }
            } as IEnumerable<Country>;

            var country = countryData.ToList()[0];

            var cityData = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   Country = country
                }
            } as IEnumerable<City>;

            var city = cityData.ToList()[0];

            var companyData = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1"
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2"
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3"
                }
            } as IEnumerable<Company>;

            var company = companyData.ToList()[0];

            var officeData = new List<Office>
            {
                new Office
                {
                    OfficeId = 1,
                    OfficeName = "Office 1",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 1",
                    StreetNumber = 1,
                    Headquarters = true
                },
                 new Office
                {
                    OfficeId = 2,
                    OfficeName = "Office 2",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 2",
                    StreetNumber = 1,
                    Headquarters = true
                }
            } as IEnumerable<Office>;

            var office = officeData.ToList()[0];

            var cityRepo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            var companyRepo = new Mock<IRepository<Company>>();
            var repo = new Mock<IRepository<Office>>();

            repo.Setup(r => r.GetByIdAsync(office.OfficeId))
                .Returns(Task.FromResult(officeData.FirstOrDefault(x => x.OfficeId == office.OfficeId)));
            cityRepo.Setup(x => x.GetByIdAsync(office.CityId))
                .Returns(Task.FromResult(cityData.FirstOrDefault(x => x.CityId == office.CityId)));
            companyRepo.Setup(x => x.GetByIdAsync(office.CompanyId))
                .Returns(Task.FromResult(companyData.FirstOrDefault(x => x.CompanyId == office.CompanyId)));

            var service = new OfficeService(repo.Object,
                                            countryRepo.Object,
                                            cityRepo.Object,
                                            companyRepo.Object);


            var currentOffice = await service.GetOfficeById(office.OfficeId);
            var expected = currentOffice.GetType();
            var actual = typeof(OfficeEditOutputDto);

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task CountAsyncShouldReturnTwo()
        {
            var countryData = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    CountryName = "Bulgaria"
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Germany"
                },
                new Country
                {
                    CountryId = 3,
                    CountryName = "UK"
                }
            } as IEnumerable<Country>;

            var country = countryData.ToList()[0];

            var cityData = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   Country = country
                }
            } as IEnumerable<City>;

            var city = cityData.ToList()[0];

            var companyData = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1"
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2"
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3"
                }
            } as IEnumerable<Company>;

            var company = companyData.ToList()[0];

            var officeData = new List<Office>
            {
                new Office
                {
                    OfficeId = 1,
                    OfficeName = "Office 1",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 1",
                    StreetNumber = 1,
                    Headquarters = true
                },
                 new Office
                {
                    OfficeId = 2,
                    OfficeName = "Office 2",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 2",
                    StreetNumber = 1,
                    Headquarters = true
                }
            } as IEnumerable<Office>;

            var office = officeData.ToList()[0];

            var cityRepo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            var companyRepo = new Mock<IRepository<Company>>();
            var repo = new Mock<IRepository<Office>>();

            repo.Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult(officeData));

            var service = new OfficeService(repo.Object,
                                            countryRepo.Object,
                                            cityRepo.Object,
                                            companyRepo.Object);


            var actual = await service.CountAsync(city.CityId);
            var expected = 2;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task CountAsyncByCompanyIdShouldReturnTwo()
        {
            var countryData = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    CountryName = "Bulgaria"
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Germany"
                },
                new Country
                {
                    CountryId = 3,
                    CountryName = "UK"
                }
            } as IEnumerable<Country>;

            var country = countryData.ToList()[0];

            var cityData = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   Country = country
                }
            } as IEnumerable<City>;

            var city = cityData.ToList()[0];

            var companyData = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1"
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2"
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3"
                }
            } as IEnumerable<Company>;

            var company = companyData.ToList()[0];

            var officeData = new List<Office>
            {
                new Office
                {
                    OfficeId = 1,
                    OfficeName = "Office 1",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 1",
                    StreetNumber = 1,
                    Headquarters = true
                },
                 new Office
                {
                    OfficeId = 2,
                    OfficeName = "Office 2",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 2",
                    StreetNumber = 1,
                    Headquarters = true
                }
            } as IEnumerable<Office>;

            var office = officeData.ToList()[0];

            var cityRepo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            var companyRepo = new Mock<IRepository<Company>>();
            var repo = new Mock<IRepository<Office>>();

            repo.Setup(r => r.GetAllAsync())
                .Returns(Task.FromResult(officeData));

            var service = new OfficeService(repo.Object,
                                            countryRepo.Object,
                                            cityRepo.Object,
                                            companyRepo.Object);


            var actual = await service.CountAsyncByCompanyId(company.CompanyId);
            var expected = 2;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void GetCompanyIdByOfficeIdShouldReturnCompanyId()
        {
            var countryData = new List<Country>
            {
                new Country
                {
                    CountryId = 1,
                    CountryName = "Bulgaria"
                },
                new Country
                {
                    CountryId = 2,
                    CountryName = "Germany"
                },
                new Country
                {
                    CountryId = 3,
                    CountryName = "UK"
                }
            } as IEnumerable<Country>;

            var country = countryData.ToList()[0];

            var cityData = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   Country = country
                }
            } as IEnumerable<City>;

            var city = cityData.ToList()[0];

            var companyData = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1"
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2"
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3"
                }
            } as IEnumerable<Company>;

            var company = companyData.ToList()[0];

            var officeData = new List<Office>
            {
                new Office
                {
                    OfficeId = 1,
                    OfficeName = "Office 1",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 1",
                    StreetNumber = 1,
                    Headquarters = true
                },
                 new Office
                {
                    OfficeId = 2,
                    OfficeName = "Office 2",
                    CompanyId = 1,
                    Company = company,
                    CountryId = 1,
                    Country = country,
                    CityId = city.CityId,
                    City = city,
                    Street = "Street 2",
                    StreetNumber = 1,
                    Headquarters = true
                }
            } as IEnumerable<Office>;

            var office = officeData.ToList()[0];

            var cityRepo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            var companyRepo = new Mock<IRepository<Company>>();
            var repo = new Mock<IRepository<Office>>();

            repo.Setup(r => r.AllAsNoTracking()).Returns(officeData.AsQueryable());

            var service = new OfficeService(repo.Object,
                                            countryRepo.Object,
                                            cityRepo.Object,
                                            companyRepo.Object);


            var actual = service.GetCompanyIdByOfficeId(office.OfficeId);
            var expected = 1;

            Assert.Equal(actual, expected);
        }
    }
}
