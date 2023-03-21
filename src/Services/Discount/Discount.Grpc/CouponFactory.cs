namespace Discount.Grpc
{
    public class CouponFactory
    {
        public static string CreateExecuteQuery(QueryType queryType)
        {
            switch (queryType)
            {
                case QueryType.SELECT_COUPON_BY_PRODUCT_NAME:
                    return CommonQuery.SELECT_COUPON_BY_PRODUCT_NAME;
                case QueryType.ADD_COUNPON:
                    return CommonQuery.ADD_COUNPON; 
                case QueryType.DELETE_COUNPON:
                    return CommonQuery.DELETE_COUNPON;
                case QueryType.UPDATE_COUNPON:
                    return CommonQuery.UPDATE_COUNPON;
                default:
                    return string.Empty;
            }
        }

        public static string CreateDropQuery(QueryType queryType)
        {
            switch (queryType)
            {
                case QueryType.DROP_COUPON_TABLE:
                    return CommonQuery.DROP_COUPON_TABLE;
                default:
                    return string.Empty;
            }
        }
    }
}
