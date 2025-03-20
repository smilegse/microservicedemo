using Discount.Grpc.Protos;

namespace Basket.API.GrpsServices
{
    public class DiscountGrpcService
    {
        public readonly DiscountProtoService.DiscountProtoServiceClient _discountServiceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountServiceClient)
        {
            _discountServiceClient = discountServiceClient;
        }

        public async Task<CouponRequest> GetDiscountAsync(string productId)
        {
            var getDiscountRequest = new GetDiscountRequest { ProductId = productId };
            return await _discountServiceClient.GetDiscountAsync(getDiscountRequest);
        }

    }   
}
