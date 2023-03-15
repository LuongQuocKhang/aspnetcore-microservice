﻿using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.API.Data
{
    public class DiscountContext : IDiscountContext
    {
        public NpgsqlConnection Connection { get; set; }
        public DiscountContext(IConfiguration configuration) 
        {
            Connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
    }
}
