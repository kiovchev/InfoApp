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
    public  class CountryServiceTests
    {
        [Fact]
        public async Task GetAllCountriesShouldReturnThree()
        {
            var data = new List<Country>
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

            var repo = new Mock<IRepository<Country>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CountryService(repo.Object);

            var levels = await service.GetAllCountries();
            var expected = levels.Count;
            var actual = 3;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IfExistsShouldReturnTrue()
        {
            var data = new List<Country>
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
            }.AsQueryable<Country>();

            var repo = new Mock<IRepository<Country>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CountryService(repo.Object);

            bool exists = service.IfExists("Bulgaria");

            Assert.True(exists);
        }

        [Fact]
        public void IfExistsShouldReturnFalse()
        {
            var data = new List<Country>
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
            }.AsQueryable<Country>();

            var repo = new Mock<IRepository<Country>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CountryService(repo.Object);

            bool exists = service.IfExists("France");

            Assert.False(exists);
        }

        [Fact]
        public async Task GetCountryByIdShouldReturnCountryEditInputDto()
        {
            var data = new List<Country>
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
            }.AsQueryable<Country>();

            var id = 1;

            var repo = new Mock<IRepository<Country>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.CountryId == id)));

            var service = new CountryService(repo.Object);

            var level = await service.GetCountryById(1);

            Assert.NotNull(level);
        }

        [Fact]
        public async Task GetCountryByIdShouldReturnNull()
        {
            var data = new List<Country>
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
            }.AsQueryable<Country>();

            var id = 4;

            var repo = new Mock<IRepository<Country>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.CountryId == id)));

            var service = new CountryService(repo.Object);

            var level = await service.GetCountryById(4);

            Assert.Null(level);
        }
    }
}
