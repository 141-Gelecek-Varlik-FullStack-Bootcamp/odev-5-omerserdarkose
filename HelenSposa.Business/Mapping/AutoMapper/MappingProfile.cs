using AutoMapper;
using HelenSposa.Core.Entities.Concrete;
using HelenSposa.Entities.Concrete;
using HelenSposa.Entities.Dtos.Customer;
using HelenSposa.Entities.Dtos.ExpenseType;
using HelenSposa.Entities.Dtos.Claim;
using HelenSposa.Entities.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelenSposa.Business.Mapping.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerAddDto, Customer>();
            CreateMap<Customer, CustomerAddDto>();
            
            CreateMap<CustomerUpdateDto, Customer>();
            CreateMap<Customer, CustomerUpdateDto>();

            CreateMap<CustomerDeleteDto, Customer>();
            CreateMap<Customer, CustomerDeleteDto>();

            CreateMap<CustomerShowDto, Customer>();
            CreateMap<Customer, CustomerShowDto>();

            CreateMap<ExpenseTypeShowDto, ExpenseType>();
            CreateMap<ExpenseType, ExpenseTypeShowDto>();

            CreateMap<ExpenseTypeAddDto, ExpenseType>();
            CreateMap<ExpenseType, ExpenseTypeAddDto>();

            CreateMap<ExpenseTypeUpdateDto, ExpenseType>();
            CreateMap<ExpenseType, ExpenseTypeUpdateDto>();

            CreateMap<UserAddDto, User>();
            CreateMap<User, UserShowDto>();

            CreateMap<ClaimShowDto, Claim>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClaimName));


        }
    }
}
