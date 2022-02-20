using AutoMapper;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Models;

namespace RapidPay.API.Domain.Mapper
{
    public class UserIdentityMapper : Profile
    {
        public UserIdentityMapper()
        {
            CreateMap<UserModel, UserIdentity>();

            CreateMap<UserIdentity, UserModel>();
        }
    }
}
