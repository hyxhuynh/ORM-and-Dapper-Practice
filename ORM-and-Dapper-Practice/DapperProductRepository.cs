using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Practice
{
    public class DapperProductRepository : IProductRepository
    {
        // Private Field
        private readonly IDbConnection _connection;
        
        //Constructor
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;").ToList();
        }
        public void InsertProduct(string newProductName, double newProductPrice, int newProductID, int newCategoryID)
        {
            _connection.Execute("INSERT INTO products (Name, Price, ProductID, CategoryID) VALUES (@productName, @productPrice, @productID, @categoryID);",
                new { productName = newProductName,
                productPrice = newProductPrice,
                productID = newProductID,
                categoryID = newCategoryID});
        }

        public void UpdateProduct(string newProductName, double newProductPrice, int newCategoryID, int newProductID)
        {
            _connection.Execute("UPDATE products SET Name = @productName, Price = @productPrice, CategoryID = @categoryID WHERE ProductID = @productID;",
                new
                {
                    productName = newProductName,
                    productPrice = newProductPrice,
                    productID = newProductID,
                    categoryID = newCategoryID
                });
        }
        public void DeleteProduct(int inputProductID)
        {
            _connection.Execute("DELETE FROM reviews WHERE ProductID = @productID;",
                new { productID = inputProductID });

            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = inputProductID });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = inputProductID });
        }

    }
}
