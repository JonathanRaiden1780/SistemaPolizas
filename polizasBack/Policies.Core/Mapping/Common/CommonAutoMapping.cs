using AutoMapper;
using Policies.Core.Dtos.Policy.Request;
using Policies.Core.Dtos.Policy.Response;

namespace Policies.Core.Mapping.Common
{
    public class CommonAutoMapping : Profile
    {
        public CommonAutoMapping()
        {
           
            CreateMap<CreateClientRequestDto, UpdateClientRequestDto>().ReverseMap();
            CreateMap<ClientResponseDto, UpdateClientRequestDto>().ReverseMap();

        }
    }
}
