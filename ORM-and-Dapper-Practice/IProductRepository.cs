using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_and_Dapper_Practice
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        void InsertProduct(string name, double price, int productID, int categoryID);
    }
}
