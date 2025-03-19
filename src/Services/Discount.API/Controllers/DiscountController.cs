using CoreApiResponse;
using Discount.API.Models;
using Discount.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        private readonly ICouponRepository _repository;
        public DiscountController(ICouponRepository couponRepository)
        {
            _repository = couponRepository;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetDiscount(string productId)
        {
            try
            {
                var coupon = await _repository.GetCoupon(productId);
                return CustomResult(coupon, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var saved = await _repository.CreateDiscount(coupon);
                if(saved)
                    return CustomResult("Coupon has been created", coupon, HttpStatusCode.OK);
                return CustomResult("Coupon created failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var updated = await _repository.UpdateDiscount(coupon);
                if (updated)
                    return CustomResult("Coupon has been modified", coupon, HttpStatusCode.OK);
                return CustomResult("Coupon modified failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            try
            {
                var deleted = await _repository.DeleteDiscount(productId);
                if (deleted)
                    return CustomResult("Coupon has been deleted", HttpStatusCode.OK);
                return CustomResult("Coupon deleted failed", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

    }
}
