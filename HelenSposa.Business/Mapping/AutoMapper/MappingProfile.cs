﻿using AutoMapper;
using HelenSposa.Entities.Concrete;
using HelenSposa.Entities.Dtos;
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

            CreateMap<ExpenseTypeGeneralDto, ExpenseType>();
            CreateMap<ExpenseType, ExpenseTypeGeneralDto>();

        }
    }
}