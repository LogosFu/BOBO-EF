using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Models;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class TestBase : IDisposable
    {
        internal CompanyService CompanyService;
        internal CompanyDbContext CompanyDbContext;
        public TestBase()
        {
            var options = new DbContextOptionsBuilder<CompanyDbContext>()
                .UseInMemoryDatabase(databaseName: "DB")
                .Options;

            CompanyDbContext = new CompanyDbContext(options);
            CompanyService = new CompanyService(CompanyDbContext);
        }

        internal void InitDataBase()
        {
            CompanyDbContext.Companies.AddRange(new List<CompanyEntity>()
            {
                new CompanyEntity(){Name = "AAA",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "AAAA", RegisteredCapital = 1000}},
                new CompanyEntity(){Name = "BBB",Employees = GetInitEmployees(), Profile = new ProfileEntity(){CertId = "GooglBBBBeCert", RegisteredCapital = 2000}}
            });
            CompanyDbContext.SaveChanges();
        }

        internal CompanyDto GetACompanyDto()
        {
            return new CompanyDto()
            {
                Name = "AAA",
                EmployeeDtos = new List<EmployeeDto>() { new EmployeeDto() { Age = 18, Name = "AA" } },
                ProfileDto = new ProfileDto() { CertId = "BB", RegisteredCapital = 1000 }
            };
        }

        internal List<EmployeeEntity> GetInitEmployees()
        {
            return new List<EmployeeEntity>()
            {
                new EmployeeEntity() { Age = 20, Name = "AAB" },
                new EmployeeEntity() { Age = 30, Name = "BBA" }
            };
        }

        public void Dispose()
        {
            CompanyDbContext.RemoveRange(CompanyDbContext.Employees);
            CompanyDbContext.RemoveRange(CompanyDbContext.Companies);
            CompanyDbContext.RemoveRange(CompanyDbContext.Profiles);
            CompanyDbContext.SaveChanges();
        }
    }
}