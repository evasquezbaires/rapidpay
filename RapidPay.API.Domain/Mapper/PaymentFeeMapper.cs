using AutoMapper;
using RapidPay.API.Domain.Entities;
using RapidPay.API.Domain.Models;

namespace RapidPay.API.Domain.Mapper
{
    public class PaymentFeeMapper : Profile
    {
        public PaymentFeeMapper()
        {
            CreateMap<PaymentFeeModel, FeeHistory>();

            CreateMap<FeeHistory, PaymentFeeModel>();
        }
    }
}
