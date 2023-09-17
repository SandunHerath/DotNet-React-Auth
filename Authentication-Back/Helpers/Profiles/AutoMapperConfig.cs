using AuthenticationOne.DTOs;
using AuthenticationOne.Models;
using AutoMapper;

namespace AuthenticationOne.Helper.Profiles
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Company
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponce>();
        }
    }
}
