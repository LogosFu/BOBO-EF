using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    [Collection("Sequential")]
    public class CompanyServiceTest : TestBase
    {
        [Fact]
        public async void Should_return_2_company_with_2_employee_when_get_all()
        {
            //given
            InitDataBase();

            //when
            var companies = await CompanyService.GetAll();

            //then
            Assert.Equal(2, companies.Count);
            Assert.Equal("AAA", companies[0].Name);
            Assert.Equal("AAAA", companies[0].ProfileDto?.CertId);
            Assert.Equal(2, companies[0].EmployeeDtos?.Count);
        }

        [Fact]
        public async void Should_create_company_successfully_when_give_a_company()
        {
            //given
            var companyDto = GetACompanyDto();

            //when
            var companyId = await CompanyService.AddCompany(companyDto);

            //then
            var companyEntity = CompanyDbContext.Companies
                .Include(_ => _.Employees)
                .Include(_ => _.Profile)
                .FirstOrDefault(_ => _.Id == companyId);

            Assert.Equal("AAA", companyEntity?.Name);
            Assert.Equal("BB", companyEntity?.Profile?.CertId);
            Assert.Equal("AA", companyEntity?.Employees?[0].Name);
        }

        [Fact]
        public async void Should_get_company_successfully_when_get_by_id_given_a_right_id()
        {
            //given
            var companyDto = GetACompanyDto();

            //when
            await CompanyService.AddCompany(companyDto);

            //then
            var companyEntities = CompanyDbContext.Companies
                .Include(_ => _.Employees)
                .Include(_ => _.Profile)
                .ToList();

            Assert.Single(companyEntities);
            Assert.Equal("AAA", companyEntities[0].Name);
        }

        [Fact]
        public async void Should_delete_company_successfully_when_delete_by_id_given_a_right_id()
        {
            //given
            var companyDto = GetACompanyDto();
            var companyId = await CompanyService.AddCompany(companyDto);
            Assert.Single(CompanyDbContext.Companies.ToList());

            //when
            await CompanyService.DeleteCompany(companyId);

            //then
            Assert.Empty(CompanyDbContext.Companies.ToList());
            Assert.Empty(CompanyDbContext.Companies.ToList());
            Assert.Empty(CompanyDbContext.Companies.ToList());
        }


    }
}