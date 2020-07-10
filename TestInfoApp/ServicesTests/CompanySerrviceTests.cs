using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ServicesTests
{
    public class CompanySerrviceTests
    {
        [Fact]
        public async Task GetAllCompaniesShouldReturnThree()
        {
            var data = new List<Company>
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

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new CompanyService(repo.Object);

            var levels = await service.GetAllCompanies();
            var expected = levels.Count;
            var actual = 3;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IfExistsShouldReturnTrue()
        {
            var data = new List<Company>
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
            }.AsQueryable<Company>();

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CompanyService(repo.Object);

            bool exists = service.IfExists("Test1");

            Assert.True(exists);
        }

        [Fact]
        public void IfExistsShouldReturnFalse()
        {
            var data = new List<Company>
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
            }.AsQueryable<Company>();

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CompanyService(repo.Object);

            bool exists = service.IfExists("Test4");

            Assert.False(exists);
        }

        [Fact]
        public async Task GetCompanyByIdShouldReturnCompanyEditInputDto()
        {
            var data = new List<Company>
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
            }.AsQueryable<Company>();

            var id = 1;

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.CompanyId == id)));

            var service = new CompanyService(repo.Object);

            var level = await service.GetCompanyById(1);

            Assert.NotNull(level);
        }

        [Fact]
        public async Task GetCompanyByIdShouldReturnNull()
        {
            var data = new List<Company>
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
            }.AsQueryable<Company>();

            var id = 4;

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.CompanyId == id)));

            var service = new CompanyService(repo.Object);

            var level = await service.GetCompanyById(4);

            Assert.Null(level);
        }

        [Fact]
        public void IsSameShouldReturnTrue()
        {
            var creationAt = DateTime.UtcNow.AddDays(-1);

            var data = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1",
                    Creationdate = creationAt
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2",
                    Creationdate = creationAt
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3",
                    Creationdate = creationAt
                }
            }.AsQueryable<Company>();

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CompanyService(repo.Object);

            bool exists = service.IsSame("Test1", creationAt);

            Assert.True(exists);
        }

        [Fact]
        public void IsSameShouldReturnFalse()
        {
            var creationAt = DateTime.UtcNow.AddDays(-1);

            var data = new List<Company>
            {
                new Company
                {
                    CompanyId = 1,
                    CompanyName = "Test1",
                    Creationdate = creationAt
                },
                 new Company
                {
                    CompanyId = 2,
                    CompanyName = "Test2",
                    Creationdate = creationAt
                },
                 new Company
                {
                    CompanyId = 3,
                    CompanyName = "Test3",
                    Creationdate = creationAt
                }
            }.AsQueryable<Company>();

            var repo = new Mock<IRepository<Company>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new CompanyService(repo.Object);

            bool exists = service.IsSame("Test4", creationAt);

            Assert.False(exists);
        }
    }
}
