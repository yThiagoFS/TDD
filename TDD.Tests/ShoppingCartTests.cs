using NUnit.Framework;
using Moq;

namespace TDD.Tests 
{
    public class ShoppingCartTests 
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void ShouldReturnArticleAddedToCart()
        {
            var item = new AddToCartItem()
            {
                ArticleId = 42,
                Quantity = 5
            };

            var request = new AddToCartRequest() 
            {
                Item = item
            };

            var managerMock = new Mock<IShoppingCartManager>();

            managerMock
            .Setup(x => x.AddToCart(It.IsAny<AddToCartRequest>()))
            .Returns((AddToCartRequest request) => new AddToCartResponse() 
            {
                Items = new List<AddToCartItem>() { request.Item }
            });

            AddToCartResponse response = 
                managerMock
                .Object
                .AddToCart(request);

            Assert.NotNull(response);
            Assert.Contains(item, response.Items);
        }

        [Test]
        public void ShouldReturnArticlesAddedToCart()
        {
            var item1 = new AddToCartItem()
            {
                ArticleId = 42,
                Quantity = 5
            };

            var request = new AddToCartRequest() 
            {
                Item = item1
            };

            var manager = new ShoppingCartManager();

            AddToCartResponse response = manager.AddToCart(request);

            var item2 = new AddToCartItem()
            {
                ArticleId = 45,
                Quantity = 1
            };

            request = new AddToCartRequest() 
            {
                Item = item2
            };

            response = manager.AddToCart(request);

            Assert.NotNull(response);
            Assert.Contains(item1, response.Items);
            Assert.Contains(item2, response.Items);
        }

        [Test]
        public void ShouldAddMoreQuantityToTheSameItem()
        {
            var item = new AddToCartItem()
            {
                ArticleId = 30,
                Quantity = 3
            };

            var request = new AddToCartRequest 
            {
                Item = item
            };

            var manager = new ShoppingCartManager();

            AddToCartResponse response = manager.AddToCart(request);

            var item2 = new AddToCartItem()
             {
                ArticleId = 30,
                Quantity = 3
            };

            request = new AddToCartRequest 
            {
                Item = item
            };

            response = manager.AddToCart(request);

            Assert.NotNull(response);
            Assert.AreEqual(response.Items.FirstOrDefault(i => i.ArticleId == item.ArticleId)?.Quantity, 6);
        }
    }
}