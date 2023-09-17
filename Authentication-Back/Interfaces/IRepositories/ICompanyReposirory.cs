using AuthenticationOne.Models;

namespace AuthenticationOne.Interfaces.IRepositories
{
    public interface ICompanyReposirory
    {
        public Task<Company> CreateCompanyAsync(Company company);
        public Task<List<Company>> GetAllCompaniesAsync();
        public Task<Company> GetByIdAsync(long id);
    }
}
