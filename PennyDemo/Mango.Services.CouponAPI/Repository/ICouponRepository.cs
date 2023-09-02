using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI.Repository
{
    public interface ICouponRepository
    {
        //僅用於管理優惠卷/返回
        //使用優惠卷 =>shoppingCartApi的工作, CouponAPI只負責取出優惠卷資料
        Task<CouponDto> GetCouponByCode(string couponCode);

    }
}
