using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Tests
{
    public class ExperienceLevelServiceTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task GetAllExperienceLevelsShouldReturnThree()
        {
            IEnumerable<ExperienceLevel> levels = new List<ExperienceLevel>
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
            };

            var mock = new Mock<IRepository<ExperienceLevel>>();
            mock.Setup(x => x.GetAllAsync()).Returns((Task<IEnumerable<ExperienceLevel>>)levels);
            var repository = mock.Object;
            var levelService = new ExperienceLevelService(repository);
            //mock.Setup(x => x.GetAllAsync().GetAwaiter().GetResult()).Returns(levels);



            var actualresult = await levelService.GetAllLevels();
            var count = actualresult.Count();

            var expected = 3;

            Assert.AreEqual(expected, count);
        }
    }
}