using Dapper;
using Discount.Grpc.Data;
using Discount.Grpc.Entities;
using Npgsql;
using System.Linq;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDiscountContext _context;
        public DiscountRepository(IDiscountContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            using (var connection = _context.Connection)
            {
                var affected = await connection.ExecuteAsync(CouponFactory.CreateExecuteQuery(QueryType.ADD_COUNPON),
                                                                new
                                                                {
                                                                    ProductName = coupon.ProductName,
                                                                    Description = coupon.Description,
                                                                    Amount = coupon.Amount
                                                                });

                return affected == 0;
            }
        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            using (var connection = _context.Connection)
            {
                var affected = await connection.ExecuteAsync(CouponFactory.CreateExecuteQuery(QueryType.DELETE_COUNPON),
                                                                new
                                                                {
                                                                    ProductName = productName
                                                                });

                return affected == 0;
            }
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            using (var connection = _context.Connection)
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(CouponFactory.CreateExecuteQuery(QueryType.SELECT_COUPON_BY_PRODUCT_NAME),
                                                                                new { ProductName = productName });
                if (coupon == null)
                {
                    return new Coupon()
                    {
                        ProductName = "No Discount",
                        Amount = 0,
                        Description = "No Discount Desc"
                    };
                }
                else
                {
                    return coupon;
                }

            }
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            using (var connection = _context.Connection)
            {
                var affected = await connection.ExecuteAsync(CouponFactory.CreateExecuteQuery(QueryType.UPDATE_COUNPON),
                                                                new
                                                                {
                                                                    ProductName = coupon.ProductName,
                                                                    Description = coupon.Description,
                                                                    Amount = coupon.Amount,
                                                                    Id = coupon.Id
                                                                });

                return affected == 0;
            }
        }
    }
}
