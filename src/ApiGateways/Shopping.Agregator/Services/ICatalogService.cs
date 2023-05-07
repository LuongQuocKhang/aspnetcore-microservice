using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalogAsync();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategoryAsync(string catalog);
        Task<CatalogModel> GetCatalogAsync(string id);
    }
}
