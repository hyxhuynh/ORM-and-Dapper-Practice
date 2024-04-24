using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_and_Dapper_Practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");
            IDbConnection conn = new MySqlConnection(connString);


            // Departments
            DapperDepartmentRepository departmentRepo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Type a new Department name:");
            string newDepartment = Console.ReadLine();
            departmentRepo.InsertDepartment(newDepartment);
            IEnumerable<Department> departments = departmentRepo.GetAllDepartments();
            foreach (var d in departments)
            {
                Console.WriteLine(d.Name);
                Console.WriteLine(d.DepartmentID);
                Console.WriteLine();
            }

            // Create an instance of DapperProductRepository
            DapperProductRepository productRepo = new DapperProductRepository(conn);

            // Products
            // INSERT a new product into the 'products' table
            Console.WriteLine("Type a new Product name:");
            string inputProductName = Console.ReadLine();
            Console.WriteLine("Type a new Product price:");
            double inputProductPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Product ID:");
            int inputProductID = int.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Category ID");
            int inputCategoryID = int.Parse(Console.ReadLine());

            productRepo.InsertProduct(inputProductName, inputProductPrice, inputProductID, inputCategoryID);

            // SELECT * FROM products
            IEnumerable<Product> products = productRepo.GetAllProducts();
            foreach (var p in products)
            {
                Console.WriteLine($"Product Name: {p.Name}");
                Console.WriteLine($"Price: ${p.Price}");
                Console.WriteLine($"Product ID: {p.ProductID}");
                Console.WriteLine($"Category ID: {p.CategoryID}");
                Console.WriteLine();
            }

            // UPDATE an existing product (Name, Price, Product ID) using Product ID
            Console.WriteLine("Type the Product ID of the product you want to update:");
            int existingProductID = int.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Product name:");
            string newProductName = Console.ReadLine();
            Console.WriteLine("Type a new Product price:");
            double newProductPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Type a new Category ID");
            int newCategoryID = int.Parse(Console.ReadLine());

            productRepo.UpdateProduct(newProductName, newProductPrice, newCategoryID, existingProductID);

            // SELECT * FROM products
            IEnumerable<Product> newProducts = productRepo.GetAllProducts();
            foreach (var p in newProducts)
            {
                Console.WriteLine($"Product Name: {p.Name}");
                Console.WriteLine($"Price: ${p.Price}");
                Console.WriteLine($"Product ID: {p.ProductID}");
                Console.WriteLine($"Category ID: {p.CategoryID}");
                Console.WriteLine();
            }

            // DELETE an existing product using Product ID
            Console.WriteLine("Type the Product ID of the product you want to delete:");
            int currentProductID = int.Parse(Console.ReadLine());

            productRepo.DeleteProduct(currentProductID);

            // SELECT * FROM products
            IEnumerable<Product> afterDeleteProducts = productRepo.GetAllProducts();
            foreach (var p in afterDeleteProducts)
            {
                Console.WriteLine($"Product Name: {p.Name}");
                Console.WriteLine($"Price: ${p.Price}");
                Console.WriteLine($"Product ID: {p.ProductID}");
                Console.WriteLine($"Category ID: {p.CategoryID}");
                Console.WriteLine();
            }
        }
    }
}
