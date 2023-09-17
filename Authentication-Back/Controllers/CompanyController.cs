using AuthenticationOne.DTOs;
using AuthenticationOne.Helper.Utils;
using AuthenticationOne.Interfaces.IService;
using AuthenticationOne.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyServices _companyServices;
        private readonly IMapper _mapper;
        public CompanyController(ICompanyServices companyServices,IMapper mapper)
        {
            _companyServices = companyServices;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CompanyResponce>>> GetAllCompanies()
        {
            var companies = await _companyServices.GetAllCompanies();
            var mappedCompanies = _mapper.Map<ICollection<CompanyResponce>>(companies);
            if (companies == null)
            {
                return NotFound();
            }
            return Ok(mappedCompanies);
        }
        // Create
        [HttpPost]
        [Route("Create")]
        [Authorize(Roles ="Company_Owner,Admin")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyRequest dto)
        {
            Company newCompany = _mapper.Map<Company>(dto);
            await _companyServices.CreateCompany(newCompany);
            return Ok("Companty Created Successfully");
        }

        // Read (Get Company By ID)
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CompanyResponce>> GetSingleCompany(long id)
        {
            var company = await _companyServices.GetById(id);
            var mapped_company = _mapper.Map<CompanyResponce>(company);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(mapped_company);
        }
    }
}
