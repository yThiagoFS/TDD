namespace TDD.Tests
{
    public interface IShoppingCartManager
    {
        AddToCartResponse AddToCart(AddToCartRequest request);
        List<AddToCartItem> GetCart();
    }
}