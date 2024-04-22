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
        public void InsertProduct(string newProductName, double newProductPrice, int newProductID, int newCategoryID)
        {
            _connection.Execute("INSERT INTO Products (Name, Price, ProductID, CategoryID) VALUES (@productName, @productPrice, @productID, @categoryID);",
                new { productName = newProductName,
                productPrice = newProductPrice,
                productID = newProductID,
                categoryID = newCategoryID});
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;").ToList();
        }
    }
}
