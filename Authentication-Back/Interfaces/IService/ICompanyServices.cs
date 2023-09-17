using AuthenticationOne.Models;

namespace AuthenticationOne.Interfaces.IService
{

    public interface ICompanyServices
    {
        public Task<Company> CreateCompany(Company company);
        public Task<List<Company>> GetAllCompanies();
        public Task<Company> GetById(long id);
    }
}
