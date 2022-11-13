using AutoMapper;
using Discount.GRPC.Protos;
using Discount.GRPC.Repositories;
using Grpc.Core;

namespace Discount.GRPC.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository repository;
    private readonly IMapper mapper;
    private readonly ILogger<DiscountService> logger;

    public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await repository.GetDiscount(request.ProductName);

        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
        }

        var couponModel = mapper.Map<CouponModel>(coupon);
        return couponModel;
    }
}
