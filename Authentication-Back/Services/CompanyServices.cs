using AuthenticationOne.Interfaces.IRepositories;
using AuthenticationOne.Interfaces.IService;
using AuthenticationOne.Models;

namespace AuthenticationOne.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly ICompanyReposirory _companyReposirory;

        public CompanyServices(ICompanyReposirory companyReposirory)
        {
            _companyReposirory = companyReposirory;
        }
        public async Task<Company> CreateCompany(Company company)
        {
            Company resultCompany = await _companyReposirory.CreateCompanyAsync(company);
            return resultCompany;

        }

        public async Task<List<Company>> GetAllCompanies()
        {
            var companies = await _companyReposirory.GetAllCompaniesAsync();
            return companies;
        }

        public async Task<Company> GetById(long id)
        {
            var company = await _companyReposirory.GetByIdAsync(id);
            return company;

        }
    }
}
