using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }

        public EmployeeDto(EmployeeEntity employeeEntity)
        {
            if (employeeEntity != null)
            {
                Name = employeeEntity.Name;
                Age = employeeEntity.Age;
            }
        }

        public string Name { get; set; }
        public int Age { get; set; }

        public EmployeeEntity ToEntity()
        {
            return new EmployeeEntity()
            {
                Name = this.Name,
                Age = this.Age,
            };
        }
    }
}