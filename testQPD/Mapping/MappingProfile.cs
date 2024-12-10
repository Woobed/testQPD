using AutoMapper;
using testQPD.DaData;
using testQPD.Models;

namespace testQPD.Mapping
{
    public class MappingProfile :Profile 
    {

        public MappingProfile() {
            CreateMap<AddressResponse, AddressModel>();
        }
    }
}
