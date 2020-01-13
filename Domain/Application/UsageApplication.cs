using System;
using Domain.Usage;


namespace Domain.Application
{
    public class UsageApplication
    {
        private readonly IUsageRepository repository;

        public UsageApplication(IUsageRepository repository)
        {
            this.repository = repository;
        }

        public void 会議室の利用を開始する()
        {
            throw new NotImplementedException();
        }

        public string 会議室の現在の利用状況を確認する()
        {
            throw new NotImplementedException();
        }
    }
}