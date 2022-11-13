using Discount.GRPC.Protos;

namespace Basket.API.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient discountProtoService;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
    {
        this.discountProtoService = discountProtoService;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest
        {
            ProductName = productName
        };

        return await discountProtoService.GetDiscountAsync(discountRequest);
    }
}
