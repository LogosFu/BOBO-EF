using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            this.Name = companyEntity.Name;
            this.ProfileDto = new ProfileDto(companyEntity.Profile);
            this.EmployeeDtos = companyEntity.Employees.Select(_ => new EmployeeDto(_)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDto { get; set; }

        public List<EmployeeDto>? EmployeeDtos { get; set; }

        public CompanyEntity ToEntity()
        {
            return new CompanyEntity()
            {
                Name = this.Name,
                Profile = ProfileDto?.ToEntity(),
                Employees = EmployeeDtos?.Select(_ => _.ToEntity()).ToList()
            };
        }
    }
}