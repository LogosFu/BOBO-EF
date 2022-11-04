using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Models
{
    public class CompanyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProfileEntity? Profile { get; set; }
        public List<EmployeeEntity>? Employees { get; set; }
    }
}