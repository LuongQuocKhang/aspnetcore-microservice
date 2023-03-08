﻿using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var deleteResult = await _context.Products
                                            .DeleteOneAsync<Product>(p => p.Id == id);
            return deleteResult.IsAcknowledged &&
                deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.Eq(p => p.Category, categoryName);

            return await _context.Products
                                .Find(filterDefinition)
                                .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Products
                                .Find(p => p.Id == id)
                                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filterDefinition = Builders<Product>.Filter.ElemMatch(p => p.Name, name);

            return await _context.Products
                                .Find(filterDefinition)
                                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
           return await _context.Products
                                .Find(p => true)
                                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await _context.Products
                                            .ReplaceOneAsync<Product>(p => p.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged &&
                updateResult.ModifiedCount > 0;
        }
    }
}
