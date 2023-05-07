using Shopping.Agregator.Extensions;
using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogAsync()
        {
            var response = await _httpClient.GetAsync("/api/v1/Catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalogAsync(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategoryAsync(string catalog)
        {
            var response = await _httpClient.GetAsync($"/api/v1/Catalog/GetCatalogByCategory/{catalog}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }
    }
}
