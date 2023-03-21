namespace Discount.Grpc
{
    public enum QueryType
    {
        SELECT_COUPON_BY_PRODUCT_NAME,
        ADD_COUNPON,
        DELETE_COUNPON,
        UPDATE_COUNPON,
        DROP_COUPON_TABLE
    }

    public class CommonQuery
    {
        public static string SELECT_COUPON_BY_PRODUCT_NAME { get; set; } = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        public static string ADD_COUNPON { get; set; } = @"INSERT INTO Coupon(ProductName, Description, Amount)
                                                           VALUES (@ProductName, @Description, @Amount)";
        public static string UPDATE_COUNPON { get; set; } = @"UPDATE Coupon
                                                              SET ProductName = @ProductName, 
                                                                  Description = @Description, 
                                                                  Amount = @Amount
                                                               WHERE Id = @Id";
        public static string DELETE_COUNPON { get; set; } = "DELETE FROM Coupon WHERE ProductName = @ProductName";
        public static string DROP_COUPON_TABLE { get; set; } = "DROP TABLE IF EXISTS Coupon";
    }
}
