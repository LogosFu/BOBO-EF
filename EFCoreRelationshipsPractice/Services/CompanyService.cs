using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Model;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationshipsPractice.Services
{
    public class CompanyService
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyService(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            var companies = companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees)
                .ToList();
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(int id)
        {
            var company = companyDbContext.Companies.FirstOrDefault(company => company.Id == id);
            return new CompanyDto(company);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            // 1. convert dto to entity
            CompanyEntity companyEntity = companyDto.ToEntity();

            // 2. save entity to db
            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();

            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var company = companyDbContext.Companies
                .Include(_ => _.Employees)
                .Include(_ => _.Profile)
                .FirstOrDefault(company => company.Id == id);
            company.Employees.Clear();
            company.Profile = null;
            _ = companyDbContext.Companies.Remove(company);
            await companyDbContext.SaveChangesAsync();
        }
    }
}