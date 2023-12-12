using AutoMapper;
using StoreProgram_.DTO.Requests;
using StoreProgram_.DTO.Responses;
using StoreProgram_.Model;

namespace StoreProgram_.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMapperClient();
            CreateMapperBasket();
        }

        private void CreateMapperClient()
        {
            CreateMap<Client, ClientResponse>();
            CreateMap<ClientResponse, Client>();
            CreateMap<ClientRequest, Client>();
            CreateMap<Client, ClientRequest>();

        }

        private void CreateMapperBasket()
        {
            CreateMap<Basket, BasketWithClientInfoResponse>()
            .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.Client.ClientID))
            .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.ClientName))
            .ForMember(dest => dest.NumberPhone, opt => opt.MapFrom(src => src.Client.NumberPhone));
            CreateMap<BasketWithClientInfoResponse, Basket>();
            CreateMap<Basket, BasketResponse>();
            CreateMap<BasketResponse, Basket>();
            CreateMap<BasketRequestcs, Basket>();
            CreateMap<Basket, BasketRequestcs>();
        }
    }
}
