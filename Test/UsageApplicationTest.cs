using System;
using Xunit;
using Domain.Reserves;
using Domain.Usages;
using Domain.Application;
using Domain.Repository;
using Moq;
using InMemoryInfrastructure;


namespace Test
{
    public class UsageApplicationTest
    {
        private readonly IUsageRepository repository;
        private readonly UsageApplication application;
        public UsageApplicationTest()
        {
            repository = new InMemoryUsageRepository();
            application = new UsageApplication(repository);
        }

        [Fact]
        public void 会議室を利用中にすることができること()
        {
            application.会議室の利用を開始する("A");

            bool flag = application.会議室が利用中かどうか確認する("A");

            Assert.True(flag);
        }
    }
}

