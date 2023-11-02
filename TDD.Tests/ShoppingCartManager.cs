namespace TDD.Tests
{
    public class ShoppingCartManager : IShoppingCartManager
    {
        List<AddToCartItem> _shoppingCart = new();

        public AddToCartResponse AddToCart(AddToCartRequest request)
        {
            var itemAlreadyExists = _shoppingCart.FirstOrDefault(i => i.ArticleId == request.Item.ArticleId);

            if(itemAlreadyExists is not null)
            {
                itemAlreadyExists.Quantity += request.Item.Quantity;
            }
            else
            {
                _shoppingCart.Add(request.Item);
            }

            return new AddToCartResponse()
            {
                Items = _shoppingCart
            };
        }

        public List<AddToCartItem> GetCart()
        {
            return _shoppingCart;
        }
    }
}