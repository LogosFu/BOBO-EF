using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity? companyEntityProfile)
        {
            if (companyEntityProfile != null)
            {
                RegisteredCapital = companyEntityProfile.RegisteredCapital;
                CertId = companyEntityProfile.CertId;
            }
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }

        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = this.RegisteredCapital,
                CertId = this.CertId
            };
        }
    }
}