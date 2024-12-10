using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPJ.TestingEnvironment.ModelSuites
{
    public class OrderTestingSuite
    {
        /// <summary>
        /// The purpose of this test is to evaluate the performance and functionality of the ListOfProducts method associated with an order.<br/>
        /// ListOfProducts returns a list of Product objects appended to the Sale in question.<br/>
        /// In this scenario, both lists should be empty.
        /// </summary>
        [Fact]
        public void Test_Order_ReturnListOfProductsEmpty_Method()
        {
            // Arrange
            // var myOrder =  new Sale();
            // var myProductList = new List<Product>();

            // Act
            // resultList = myOrder.ListOfProducts(myProductList);

            // Assert
            // Assert.IsType<List<Product>>(resultList); // Expected type is List<Product> ...
            // Assert.Empty(resultList); // Expected to not be empty ...
            // Assert.Equal(myProductList.Count, resultList.Count); // Expected *.Count to be equal (0)...
            // Assert.Equal(myProductList, resultList); // Expected both lists to be equal ...
        }
        /// <summary>
        /// The purpose of this test is to evaluate the performance and functionality of the ListOfProducts method associated with an order.<br/>
        /// ListOfProducts returns a list of Product objects appended to the Sale in question.<br/>
        /// </summary>
        [Fact]
        public void Test_Order_ReturnListOfProductsNotEmpty_Method()
        {
            // Arrange
            // var myOrder =  new Sale();
            // var myProductList = new List<Product>() { productOne, productTwo, productThree };

            // Act
            // resultList = myOrder.ListOfProducts(myProductList);

            // Assert
            // Assert.IsType<List<Product>>(resultList); // Expected type is List<Product> ...
            // Assert.NotEmpty(resultList); // Expected to not be empty ...
            // Assert.Equal(myProductList.Count, resultList.Count); // Expected *.Count to be equal ...
            // Assert.Equal(myProductList, resultList); // Expected both lists to be equal ...
        }
    }
}
