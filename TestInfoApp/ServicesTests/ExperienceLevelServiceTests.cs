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
    public class ExperienceLevelServiceTests
    {
        [Fact]
        public async Task GetAllLevelsShouldReturnThree()
        {
            var data = new List<ExperienceLevel>
            {
                new ExperienceLevel
                {
                    ExperienceLevelId = 1,
                    ExperienceLevelName = "junior"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 2,
                    ExperienceLevelName = "mid"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 3,
                    ExperienceLevelName = "senior"
                }
            } as IEnumerable<ExperienceLevel>;

            var repo = new Mock<IRepository<ExperienceLevel>>();
            repo.Setup(r => r.GetAllAsync()).Returns(Task.FromResult(data));

            var service = new ExperienceLevelService(repo.Object);

            var levels = await service.GetAllLevels();
            var expected = levels.Count;
            var actual = 3;

            Assert.Equal(actual, expected);
        }

        [Fact]
        public void IfExistsShouldReturnTrue()
        {
            var data = new List<ExperienceLevel>
            {
                new ExperienceLevel
                {
                    ExperienceLevelId = 1,
                    ExperienceLevelName = "junior"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 2,
                    ExperienceLevelName = "mid"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 3,
                    ExperienceLevelName = "senior"
                }
            }.AsQueryable<ExperienceLevel>();

            var repo = new Mock<IRepository<ExperienceLevel>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new ExperienceLevelService(repo.Object);

            bool exists = service.IfExists("junior");

            Assert.True(exists);
        }

        [Fact]
        public void IfExistsShouldReturnFalse()
        {
            var data = new List<ExperienceLevel>
            {
                new ExperienceLevel
                {
                    ExperienceLevelId = 1,
                    ExperienceLevelName = "junior"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 2,
                    ExperienceLevelName = "mid"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 3,
                    ExperienceLevelName = "senior"
                }
            }.AsQueryable<ExperienceLevel>();

            var repo = new Mock<IRepository<ExperienceLevel>>();
            repo.Setup(x => x.AllAsNoTracking()).Returns(data);

            var service = new ExperienceLevelService(repo.Object);

            bool exists = service.IfExists("consultant");

            Assert.False(exists);
        }

        [Fact]
        public async Task GetLevelByIdShouldReturnLevelEditInputDto()
        {
            var data = new List<ExperienceLevel>
            {
                new ExperienceLevel
                {
                    ExperienceLevelId = 1,
                    ExperienceLevelName = "junior"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 2,
                    ExperienceLevelName = "mid"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 3,
                    ExperienceLevelName = "senior"
                }
            }.AsQueryable<ExperienceLevel>();

            var id = 1;

            var repo = new Mock<IRepository<ExperienceLevel>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.ExperienceLevelId == id)));

            var service = new ExperienceLevelService(repo.Object);

            var level = await service.GetLevelById(1);

            Assert.NotNull(level);
        }

        [Fact]
        public async Task GetLevelByIdShouldReturnNull()
        {
            var data = new List<ExperienceLevel>
            {
                new ExperienceLevel
                {
                    ExperienceLevelId = 1,
                    ExperienceLevelName = "junior"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 2,
                    ExperienceLevelName = "mid"
                },
                new ExperienceLevel
                {
                    ExperienceLevelId = 3,
                    ExperienceLevelName = "senior"
                }
            }.AsQueryable<ExperienceLevel>();

            var id = 4;

            var repo = new Mock<IRepository<ExperienceLevel>>();
            repo.Setup(x => x.GetByIdAsync(id)).Returns(Task.FromResult(data.FirstOrDefault(x => x.ExperienceLevelId == id)));

            var service = new ExperienceLevelService(repo.Object);

            var level = await service.GetLevelById(4);

            Assert.Null(level);
        }
    }
}
 