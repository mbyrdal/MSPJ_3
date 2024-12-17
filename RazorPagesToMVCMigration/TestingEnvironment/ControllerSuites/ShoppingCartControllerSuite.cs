using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceAPI.Controllers;
using ServiceAPI.BusinessLogic.Interfaces;
using ServiceAPI.DatabaseAccess;
using ServiceAPI.Models;
using ServiceAPI.Utilities;
using ServiceAPI.DTOs;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MSPJ.TestingEnvironment.ControllerSuites
{
    public class ShoppingCartControllerSuite
    {
        private Mock<DbProduct> _mockDbProduct;
        private Mock<IProductControl> _mockProductControl;
        private Mock<ISession> _mockSession;
        private ShoppingCartController _controller;

        public ShoppingCartControllerSuite()
        {
            _mockDbProduct = new Mock<DbProduct>();
            _mockProductControl = new Mock<IProductControl>();

            var httpContext = new DefaultHttpContext();
            _mockSession = new Mock<ISession>();
            httpContext.Session = _mockSession.Object;

            _controller = new ShoppingCartController(_mockDbProduct.Object, _mockProductControl.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };
        }

        /// <summary>
        /// Ensures the Index action returns the cart view with the cart object from the session.
        /// </summary>
        [Fact]
        public void ShoppingCartController_Index_ReturnsCartViewWithCartObject()
        {
            // Arrange
            var cart = new ShoppingCart { Items = new List<ProductInventoryDTO>(), TotalPrice = 0 };
            _mockSession.Setup(s => s.GetObjectFromJSON<ShoppingCart>(It.IsAny<string>())).Returns(cart);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(cart, viewResult.Model);
        }

        /// <summary>
        /// Ensures AddToCart adds a product to the cart and updates the session.
        /// </summary>
        [Fact]
        public void ShoppingCartController_AddToCart_AddsProductToCart()
        {
            // Arrange
            var product = new Product { ID = 1, ItemAvailable = true, Price = 100 };
            _mockDbProduct.Setup(db => db.GetByIdentifier(1)).Returns(product);

            var cart = new ShoppingCart { Items = new List<ProductInventoryDTO>(), TotalPrice = 0 };
            _mockSession.Setup(s => s.GetObjectFromJSON<ShoppingCart>(It.IsAny<string>())).Returns(cart);

            // Act
            var result = _controller.AddToCart(1);

            // Assert
            _mockSession.Verify(s => s.SetObjectAsJSON(It.IsAny<string>(), It.Is<ShoppingCart>(c => c.Items.Count == 1 && c.TotalPrice == 100)), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary>
        /// Ensures ClearCart removes the cart session.
        /// </summary>
        [Fact]
        public void ShoppingCartController_ClearCart_RemovesCartSession()
        {
            // Act
            var result = _controller.ClearCart();

            // Assert
            _mockSession.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        /// <summary>
        /// Ensures CheckOut processes the order and clears the cart.
        /// </summary>
        [Fact]
        public void ShoppingCartController_CheckOut_ProcessesOrderAndClearsCart()
        {
            // Arrange
            var cart = new ShoppingCart
            {
                Items = new List<ProductInventoryDTO>
                {
                    new ProductInventoryDTO { ID = 1, ItemAvailable = true, Price = 100 }
                },
                TotalPrice = 100
            };
            _mockSession.Setup(s => s.GetObjectFromJSON<ShoppingCart>(It.IsAny<string>())).Returns(cart);

            // Act
            var result = _controller.CheckOut();

            // Assert
            _mockSession.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("OrderConfirmation", redirectResult.ActionName);
        }

        /// <summary>
        /// Ensures RemoveFromCart removes a product from the cart.
        /// </summary>
        [Fact]
        public void ShoppingCartController_RemoveFromCart_RemovesProductFromCart()
        {
            // Arrange
            var cart = new ShoppingCart
            {
                Items = new List<ProductInventoryDTO>
                {
                    new ProductInventoryDTO { ID = 1, Price = 100 }
                },
                TotalPrice = 100
            };
            _mockSession.Setup(s => s.GetObjectFromJSON<ShoppingCart>(It.IsAny<string>())).Returns(cart);

            // Act
            var result = _controller.RemoveFromCart(1);

            // Assert
            _mockSession.Verify(s => s.SetObjectAsJSON(It.IsAny<string>(), It.Is<ShoppingCart>(c => c.Items.Count == 0 && c.TotalPrice == 0)), Times.Once);
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }
    }
}
