using AuthenticationOne.DBContext;
using AuthenticationOne.Interfaces.IRepositories;
using AuthenticationOne.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationOne.Repositories
{
    public class CompanyRepository : ICompanyReposirory
    {
        private readonly AppDBContext _context;
        public CompanyRepository(AppDBContext dBContext)
        {
            _context = dBContext;
        }
        public async Task<Company> CreateCompanyAsync(Company company)
        {
            _context.companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<List<Company>> GetAllCompaniesAsync()
        {
            var companies = await _context.companies.OrderByDescending(q => q.CreatedAt).ToListAsync();
            return (companies!);
        }

        public async Task<Company> GetByIdAsync(long id)
        {
            var company = await _context.companies.FindAsync(id);
            return (company!);
        }
    }
}
