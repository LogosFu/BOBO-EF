using EFCoreRelationshipsPractice.Models;
using EFCoreRelationshipsPracticeTest.ServiceTest;
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
        public async void Should_return_2_company_with_2_employee_when_get_all()
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

            //when
            var companies = await CompanyService.GetAll();

            //then
            Assert.Equal(2, companies.Count);
            Assert.Equal("AAA", companies[0].Name);
            Assert.Equal("AAAA", companies[0].ProfileDto?.CertId);
            Assert.Equal(2, companies[0].EmployeeDtos?.Count);
        }
    }
}
