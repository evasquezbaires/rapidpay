using AutoMapper;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Models;

namespace RapidPay.API.Domain.Mapper
{
    public class PaymentCardMapper : Profile
    {
        public PaymentCardMapper()
        {
            CreateMap<PaymentCardModel, PaymentCard>()
                .ForMember(d => d.CreditCardId, o => o.MapFrom(s => s.CardId));

            CreateMap<PaymentCard, PaymentCardModel>();
        }
    }
}
