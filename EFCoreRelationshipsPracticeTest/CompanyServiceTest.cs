using EFCoreRelationshipsPractice.Models;
using EFCoreRelationshipsPracticeTest.ServiceTest;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPracticeTest
{
    public class CompanyServiceTest : TestBase
    {
        [Fact]
        public async void Should_return_companies_with_employees_when_get_all()
        {
            CompanyDbContext.Companies.AddRange(new List<CompanyEntity>()
            {
                new CompanyEntity()
                {
                    Name = "AAA",Employees = GetInitEmployees(),
                    Profile = new ProfileEntity()
                    {
                        CertId = "AAAA", 
                        RegisteredCapital = 1000
                    }
                },
                new CompanyEntity()
                {
                    Name = "BBB",
                    Employees = GetInitEmployees(), 
                    Profile = new ProfileEntity()
                    {
                        CertId = "GooglBBBBeCert", 
                        RegisteredCapital = 2000
                    }
                }
            });
            CompanyDbContext.SaveChanges();
            var companies = await CompanyService.GetAll();
            Assert.Equal(2, companies.Count);
        }

        [Fact]
        public async void Should_create_company_successfully_when_give_a_company()
        {
            var companyDto = GetACompanyDto();
            var companyId = await CompanyService.AddCompany(companyDto);
            var companyEntity = CompanyDbContext.Companies
                .Include(_ => _.Employees)
                .Include(_ => _.Profile)
                .FirstOrDefault(_ => _.Id == companyId);

            Assert.Equal("AAA", companyEntity.Name);
        }

    }
}
