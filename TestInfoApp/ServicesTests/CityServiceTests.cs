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
    public class CityServiceTests
    {
        [Fact]
        public async Task GetAllCitiesShouldReturnThree()
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

            var data = new List<City>
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

            var repo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CityService(repo.Object, countryRepo.Object);

            var levels = await service.GetAllCities();
            var expected = levels.Count;
            var actual = 3;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task GetAllCitiesByCountryShouldReturnThree()
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

            var data = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   CountryId = 1,
                   Country = country
                }
            } as IEnumerable<City>;

            var countryId = 1;

            var repo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CityService(repo.Object, countryRepo.Object);

            var levels = await service.GetAllCitiesByCountry(countryId);
            var expected = levels.Count;
            var actual = 3;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public async Task IfExistsShouldReturnTrue()
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

            var data = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   CountryId = 1,
                   Country = country
                }
            } as IEnumerable<City>;

            var cityName = "Stara Zagora";

            var repo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CityService(repo.Object, countryRepo.Object);

            var exists = await service.IfExists(cityName);

            Assert.True(exists);
        }

        [Fact]
        public async Task IfExistsShouldReturnFalse()
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

            var data = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   CountryId = 1,
                   Country = country
                }
            } as IEnumerable<City>;

            var cityName = "Russe";

            var repo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CityService(repo.Object, countryRepo.Object);

            var exists = await service.IfExists(cityName);

            Assert.False(exists);
        }

        [Fact]
        public async Task CountAsyncShouldReturnThree()
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

            var data = new List<City>
            {
                new City
                {
                   CityId = 1,
                   Name = "Sofia",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 2,
                   Name = "Plovdiv",
                   CountryId = 1,
                   Country = country
                },
                new City
                {
                   CityId = 3,
                   Name = "Stara Zagora",
                   CountryId = 1,
                   Country = country
                }
            } as IEnumerable<City>;

            var countryId = 1;

            var repo = new Mock<IRepository<City>>();
            var countryRepo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CityService(repo.Object, countryRepo.Object);

            var actual = await service.CountAsync(countryId);
            var expected = 3;

            Assert.Equal(expected, actual);
        }
    }
}
