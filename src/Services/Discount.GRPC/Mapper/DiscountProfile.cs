using AutoMapper;
using Discount.GRPC.Entities;
using Discount.GRPC.Protos;

namespace Discount.GRPC.Mapper;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<Coupon, CouponModel>().ReverseMap();
    }
}
