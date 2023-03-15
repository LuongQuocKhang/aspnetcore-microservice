using Discount.API.Data;
using Npgsql;

namespace Discount.API.Extentions
{
    public static class WebApplicationExtensions
    {
        public static WebApplication MigrateDatabase<TContext>(this WebApplication app, int? retry = 0)
        {
            int retryForAvaiability = retry.HasValue ? retry.Value: 0;
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Migrating PostgreSQL database .......");
                    var context = services.GetRequiredService<IDiscountContext>();
                    using (var connection = context.Connection)
                    {
                        connection.Open();
                        using (var command = new NpgsqlCommand
                        {
                            Connection = connection
                        })
                        {
                            command.CommandText = CouponFactory.CreateDropQuery(QueryType.DROP_COUPON_TABLE);
                            command.ExecuteNonQuery();
                        }

                        logger.LogInformation("Migrated PostgreSQL database .......");
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occured while migrating the postgresql database.");

                    if(retryForAvaiability < 50)
                    {
                        retryForAvaiability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, retryForAvaiability);
                    }
                }
            }

            return app;
        }
    }
}
