using Shopping.Agregator.Models;

namespace Shopping.Agregator.Services
{
    public interface IBasketService
    {
        Task<BasketModel> GetBasketAsync(string userName);
    }
}
