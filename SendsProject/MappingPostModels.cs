using AutoMapper;
using SendsProject.Core.Models.Classes;
using SendsProject.Models;

namespace SendsProject
{
    public class MappingPostModels : Profile
    {
        public MappingPostModels() 
        {
            CreateMap<PackagePostModel, Package>();
            CreateMap<RecipientPostModel , Recipient>();   
            CreateMap<DeliveryPersonPostModel, DeliveryPerson>();
        }
    }
}
