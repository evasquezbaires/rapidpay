using AutoMapper;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Models;

namespace RapidPay.API.Domain.Mapper
{
    public class CreditCardMapper : Profile
    {
        public CreditCardMapper()
        {
            CreateMap<CreditCardModel, CreditCard>()
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => decimal.Round(s.TotalAmount, 2)))
                .ForMember(d => d.BalanceAmount, o => o.MapFrom(s => decimal.Round(s.TotalAmount, 2)))
                .ForMember(d => d.CardHolder, o => o.MapFrom(s => s.CardHolder.ToUpper()));

            CreateMap<CreditCard, CreditCardModel>();
        }
    }
}
