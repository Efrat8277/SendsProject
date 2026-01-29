using AutoMapper;
using SendsProject.Core.DTO;
using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core
{
    public class MappingProfile:Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Package,PackageDTO>().ReverseMap();
            CreateMap<Recipient,RecipientDTO>().ReverseMap();
            CreateMap<DeliveryPerson,DeliveryPersonDTO>().ReverseMap();
        }

    }
}
