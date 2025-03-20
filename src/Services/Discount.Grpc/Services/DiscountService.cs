using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _repository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;
        public DiscountService(ICouponRepository couponRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetCoupon(request.ProductId);
            if (coupon == null)
            {
                _logger.LogError("Discount not found. ProductId={ProductId}", request.ProductId);
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.ProductId} is not found."));
            }
            _logger.LogInformation("Discount is retrieved for ProductId : {ProductId}, ProductName: {ProductName}, Amount : {Amount}", coupon.ProductId, coupon.ProductName, coupon.Amount);
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> CreateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isSaved = await _repository.CreateDiscount(coupon);
            if(isSaved)
            {
                _logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", request.ProductName);
            }
            else
            {
                _logger.LogError("Discount create failed. ProductName={ProductName}", request.ProductName);
            }            
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<CouponRequest> UpdateDiscount(CouponRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            bool isUpdated = await _repository.UpdateDiscount(coupon);
            if (isUpdated)
            {
                _logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", request.ProductName);
            }
            else
            {
                _logger.LogError("Discount update failed. ProductName={ProductName}", request.ProductName);
            }
            return _mapper.Map<CouponRequest>(coupon);
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool isDeleted = await _repository.DeleteDiscount(request.ProductId);
            if (isDeleted)
            {
                _logger.LogInformation("Discount is successfully deleted. ProductId : {ProductId}", request.ProductId);
            }
            else
            {
                _logger.LogError("Discount delete failed. ProductId={ProductId}", request.ProductId);
            }
            return new DeleteDiscountResponse
            {
                Success = isDeleted
            };
        }
    }
}
