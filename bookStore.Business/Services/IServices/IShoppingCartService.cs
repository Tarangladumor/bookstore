using bookStore.Models;

namespace bookStore.Business.Services.IServices
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart?> GetCardByIdAsync(int cartId);
        Task<IEnumerable<ShoppingCart>> GetUserCartItemsAsync(string userId);
        Task<int> GetCartCountAsync(string userId);

        Task<ShoppingCart> AddToCartAsync(ShoppingCart cart);
        Task UpdateCartAsync(ShoppingCart cart);

        Task CleatCartAsync(string userId);
    }
}